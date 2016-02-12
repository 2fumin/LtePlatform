using System;
using Microsoft.Owin.Security.OAuth;
using Owin;

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
            app.Use(typeof(OAuthAuthorizationServerMiddleware), app, options);
            return app;
        }
    }
}
