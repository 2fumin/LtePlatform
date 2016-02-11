using System.Security.Claims;

namespace Microsoft.Owin.Security.OAuth
{
    public abstract class BaseValidatingTicketContext<TOptions> : BaseValidatingContext<TOptions>
    {
        protected BaseValidatingTicketContext(IOwinContext context, TOptions options, 
            AuthenticationTicket ticket) : base(context, options)
        {
            Ticket = ticket;
        }

        public bool Validated(AuthenticationTicket ticket)
        {
            Ticket = ticket;
            return Validated();
        }

        public bool Validated(ClaimsIdentity identity)
        {
            AuthenticationProperties properties = (Ticket != null) ? Ticket.Properties : new AuthenticationProperties();
            return Validated(new AuthenticationTicket(identity, properties));
        }

        public AuthenticationTicket Ticket { get; private set; }
    }
}
