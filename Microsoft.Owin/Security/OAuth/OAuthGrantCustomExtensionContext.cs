namespace Microsoft.Owin.Security.OAuth
{
    public class OAuthGrantCustomExtensionContext : BaseValidatingTicketContext<OAuthAuthorizationServerOptions>
    {
        public OAuthGrantCustomExtensionContext(IOwinContext context, OAuthAuthorizationServerOptions options, 
            string clientId, string grantType, IReadableStringCollection parameters) : base(context, options, null)
        {
            ClientId = clientId;
            GrantType = grantType;
            Parameters = parameters;
        }

        public string ClientId { get; private set; }

        public string GrantType { get; private set; }

        public IReadableStringCollection Parameters { get; private set; }
    }
}
