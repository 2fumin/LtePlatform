using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Notifications
{
    public class RedirectToIdentityProviderNotification<TMessage, TOptions> : BaseContext<TOptions>
    {
        public RedirectToIdentityProviderNotification(IOwinContext context, TOptions options) : base(context, options)
        {
        }

        public void HandleResponse()
        {
            State = NotificationResultState.HandledResponse;
        }

        public bool HandledResponse => (State == NotificationResultState.HandledResponse);

        public TMessage ProtocolMessage { get; set; }

        public NotificationResultState State { get; set; }
    }
}
