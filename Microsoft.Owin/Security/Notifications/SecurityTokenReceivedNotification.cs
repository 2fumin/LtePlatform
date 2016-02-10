namespace Microsoft.Owin.Security.Notifications
{
    public class SecurityTokenReceivedNotification<TMessage, TOptions> : BaseNotification<TOptions>
    {
        public SecurityTokenReceivedNotification(IOwinContext context, TOptions options) : base(context, options)
        {
        }

        public TMessage ProtocolMessage { get; set; }
    }
}
