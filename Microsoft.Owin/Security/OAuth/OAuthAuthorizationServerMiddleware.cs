using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;
using Owin;

namespace Microsoft.Owin.Security.OAuth
{
    public class OAuthAuthorizationServerMiddleware : AuthenticationMiddleware<OAuthAuthorizationServerOptions>
    {
        private readonly ILogger _logger;

        public OAuthAuthorizationServerMiddleware(OwinMiddleware next, IAppBuilder app, OAuthAuthorizationServerOptions options) : base(next, options)
        {
            _logger = app.CreateLogger<OAuthAuthorizationServerMiddleware>();
            if (Options.Provider == null)
            {
                Options.Provider = new OAuthAuthorizationServerProvider();
            }
            if (Options.AuthorizationCodeFormat == null)
            {
                var protector = app.CreateDataProtector(typeof(OAuthAuthorizationServerMiddleware).FullName, "Authentication_Code", "v1");
                Options.AuthorizationCodeFormat = new TicketDataFormat(protector);
            }
            if (Options.AccessTokenFormat == null)
            {
                var protector2 = app.CreateDataProtector(typeof(OAuthAuthorizationServerMiddleware).Namespace, "Access_Token", "v1");
                Options.AccessTokenFormat = new TicketDataFormat(protector2);
            }
            if (Options.RefreshTokenFormat == null)
            {
                var protector3 = app.CreateDataProtector(typeof(OAuthAuthorizationServerMiddleware).Namespace, "Refresh_Token", "v1");
                Options.RefreshTokenFormat = new TicketDataFormat(protector3);
            }
            if (Options.AuthorizationCodeProvider == null)
            {
                Options.AuthorizationCodeProvider = new AuthenticationTokenProvider();
            }
            if (Options.AccessTokenProvider == null)
            {
                Options.AccessTokenProvider = new AuthenticationTokenProvider();
            }
            if (Options.RefreshTokenProvider == null)
            {
                Options.RefreshTokenProvider = new AuthenticationTokenProvider();
            }
        }

        protected override AuthenticationHandler<OAuthAuthorizationServerOptions> CreateHandler()
        {
            return new OAuthAuthorizationServerHandler(_logger);
        }
    }
}
