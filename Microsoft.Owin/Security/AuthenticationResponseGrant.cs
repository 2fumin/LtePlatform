using System;
using System.Linq;
using System.Security.Claims;

namespace Microsoft.Owin.Security
{
    public class AuthenticationResponseGrant
    {
        public AuthenticationResponseGrant(ClaimsIdentity identity, AuthenticationProperties properties)
        {
            Principal = new ClaimsPrincipal(identity);
            Identity = identity;
            Properties = properties;
        }

        public AuthenticationResponseGrant(ClaimsPrincipal principal, AuthenticationProperties properties)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            Principal = principal;
            Identity = principal.Identities.FirstOrDefault<ClaimsIdentity>();
            Properties = properties;
        }

        public ClaimsIdentity Identity { get; private set; }

        public ClaimsPrincipal Principal { get; private set; }

        public AuthenticationProperties Properties { get; private set; }
    }
}
