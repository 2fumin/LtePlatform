using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.ExceptionServices;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Hosting;
using System.Web.Http.Owin.ExceptionHandling;
using Microsoft.Owin;

namespace System.Web.Http.Owin
{
    public class HttpMessageHandlerAdapter : OwinMiddleware, IDisposable
    {
        private readonly CancellationToken _appDisposing;
        private readonly IHostBufferPolicySelector _bufferPolicySelector;
        private bool _disposed;
        private readonly IExceptionHandler _exceptionHandler;
        private readonly IExceptionLogger _exceptionLogger;
        private readonly HttpMessageHandler _messageHandler;
        private readonly HttpMessageInvoker _messageInvoker;

        public HttpMessageHandlerAdapter(OwinMiddleware next, HttpMessageHandlerOptions options) : base(next)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _messageHandler = options.MessageHandler;
            if (_messageHandler == null)
            {
                throw new ArgumentException(Error.Format(Properties.Resources.TypePropertyMustNotBeNull, 
                    typeof(HttpMessageHandlerOptions).Name, "MessageHandler"), nameof(options));
            }
            _messageInvoker = new HttpMessageInvoker(_messageHandler);
            _bufferPolicySelector = options.BufferPolicySelector;
            if (_bufferPolicySelector == null)
            {
                throw new ArgumentException(
                    Error.Format(Properties.Resources.TypePropertyMustNotBeNull, typeof (HttpMessageHandlerOptions).Name,
                        "BufferPolicySelector"), nameof(options));
            }
            _exceptionLogger = options.ExceptionLogger;
            if (_exceptionLogger == null)
            {
                throw new ArgumentException(Error.Format(Properties.Resources.TypePropertyMustNotBeNull, typeof(HttpMessageHandlerOptions).Name, 
                    "ExceptionLogger"), nameof(options));
            }
            _exceptionHandler = options.ExceptionHandler;
            if (_exceptionHandler == null)
            {
                throw new ArgumentException(Error.Format(Properties.Resources.TypePropertyMustNotBeNull, typeof(HttpMessageHandlerOptions).Name, 
                    "ExceptionHandler"), nameof(options));
            }
            _appDisposing = options.AppDisposing;
            if (_appDisposing.CanBeCanceled)
            {
                _appDisposing.Register(OnAppDisposing);
            }
        }

        [Obsolete("Use the HttpMessageHandlerAdapter(OwinMiddleware, HttpMessageHandlerOptions) constructor instead.")]
        public HttpMessageHandlerAdapter(OwinMiddleware next, HttpMessageHandler messageHandler, IHostBufferPolicySelector bufferPolicySelector) 
            : this(next, CreateOptions(messageHandler, bufferPolicySelector))
        {
        }

        private static Task AbortResponseAsync()
        {
            return TaskHelpers.Canceled();
        }

