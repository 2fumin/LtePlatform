using System;
using Microsoft.AspNet.Identity.Owin.Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace Microsoft.AspNet.Identity.Owin
{
    public class IdentityFactoryOptions<T> where T : IDisposable
    {
        public IDataProtectionProvider DataProtectionProvider { get; set; }

        public IIdentityFactoryProvider<T> Provider { get; set; }
    }
}
