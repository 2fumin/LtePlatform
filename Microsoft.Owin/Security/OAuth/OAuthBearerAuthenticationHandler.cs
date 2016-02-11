using System;
using System.Threading.Tasks;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.Infrastructure;

namespace Microsoft.Owin.Security.OAuth
{
    internal class OAuthBearerAuthenticationHandler : AuthenticationHandler<OAuthBearerAuthenticationOptions>
    {
        private readonly string _challenge;
        private readonly ILogger _logger;

        public OAuthBearerAuthenticationHandler(ILogger logger, string challenge)
        {
            _logger = logger;
            _challenge = challenge;
        }

        protected override Task ApplyResponseChallengeAsync()
        {
            if ((Response.StatusCode != 0x191) ||
                (Helper.LookupChallenge(Options.AuthenticationType, Options.AuthenticationMode) == null))
                return Task.FromResult<object>(null);
            var context = new OAuthChallengeContext(Context, _challenge);
            Options.Provider.ApplyChallenge(context);
            return Task.FromResult<object>(null);
        }

        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            AuthenticationTicket ticket;
            try
            {
                string token = null;
                var authorization = Request.Headers.Get("Authorization");
                if (!string.IsNullOrEmpty(authorization) &&
                    authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    token = authorization.Substring("Bearer ".Length).Trim();
                }
                var requestTokenContext = new OAuthRequestTokenContext(Context, token);
                await Options.Provider.RequestToken(requestTokenContext);
                if (string.IsNullOrEmpty(requestTokenContext.Token))
                {
                    return null;
                }
                var tokenReceiveContext =
                    new AuthenticationTokenReceiveContext(Context, Options.AccessTokenFormat,
                        requestTokenContext.Token);
                await Options.AccessTokenProvider.ReceiveAsync(tokenReceiveContext);
                if (tokenReceiveContext.Ticket == null)
                {
                    tokenReceiveContext.DeserializeTicket(tokenReceiveContext.Token);
                }
                var asyncVariable0 = tokenReceiveContext.Ticket;
                if (asyncVariable0 == null)
                {
                    _logger.WriteWarning("invalid bearer token received", new string[0]);
                    return null;
                }
                var utcNow = Options.SystemClock.UtcNow;
                if (asyncVariable0.Properties.ExpiresUtc.HasValue &&
                    (asyncVariable0.Properties.ExpiresUtc.Value < utcNow))
                {
                    _logger.WriteWarning("expired bearer token received", new string[0]);
                    ticket = null;
                }
                else
                {
                    var context = new OAuthValidateIdentityContext(Context, Options,
                        asyncVariable0);
                    if (asyncVariable0?.Identity != null && asyncVariable0.Identity.IsAuthenticated)
                    {
                        context.Validated();
                    }
                    if (Options.Provider != null)
                    {
                        await Options.Provider.ValidateIdentity(context);
                    }
                    ticket = !context.IsValidated ? null : context.Ticket;
                }
            }
            catch (Exception exception)
            {
                _logger.WriteError("Authentication failed", exception);
                ticket = null;
            }
            return ticket;
        }

    }
}
