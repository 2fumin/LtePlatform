using System.Collections.Generic;
using Microsoft.Owin;
using Microsoft.Owin.Host.SystemWeb;

namespace System.Web
{
    public static class HttpContextExtensions
    {
        public static IOwinContext GetOwinContext(this HttpContext context)
        {
            var owinEnvironment = context.GetOwinEnvironment();
            if (owinEnvironment == null)
            {
                throw new InvalidOperationException(Microsoft.Owin.Properties.Resources.HttpContext_OwinEnvironmentNotFound);
            }
            return new OwinContext(owinEnvironment);
        }

        public static IOwinContext GetOwinContext(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            return request.RequestContext.HttpContext.GetOwinContext();
        }

        private static IDictionary<string, object> GetOwinEnvironment(this HttpContext context)
        {
            return (IDictionary<string, object>)context.Items[HttpContextItemKeys.OwinEnvironmentKey];
        }
    }
}
