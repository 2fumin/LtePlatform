namespace Microsoft.Owin.Security.OAuth
{
    public abstract class BaseValidatingClientContext : BaseValidatingContext<OAuthAuthorizationServerOptions>
    {
        protected BaseValidatingClientContext(IOwinContext context, OAuthAuthorizationServerOptions options, 
            string clientId) : base(context, options)
        {
            ClientId = clientId;
        }

        public string ClientId { get; protected set; }
    }
}
