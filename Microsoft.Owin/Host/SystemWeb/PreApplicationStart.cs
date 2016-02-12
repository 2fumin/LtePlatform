using System;
using System.ComponentModel;
using System.Web;
using Microsoft.Owin.Host.SystemWeb.Infrastructure;
using Microsoft.Owin.Properties;

namespace Microsoft.Owin.Host.SystemWeb
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class PreApplicationStart
    {
        private const string TraceName = "Microsoft.Owin.Host.SystemWeb.PreApplicationStart";

        public static void Initialize()
        {
            try
            {
                if (OwinBuilder.IsAutomaticAppStartupEnabled)
                {
                    HttpApplication.RegisterModule(typeof(OwinHttpModule));
                }
            }
            catch (Exception exception)
            {
                TraceFactory.Create(TraceName).WriteError(Resources.Trace_RegisterModuleException, exception);
                throw;
            }
        }
    }
}
