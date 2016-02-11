using System;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.Security.OAuth;

namespace Microsoft.Owin
{
    public static class OAuthBearerAuthenticationExtensions
    {
        public static IAppBuilder UseOAuthBearerAuthentication(this IAppBuilder app, OAuthBearerAuthenticationOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            app.Use(typeof(OAuthBearerAuthenticationMiddleware), app, options);
            app.UseStageMarker(PipelineStage.Authenticate);
            return app;
        }
    }
}
