using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;
using Owin;

namespace Microsoft.Owin.Security.OAuth
{
    public class OAuthBearerAuthenticationMiddleware : AuthenticationMiddleware<OAuthBearerAuthenticationOptions>
    {
        private readonly string _challenge;
        private readonly ILogger _logger;

        public OAuthBearerAuthenticationMiddleware(OwinMiddleware next, IAppBuilder app, 
            OAuthBearerAuthenticationOptions options) : base(next, options)
        {
            _logger = app.CreateLogger<OAuthBearerAuthenticationMiddleware>();
            if (!string.IsNullOrWhiteSpace(Options.Challenge))
            {
                _challenge = Options.Challenge;
            }
            else if (string.IsNullOrWhiteSpace(Options.Realm))
            {
                _challenge = "Bearer";
            }
            else
            {
                _challenge = "Bearer realm=\"" + Options.Realm + "\"";
            }
            if (Options.Provider == null)
            {
                Options.Provider = new OAuthBearerAuthenticationProvider();
            }
            if (Options.AccessTokenFormat == null)
            {
                IDataProtector protector = app.CreateDataProtector(typeof(OAuthBearerAuthenticationMiddleware).Namespace, 
                    "Access_Token", "v1");
                Options.AccessTokenFormat = new TicketDataFormat(protector);
            }
            if (Options.AccessTokenProvider == null)
            {
                Options.AccessTokenProvider = new AuthenticationTokenProvider();
            }
        }

        protected override AuthenticationHandler<OAuthBearerAuthenticationOptions> CreateHandler()
        {
            return new OAuthBearerAuthenticationHandler(_logger, _challenge);
        }
    }
}
