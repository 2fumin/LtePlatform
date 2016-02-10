using System.Security.Claims;

namespace Microsoft.Owin.Security.Provider
{
    public abstract class ReturnEndpointContext : EndpointContext
    {
        protected ReturnEndpointContext(IOwinContext context, AuthenticationTicket ticket) : base(context)
        {
            if (ticket == null) return;
            Identity = ticket.Identity;
            Properties = ticket.Properties;
        }

        public ClaimsIdentity Identity { get; set; }

        public AuthenticationProperties Properties { get; set; }

        public string RedirectUri { get; set; }

        public string SignInAsAuthenticationType { get; set; }
    }
}
