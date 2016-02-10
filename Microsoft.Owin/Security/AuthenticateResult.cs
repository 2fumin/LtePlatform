using System;
using System.Security.Claims;
using System.Security.Principal;

namespace Microsoft.Owin.Security
{
    public class AuthenticateResult
    {
        public AuthenticateResult(IIdentity identity, AuthenticationProperties properties, AuthenticationDescription description)
        {
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }
            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }
            if (identity != null)
            {
                Identity = (identity as ClaimsIdentity) ?? new ClaimsIdentity(identity);
            }
            Properties = properties;
            Description = description;
        }

        public AuthenticationDescription Description { get; private set; }

        public ClaimsIdentity Identity { get; private set; }

        public AuthenticationProperties Properties { get; private set; }
    }
}
