namespace Microsoft.Owin.Security.OAuth.Messages
{
    public class TokenEndpointRequestAuthorizationCode
    {
        public string Code { get; set; }

        public string RedirectUri { get; set; }
    }
}
