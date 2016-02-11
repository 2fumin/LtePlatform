using System;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security.Infrastructure;

namespace Microsoft.Owin.Security.OAuth
{
    public class OAuthAuthorizationServerOptions : AuthenticationOptions
    {
        public OAuthAuthorizationServerOptions() : base("Bearer")
        {
            AuthorizationCodeExpireTimeSpan = TimeSpan.FromMinutes(5.0);
            AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(20.0);
            SystemClock = new SystemClock();
        }

        public TimeSpan AccessTokenExpireTimeSpan { get; set; }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; set; }

        public IAuthenticationTokenProvider AccessTokenProvider { get; set; }

        public bool AllowInsecureHttp { get; set; }

        public bool ApplicationCanDisplayErrors { get; set; }

        public TimeSpan AuthorizationCodeExpireTimeSpan { get; set; }

        public ISecureDataFormat<AuthenticationTicket> AuthorizationCodeFormat { get; set; }

        public IAuthenticationTokenProvider AuthorizationCodeProvider { get; set; }

        public PathString AuthorizeEndpointPath { get; set; }

        public PathString FormPostEndpoint { get; set; }

        public IOAuthAuthorizationServerProvider Provider { get; set; }

        public ISecureDataFormat<AuthenticationTicket> RefreshTokenFormat { get; set; }

        public IAuthenticationTokenProvider RefreshTokenProvider { get; set; }

        public ISystemClock SystemClock { get; set; }

        public PathString TokenEndpointPath { get; set; }
    }
}
