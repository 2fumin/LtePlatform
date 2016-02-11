using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Owin
{
    using Microsoft.Owin.Security.OAuth;
    using System;
    using System.Runtime.CompilerServices;

    public static class OAuthAuthorizationServerExtensions
    {
        public static IAppBuilder UseOAuthAuthorizationServer(this IAppBuilder app, OAuthAuthorizationServerOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }
            app.Use(typeof(OAuthAuthorizationServerMiddleware), new object[] { app, options });
            return app;
        }
    }
}
