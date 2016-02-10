namespace Microsoft.Owin.Security.Notifications
{
    public class SecurityTokenValidatedNotification<TMessage, TOptions> : BaseNotification<TOptions>
    {
        public SecurityTokenValidatedNotification(IOwinContext context, TOptions options) : base(context, options)
        {
        }

        public AuthenticationTicket AuthenticationTicket { get; set; }

        public TMessage ProtocolMessage { get; set; }
    }
}
