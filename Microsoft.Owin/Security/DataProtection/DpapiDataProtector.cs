using System.Security.Cryptography;

namespace Microsoft.Owin.Security.DataProtection
{
    internal class DpapiDataProtector : IDataProtector
    {
        private readonly System.Security.Cryptography.DpapiDataProtector _protector;

        public DpapiDataProtector(string appName, string[] purposes)
        {
            var protector = new System.Security.Cryptography.DpapiDataProtector(appName, "Microsoft.Owin.Security.IDataProtector", purposes)
            {
                Scope = DataProtectionScope.CurrentUser
            };
            _protector = protector;
        }

        public byte[] Protect(byte[] userData)
        {
            return _protector.Protect(userData);
        }

        public byte[] Unprotect(byte[] protectedData)
        {
            return _protector.Unprotect(protectedData);
        }
    }
}
