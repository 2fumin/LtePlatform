using System;
using Microsoft.Owin.Security.OAuth;

namespace Microsoft.Owin
{
    public static class OAuthAuthorizationServerExtensions
    {
        public static IAppBuilder UseOAuthAuthorizationServer(this IAppBuilder app, OAuthAuthorizationServerOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            app.Use(typeof(OAuthAuthorizationServerMiddleware), new object[] { app, options });
            return app;
        }
    }
}
