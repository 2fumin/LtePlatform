using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Http
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Web.Http.Owin;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class OwinHttpConfigurationExtensions
    {
        public static void SuppressDefaultHostAuthentication(this HttpConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }
            configuration.MessageHandlers.Insert(0, new PassiveAuthenticationMessageHandler());
        }
    }
}
