using System;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Cookies
{
    public class CookieValidateIdentityContext : BaseContext<CookieAuthenticationOptions>
    {
        public CookieValidateIdentityContext(IOwinContext context, AuthenticationTicket ticket,
            CookieAuthenticationOptions options) : base(context, options)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException(nameof(ticket));
            }
            Identity = ticket.Identity;
            Properties = ticket.Properties;
        }

        public void RejectIdentity()
        {
            Identity = null;
        }

        public void ReplaceIdentity(IIdentity identity)
        {
            Identity = new ClaimsIdentity(identity);
        }

        public ClaimsIdentity Identity { get; private set; }

        public AuthenticationProperties Properties { get; private set; }
    }
}
