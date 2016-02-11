namespace Microsoft.Owin.Security.OAuth
{
    public class OAuthGrantRefreshTokenContext : BaseValidatingTicketContext<OAuthAuthorizationServerOptions>
    {
        public OAuthGrantRefreshTokenContext(IOwinContext context, OAuthAuthorizationServerOptions options, 
            AuthenticationTicket ticket, string clientId) : base(context, options, ticket)
        {
            ClientId = clientId;
        }

        public string ClientId { get; private set; }
    }
}
