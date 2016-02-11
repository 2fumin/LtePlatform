using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.OAuth
{
    using Microsoft.Owin;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class BaseValidatingClientContext : BaseValidatingContext<OAuthAuthorizationServerOptions>
    {
        protected BaseValidatingClientContext(IOwinContext context, OAuthAuthorizationServerOptions options, string clientId) : base(context, options)
        {
            this.ClientId = clientId;
        }

        public string ClientId { get; protected set; }
    }
}
