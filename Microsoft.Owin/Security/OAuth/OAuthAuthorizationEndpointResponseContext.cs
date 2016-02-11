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

    public class OAuthAuthorizationEndpointResponseContext : EndpointContext<OAuthAuthorizationServerOptions>
    {
        public OAuthAuthorizationEndpointResponseContext(IOwinContext context, OAuthAuthorizationServerOptions options, AuthenticationTicket ticket, Microsoft.Owin.Security.OAuth.Messages.AuthorizeEndpointRequest authorizeEndpointRequest, string accessToken, string authorizationCode) : base(context, options)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException("ticket");
            }
            this.Identity = ticket.Identity;
            this.Properties = ticket.Properties;
            this.AuthorizeEndpointRequest = authorizeEndpointRequest;
            this.AdditionalResponseParameters = new Dictionary<string, object>(StringComparer.Ordinal);
            this.AccessToken = accessToken;
            this.AuthorizationCode = authorizationCode;
        }

        public string AccessToken { get; private set; }

        public IDictionary<string, object> AdditionalResponseParameters { get; private set; }

        public string AuthorizationCode { get; private set; }

        public Microsoft.Owin.Security.OAuth.Messages.AuthorizeEndpointRequest AuthorizeEndpointRequest { get; private set; }

        public ClaimsIdentity Identity { get; private set; }

        public AuthenticationProperties Properties { get; private set; }
    }
}
