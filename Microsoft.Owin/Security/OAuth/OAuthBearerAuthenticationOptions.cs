using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security.Infrastructure;

namespace Microsoft.Owin.Security.OAuth
{
    public class OAuthBearerAuthenticationOptions : AuthenticationOptions
    {
        public OAuthBearerAuthenticationOptions() : base("Bearer")
        {
            this.SystemClock = new SystemClock();
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; set; }

        public IAuthenticationTokenProvider AccessTokenProvider { get; set; }

        public string Challenge { get; set; }

        public IOAuthBearerAuthenticationProvider Provider { get; set; }

        public string Realm { get; set; }

        public ISystemClock SystemClock { get; set; }
    }
}
