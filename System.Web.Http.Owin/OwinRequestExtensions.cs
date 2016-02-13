using System.Collections.Generic;
using Microsoft.Owin;

namespace System.Web.Http.Owin
{
    internal static class OwinRequestExtensions
    {
        private const string ContentLengthHeaderName = "Content-Length";
        private const string DisableRequestBufferingKey = "server.DisableRequestBuffering";

        public static void DisableBuffering(this IOwinRequest request)
        {
            Action action;
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            var environment = request.Environment;
            if ((environment != null) && environment.TryGetValue(DisableRequestBufferingKey, out action))
            {
                action();
            }
        }

        public static int? GetContentLength(this IOwinRequest request)
        {
            string[] strArray;
            int num;
            var headers = request.Headers;
            if (headers == null)
            {
                return null;
            }
            if (!headers.TryGetValue(ContentLengthHeaderName, out strArray))
            {
                return null;
            }
            if ((strArray == null) || (strArray.Length != 1))
            {
                return null;
            }
            var s = strArray[0];
            if (s == null)
            {
                return null;
            }
            if (!int.TryParse(s, out num))
            {
                return null;
            }
            if (num < 0)
            {
                return null;
            }
            return num;
        }
    }
}
