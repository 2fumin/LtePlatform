using System;

namespace Microsoft.Owin.Security.DataProtection
{
    public class DpapiDataProtectionProvider : IDataProtectionProvider
    {
        private readonly string _appName;

        public DpapiDataProtectionProvider() : this(Guid.NewGuid().ToString())
        {
        }

        public DpapiDataProtectionProvider(string appName)
        {
            if (appName == null)
            {
                throw new ArgumentNullException(nameof(appName));
            }
            _appName = appName;
        }

        public IDataProtector Create(params string[] purposes)
        {
            if (purposes == null)
            {
                throw new ArgumentNullException(nameof(purposes));
            }
            return new DpapiDataProtector(_appName, purposes);
        }
    }
}
