using System.Security.Claims;

namespace Microsoft.Owin.Security
{
    public class AuthenticationTicket
    {
        public AuthenticationTicket(ClaimsIdentity identity, AuthenticationProperties properties)
        {
            Identity = identity;
            Properties = properties ?? new AuthenticationProperties();
        }

        public ClaimsIdentity Identity { get; private set; }

        public AuthenticationProperties Properties { get; private set; }
    }
}
