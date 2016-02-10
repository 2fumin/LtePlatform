namespace Microsoft.Owin.Security.Notifications
{
    public class MessageReceivedNotification<TMessage, TOptions> : BaseNotification<TOptions>
    {
        public MessageReceivedNotification(IOwinContext context, TOptions options) : base(context, options)
        {
        }

        public TMessage ProtocolMessage { get; set; }
    }
}
