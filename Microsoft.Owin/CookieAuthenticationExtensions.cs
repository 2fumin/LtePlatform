using System;
using Microsoft.Owin.Extensions;

namespace Microsoft.Owin
{
    public static class CookieAuthenticationExtensions
    {
        public static IAppBuilder UseCookieAuthentication(this IAppBuilder app, CookieAuthenticationOptions options)
        {
            return app.UseCookieAuthentication(options, PipelineStage.Authenticate);
        }

        public static IAppBuilder UseCookieAuthentication(this IAppBuilder app, CookieAuthenticationOptions options, PipelineStage stage)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            app.Use(typeof(CookieAuthenticationMiddleware), new object[] { app, options });
            app.UseStageMarker(stage);
            return app;
        }
    }
}
