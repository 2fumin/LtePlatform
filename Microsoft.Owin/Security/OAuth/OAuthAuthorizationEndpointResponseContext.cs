using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Owin.Security.OAuth.Messages;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.OAuth
{
    public class OAuthAuthorizationEndpointResponseContext : EndpointContext<OAuthAuthorizationServerOptions>
    {
        public OAuthAuthorizationEndpointResponseContext(IOwinContext context, OAuthAuthorizationServerOptions options, 
            AuthenticationTicket ticket, AuthorizeEndpointRequest authorizeEndpointRequest, string accessToken, 
            string authorizationCode) : base(context, options)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException(nameof(ticket));
            }
            Identity = ticket.Identity;
            Properties = ticket.Properties;
            AuthorizeEndpointRequest = authorizeEndpointRequest;
            AdditionalResponseParameters = new Dictionary<string, object>(StringComparer.Ordinal);
            AccessToken = accessToken;
            AuthorizationCode = authorizationCode;
        }

        public string AccessToken { get; private set; }

        public IDictionary<string, object> AdditionalResponseParameters { get; private set; }

        public string AuthorizationCode { get; private set; }

        public AuthorizeEndpointRequest AuthorizeEndpointRequest { get; private set; }

        public ClaimsIdentity Identity { get; private set; }

        public AuthenticationProperties Properties { get; private set; }
    }
}
