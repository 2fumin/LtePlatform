using System;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Infrastructure
{
    public class AuthenticationTokenCreateContext : BaseContext
    {
        private readonly ISecureDataFormat<AuthenticationTicket> _secureDataFormat;

        public AuthenticationTokenCreateContext(IOwinContext context,
            ISecureDataFormat<AuthenticationTicket> secureDataFormat, AuthenticationTicket ticket) : base(context)
        {
            if (secureDataFormat == null)
            {
                throw new ArgumentNullException(nameof(secureDataFormat));
            }
            if (ticket == null)
            {
                throw new ArgumentNullException(nameof(ticket));
            }
            _secureDataFormat = secureDataFormat;
            Ticket = ticket;
        }

        public string SerializeTicket()
        {
            return _secureDataFormat.Protect(Ticket);
        }

        public void SetToken(string tokenValue)
        {
            if (tokenValue == null)
            {
                throw new ArgumentNullException(nameof(tokenValue));
            }
            Token = tokenValue;
        }

        public AuthenticationTicket Ticket { get; }

        public string Token { get; protected set; }
    }
}
