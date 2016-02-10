using System;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Infrastructure
{
    public class AuthenticationTokenReceiveContext : BaseContext
    {
        private readonly ISecureDataFormat<AuthenticationTicket> _secureDataFormat;

        public AuthenticationTokenReceiveContext(IOwinContext context,
            ISecureDataFormat<AuthenticationTicket> secureDataFormat, string token) : base(context)
        {
            if (secureDataFormat == null)
            {
                throw new ArgumentNullException(nameof(secureDataFormat));
            }
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }
            _secureDataFormat = secureDataFormat;
            Token = token;
        }

        public void DeserializeTicket(string protectedData)
        {
            Ticket = _secureDataFormat.Unprotect(protectedData);
        }

        public void SetTicket(AuthenticationTicket ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException(nameof(ticket));
            }
            Ticket = ticket;
        }

        public AuthenticationTicket Ticket { get; protected set; }

        public string Token { get; protected set; }
    }
}
