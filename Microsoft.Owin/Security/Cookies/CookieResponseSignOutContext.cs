using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Cookies
{
    public class CookieResponseSignOutContext : BaseContext<CookieAuthenticationOptions>
    {
        public CookieResponseSignOutContext(IOwinContext context, CookieAuthenticationOptions options,
            CookieOptions cookieOptions) : base(context, options)
        {
            CookieOptions = cookieOptions;
        }

        public CookieOptions CookieOptions { get; set; }
    }
}
