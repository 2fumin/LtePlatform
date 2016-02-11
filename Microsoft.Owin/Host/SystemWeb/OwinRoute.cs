using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin.Host.SystemWeb
{
    using System;
    using System.Web;
    using System.Web.Routing;

    internal sealed class OwinRoute : RouteBase
    {
        private readonly Func<OwinAppContext> _appAccessor;
        private readonly string _pathBase;

        internal OwinRoute(string pathBase, Func<OwinAppContext> appAccessor)
        {
            this._pathBase = Utils.NormalizePath(HttpRuntime.AppDomainAppVirtualPath) + Utils.NormalizePath(pathBase);
            this._appAccessor = appAccessor;
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            string str = httpContext.Request.CurrentExecutionFilePath + httpContext.Request.PathInfo;
            int length = str.Length;
            int startIndex = this._pathBase.Length;
            if (length < startIndex)
            {
                return null;
            }
            if ((length > startIndex) && (str[startIndex] != '/'))
            {
                return null;
            }
            if (!str.StartsWith(this._pathBase, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            return new RouteData(this, new OwinRouteHandler(this._pathBase, str.Substring(startIndex), this._appAccessor));
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}
