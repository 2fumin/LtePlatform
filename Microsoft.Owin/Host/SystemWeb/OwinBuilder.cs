using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Owin.Host.SystemWeb.Infrastructure;
using Microsoft.Owin.Loader;
using Microsoft.Owin.Properties;

namespace Microsoft.Owin.Host.SystemWeb
{
    internal static class OwinBuilder
    {
        internal static OwinAppContext Build()
        {
            return Build(GetAppStartup());
        }

        internal static OwinAppContext Build(Action<IAppBuilder> startup)
        {
            if (startup == null)
            {
                throw new ArgumentNullException(nameof(startup));
            }
            var context = new OwinAppContext();
            context.Initialize(startup);
            return context;
        }

        internal static OwinAppContext Build(Func<IDictionary<string, object>, Task> app)
        {
            return Build(delegate (IAppBuilder builder) {
                builder.Use(app);
            });
        }

        internal static Action<IAppBuilder> GetAppStartup()
        {
            var str = ConfigurationManager.AppSettings[Constants.OwinAppStartup];
            var loader = new DefaultLoader(new ReferencedAssembliesWrapper());
            IList<string> errorDetails = new List<string>();
            var action = loader.Load(str ?? string.Empty, errorDetails);
            if (action == null)
            {
                throw new EntryPointNotFoundException(Resources.Exception_AppLoderFailure + Environment.NewLine + " - " +
                                                      string.Join(Environment.NewLine + " - ", errorDetails) +
                                                      (IsAutomaticAppStartupEnabled
                                                          ? (Environment.NewLine +
                                                             Resources.Exception_HowToDisableAutoAppStartup)
                                                          : string.Empty) + Environment.NewLine +
                                                      Resources.Exception_HowToSpecifyAppStartup);
            }
            return action;
        }

        internal static bool IsAutomaticAppStartupEnabled
        {
            get
            {
                var str = ConfigurationManager.AppSettings["owin:AutomaticAppStartup"];
                return string.IsNullOrWhiteSpace(str) || string.Equals("true", str, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}
