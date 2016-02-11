﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Owin.Security.OAuth.Messages;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.OAuth
{
    public class OAuthTokenEndpointContext : EndpointContext<OAuthAuthorizationServerOptions>
    {
        public OAuthTokenEndpointContext(IOwinContext context, OAuthAuthorizationServerOptions options, 
            AuthenticationTicket ticket, TokenEndpointRequest tokenEndpointRequest) : base(context, options)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException(nameof(ticket));
            }
            Identity = ticket.Identity;
            Properties = ticket.Properties;
            TokenEndpointRequest = tokenEndpointRequest;
            AdditionalResponseParameters = new Dictionary<string, object>(StringComparer.Ordinal);
            TokenIssued = Identity != null;
        }

        public void Issue(ClaimsIdentity identity, AuthenticationProperties properties)
        {
            Identity = identity;
            Properties = properties;
            TokenIssued = true;
        }

        public IDictionary<string, object> AdditionalResponseParameters { get; private set; }

        public ClaimsIdentity Identity { get; private set; }

        public AuthenticationProperties Properties { get; private set; }

        public TokenEndpointRequest TokenEndpointRequest { get; set; }

        public bool TokenIssued { get; private set; }
    }
}
