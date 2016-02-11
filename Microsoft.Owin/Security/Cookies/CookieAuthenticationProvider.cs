using System;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.Cookies
{
    public class CookieAuthenticationProvider : ICookieAuthenticationProvider
    {
        public CookieAuthenticationProvider()
        {
            this.OnValidateIdentity = context => Task.FromResult<object>(null);
            this.OnResponseSignIn = delegate (CookieResponseSignInContext context) {
            };
            this.OnResponseSignedIn = delegate (CookieResponseSignedInContext context) {
            };
            this.OnResponseSignOut = delegate (CookieResponseSignOutContext context) {
            };
            this.OnApplyRedirect = DefaultBehavior.ApplyRedirect;
            this.OnException = delegate (CookieExceptionContext context) {
            };
        }

        public virtual void ApplyRedirect(CookieApplyRedirectContext context)
        {
            this.OnApplyRedirect(context);
        }

        public virtual void Exception(CookieExceptionContext context)
        {
            this.OnException(context);
        }

        public virtual void ResponseSignedIn(CookieResponseSignedInContext context)
        {
            this.OnResponseSignedIn(context);
        }

        public virtual void ResponseSignIn(CookieResponseSignInContext context)
        {
            this.OnResponseSignIn(context);
        }

        public virtual void ResponseSignOut(CookieResponseSignOutContext context)
        {
            this.OnResponseSignOut(context);
        }

        public virtual Task ValidateIdentity(CookieValidateIdentityContext context)
        {
            return this.OnValidateIdentity(context);
        }

        public Action<CookieApplyRedirectContext> OnApplyRedirect { get; set; }

        public Action<CookieExceptionContext> OnException { get; set; }

        public Action<CookieResponseSignedInContext> OnResponseSignedIn { get; set; }

        public Action<CookieResponseSignInContext> OnResponseSignIn { get; set; }

        public Action<CookieResponseSignOutContext> OnResponseSignOut { get; set; }

        public Func<CookieValidateIdentityContext, Task> OnValidateIdentity { get; set; }
    }
}
