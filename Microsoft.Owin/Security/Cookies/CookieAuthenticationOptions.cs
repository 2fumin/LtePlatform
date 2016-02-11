using System;
using Microsoft.Owin.Infrastructure;

namespace Microsoft.Owin.Security.Cookies
{
    public class CookieAuthenticationOptions : AuthenticationOptions
    {
        private string _cookieName;

        public CookieAuthenticationOptions() : base(CookieAuthenticationDefaults.AuthenticationType)
        {
            ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            CookiePath = "/";
            ExpireTimeSpan = TimeSpan.FromDays(14.0);
            SlidingExpiration = true;
            CookieHttpOnly = true;
            CookieSecure = CookieSecureOption.SameAsRequest;
            SystemClock = new SystemClock();
            Provider = new CookieAuthenticationProvider();
        }

        public string CookieDomain { get; set; }

        public bool CookieHttpOnly { get; set; }

        public ICookieManager CookieManager { get; set; }

        public string CookieName
        {
            get
            {
                return _cookieName;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _cookieName = value;
            }
        }

        public string CookiePath { get; set; }

        public CookieSecureOption CookieSecure { get; set; }

        public TimeSpan ExpireTimeSpan { get; set; }

        public PathString LoginPath { get; set; }

        public PathString LogoutPath { get; set; }

        public ICookieAuthenticationProvider Provider { get; set; }

        public string ReturnUrlParameter { get; set; }

        public IAuthenticationSessionStore SessionStore { get; set; }

        public bool SlidingExpiration { get; set; }

        public ISystemClock SystemClock { get; set; }

        public ISecureDataFormat<AuthenticationTicket> TicketDataFormat { get; set; }
    }
}
