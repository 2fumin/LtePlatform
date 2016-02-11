using System.Threading.Tasks;

namespace Microsoft.Owin.Security.Cookies
{
    public interface ICookieAuthenticationProvider
    {
        void ApplyRedirect(CookieApplyRedirectContext context);

        void Exception(CookieExceptionContext context);

        void ResponseSignedIn(CookieResponseSignedInContext context);

        void ResponseSignIn(CookieResponseSignInContext context);

        void ResponseSignOut(CookieResponseSignOutContext context);

        Task ValidateIdentity(CookieValidateIdentityContext context);
    }
}
