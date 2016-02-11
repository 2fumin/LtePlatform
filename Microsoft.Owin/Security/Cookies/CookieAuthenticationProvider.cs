using System;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.Cookies
{
    public class CookieAuthenticationProvider : ICookieAuthenticationProvider
    {
        public CookieAuthenticationProvider()
        {
            OnValidateIdentity = context => Task.FromResult<object>(null);
            OnResponseSignIn = delegate {
            };
            OnResponseSignedIn = delegate {
            };
            OnResponseSignOut = delegate {
            };
            OnApplyRedirect = DefaultBehavior.ApplyRedirect;
            OnException = delegate {
            };
        }

        public virtual void ApplyRedirect(CookieApplyRedirectContext context)
        {
            OnApplyRedirect(context);
        }

        public virtual void Exception(CookieExceptionContext context)
        {
            OnException(context);
        }

        public virtual void ResponseSignedIn(CookieResponseSignedInContext context)
        {
            OnResponseSignedIn(context);
        }

        public virtual void ResponseSignIn(CookieResponseSignInContext context)
        {
            OnResponseSignIn(context);
        }

        public virtual void ResponseSignOut(CookieResponseSignOutContext context)
        {
            OnResponseSignOut(context);
        }

        public virtual Task ValidateIdentity(CookieValidateIdentityContext context)
        {
            return OnValidateIdentity(context);
        }

        public Action<CookieApplyRedirectContext> OnApplyRedirect { get; set; }

        public Action<CookieExceptionContext> OnException { get; set; }

        public Action<CookieResponseSignedInContext> OnResponseSignedIn { get; set; }

        public Action<CookieResponseSignInContext> OnResponseSignIn { get; set; }

        public Action<CookieResponseSignOutContext> OnResponseSignOut { get; set; }

        public Func<CookieValidateIdentityContext, Task> OnValidateIdentity { get; set; }
    }
}
