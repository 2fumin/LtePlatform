using System;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Cookies
{
    public class CookieExceptionContext : BaseContext<CookieAuthenticationOptions>
    {
        public CookieExceptionContext(IOwinContext context, CookieAuthenticationOptions options, ExceptionLocation location, 
            Exception exception, AuthenticationTicket ticket) : base(context, options)
        {
            Location = location;
            Exception = exception;
            Rethrow = true;
            Ticket = ticket;
        }

        public Exception Exception { get; private set; }

        public ExceptionLocation Location { get; private set; }

        public bool Rethrow { get; set; }

        public AuthenticationTicket Ticket { get; set; }

        public enum ExceptionLocation
        {
            AuthenticateAsync,
            ApplyResponseGrant,
            ApplyResponseChallenge
        }
    }
}
