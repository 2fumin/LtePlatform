using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.OAuth
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public class OAuthAuthorizationServerProvider : IOAuthAuthorizationServerProvider
    {
        public OAuthAuthorizationServerProvider()
        {
            this.OnMatchEndpoint = context => Task.FromResult<object>(null);
            this.OnValidateClientRedirectUri = context => Task.FromResult<object>(null);
            this.OnValidateClientAuthentication = context => Task.FromResult<object>(null);
            this.OnValidateAuthorizeRequest = DefaultBehavior.ValidateAuthorizeRequest;
            this.OnValidateTokenRequest = DefaultBehavior.ValidateTokenRequest;
            this.OnGrantAuthorizationCode = DefaultBehavior.GrantAuthorizationCode;
            this.OnGrantResourceOwnerCredentials = context => Task.FromResult<object>(null);
            this.OnGrantRefreshToken = DefaultBehavior.GrantRefreshToken;
            this.OnGrantClientCredentials = context => Task.FromResult<object>(null);
            this.OnGrantCustomExtension = context => Task.FromResult<object>(null);
            this.OnAuthorizeEndpoint = context => Task.FromResult<object>(null);
            this.OnTokenEndpoint = context => Task.FromResult<object>(null);
            this.OnAuthorizationEndpointResponse = context => Task.FromResult<object>(null);
            this.OnTokenEndpointResponse = context => Task.FromResult<object>(null);
        }

        public virtual Task AuthorizationEndpointResponse(OAuthAuthorizationEndpointResponseContext context)
        {
            return this.OnAuthorizationEndpointResponse(context);
        }

        public virtual Task AuthorizeEndpoint(OAuthAuthorizeEndpointContext context)
        {
            return this.OnAuthorizeEndpoint(context);
        }

        public virtual Task GrantAuthorizationCode(OAuthGrantAuthorizationCodeContext context)
        {
            return this.OnGrantAuthorizationCode(context);
        }

        public virtual Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            return this.OnGrantClientCredentials(context);
        }

        public virtual Task GrantCustomExtension(OAuthGrantCustomExtensionContext context)
        {
            return this.OnGrantCustomExtension(context);
        }

        public virtual Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            return this.OnGrantRefreshToken(context);
        }

        public virtual Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return this.OnGrantResourceOwnerCredentials(context);
        }

        public virtual Task MatchEndpoint(OAuthMatchEndpointContext context)
        {
            return this.OnMatchEndpoint(context);
        }

        public virtual Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            return this.OnTokenEndpoint(context);
        }

        public virtual Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            return this.OnTokenEndpointResponse(context);
        }

        public virtual Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
        {
            return this.OnValidateAuthorizeRequest(context);
        }

        public virtual Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            return this.OnValidateClientAuthentication(context);
        }

        public virtual Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            return this.OnValidateClientRedirectUri(context);
        }

        public virtual Task ValidateTokenRequest(OAuthValidateTokenRequestContext context)
        {
            return this.OnValidateTokenRequest(context);
        }

        public Func<OAuthAuthorizationEndpointResponseContext, Task> OnAuthorizationEndpointResponse { get; set; }

        public Func<OAuthAuthorizeEndpointContext, Task> OnAuthorizeEndpoint { get; set; }

        public Func<OAuthGrantAuthorizationCodeContext, Task> OnGrantAuthorizationCode { get; set; }

        public Func<OAuthGrantClientCredentialsContext, Task> OnGrantClientCredentials { get; set; }

        public Func<OAuthGrantCustomExtensionContext, Task> OnGrantCustomExtension { get; set; }

        public Func<OAuthGrantRefreshTokenContext, Task> OnGrantRefreshToken { get; set; }

        public Func<OAuthGrantResourceOwnerCredentialsContext, Task> OnGrantResourceOwnerCredentials { get; set; }

        public Func<OAuthMatchEndpointContext, Task> OnMatchEndpoint { get; set; }

        public Func<OAuthTokenEndpointContext, Task> OnTokenEndpoint { get; set; }

        public Func<OAuthTokenEndpointResponseContext, Task> OnTokenEndpointResponse { get; set; }

        public Func<OAuthValidateAuthorizeRequestContext, Task> OnValidateAuthorizeRequest { get; set; }

        public Func<OAuthValidateClientAuthenticationContext, Task> OnValidateClientAuthentication { get; set; }

        public Func<OAuthValidateClientRedirectUriContext, Task> OnValidateClientRedirectUri { get; set; }

        public Func<OAuthValidateTokenRequestContext, Task> OnValidateTokenRequest { get; set; }
    }
}
