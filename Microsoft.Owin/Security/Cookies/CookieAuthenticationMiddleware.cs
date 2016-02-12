using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;
using Owin;

namespace Microsoft.Owin.Security.Cookies
{
    public class CookieAuthenticationMiddleware : AuthenticationMiddleware<CookieAuthenticationOptions>
    {
        private readonly ILogger _logger;

        public CookieAuthenticationMiddleware(OwinMiddleware next, IAppBuilder app, 
            CookieAuthenticationOptions options) : base(next, options)
        {
            if (Options.Provider == null)
            {
                Options.Provider = new CookieAuthenticationProvider();
            }
            if (string.IsNullOrEmpty(Options.CookieName))
            {
                Options.CookieName = ".AspNet." + Options.AuthenticationType;
            }
            _logger = app.CreateLogger<CookieAuthenticationMiddleware>();
            if (Options.TicketDataFormat == null)
            {
                var protector = app.CreateDataProtector(typeof (CookieAuthenticationMiddleware).FullName,
                    Options.AuthenticationType, "v1");
                Options.TicketDataFormat = new TicketDataFormat(protector);
            }
            if (Options.CookieManager == null)
            {
                Options.CookieManager = new ChunkingCookieManager();
            }
        }

        protected override AuthenticationHandler<CookieAuthenticationOptions> CreateHandler()
        {
            return new CookieAuthenticationHandler(_logger);
        }
    }
}
