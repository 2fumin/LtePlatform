using System;
using System.Threading;
using System.Web;
using System.Web.Routing;
using Owin;

namespace Microsoft.Owin.Host.SystemWeb
{
    public class OwinRouteHandler : IRouteHandler
    {
        private readonly Func<OwinAppContext> _appAccessor;
        private readonly string _path;
        private readonly string _pathBase;

        public OwinRouteHandler(Action<IAppBuilder> startup)
        {
            _pathBase = Utils.NormalizePath(HttpRuntime.AppDomainAppVirtualPath);
            OwinAppContext app = null;
            var initialized = false;
            var syncLock = new object();
            _appAccessor = () => LazyInitializer.EnsureInitialized(ref app, ref initialized, ref syncLock, () => OwinBuilder.Build(startup));
        }

        internal OwinRouteHandler(string pathBase, string path, Func<OwinAppContext> appAccessor)
        {
            _pathBase = pathBase;
            _path = path;
            _appAccessor = appAccessor;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return ((IRouteHandler)this).GetHttpHandler(requestContext);
        }

        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            return new OwinHttpHandler(_pathBase, _appAccessor, requestContext, _path);
        }
    }
}
