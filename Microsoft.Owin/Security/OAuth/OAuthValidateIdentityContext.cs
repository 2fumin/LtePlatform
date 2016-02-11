namespace Microsoft.Owin.Security.OAuth
{
    public class OAuthValidateIdentityContext : BaseValidatingTicketContext<OAuthBearerAuthenticationOptions>
    {
        public OAuthValidateIdentityContext(IOwinContext context, OAuthBearerAuthenticationOptions options, 
            AuthenticationTicket ticket) : base(context, options, ticket)
        {
        }
    }
}
