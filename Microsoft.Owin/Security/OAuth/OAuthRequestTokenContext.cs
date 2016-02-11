using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.OAuth
{
    public class OAuthRequestTokenContext : BaseContext
    {
        public OAuthRequestTokenContext(IOwinContext context, string token) : base(context)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
