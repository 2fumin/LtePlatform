using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin.Host.SystemWeb
{
    using CallEnvironment;
    using CallHeaders;
    using SystemWeb.CallStreams;
    using Infrastructure;
    using IntegratedPipeline;
    using SystemWeb.WebSockets;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Routing;
    using System.Web.WebSockets;

    internal class OwinCallContext : AspNetDictionary.IPropertySource, IDisposable
    {
        private readonly OwinAppContext _appContext;
        private int _completedSynchronouslyThreadId;
        private bool _compressionDisabled;
        private readonly DisconnectWatcher _disconnectWatcher;
        private AspNetDictionary _env;
        private bool _headersSent;
        private readonly HttpContextBase _httpContext;
        private readonly HttpRequestBase _httpRequest;
        private readonly HttpResponseBase _httpResponse;
        private readonly RequestContext _requestContext;
        private readonly string _requestPath;
        private readonly string _requestPathBase;
        private readonly SendingHeadersEvent _sendingHeadersEvent = new SendingHeadersEvent();
        private bool _startCalled;
        private Exception _startException;
        private object _startLock = new object();
        private Func<IDictionary<string, object>, Task> _webSocketFunc;
        private static readonly PropertyInfo HeadersWrittenProperty = typeof(HttpResponseBase).GetProperty("HeadersWritten");
        private static readonly MethodInfo CheckHeadersWritten = HeadersWrittenProperty?.GetMethod;
        private static readonly Lazy<RemoveHeaderDel> IIS7RemoveHeader = new Lazy<RemoveHeaderDel>(GetRemoveHeaderDelegate);
        private const string IIS7WorkerRequestTypeName = "System.Web.Hosting.IIS7WorkerRequest";
        private static readonly MethodInfo OnSendingHeadersRegister = typeof(HttpResponseBase).GetMethod("AddOnSendingHeaders");
        private static readonly MethodInfo PushPromiseMethod = typeof(HttpResponseBase).GetMethods().FirstOrDefault(info => info.Name.Equals("PushPromise"));
        private const string TraceName = "Microsoft.Owin.Host.SystemWeb.OwinCallContext";
        private static readonly ITrace Trace = TraceFactory.Create(TraceName);

        internal OwinCallContext(OwinAppContext appContext, RequestContext requestContext, string requestPathBase, string requestPath, AsyncCallback cb, object extraData)
        {
            _appContext = appContext;
            _requestContext = requestContext;
            _requestPathBase = requestPathBase;
            _requestPath = requestPath;
            AsyncResult = new CallContextAsyncResult(this, cb, extraData);
            _httpContext = _requestContext.HttpContext;
            _httpRequest = _httpContext.Request;
            _httpResponse = _httpContext.Response;
            _disconnectWatcher = new DisconnectWatcher(_httpResponse);
            RegisterForOnSendingHeaders();
        }
        
        internal void AbortIfHeaderSent()
        {
            if (CheckHeadersWritten != null)
            {
                try
                {
                    if ((bool)CheckHeadersWritten.Invoke(_httpResponse, null))
                    {
                        _httpRequest.Abort();
                    }
                    return;
                }
                catch (TargetInvocationException)
                {
                }
            }
            if (_headersSent)
            {
                _httpRequest.Abort();
            }
        }

        private async Task AcceptCallback(AspNetWebSocketContext webSocketContext)
        {
            OwinWebSocketWrapper wrapper = null;
            try
            {
                wrapper = new OwinWebSocketWrapper(webSocketContext);
                await _webSocketFunc(wrapper.Environment);
                wrapper.Dispose();
            }
            catch (Exception exception)
            {
                if (wrapper != null)
                {
                    wrapper.Cancel();
                    wrapper.Dispose();
                }
                Trace.WriteWarning(Resources.Trace_WebSocketException, exception);
                throw;
            }
        }

        private void Complete()
        {
            AsyncResult.Complete(_completedSynchronouslyThreadId == Thread.CurrentThread.ManagedThreadId, null);
        }

        private void Complete(ErrorState errorState)
        {
            Complete(_completedSynchronouslyThreadId == Thread.CurrentThread.ManagedThreadId, errorState);
        }

        internal void Complete(bool sync, ErrorState errorState)
        {
            AbortIfHeaderSent();
            AsyncResult.Complete(sync, errorState);
        }

        public void CreateEnvironment()
        {
            if (_httpContext.Items.Contains(HttpContextItemKeys.OwinEnvironmentKey))
            {
                _env = _httpContext.Items[HttpContextItemKeys.OwinEnvironmentKey] as AspNetDictionary;
            }
            else
            {
                _env = new AspNetDictionary(this);
                _env.OwinVersion = "1.0";
                _env.RequestPathBase = _requestPathBase;
                _env.RequestPath = _requestPath;
                _env.RequestMethod = _httpRequest.HttpMethod;
                _env.RequestHeaders = new AspNetRequestHeaders(_httpRequest);
                _env.ResponseHeaders = new AspNetResponseHeaders(_httpResponse);
                _env.OnSendingHeaders = _sendingHeadersEvent.Register;
                _env.HostTraceOutput = TraceTextWriter.Instance;
                _env.HostAppName = _appContext.AppName;
                _env.DisableResponseCompression = DisableResponseCompression;
                _env.ServerCapabilities = _appContext.Capabilities;
                _env.RequestContext = _requestContext;
                _env.HttpContextBase = _httpContext;
                _httpContext.Items[HttpContextItemKeys.OwinEnvironmentKey] = _env;
            }
        }

        private void DisableResponseCompression()
        {
            if (!_compressionDisabled)
            {
                RemoveAcceptEncoding();
                _httpResponse.CacheControl = "no-cache";
                _httpResponse.AddHeader("Connection", "keep-alive");
                _compressionDisabled = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _disconnectWatcher.Dispose();
            }
        }

        private void DoWebSocketUpgrade(IDictionary<string, object> acceptOptions, Func<IDictionary<string, object>, Task> webSocketFunc)
        {
            if (webSocketFunc == null)
            {
                throw new ArgumentNullException("webSocketFunc");
            }
            _env.ResponseStatusCode = 0x65;
            _webSocketFunc = webSocketFunc;
            AspNetWebSocketOptions options = new AspNetWebSocketOptions
            {
                SubProtocol = GetWebSocketSubProtocol(_env, acceptOptions)
            };
            OnStart();
            _httpContext.AcceptWebSocketRequest(AcceptCallback, options);
        }

        internal void Execute()
        {
            Action<Task> continuationAction = null;
            CreateEnvironment();
            _completedSynchronouslyThreadId = Thread.CurrentThread.ManagedThreadId;
            try
            {
                if (continuationAction == null)
                {
                    continuationAction = delegate (Task appTask) {
                        if (appTask.IsFaulted)
                        {
                            if (!this.TryRelayExceptionToIntegratedPipeline(false, appTask.Exception))
                            {
                                this.Complete(ErrorState.Capture(appTask.Exception));
                            }
                        }
                        else if (appTask.IsCanceled)
                        {
                            Exception ex = new TaskCanceledException(appTask);
                            if (!this.TryRelayExceptionToIntegratedPipeline(false, ex))
                            {
                                this.Complete(ErrorState.Capture(ex));
                            }
                        }
                        else
                        {
                            this.OnEnd();
                        }
                    };
                }
                _appContext.AppFunc(_env).ContinueWith(continuationAction);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _completedSynchronouslyThreadId = -2147483648;
            }
        }

        private static RemoveHeaderDel GetRemoveHeaderDelegate()
        {
            try
            {
                Type type = typeof(HttpContext).Assembly.GetType(IIS7WorkerRequestTypeName);
                MethodInfo method = type.GetMethod("SetKnownRequestHeader", BindingFlags.NonPublic | BindingFlags.Instance);
                ParameterExpression expression = Expression.Parameter(typeof(HttpWorkerRequest));
                return Expression.Lambda<RemoveHeaderDel>(Expression.Call(Expression.Convert(expression, type), method, Expression.Constant(0x16), Expression.Constant(null, typeof(string)), Expression.Constant(false)), new ParameterExpression[] { expression }).Compile();
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static string GetWebSocketSubProtocol(AspNetDictionary env, IDictionary<string, object> accpetOptions)
        {
            string[] strArray;
            IDictionary<string, string[]> responseHeaders = env.ResponseHeaders;
            string str = null;
            if (responseHeaders.TryGetValue("Sec-WebSocket-Protocol", out strArray) && (strArray.Length > 0))
            {
                str = strArray[0];
                responseHeaders.Remove("Sec-WebSocket-Protocol");
            }
            if ((accpetOptions != null) && accpetOptions.ContainsKey("websocket.SubProtocol"))
            {
                str = accpetOptions.Get<string>("websocket.SubProtocol", null);
            }
            return str;
        }

        private X509Certificate LoadClientCert()
        {
            if (_httpContext.Request.IsSecureConnection)
            {
                try
                {
                    if ((_httpContext.Request.ClientCertificate != null) && _httpContext.Request.ClientCertificate.IsPresent)
                    {
                        return new X509Certificate2(_httpContext.Request.ClientCertificate.Certificate);
                    }
                }
                catch (CryptographicException exception)
                {
                    Trace.WriteError(Resources.Trace_ClientCertException, exception);
                }
            }
            return null;
        }

        private Task LoadClientCertAsync()
        {
            try
            {
                if ((_httpContext.Request.ClientCertificate != null) && _httpContext.Request.ClientCertificate.IsPresent)
                {
                    _env.ClientCert = new X509Certificate2(_httpContext.Request.ClientCertificate.Certificate);
                }
            }
            catch (CryptographicException exception)
            {
                Trace.WriteError(Resources.Trace_ClientCertException, exception);
            }
            return SystemWeb.Utils.CompletedTask;
        }

        CancellationToken AspNetDictionary.IPropertySource.GetCallCancelled()
        {
            return _disconnectWatcher.BindDisconnectNotification();
        }

        Action AspNetDictionary.IPropertySource.GetDisableResponseBuffering()
        {
            return delegate {
                this._httpResponse.BufferOutput = false;
            };
        }

        CancellationToken AspNetDictionary.IPropertySource.GetOnAppDisposing()
        {
            return OwinApplication.ShutdownToken;
        }

        Stream AspNetDictionary.IPropertySource.GetRequestBody()
        {
            return new InputStream(_httpRequest);
        }

        string AspNetDictionary.IPropertySource.GetRequestId()
        {
            HttpWorkerRequest service = (HttpWorkerRequest)_httpContext.GetService(typeof(HttpWorkerRequest));
            if (service != null)
            {
                return service.RequestTraceIdentifier.ToString();
            }
            return null;
        }

        string AspNetDictionary.IPropertySource.GetRequestProtocol()
        {
            return _httpRequest.ServerVariables["SERVER_PROTOCOL"];
        }

        string AspNetDictionary.IPropertySource.GetRequestQueryString()
        {
            string str = string.Empty;
            Uri url = _httpRequest.Url;
            if (url != null)
            {
                string str2 = url.Query + url.Fragment;
                if (str2.Length > 1)
                {
                    str = str2.Substring(1);
                }
            }
            return str;
        }

        string AspNetDictionary.IPropertySource.GetRequestScheme()
        {
            if (!_httpRequest.IsSecureConnection)
            {
                return Uri.UriSchemeHttp;
            }
            return Uri.UriSchemeHttps;
        }

        Stream AspNetDictionary.IPropertySource.GetResponseBody()
        {
            return new OutputStream(_httpResponse, _httpResponse.OutputStream, new Action(OnStart), new Action(_disconnectWatcher.OnFaulted));
        }

        string AspNetDictionary.IPropertySource.GetResponseReasonPhrase()
        {
            return _httpResponse.StatusDescription;
        }

        int AspNetDictionary.IPropertySource.GetResponseStatusCode()
        {
            return _httpResponse.StatusCode;
        }

        Func<string, long, long?, CancellationToken, Task> AspNetDictionary.IPropertySource.GetSendFileAsync()
        {
            return SendFileAsync;
        }

        bool AspNetDictionary.IPropertySource.GetServerIsLocal()
        {
            return _httpRequest.IsLocal;
        }

        string AspNetDictionary.IPropertySource.GetServerLocalIpAddress()
        {
            return _httpRequest.ServerVariables["LOCAL_ADDR"];
        }

        string AspNetDictionary.IPropertySource.GetServerLocalPort()
        {
            return _httpRequest.ServerVariables["SERVER_PORT"];
        }

        string AspNetDictionary.IPropertySource.GetServerRemoteIpAddress()
        {
            return _httpRequest.ServerVariables["REMOTE_ADDR"];
        }

        string AspNetDictionary.IPropertySource.GetServerRemotePort()
        {
            return _httpRequest.ServerVariables["REMOTE_PORT"];
        }

        IPrincipal AspNetDictionary.IPropertySource.GetServerUser()
        {
            return _httpContext.User;
        }

        void AspNetDictionary.IPropertySource.SetResponseReasonPhrase(string value)
        {
            _httpResponse.StatusDescription = value;
        }

        void AspNetDictionary.IPropertySource.SetResponseStatusCode(int value)
        {
            _httpResponse.StatusCode = value;
            if (value >= 400)
            {
                _httpResponse.TrySkipIisCustomErrors = true;
            }
        }

        void AspNetDictionary.IPropertySource.SetServerUser(IPrincipal value)
        {
            _httpContext.User = value;
            Thread.CurrentPrincipal = value;
        }

        bool AspNetDictionary.IPropertySource.TryGetClientCert(ref X509Certificate value)
        {
            value = LoadClientCert();
            return (value != null);
        }

        bool AspNetDictionary.IPropertySource.TryGetDisableRequestBuffering(ref Action action)
        {
            InputStream requestBody = Environment.RequestBody as InputStream;
            if (requestBody != null)
            {
                action = requestBody.DisableBuffering;
                return true;
            }
            action = null;
            return false;
        }

        bool AspNetDictionary.IPropertySource.TryGetHostAppMode(ref string value)
        {
            if (_httpContext.IsDebuggingEnabled)
            {
                value = "development";
                return true;
            }
            return false;
        }

        bool AspNetDictionary.IPropertySource.TryGetLoadClientCert(ref Func<Task> value)
        {
            if (_httpContext.Request.IsSecureConnection)
            {
                value = LoadClientCertAsync;
                return true;
            }
            return false;
        }

        bool AspNetDictionary.IPropertySource.TryGetWebSocketAccept(ref Action<IDictionary<string, object>, Func<IDictionary<string, object>, Task>> value)
        {
            if (_appContext.WebSocketSupport && _httpContext.IsWebSocketRequest)
            {
                value = DoWebSocketUpgrade;
                return true;
            }
            return false;
        }

        public void OnEnd()
        {
            try
            {
                OnStart();
            }
            catch (Exception exception)
            {
                Complete(ErrorState.Capture(exception));
                return;
            }
            Complete();
        }

        public void OnStart()
        {
            Exception innerException = LazyInitializer.EnsureInitialized(ref _startException, ref _startCalled, ref _startLock, StartOnce);
            if (innerException != null)
            {
                throw new InvalidOperationException(string.Empty, innerException);
            }
        }

        private void RegisterForOnSendingHeaders()
        {
            Action<HttpContextBase> action = null;
            if ((OnSendingHeadersRegister != null) && (PushPromiseMethod != null))
            {
                try
                {
                    object[] parameters = new object[1];
                    if (action == null)
                    {
                        action = _ => OnStart();
                    }
                    parameters[0] = action;
                    OnSendingHeadersRegister.Invoke(_httpResponse, parameters);
                }
                catch (TargetInvocationException)
                {
                }
            }
        }

        private void RemoveAcceptEncoding()
        {
            try
            {
                HttpWorkerRequest service = (HttpWorkerRequest)_httpContext.GetService(typeof(HttpWorkerRequest));
                if (HttpRuntime.UsingIntegratedPipeline && (IIS7RemoveHeader.Value != null))
                {
                    IIS7RemoveHeader.Value(service);
                }
                else
                {
                    try
                    {
                        _httpRequest.Headers.Remove("Accept-Encoding");
                    }
                    catch (PlatformNotSupportedException)
                    {
                    }
                }
            }
            catch (NotImplementedException)
            {
            }
        }

        private Task SendFileAsync(string name, long offset, long? count, CancellationToken cancel)
        {
            if (cancel.IsCancellationRequested)
            {
                return SystemWeb.Utils.CancelledTask;
            }
            try
            {
                OnStart();
                long? nullable = count;
                _httpContext.Response.TransmitFile(name, offset, nullable.HasValue ? nullable.GetValueOrDefault() : -1L);
                return SystemWeb.Utils.CompletedTask;
            }
            catch (Exception exception)
            {
                return SystemWeb.Utils.CreateFaultedTask(exception);
            }
        }

        private Exception StartOnce()
        {
            try
            {
                _sendingHeadersEvent.Fire();
                _headersSent = true;
            }
            catch (Exception exception)
            {
                return exception;
            }
            return null;
        }

        internal bool TryRelayExceptionToIntegratedPipeline(bool sync, Exception ex)
        {
            object obj2;
            if (Environment.TryGetValue("integratedpipeline.Context", out obj2))
            {
                IntegratedPipelineContext context = obj2 as IntegratedPipelineContext;
                if (context != null)
                {
                    context.TakeLastCompletionSource().TrySetException(ex);
                    AsyncResult.Complete(sync, null);
                    return true;
                }
            }
            return false;
        }

        internal CallContextAsyncResult AsyncResult { get; }

        internal AspNetDictionary Environment
        {
            get
            {
                return _env;
            }
        }
        
    private delegate void RemoveHeaderDel(HttpWorkerRequest workerRequest);
}
}
