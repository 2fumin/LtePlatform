using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.OAuth
{
    using Microsoft.Owin.Infrastructure;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Infrastructure;
    using System;
    using System.Runtime.CompilerServices;

    public class OAuthBearerAuthenticationOptions : AuthenticationOptions
    {
        public OAuthBearerAuthenticationOptions() : base("Bearer")
        {
            this.SystemClock = new Microsoft.Owin.Infrastructure.SystemClock();
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; set; }

        public IAuthenticationTokenProvider AccessTokenProvider { get; set; }

        public string Challenge { get; set; }

        public IOAuthBearerAuthenticationProvider Provider { get; set; }

        public string Realm { get; set; }

        public ISystemClock SystemClock { get; set; }
    }
}
