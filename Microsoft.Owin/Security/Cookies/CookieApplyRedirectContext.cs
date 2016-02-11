using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Cookies
{
    public class CookieApplyRedirectContext : BaseContext<CookieAuthenticationOptions>
    {
        public CookieApplyRedirectContext(IOwinContext context, CookieAuthenticationOptions options, string redirectUri) : base(context, options)
        {
            RedirectUri = redirectUri;
        }

        public string RedirectUri { get; set; }
    }
}
