using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Notifications
{
    public class BaseNotification<TOptions> : BaseContext<TOptions>
    {
        protected BaseNotification(IOwinContext context, TOptions options) : base(context, options)
        {
        }

        public void HandleResponse()
        {
            State = NotificationResultState.HandledResponse;
        }

        public void SkipToNextMiddleware()
        {
            State = NotificationResultState.Skipped;
        }

        public bool HandledResponse => (State == NotificationResultState.HandledResponse);

        public bool Skipped => (State == NotificationResultState.Skipped);

        public NotificationResultState State { get; set; }
    }
}
