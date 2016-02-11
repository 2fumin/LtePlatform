using System.Threading.Tasks;

namespace Microsoft.Owin.Security.OAuth
{
    public interface IOAuthBearerAuthenticationProvider
    {
        Task ApplyChallenge(OAuthChallengeContext context);

        Task RequestToken(OAuthRequestTokenContext context);

        Task ValidateIdentity(OAuthValidateIdentityContext context);
    }
}
