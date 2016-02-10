using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.DataProtection
{
    using Microsoft.Owin.Security;
    using Owin;
    using System;
    using System.Runtime.CompilerServices;

    public static class AppBuilderExtensions
    {
        public static IDataProtector CreateDataProtector(this IAppBuilder app, params string[] purposes)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }
            IDataProtectionProvider dataProtectionProvider = app.GetDataProtectionProvider();
            if (dataProtectionProvider == null)
            {
                dataProtectionProvider = FallbackDataProtectionProvider(app);
            }
            return dataProtectionProvider.Create(purposes);
        }

        private static IDataProtectionProvider FallbackDataProtectionProvider(IAppBuilder app)
        {
            return new DpapiDataProtectionProvider(GetAppName(app));
        }

        private static string GetAppName(IAppBuilder app)
        {
            object obj2;
            if (app.Properties.TryGetValue("host.AppName", out obj2))
            {
                string str = obj2 as string;
                if (!string.IsNullOrEmpty(str))
                {
                    return str;
                }
            }
            throw new NotSupportedException(Resources.Exception_DefaultDpapiRequiresAppNameKey);
        }

        public static IDataProtectionProvider GetDataProtectionProvider(this IAppBuilder app)
        {
            object obj2;
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }
            if (app.Properties.TryGetValue("security.DataProtectionProvider", out obj2))
            {
                Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>> create = obj2 as Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>>;
                if (create != null)
                {
                    return new CallDataProtectionProvider(create);
                }
            }
            return null;
        }

        public static void SetDataProtectionProvider(this IAppBuilder app, IDataProtectionProvider dataProtectionProvider)
        {
            Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>> func = null;
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }
            if (dataProtectionProvider == null)
            {
                app.Properties.Remove("security.DataProtectionProvider");
            }
            else
            {
                if (func == null)
                {
                    func = delegate (string[] purposes) {
                        IDataProtector protector = dataProtectionProvider.Create(purposes);
                        return new Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>(new Func<byte[], byte[]>(protector.Protect), new Func<byte[], byte[]>(protector.Unprotect));
                    };
                }
                app.Properties["security.DataProtectionProvider"] = func;
            }
        }

        private class CallDataProtectionProvider : IDataProtectionProvider
        {
            private readonly Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>> _create;

            public CallDataProtectionProvider(Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>> create)
            {
                this._create = create;
            }

            public IDataProtector Create(params string[] purposes)
            {
                Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>> tuple = this._create(purposes);
                return new CallDataProtection(tuple.Item1, tuple.Item2);
            }

            private class CallDataProtection : IDataProtector
            {
                private readonly Func<byte[], byte[]> _protect;
                private readonly Func<byte[], byte[]> _unprotect;

                public CallDataProtection(Func<byte[], byte[]> protect, Func<byte[], byte[]> unprotect)
                {
                    this._protect = protect;
                    this._unprotect = unprotect;
                }

                public byte[] Protect(byte[] userData)
                {
                    return this._protect(userData);
                }

                public byte[] Unprotect(byte[] protectedData)
                {
                    return this._unprotect(protectedData);
                }
            }
        }
    }
}
