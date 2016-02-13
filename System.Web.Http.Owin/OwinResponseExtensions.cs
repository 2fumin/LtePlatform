using System.Collections.Generic;
using Microsoft.Owin;

namespace System.Web.Http.Owin
{
    internal static class OwinResponseExtensions
    {
        private const string DisableResponseBufferingKey = "server.DisableResponseBuffering";

        public static void DisableBuffering(this IOwinResponse response)
        {
            Action action;
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }
            var environment = response.Environment;
            if ((environment != null) && environment.TryGetValue(DisableResponseBufferingKey, out action))
            {
                action();
            }
        }
    }
}
