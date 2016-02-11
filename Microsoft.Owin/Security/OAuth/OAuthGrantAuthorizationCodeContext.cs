namespace Microsoft.Owin.Security.OAuth
{
    public class OAuthGrantAuthorizationCodeContext : BaseValidatingTicketContext<OAuthAuthorizationServerOptions>
    {
        public OAuthGrantAuthorizationCodeContext(IOwinContext context, OAuthAuthorizationServerOptions options, 
            AuthenticationTicket ticket) : base(context, options, ticket)
        {
        }
    }
}
