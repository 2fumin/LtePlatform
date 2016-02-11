﻿using Microsoft.Owin.Security.OAuth.Messages;

namespace Microsoft.Owin.Security.OAuth
{
    public class OAuthValidateAuthorizeRequestContext : BaseValidatingContext<OAuthAuthorizationServerOptions>
    {
        public OAuthValidateAuthorizeRequestContext(IOwinContext context, OAuthAuthorizationServerOptions options, 
            AuthorizeEndpointRequest authorizeRequest, OAuthValidateClientRedirectUriContext clientContext) 
            : base(context, options)
        {
            AuthorizeRequest = authorizeRequest;
            ClientContext = clientContext;
        }

        public AuthorizeEndpointRequest AuthorizeRequest { get; private set; }

        public OAuthValidateClientRedirectUriContext ClientContext { get; private set; }
    }
}
