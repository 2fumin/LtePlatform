using System.Security.Claims;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Cookies
{
    public class CookieResponseSignInContext : BaseContext<CookieAuthenticationOptions>
    {
        public CookieResponseSignInContext(IOwinContext context, CookieAuthenticationOptions options, string authenticationType, 
            ClaimsIdentity identity, AuthenticationProperties properties, CookieOptions cookieOptions) : base(context, options)
        {
            AuthenticationType = authenticationType;
            Identity = identity;
            Properties = properties;
            CookieOptions = cookieOptions;
        }

        public string AuthenticationType { get; private set; }

        public CookieOptions CookieOptions { get; set; }

        public ClaimsIdentity Identity { get; set; }

        public AuthenticationProperties Properties { get; set; }
    }
}
