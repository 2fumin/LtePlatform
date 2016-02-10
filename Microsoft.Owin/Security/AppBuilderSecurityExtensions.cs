using System;
using Microsoft.Owin.Properties;

namespace Microsoft.Owin.Security
{
    public static class AppBuilderSecurityExtensions
    {
        public static string GetDefaultSignInAsAuthenticationType(this IAppBuilder app)
        {
            object obj2;
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (!app.Properties.TryGetValue(Constants.DefaultSignInAsAuthenticationType, out obj2))
                throw new InvalidOperationException(Resources.Exception_MissingDefaultSignInAsAuthenticationType);
            var str = obj2 as string;
            if (!string.IsNullOrEmpty(str))
            {
                return str;
            }
            throw new InvalidOperationException(Resources.Exception_MissingDefaultSignInAsAuthenticationType);
        }

        public static void SetDefaultSignInAsAuthenticationType(this IAppBuilder app, string authenticationType)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (authenticationType == null)
            {
                throw new ArgumentNullException(nameof(authenticationType));
            }
            app.Properties[Constants.DefaultSignInAsAuthenticationType] = authenticationType;
        }
    }
}
