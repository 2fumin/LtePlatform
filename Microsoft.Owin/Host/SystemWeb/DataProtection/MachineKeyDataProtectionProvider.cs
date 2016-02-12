using System;

namespace Microsoft.Owin.Host.SystemWeb.DataProtection
{
    internal class MachineKeyDataProtectionProvider
    {
        public virtual MachineKeyDataProtector Create(params string[] purposes)
        {
            return new MachineKeyDataProtector(purposes);
        }

        public virtual Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>> ToOwinFunction()
        {
            return delegate (string[] purposes) {
                MachineKeyDataProtector protector = this.Create(purposes);
                return new Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>(protector.Protect, protector.Unprotect);
            };
        }
    }
}
