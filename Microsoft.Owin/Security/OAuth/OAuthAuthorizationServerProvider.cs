using System;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.OAuth
{
    public class OAuthAuthorizationServerProvider : IOAuthAuthorizationServerProvider
    {
        public OAuthAuthorizationServerProvider()
        {
            OnMatchEndpoint = context => Task.FromResult<object>(null);
            OnValidateClientRedirectUri = context => Task.FromResult<object>(null);
            OnValidateClientAuthentication = context => Task.FromResult<object>(null);
            OnValidateAuthorizeRequest = DefaultBehavior.ValidateAuthorizeRequest;
            OnValidateTokenRequest = DefaultBehavior.ValidateTokenRequest;
            OnGrantAuthorizationCode = DefaultBehavior.GrantAuthorizationCode;
            OnGrantResourceOwnerCredentials = context => Task.FromResult<object>(null);
            OnGrantRefreshToken = DefaultBehavior.GrantRefreshToken;
            OnGrantClientCredentials = context => Task.FromResult<object>(null);
            OnGrantCustomExtension = context => Task.FromResult<object>(null);
            OnAuthorizeEndpoint = context => Task.FromResult<object>(null);
            OnTokenEndpoint = context => Task.FromResult<object>(null);
            OnAuthorizationEndpointResponse = context => Task.FromResult<object>(null);
            OnTokenEndpointResponse = context => Task.FromResult<object>(null);
        }

        public virtual Task AuthorizationEndpointResponse(OAuthAuthorizationEndpointResponseContext context)
        {
            return OnAuthorizationEndpointResponse(context);
        }

        public virtual Task AuthorizeEndpoint(OAuthAuthorizeEndpointContext context)
        {
            return OnAuthorizeEndpoint(context);
        }

        public virtual Task GrantAuthorizationCode(OAuthGrantAuthorizationCodeContext context)
        {
            return OnGrantAuthorizationCode(context);
        }

        public virtual Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            return OnGrantClientCredentials(context);
        }

        public virtual Task GrantCustomExtension(OAuthGrantCustomExtensionContext context)
        {
            return OnGrantCustomExtension(context);
        }

        public virtual Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            return OnGrantRefreshToken(context);
        }

        public virtual Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return OnGrantResourceOwnerCredentials(context);
        }

        public virtual Task MatchEndpoint(OAuthMatchEndpointContext context)
        {
            return OnMatchEndpoint(context);
        }

        public virtual Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            return OnTokenEndpoint(context);
        }

        public virtual Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            return OnTokenEndpointResponse(context);
        }

        public virtual Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
        {
            return OnValidateAuthorizeRequest(context);
        }

        public virtual Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            return OnValidateClientAuthentication(context);
        }

        public virtual Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            return OnValidateClientRedirectUri(context);
        }

        public virtual Task ValidateTokenRequest(OAuthValidateTokenRequestContext context)
        {
            return OnValidateTokenRequest(context);
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
