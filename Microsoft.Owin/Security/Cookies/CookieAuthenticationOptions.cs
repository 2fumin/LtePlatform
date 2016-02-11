using System;
using Microsoft.Owin.Infrastructure;

namespace Microsoft.Owin.Security.Cookies
{
    public class CookieAuthenticationOptions : AuthenticationOptions
    {
        private string _cookieName;

        public CookieAuthenticationOptions() : base("Cookies")
        {
            this.ReturnUrlParameter = "ReturnUrl";
            this.CookiePath = "/";
            this.ExpireTimeSpan = TimeSpan.FromDays(14.0);
            this.SlidingExpiration = true;
            this.CookieHttpOnly = true;
            this.CookieSecure = CookieSecureOption.SameAsRequest;
            this.SystemClock = new SystemClock();
            this.Provider = new CookieAuthenticationProvider();
        }

        public string CookieDomain { get; set; }

        public bool CookieHttpOnly { get; set; }

        public ICookieManager CookieManager { get; set; }

        public string CookieName
        {
            get
            {
                return this._cookieName;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                this._cookieName = value;
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
