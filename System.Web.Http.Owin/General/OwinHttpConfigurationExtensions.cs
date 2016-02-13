using System.ComponentModel;
using System.Web.Http.Owin;

namespace System.Web.Http
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class OwinHttpConfigurationExtensions
    {
        public static void SuppressDefaultHostAuthentication(this HttpConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            configuration.MessageHandlers.Insert(0, new PassiveAuthenticationMessageHandler());
        }
    }
}
