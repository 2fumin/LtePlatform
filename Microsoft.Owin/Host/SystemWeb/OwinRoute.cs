using System;
using System.Web;
using System.Web.Routing;

namespace Microsoft.Owin.Host.SystemWeb
{
    internal sealed class OwinRoute : RouteBase
    {
        private readonly Func<OwinAppContext> _appAccessor;
        private readonly string _pathBase;

        internal OwinRoute(string pathBase, Func<OwinAppContext> appAccessor)
        {
            _pathBase = Utils.NormalizePath(HttpRuntime.AppDomainAppVirtualPath) + Utils.NormalizePath(pathBase);
            _appAccessor = appAccessor;
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
            var str = httpContext.Request.CurrentExecutionFilePath + httpContext.Request.PathInfo;
            var length = str.Length;
            var startIndex = _pathBase.Length;
            if (length < startIndex)
            {
                return null;
            }
            if ((length > startIndex) && (str[startIndex] != '/'))
            {
                return null;
            }
            return !str.StartsWith(_pathBase, StringComparison.OrdinalIgnoreCase)
                ? null
                : new RouteData(this, new OwinRouteHandler(_pathBase, str.Substring(startIndex), _appAccessor));
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}
