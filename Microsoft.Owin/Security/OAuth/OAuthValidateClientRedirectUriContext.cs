using System;

namespace Microsoft.Owin.Security.OAuth
{
    public class OAuthValidateClientRedirectUriContext : BaseValidatingClientContext
    {
        public OAuthValidateClientRedirectUriContext(IOwinContext context, OAuthAuthorizationServerOptions options, 
            string clientId, string redirectUri) : base(context, options, clientId)
        {
            RedirectUri = redirectUri;
        }

        public override bool Validated()
        {
            return !string.IsNullOrEmpty(RedirectUri) && base.Validated();
        }

        public bool Validated(string redirectUri)
        {
            if (redirectUri == null)
            {
                throw new ArgumentNullException(nameof(redirectUri));
            }
            if (!string.IsNullOrEmpty(RedirectUri) && !string.Equals(RedirectUri, redirectUri, StringComparison.Ordinal))
            {
                return false;
            }
            RedirectUri = redirectUri;
            return Validated();
        }

        public string RedirectUri { get; private set; }
    }
}
