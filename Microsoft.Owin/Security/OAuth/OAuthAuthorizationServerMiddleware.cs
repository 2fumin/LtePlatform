using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.OAuth
{
    using Microsoft.Owin;
    using Microsoft.Owin.Logging;
    using Microsoft.Owin.Security.DataHandler;
    using Microsoft.Owin.Security.DataProtection;
    using Microsoft.Owin.Security.Infrastructure;
    using Owin;
    using System;

    public class OAuthAuthorizationServerMiddleware : AuthenticationMiddleware<OAuthAuthorizationServerOptions>
    {
        private readonly ILogger _logger;

        public OAuthAuthorizationServerMiddleware(OwinMiddleware next, IAppBuilder app, OAuthAuthorizationServerOptions options) : base(next, options)
        {
            this._logger = app.CreateLogger<OAuthAuthorizationServerMiddleware>();
            if (base.Options.Provider == null)
            {
                base.Options.Provider = new OAuthAuthorizationServerProvider();
            }
            if (base.Options.AuthorizationCodeFormat == null)
            {
                IDataProtector protector = app.CreateDataProtector(new string[] { typeof(OAuthAuthorizationServerMiddleware).FullName, "Authentication_Code", "v1" });
                base.Options.AuthorizationCodeFormat = new TicketDataFormat(protector);
            }
            if (base.Options.AccessTokenFormat == null)
            {
                IDataProtector protector2 = app.CreateDataProtector(new string[] { typeof(OAuthAuthorizationServerMiddleware).Namespace, "Access_Token", "v1" });
                base.Options.AccessTokenFormat = new TicketDataFormat(protector2);
            }
            if (base.Options.RefreshTokenFormat == null)
            {
                IDataProtector protector3 = app.CreateDataProtector(new string[] { typeof(OAuthAuthorizationServerMiddleware).Namespace, "Refresh_Token", "v1" });
                base.Options.RefreshTokenFormat = new TicketDataFormat(protector3);
            }
            if (base.Options.AuthorizationCodeProvider == null)
            {
                base.Options.AuthorizationCodeProvider = new AuthenticationTokenProvider();
            }
            if (base.Options.AccessTokenProvider == null)
            {
                base.Options.AccessTokenProvider = new AuthenticationTokenProvider();
            }
            if (base.Options.RefreshTokenProvider == null)
            {
                base.Options.RefreshTokenProvider = new AuthenticationTokenProvider();
            }
        }

        protected override AuthenticationHandler<OAuthAuthorizationServerOptions> CreateHandler()
        {
            return new OAuthAuthorizationServerHandler(this._logger);
        }
    }
}
