using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
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
        private bool _disposed;
        private readonly HttpMessageInvoker _messageInvoker;

        public HttpMessageHandlerAdapter(OwinMiddleware next, HttpMessageHandlerOptions options) : base(next)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            MessageHandler = options.MessageHandler;
            if (MessageHandler == null)
            {
                throw new ArgumentException(Error.Format(Properties.Resources.TypePropertyMustNotBeNull, 
                    typeof(HttpMessageHandlerOptions).Name, "MessageHandler"), nameof(options));
            }
            _messageInvoker = new HttpMessageInvoker(MessageHandler);
            BufferPolicySelector = options.BufferPolicySelector;
            if (BufferPolicySelector == null)
            {
                throw new ArgumentException(
                    Error.Format(Properties.Resources.TypePropertyMustNotBeNull, typeof (HttpMessageHandlerOptions).Name,
                        "BufferPolicySelector"), nameof(options));
            }
            ExceptionLogger = options.ExceptionLogger;
            if (ExceptionLogger == null)
            {
                throw new ArgumentException(Error.Format(Properties.Resources.TypePropertyMustNotBeNull, typeof(HttpMessageHandlerOptions).Name, 
                    "ExceptionLogger"), nameof(options));
            }
            ExceptionHandler = options.ExceptionHandler;
            if (ExceptionHandler == null)
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
            var context = new ExceptionContext(exceptionInfo.SourceException, 
                OwinExceptionCatchBlocks.HttpMessageHandlerAdapterBufferContent, request, response);
            await ExceptionLogger.LogAsync(context, cancellationToken);
            var errorResponse = await ExceptionHandler.HandleAsync(context, cancellationToken);
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
            var errorExceptionContext = new ExceptionContext(errorException, 
                OwinExceptionCatchBlocks.HttpMessageHandlerAdapterBufferError, request, response);
            await ExceptionLogger.LogAsync(errorExceptionContext, cancellationToken);
            response.Dispose();
            return request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        private Task<bool> ComputeContentLengthAsync(HttpRequestMessage request, HttpResponseMessage response, 
            IOwinResponse owinResponse, CancellationToken cancellationToken)
        {
            Exception exception;
            try
            {
                return Task.FromResult(true);
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
            var contentLength = owinRequest.GetContentLength();
            if (!contentLength.HasValue)
            {
                buffer = new MemoryStream();
            }
            else
            {
                buffer = new MemoryStream(contentLength.Value);
            }
            cancellationToken.ThrowIfCancellationRequested();
            using (var copier = new StreamContent(owinRequest.Body))
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
            var message = new HttpRequestMessage(new HttpMethod(owinRequest.Method), owinRequest.Uri);
            try
            {
                message.Content = requestContent;
                foreach (var pair in owinRequest.Headers)
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

        private async Task<bool> HandleTryComputeLengthExceptionAsync(Exception exception, HttpRequestMessage request, 
            HttpResponseMessage response, IOwinResponse owinResponse, CancellationToken cancellationToken)
        {
            var context = new ExceptionContext(exception, 
                OwinExceptionCatchBlocks.HttpMessageHandlerAdapterComputeContentLength, request, response);
            await ExceptionLogger.LogAsync(context, cancellationToken);
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
            var owinRequest = context.Request;
            var owinResponse = context.Response;
            if (owinRequest == null)
            {
                throw Error.InvalidOperation(Properties.Resources.OwinContext_NullRequest);
            }
            if (owinResponse == null)
            {
                throw Error.InvalidOperation(Properties.Resources.OwinContext_NullResponse);
            }
            return InvokeCore(context, owinRequest, owinResponse);
        }

        private async Task InvokeCore(IOwinContext context, IOwinRequest owinRequest, IOwinResponse owinResponse)
        {
            HttpContent requestContent;
            bool callNext;
            var callCancelled = owinRequest.CallCancelled;
            var bufferInput = BufferPolicySelector.UseBufferedInputStream(context);
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
            var request = CreateRequestMessage(owinRequest, requestContent);
            MapRequestProperties(request, context);
            SetPrincipal(owinRequest.User);
            HttpResponseMessage response = null;
            try
            {
                response = await _messageInvoker.SendAsync(request, callCancelled);
                if (response == null)
                {
                    throw Error.InvalidOperation(Properties.Resources.SendAsync_ReturnedNull);
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
                        var introduced23 = await ComputeContentLengthAsync(request, response, owinResponse, callCancelled);
                        if (!introduced23)
                        {
                            goto Label_04E5;
                        }
                    }
                    var bufferOutput = BufferPolicySelector.UseBufferedOutputStream(response);
                    if (!bufferOutput)
                    {
                        owinResponse.DisableBuffering();
                    }
                    else if (response.Content != null)
                    {
                        response = await BufferResponseContentAsync(request, response, callCancelled);
                    }
                    var introduced25 = await PrepareHeadersAsync(request, response, owinResponse, callCancelled);
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
                response?.Dispose();
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
            return (((response.StatusCode == HttpStatusCode.NotFound) && request.Properties.TryGetValue(HttpPropertyKeys.NoRouteMatched, out flag)) && flag);
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

        private Task<bool> PrepareHeadersAsync(HttpRequestMessage request, HttpResponseMessage response, IOwinResponse owinResponse, 
            CancellationToken cancellationToken)
        {
            var headers = response.Headers;
            var content = response.Content;
            var flag = headers.TransferEncodingChunked == true;
            var transferEncoding = headers.TransferEncoding;
            if (content != null)
            {
                var headers2 = content.Headers;
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
            return Task.FromResult(true);
        }

        private async Task SendResponseContentAsync(HttpRequestMessage request, HttpResponseMessage response, IOwinResponse owinResponse, 
            CancellationToken cancellationToken)
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
            var context = new ExceptionContext(asyncVariable0, OwinExceptionCatchBlocks.HttpMessageHandlerAdapterStreamContent, request, response);
            await ExceptionLogger.LogAsync(context, cancellationToken);
            await AbortResponseAsync();
        }

        private Task SendResponseMessageAsync(HttpRequestMessage request, HttpResponseMessage response, IOwinResponse owinResponse, CancellationToken cancellationToken)
        {
            owinResponse.StatusCode = (int)response.StatusCode;
            owinResponse.ReasonPhrase = response.ReasonPhrase;
            IDictionary<string, string[]> headers = owinResponse.Headers;
            foreach (var pair in response.Headers)
            {
                headers[pair.Key] = pair.Value.AsArray();
            }
            var content = response.Content;
            if (content == null)
            {
                SetHeadersForEmptyResponse(headers);
                return TaskHelpers.Completed();
            }
            foreach (var pair2 in content.Headers)
            {
                headers[pair2.Key] = pair2.Value.AsArray();
            }
            return SendResponseContentAsync(request, response, owinResponse, cancellationToken);
        }

        private static void SetHeadersForEmptyResponse(IDictionary<string, string[]> headers)
        {
            headers["Content-Length"] = new[] { "0" };
        }

        private static void SetPrincipal(IPrincipal user)
        {
            if (user != null)
            {
                Thread.CurrentPrincipal = user;
            }
        }

        public CancellationToken AppDisposing => _appDisposing;

        public IHostBufferPolicySelector BufferPolicySelector { get; }

        public IExceptionHandler ExceptionHandler { get; }

        public IExceptionLogger ExceptionLogger { get; }

        public HttpMessageHandler MessageHandler { get; }
    }
}