        private async Task<HttpResponseMessage> BufferResponseContentAsync(HttpRequestMessage request, 
            HttpResponseMessage response, CancellationToken cancellationToken)
        {
            ExceptionDispatchInfo exceptionInfo;
            Exception errorException;
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                await response.Content.LoadIntoBufferAsync();
                return response;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                exceptionInfo = ExceptionDispatchInfo.Capture(exception);
            }
            ExceptionContext context = new ExceptionContext(exceptionInfo.SourceException, 
                OwinExceptionCatchBlocks.HttpMessageHandlerAdapterBufferContent, request, response);
            await _exceptionLogger.LogAsync(context, cancellationToken);
            HttpResponseMessage errorResponse = await _exceptionHandler.HandleAsync(context, cancellationToken);
            response.Dispose();
            if (errorResponse == null)
            {
                exceptionInfo.Throw();
                return null;
            }
            response = errorResponse;
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                await response.Content.LoadIntoBufferAsync();
                return response;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception2)
            {
                errorException = exception2;
            }
            ExceptionContext errorExceptionContext = new ExceptionContext(errorException, 
                OwinExceptionCatchBlocks.HttpMessageHandlerAdapterBufferError, request, response);
            await _exceptionLogger.LogAsync(errorExceptionContext, cancellationToken);
            response.Dispose();
            return request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        private Task<bool> ComputeContentLengthAsync(HttpRequestMessage request, HttpResponseMessage response, 
            IOwinResponse owinResponse, CancellationToken cancellationToken)
        {
            Exception exception;
            HttpResponseHeaders headers1 = response.Headers;
            HttpContentHeaders headers = response.Content.Headers;
            try
            {
                long? contentLength = headers.ContentLength;
                return Task.FromResult<bool>(true);
            }
            catch (Exception exception2)
            {
                exception = exception2;
            }
            return HandleTryComputeLengthExceptionAsync(exception, request, response, owinResponse, cancellationToken);
        }

        private async static Task<HttpContent> CreateBufferedRequestContentAsync(IOwinRequest owinRequest, 
            CancellationToken cancellationToken)
        {
            MemoryStream buffer;
            int? contentLength = owinRequest.GetContentLength();
            if (!contentLength.HasValue)
            {
                buffer = new MemoryStream();
            }
            else
            {
                buffer = new MemoryStream(contentLength.Value);
            }
            cancellationToken.ThrowIfCancellationRequested();
            using (StreamContent copier = new StreamContent(owinRequest.Body))
            {
                await copier.CopyToAsync(buffer);
            }
            buffer.Position = 0L;
            owinRequest.Body = buffer;
            return new ByteArrayContent(buffer.GetBuffer(), 0, (int)buffer.Length);
        }

        private static HttpMessageHandlerOptions CreateOptions(HttpMessageHandler messageHandler, IHostBufferPolicySelector bufferPolicySelector)
        {
            if (messageHandler == null)
            {
                throw new ArgumentNullException(nameof(messageHandler));
            }
            if (bufferPolicySelector == null)
            {
                throw new ArgumentNullException(nameof(bufferPolicySelector));
            }
            return new HttpMessageHandlerOptions
            {
                MessageHandler = messageHandler,
                BufferPolicySelector = bufferPolicySelector,
                ExceptionLogger = new EmptyExceptionLogger(),
                ExceptionHandler = new DefaultExceptionHandler(),
                AppDisposing = CancellationToken.None
            };
        }

        private static HttpRequestMessage CreateRequestMessage(IOwinRequest owinRequest, HttpContent requestContent)
        {
            HttpRequestMessage message = new HttpRequestMessage(new HttpMethod(owinRequest.Method), owinRequest.Uri);
            try
            {
                message.Content = requestContent;
                foreach (KeyValuePair<string, string[]> pair in owinRequest.Headers)
                {
                    if (!message.Headers.TryAddWithoutValidation(pair.Key, pair.Value))
                    {
                        requestContent.Headers.TryAddWithoutValidation(pair.Key, pair.Value);
                    }
                }
            }
            catch
            {
                message.Dispose();
                throw;
            }
            return message;
        }

        private static HttpContent CreateStreamedRequestContent(IOwinRequest owinRequest)
        {
            return new StreamContent(new NonOwnedStream(owinRequest.Body));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                OnAppDisposing();
            }
        }

        private async Task<bool> HandleTryComputeLengthExceptionAsync(Exception exception, HttpRequestMessage request, HttpResponseMessage response, IOwinResponse owinResponse, CancellationToken cancellationToken)
        {
            ExceptionContext context = new ExceptionContext(exception, OwinExceptionCatchBlocks.HttpMessageHandlerAdapterComputeContentLength, request, response);
            await _exceptionLogger.LogAsync(context, cancellationToken);
            owinResponse.StatusCode = 500;
            SetHeadersForEmptyResponse(owinResponse.Headers);
            return false;
        }

        public override Task Invoke(IOwinContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            IOwinRequest owinRequest = context.Request;
            IOwinResponse owinResponse = context.Response;
            if (owinRequest == null)
            {
                throw Error.InvalidOperation(OwinResources.OwinContext_NullRequest);
            }
            if (owinResponse == null)
            {
                throw Error.InvalidOperation(OwinResources.OwinContext_NullResponse);
            }
            return InvokeCore(context, owinRequest, owinResponse);
        }

        private async Task InvokeCore(IOwinContext context, IOwinRequest owinRequest, IOwinResponse owinResponse)
        {
            HttpContent requestContent;
            bool callNext;
            CancellationToken callCancelled = owinRequest.CallCancelled;
            bool bufferInput = _bufferPolicySelector.UseBufferedInputStream(context);
            if (!bufferInput)
            {
                owinRequest.DisableBuffering();
            }
            if (!owinRequest.Body.CanSeek && bufferInput)
            {
                requestContent = await CreateBufferedRequestContentAsync(owinRequest, callCancelled);
            }
            else
            {
                requestContent = CreateStreamedRequestContent(owinRequest);
            }
            HttpRequestMessage request = CreateRequestMessage(owinRequest, requestContent);
            MapRequestProperties(request, context);
            SetPrincipal(owinRequest.User);
            HttpResponseMessage response = null;
            try
            {
                response = await _messageInvoker.SendAsync(request, callCancelled);
                if (response == null)
                {
                    throw Error.InvalidOperation(OwinResources.SendAsync_ReturnedNull);
                }
                if (IsSoftNotFound(request, response))
                {
                    callNext = true;
                }
                else
                {
                    callNext = false;
                    if (response.Content != null)
                    {
                        bool introduced23 = await ComputeContentLengthAsync(request, response, owinResponse, callCancelled);
                        if (!introduced23)
                        {
                            goto Label_04E5;
                        }
                    }
                    bool bufferOutput = _bufferPolicySelector.UseBufferedOutputStream(response);
                    if (!bufferOutput)
                    {
                        owinResponse.DisableBuffering();
                    }
                    else if (response.Content != null)
                    {
                        response = await BufferResponseContentAsync(request, response, callCancelled);
                    }
                    bool introduced25 = await PrepareHeadersAsync(request, response, owinResponse, callCancelled);
                    if (introduced25)
                    {
                        await SendResponseMessageAsync(request, response, owinResponse, callCancelled);
                    }
                }
            }
            finally
            {
                request.DisposeRequestResources();
                request.Dispose();
                if (response != null)
                {
                    response.Dispose();
                }
            }
            Label_04E5:
            if (callNext && (Next != null))
            {
                await Next.Invoke(context);
            }
        }

        private static bool IsSoftNotFound(HttpRequestMessage request, HttpResponseMessage response)
        {
            bool flag;
            return (((response.StatusCode == HttpStatusCode.NotFound) && request.Properties.TryGetValue<bool>(HttpPropertyKeys.NoRouteMatched, out flag)) && flag);
        }

        private static void MapRequestProperties(HttpRequestMessage request, IOwinContext context)
        {
            request.SetOwinContext(context);
            HttpRequestContext context2 = new OwinHttpRequestContext(context, request);
            request.SetRequestContext(context2);
        }

        private void OnAppDisposing()
        {
            if (!_disposed)
            {
                _messageInvoker.Dispose();
                _disposed = true;
            }
        }

        private Task<bool> PrepareHeadersAsync(HttpRequestMessage request, HttpResponseMessage response, IOwinResponse owinResponse, CancellationToken cancellationToken)
        {
            HttpResponseHeaders headers = response.Headers;
            HttpContent content = response.Content;
            bool flag = headers.TransferEncodingChunked == true;
            HttpHeaderValueCollection<TransferCodingHeaderValue> transferEncoding = headers.TransferEncoding;
            if (content != null)
            {
                HttpContentHeaders headers2 = content.Headers;
                if (!flag)
                {
                    return ComputeContentLengthAsync(request, response, owinResponse, cancellationToken);
                }
                headers2.ContentLength = null;
            }
            if (flag && (transferEncoding.Count == 1))
            {
                transferEncoding.Clear();
            }
            return Task.FromResult<bool>(true);
        }

        private async Task SendResponseContentAsync(HttpRequestMessage request, HttpResponseMessage response, IOwinResponse owinResponse, CancellationToken cancellationToken)
        {
            Exception asyncVariable0;
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                await response.Content.CopyToAsync(owinResponse.Body);
                return;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                asyncVariable0 = exception;
            }
            ExceptionContext context = new ExceptionContext(asyncVariable0, OwinExceptionCatchBlocks.HttpMessageHandlerAdapterStreamContent, request, response);
            await _exceptionLogger.LogAsync(context, cancellationToken);
            await AbortResponseAsync();
        }

        private Task SendResponseMessageAsync(HttpRequestMessage request, HttpResponseMessage response, IOwinResponse owinResponse, CancellationToken cancellationToken)
        {
            owinResponse.StatusCode = (int)response.StatusCode;
            owinResponse.ReasonPhrase = response.ReasonPhrase;
            IDictionary<string, string[]> headers = owinResponse.Headers;
            foreach (KeyValuePair<string, IEnumerable<string>> pair in response.Headers)
            {
                headers[pair.Key] = pair.Value.AsArray<string>();
            }
            HttpContent content = response.Content;
            if (content == null)
            {
                SetHeadersForEmptyResponse(headers);
                return TaskHelpers.Completed();
            }
            foreach (KeyValuePair<string, IEnumerable<string>> pair2 in content.Headers)
            {
                headers[pair2.Key] = pair2.Value.AsArray<string>();
            }
            return SendResponseContentAsync(request, response, owinResponse, cancellationToken);
        }

        private static void SetHeadersForEmptyResponse(IDictionary<string, string[]> headers)
        {
            headers["Content-Length"] = new string[] { "0" };
        }

        private static void SetPrincipal(IPrincipal user)
        {
            if (user != null)
            {
                Thread.CurrentPrincipal = user;
            }
        }

        public CancellationToken AppDisposing
        {
            get
            {
                return _appDisposing;
            }
        }

        public IHostBufferPolicySelector BufferPolicySelector
        {
            get
            {
                return _bufferPolicySelector;
            }
        }

        public IExceptionHandler ExceptionHandler
        {
            get
            {
                return _exceptionHandler;
            }
        }

        public IExceptionLogger ExceptionLogger
        {
            get
            {
                return _exceptionLogger;
            }
        }

        public HttpMessageHandler MessageHandler
        {
            get
            {
                return _messageHandler;
            }
        }
        
    }
}
