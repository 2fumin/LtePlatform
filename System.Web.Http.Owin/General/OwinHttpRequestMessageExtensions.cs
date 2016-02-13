using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace System.Net.Http
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class OwinHttpRequestMessageExtensions
    {
        private const string OwinContextKey = "MS_OwinContext";
        private const string OwinEnvironmentKey = "MS_OwinEnvironment";

        internal static IAuthenticationManager GetAuthenticationManager(this HttpRequestMessage request)
        {
            var owinContext = request.GetOwinContext();
            return owinContext?.Authentication;
        }

        public static IOwinContext GetOwinContext(this HttpRequestMessage request)
        {
            IOwinContext context;
            IDictionary<string, object> dictionary;
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.Properties.TryGetValue(OwinContextKey, out context) ||
                !request.Properties.TryGetValue(OwinEnvironmentKey, out dictionary)) return context;
            context = new OwinContext(dictionary);
            request.SetOwinContext(context);
            request.Properties.Remove(OwinEnvironmentKey);
            return context;
        }

        public static IDictionary<string, object> GetOwinEnvironment(this HttpRequestMessage request)
        {
            var owinContext = request.GetOwinContext();
            return owinContext?.Environment;
        }

        public static void SetOwinContext(this HttpRequestMessage request, IOwinContext context)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            request.Properties[OwinContextKey] = context;
            request.Properties.Remove(OwinEnvironmentKey);
        }

        public static void SetOwinEnvironment(this HttpRequestMessage request, IDictionary<string, object> environment)
        {
            request.SetOwinContext(new OwinContext(environment));
        }
    }
}
