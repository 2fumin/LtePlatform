using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;

namespace Microsoft.Owin.Security.Cookies
{
    public class CookieAuthenticationMiddleware : AuthenticationMiddleware<CookieAuthenticationOptions>
    {
        private readonly ILogger _logger;

        public CookieAuthenticationMiddleware(OwinMiddleware next, IAppBuilder app, CookieAuthenticationOptions options) : base(next, options)
        {
            if (base.Options.Provider == null)
            {
                base.Options.Provider = new CookieAuthenticationProvider();
            }
            if (string.IsNullOrEmpty(base.Options.CookieName))
            {
                base.Options.CookieName = ".AspNet." + base.Options.AuthenticationType;
            }
            this._logger = app.CreateLogger<CookieAuthenticationMiddleware>();
            if (base.Options.TicketDataFormat == null)
            {
                IDataProtector protector = app.CreateDataProtector(new string[] { typeof(CookieAuthenticationMiddleware).FullName, base.Options.AuthenticationType, "v1" });
                base.Options.TicketDataFormat = new TicketDataFormat(protector);
            }
            if (base.Options.CookieManager == null)
            {
                base.Options.CookieManager = new ChunkingCookieManager();
            }
        }

        protected override AuthenticationHandler<CookieAuthenticationOptions> CreateHandler()
        {
            return new CookieAuthenticationHandler(this._logger);
        }
    }
}
