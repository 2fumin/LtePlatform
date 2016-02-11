using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.OAuth
{
    public class OAuthChallengeContext : BaseContext
    {
        public OAuthChallengeContext(IOwinContext context, string challenge) : base(context)
        {
            Challenge = challenge;
        }

        public string Challenge { get; protected set; }
    }
}
