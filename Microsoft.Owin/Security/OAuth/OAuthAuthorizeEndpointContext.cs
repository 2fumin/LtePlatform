using Microsoft.Owin.Security.OAuth.Messages;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.OAuth
{
    public class OAuthAuthorizeEndpointContext : EndpointContext<OAuthAuthorizationServerOptions>
    {
        public OAuthAuthorizeEndpointContext(IOwinContext context, OAuthAuthorizationServerOptions options, 
            AuthorizeEndpointRequest authorizeRequest) : base(context, options)
        {
            AuthorizeRequest = authorizeRequest;
        }

        public AuthorizeEndpointRequest AuthorizeRequest { get; private set; }
    }
}
