using System.Security.Claims;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Cookies
{
    public class CookieResponseSignedInContext : BaseContext<CookieAuthenticationOptions>
    {
        public CookieResponseSignedInContext(IOwinContext context, CookieAuthenticationOptions options, 
            string authenticationType, ClaimsIdentity identity, AuthenticationProperties properties) : base(context, options)
        {
            AuthenticationType = authenticationType;
            Identity = identity;
            Properties = properties;
        }

        public string AuthenticationType { get; private set; }

        public ClaimsIdentity Identity { get; private set; }

        public AuthenticationProperties Properties { get; private set; }
    }
}
