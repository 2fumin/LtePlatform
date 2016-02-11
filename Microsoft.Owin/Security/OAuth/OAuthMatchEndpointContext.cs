using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.OAuth
{
    public class OAuthMatchEndpointContext : EndpointContext<OAuthAuthorizationServerOptions>
    {
        public OAuthMatchEndpointContext(IOwinContext context, OAuthAuthorizationServerOptions options) 
            : base(context, options)
        {
        }

        public void MatchesAuthorizeEndpoint()
        {
            IsAuthorizeEndpoint = true;
            IsTokenEndpoint = false;
        }

        public void MatchesNothing()
        {
            IsAuthorizeEndpoint = false;
            IsTokenEndpoint = false;
        }

        public void MatchesTokenEndpoint()
        {
            IsAuthorizeEndpoint = false;
            IsTokenEndpoint = true;
        }

        public bool IsAuthorizeEndpoint { get; private set; }

        public bool IsTokenEndpoint { get; private set; }
    }
}
