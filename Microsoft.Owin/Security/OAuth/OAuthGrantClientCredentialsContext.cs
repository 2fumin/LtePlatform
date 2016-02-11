using System.Collections.Generic;

namespace Microsoft.Owin.Security.OAuth
{
    public class OAuthGrantClientCredentialsContext : BaseValidatingTicketContext<OAuthAuthorizationServerOptions>
    {
        public OAuthGrantClientCredentialsContext(IOwinContext context, OAuthAuthorizationServerOptions options, 
            string clientId, IList<string> scope) : base(context, options, null)
        {
            ClientId = clientId;
            Scope = scope;
        }

        public string ClientId { get; private set; }

        public IList<string> Scope { get; private set; }
    }
}
