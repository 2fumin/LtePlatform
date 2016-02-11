using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.OAuth
{
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using System;

    public class OAuthValidateIdentityContext : BaseValidatingTicketContext<OAuthBearerAuthenticationOptions>
    {
        public OAuthValidateIdentityContext(IOwinContext context, OAuthBearerAuthenticationOptions options, AuthenticationTicket ticket) : base(context, options, ticket)
        {
        }
    }
}
