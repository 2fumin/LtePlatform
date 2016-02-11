using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin.Host.SystemWeb
{
    using Microsoft.Owin.Host.SystemWeb.CallEnvironment;
    using Microsoft.Owin.Host.SystemWeb.CallHeaders;
    using Microsoft.Owin.Host.SystemWeb.CallStreams;
    using Microsoft.Owin.Host.SystemWeb.Infrastructure;
    using Microsoft.Owin.Host.SystemWeb.IntegratedPipeline;
    using Microsoft.Owin.Host.SystemWeb.WebSockets;
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
        private DisconnectWatcher _disconnectWatcher;
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
        private static readonly MethodInfo CheckHeadersWritten = ((HeadersWrittenProperty != null) ? HeadersWrittenProperty.GetMethod : null);
        private static readonly PropertyInfo HeadersWrittenProperty = typeof(HttpResponseBase).GetProperty("HeadersWritten");
        private static readonly Lazy<RemoveHeaderDel> IIS7RemoveHeader = new Lazy<RemoveHeaderDel>(new Func<RemoveHeaderDel>(OwinCallContext.GetRemoveHeaderDelegate));
        private const string IIS7WorkerRequestTypeName = "System.Web.Hosting.IIS7WorkerRequest";
        private static readonly MethodInfo OnSendingHeadersRegister = typeof(HttpResponseBase).GetMethod("AddOnSendingHeaders");
        private static readonly MethodInfo PushPromiseMethod = typeof(HttpResponseBase).GetMethods().FirstOrDefault<MethodInfo>(info => info.Name.Equals("PushPromise"));
        private static readonly ITrace Trace = TraceFactory.Create("Microsoft.Owin.Host.SystemWeb.OwinCallContext");
        private const string TraceName = "Microsoft.Owin.Host.SystemWeb.OwinCallContext";

        internal OwinCallContext(OwinAppContext appContext, RequestContext requestContext, string requestPathBase, string requestPath, AsyncCallback cb, object extraData)
        {
            this._appContext = appContext;
            this._requestContext = requestContext;
            this._requestPathBase = requestPathBase;
            this._requestPath = requestPath;
            this.AsyncResult = new CallContextAsyncResult(this, cb, extraData);
            this._httpContext = this._requestContext.HttpContext;
            this._httpRequest = this._httpContext.Request;
            this._httpResponse = this._httpContext.Response;
            this._disconnectWatcher = new DisconnectWatcher(this._httpResponse);
            this.RegisterForOnSendingHeaders();
        }

        [CompilerGenerated]
        private static bool <.cctor>b__12(MethodInfo info)
        {
            return info.Name.Equals("PushPromise");
        }

        internal void AbortIfHeaderSent()
        {
            if (CheckHeadersWritten != null)
            {
                try
                {
                    if ((bool)CheckHeadersWritten.Invoke(this._httpResponse, null))
                    {
                        this._httpRequest.Abort();
                    }
                    return;
                }
                catch (TargetInvocationException)
                {
                }
            }
            if (this._headersSent)
            {
                this._httpRequest.Abort();
            }
        }

        private async Task AcceptCallback(AspNetWebSocketContext webSocketContext)
        {
            OwinWebSocketWrapper wrapper = null;
            try
            {
                wrapper = new OwinWebSocketWrapper(webSocketContext);
                await this._webSocketFunc(wrapper.Environment);
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
            this.AsyncResult.Complete(this._completedSynchronouslyThreadId == Thread.CurrentThread.ManagedThreadId, null);
        }

        private void Complete(ErrorState errorState)
        {
            this.Complete(this._completedSynchronouslyThreadId == Thread.CurrentThread.ManagedThreadId, errorState);
        }

        internal void Complete(bool sync, ErrorState errorState)
        {
            this.AbortIfHeaderSent();
            this.AsyncResult.Complete(sync, errorState);
        }

        public void CreateEnvironment()
        {
            if (this._httpContext.Items.Contains(HttpContextItemKeys.OwinEnvironmentKey))
            {
                this._env = this._httpContext.Items[HttpContextItemKeys.OwinEnvironmentKey] as AspNetDictionary;
            }
            else
            {
                this._env = new AspNetDictionary(this);
                this._env.OwinVersion = "1.0";
                this._env.RequestPathBase = this._requestPathBase;
                this._env.RequestPath = this._requestPath;
                this._env.RequestMethod = this._httpRequest.HttpMethod;
                this._env.RequestHeaders = new AspNetRequestHeaders(this._httpRequest);
                this._env.ResponseHeaders = new AspNetResponseHeaders(this._httpResponse);
                this._env.OnSendingHeaders = new Action<Action<object>, object>(this._sendingHeadersEvent.Register);
                this._env.HostTraceOutput = TraceTextWriter.Instance;
                this._env.HostAppName = this._appContext.AppName;
                this._env.DisableResponseCompression = new Action(this.DisableResponseCompression);
                this._env.ServerCapabilities = this._appContext.Capabilities;
                this._env.RequestContext = this._requestContext;
                this._env.HttpContextBase = this._httpContext;
                this._httpContext.Items[HttpContextItemKeys.OwinEnvironmentKey] = this._env;
            }
        }

        private void DisableResponseCompression()
        {
            if (!this._compressionDisabled)
            {
                this.RemoveAcceptEncoding();
                this._httpResponse.CacheControl = "no-cache";
                this._httpResponse.AddHeader("Connection", "keep-alive");
                this._compressionDisabled = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._disconnectWatcher.Dispose();
            }
        }

        private void DoWebSocketUpgrade(IDictionary<string, object> acceptOptions, Func<IDictionary<string, object>, Task> webSocketFunc)
        {
            if (webSocketFunc == null)
            {
                throw new ArgumentNullException("webSocketFunc");
            }
            this._env.ResponseStatusCode = 0x65;
            this._webSocketFunc = webSocketFunc;
            AspNetWebSocketOptions options = new AspNetWebSocketOptions
            {
                SubProtocol = GetWebSocketSubProtocol(this._env, acceptOptions)
            };
            this.OnStart();
            this._httpContext.AcceptWebSocketRequest(new Func<AspNetWebSocketContext, Task>(this.AcceptCallback), options);
        }

        internal void Execute()
        {
            Action<Task> continuationAction = null;
            this.CreateEnvironment();
            this._completedSynchronouslyThreadId = Thread.CurrentThread.ManagedThreadId;
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
                this._appContext.AppFunc(this._env).ContinueWith(continuationAction);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this._completedSynchronouslyThreadId = -2147483648;
            }
        }

        private static RemoveHeaderDel GetRemoveHeaderDelegate()
        {
            try
            {
                Type type = typeof(HttpContext).Assembly.GetType("System.Web.Hosting.IIS7WorkerRequest");
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
            if (this._httpContext.Request.IsSecureConnection)
            {
                try
                {
                    if ((this._httpContext.Request.ClientCertificate != null) && this._httpContext.Request.ClientCertificate.IsPresent)
                    {
                        return new X509Certificate2(this._httpContext.Request.ClientCertificate.Certificate);
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
                if ((this._httpContext.Request.ClientCertificate != null) && this._httpContext.Request.ClientCertificate.IsPresent)
                {
                    this._env.ClientCert = new X509Certificate2(this._httpContext.Request.ClientCertificate.Certificate);
                }
            }
            catch (CryptographicException exception)
            {
                Trace.WriteError(Resources.Trace_ClientCertException, exception);
            }
            return Microsoft.Owin.Host.SystemWeb.Utils.CompletedTask;
        }

        CancellationToken AspNetDictionary.IPropertySource.GetCallCancelled()
        {
            return this._disconnectWatcher.BindDisconnectNotification();
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
            return new InputStream(this._httpRequest);
        }

        string AspNetDictionary.IPropertySource.GetRequestId()
        {
            HttpWorkerRequest service = (HttpWorkerRequest)this._httpContext.GetService(typeof(HttpWorkerRequest));
            if (service != null)
            {
                return service.RequestTraceIdentifier.ToString();
            }
            return null;
        }

        string AspNetDictionary.IPropertySource.GetRequestProtocol()
        {
            return this._httpRequest.ServerVariables["SERVER_PROTOCOL"];
        }

        string AspNetDictionary.IPropertySource.GetRequestQueryString()
        {
            string str = string.Empty;
            Uri url = this._httpRequest.Url;
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
            if (!this._httpRequest.IsSecureConnection)
            {
                return Uri.UriSchemeHttp;
            }
            return Uri.UriSchemeHttps;
        }

        Stream AspNetDictionary.IPropertySource.GetResponseBody()
        {
            return new OutputStream(this._httpResponse, this._httpResponse.OutputStream, new Action(this.OnStart), new Action(this._disconnectWatcher.OnFaulted));
        }

        string AspNetDictionary.IPropertySource.GetResponseReasonPhrase()
        {
            return this._httpResponse.StatusDescription;
        }

        int AspNetDictionary.IPropertySource.GetResponseStatusCode()
        {
            return this._httpResponse.StatusCode;
        }

        Func<string, long, long?, CancellationToken, Task> AspNetDictionary.IPropertySource.GetSendFileAsync()
        {
            return new Func<string, long, long?, CancellationToken, Task>(this.SendFileAsync);
        }

        bool AspNetDictionary.IPropertySource.GetServerIsLocal()
        {
            return this._httpRequest.IsLocal;
        }

        string AspNetDictionary.IPropertySource.GetServerLocalIpAddress()
        {
            return this._httpRequest.ServerVariables["LOCAL_ADDR"];
        }

        string AspNetDictionary.IPropertySource.GetServerLocalPort()
        {
            return this._httpRequest.ServerVariables["SERVER_PORT"];
        }

        string AspNetDictionary.IPropertySource.GetServerRemoteIpAddress()
        {
            return this._httpRequest.ServerVariables["REMOTE_ADDR"];
        }

        string AspNetDictionary.IPropertySource.GetServerRemotePort()
        {
            return this._httpRequest.ServerVariables["REMOTE_PORT"];
        }

        IPrincipal AspNetDictionary.IPropertySource.GetServerUser()
        {
            return this._httpContext.User;
        }

        void AspNetDictionary.IPropertySource.SetResponseReasonPhrase(string value)
        {
            this._httpResponse.StatusDescription = value;
        }

        void AspNetDictionary.IPropertySource.SetResponseStatusCode(int value)
        {
            this._httpResponse.StatusCode = value;
            if (value >= 400)
            {
                this._httpResponse.TrySkipIisCustomErrors = true;
            }
        }

        void AspNetDictionary.IPropertySource.SetServerUser(IPrincipal value)
        {
            this._httpContext.User = value;
            Thread.CurrentPrincipal = value;
        }

        bool AspNetDictionary.IPropertySource.TryGetClientCert(ref X509Certificate value)
        {
            value = this.LoadClientCert();
            return (value != null);
        }

        bool AspNetDictionary.IPropertySource.TryGetDisableRequestBuffering(ref Action action)
        {
            InputStream requestBody = this.Environment.RequestBody as InputStream;
            if (requestBody != null)
            {
                action = new Action(requestBody.DisableBuffering);
                return true;
            }
            action = null;
            return false;
        }

        bool AspNetDictionary.IPropertySource.TryGetHostAppMode(ref string value)
        {
            if (this._httpContext.IsDebuggingEnabled)
            {
                value = "development";
                return true;
            }
            return false;
        }

        bool AspNetDictionary.IPropertySource.TryGetLoadClientCert(ref Func<Task> value)
        {
            if (this._httpContext.Request.IsSecureConnection)
            {
                value = new Func<Task>(this.LoadClientCertAsync);
                return true;
            }
            return false;
        }

        bool AspNetDictionary.IPropertySource.TryGetWebSocketAccept(ref Action<IDictionary<string, object>, Func<IDictionary<string, object>, Task>> value)
        {
            if (this._appContext.WebSocketSupport && this._httpContext.IsWebSocketRequest)
            {
                value = new Action<IDictionary<string, object>, Func<IDictionary<string, object>, Task>>(this.DoWebSocketUpgrade);
                return true;
            }
            return false;
        }

        public void OnEnd()
        {
            try
            {
                this.OnStart();
            }
            catch (Exception exception)
            {
                this.Complete(ErrorState.Capture(exception));
                return;
            }
            this.Complete();
        }

        public void OnStart()
        {
            Exception innerException = LazyInitializer.EnsureInitialized<Exception>(ref this._startException, ref this._startCalled, ref this._startLock, new Func<Exception>(this.StartOnce));
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
                        action = _ => this.OnStart();
                    }
                    parameters[0] = action;
                    OnSendingHeadersRegister.Invoke(this._httpResponse, parameters);
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
                HttpWorkerRequest service = (HttpWorkerRequest)this._httpContext.GetService(typeof(HttpWorkerRequest));
                if (HttpRuntime.UsingIntegratedPipeline && (IIS7RemoveHeader.Value != null))
                {
                    IIS7RemoveHeader.Value(service);
                }
                else
                {
                    try
                    {
                        this._httpRequest.Headers.Remove("Accept-Encoding");
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
                return Microsoft.Owin.Host.SystemWeb.Utils.CancelledTask;
            }
            try
            {
                this.OnStart();
                long? nullable = count;
                this._httpContext.Response.TransmitFile(name, offset, nullable.HasValue ? nullable.GetValueOrDefault() : -1L);
                return Microsoft.Owin.Host.SystemWeb.Utils.CompletedTask;
            }
            catch (Exception exception)
            {
                return Microsoft.Owin.Host.SystemWeb.Utils.CreateFaultedTask(exception);
            }
        }

        private Exception StartOnce()
        {
            try
            {
                this._sendingHeadersEvent.Fire();
                this._headersSent = true;
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
            if (this.Environment.TryGetValue("integratedpipeline.Context", out obj2))
            {
                IntegratedPipelineContext context = obj2 as IntegratedPipelineContext;
                if (context != null)
                {
                    context.TakeLastCompletionSource().TrySetException(ex);
                    this.AsyncResult.Complete(sync, null);
                    return true;
                }
            }
            return false;
        }

        internal CallContextAsyncResult AsyncResult { get; private set; }

        internal AspNetDictionary Environment
        {
            get
            {
                return this._env;
            }
        }
        
    private delegate void RemoveHeaderDel(HttpWorkerRequest workerRequest);
}
}
