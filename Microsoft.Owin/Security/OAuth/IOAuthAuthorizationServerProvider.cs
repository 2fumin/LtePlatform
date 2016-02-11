using System.Threading.Tasks;

namespace Microsoft.Owin.Security.OAuth
{
    public interface IOAuthAuthorizationServerProvider
    {
        Task AuthorizationEndpointResponse(OAuthAuthorizationEndpointResponseContext context);

        Task AuthorizeEndpoint(OAuthAuthorizeEndpointContext context);

        Task GrantAuthorizationCode(OAuthGrantAuthorizationCodeContext context);

        Task GrantClientCredentials(OAuthGrantClientCredentialsContext context);

        Task GrantCustomExtension(OAuthGrantCustomExtensionContext context);

        Task GrantRefreshToken(OAuthGrantRefreshTokenContext context);

        Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context);

        Task MatchEndpoint(OAuthMatchEndpointContext context);

        Task TokenEndpoint(OAuthTokenEndpointContext context);

        Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context);

        Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context);

        Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context);

        Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context);

        Task ValidateTokenRequest(OAuthValidateTokenRequestContext context);
    }
}
