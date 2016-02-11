using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.OAuth
{
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth.Messages;
    using Microsoft.Owin.Security.Provider;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Security.Claims;

    public class OAuthTokenEndpointContext : EndpointContext<OAuthAuthorizationServerOptions>
    {
        public OAuthTokenEndpointContext(IOwinContext context, OAuthAuthorizationServerOptions options, AuthenticationTicket ticket, Microsoft.Owin.Security.OAuth.Messages.TokenEndpointRequest tokenEndpointRequest) : base(context, options)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException("ticket");
            }
            this.Identity = ticket.Identity;
            this.Properties = ticket.Properties;
            this.TokenEndpointRequest = tokenEndpointRequest;
            this.AdditionalResponseParameters = new Dictionary<string, object>(StringComparer.Ordinal);
            this.TokenIssued = this.Identity != null;
        }

        public void Issue(ClaimsIdentity identity, AuthenticationProperties properties)
        {
            this.Identity = identity;
            this.Properties = properties;
            this.TokenIssued = true;
        }

        public IDictionary<string, object> AdditionalResponseParameters { get; private set; }

        public ClaimsIdentity Identity { get; private set; }

        public AuthenticationProperties Properties { get; private set; }

        public Microsoft.Owin.Security.OAuth.Messages.TokenEndpointRequest TokenEndpointRequest { get; set; }

        public bool TokenIssued { get; private set; }
    }
}
