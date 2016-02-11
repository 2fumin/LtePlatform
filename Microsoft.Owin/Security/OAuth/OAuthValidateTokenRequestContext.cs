using Microsoft.Owin.Security.OAuth.Messages;

namespace Microsoft.Owin.Security.OAuth
{
    public class OAuthValidateTokenRequestContext : BaseValidatingContext<OAuthAuthorizationServerOptions>
    {
        public OAuthValidateTokenRequestContext(IOwinContext context, OAuthAuthorizationServerOptions options, 
            TokenEndpointRequest tokenRequest, BaseValidatingClientContext clientContext) 
            : base(context, options)
        {
            TokenRequest = tokenRequest;
            ClientContext = clientContext;
        }

        public BaseValidatingClientContext ClientContext { get; private set; }

        public TokenEndpointRequest TokenRequest { get; private set; }
    }
}
