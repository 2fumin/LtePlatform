using System;
using System.Web;
using System.Web.Routing;
using Microsoft.Owin.Host.SystemWeb.Infrastructure;

namespace Microsoft.Owin.Host.SystemWeb
{
    public sealed class OwinHttpHandler : IHttpAsyncHandler, IHttpHandler
    {
        private readonly Func<OwinAppContext> _appAccessor;
        private readonly string _pathBase;
        private readonly RequestContext _requestContext;
        private readonly string _requestPath;

        public OwinHttpHandler() : this(Utils.NormalizePath(HttpRuntime.AppDomainAppVirtualPath), OwinApplication.Accessor)
        {
        }

        internal OwinHttpHandler(string pathBase, OwinAppContext app) : this(pathBase, () => app)
        {
        }

        internal OwinHttpHandler(string pathBase, Func<OwinAppContext> appAccessor)
        {
            _pathBase = pathBase;
            _appAccessor = appAccessor;
        }

        internal OwinHttpHandler(string pathBase, Func<OwinAppContext> appAccessor, RequestContext context, string path) 
            : this(pathBase, appAccessor)
        {
            _requestContext = context;
            _requestPath = path;
        }

        public IAsyncResult BeginProcessRequest(HttpContextBase httpContext, AsyncCallback callback, object extraData)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
            try
            {
                var context = _appAccessor();
                var requestContext = _requestContext ?? new RequestContext(httpContext, new RouteData());
                var requestPathBase = _pathBase;
                var requestPath 
                    = _requestPath 
                    ?? (httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(1) + httpContext.Request.PathInfo);
                var context3 
                    = context.CreateCallContext(requestContext, requestPathBase, requestPath, callback, extraData);
                try
                {
                    context3.Execute();
                }
                catch (Exception exception)
                {
                    if (!context3.TryRelayExceptionToIntegratedPipeline(true, exception))
                    {
                        context3.Complete(true, ErrorState.Capture(exception));
                    }
                }
                return context3.AsyncResult;
            }
            catch (Exception exception2)
            {
                var result = new CallContextAsyncResult(null, callback, extraData);
                result.Complete(true, ErrorState.Capture(exception2));
                return result;
            }
        }

        public void EndProcessRequest(IAsyncResult result)
        {
            CallContextAsyncResult.End(result);
        }

        IAsyncResult IHttpAsyncHandler.BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            return BeginProcessRequest(new HttpContextWrapper(context), cb, extraData);
        }

        void IHttpHandler.ProcessRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }

        public bool IsReusable => true;
    }
}
