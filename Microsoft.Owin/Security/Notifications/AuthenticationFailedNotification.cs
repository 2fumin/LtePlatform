using System;

namespace Microsoft.Owin.Security.Notifications
{
    public class AuthenticationFailedNotification<TMessage, TOptions> : BaseNotification<TOptions>
    {
        public AuthenticationFailedNotification(IOwinContext context, TOptions options) : base(context, options)
        {
        }

        public Exception Exception { get; set; }

        public TMessage ProtocolMessage { get; set; }
    }
}
