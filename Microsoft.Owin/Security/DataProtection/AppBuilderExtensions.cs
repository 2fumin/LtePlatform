using System;
using Microsoft.Owin.Properties;

namespace Microsoft.Owin.Security.DataProtection
{
    public static class AppBuilderExtensions
    {
        public static IDataProtector CreateDataProtector(this IAppBuilder app, params string[] purposes)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            var dataProtectionProvider = app.GetDataProtectionProvider() ?? FallbackDataProtectionProvider(app);
            return dataProtectionProvider.Create(purposes);
        }

        private static IDataProtectionProvider FallbackDataProtectionProvider(IAppBuilder app)
        {
            return new DpapiDataProtectionProvider(GetAppName(app));
        }

        private static string GetAppName(IAppBuilder app)
        {
            object obj2;
            if (!app.Properties.TryGetValue(OwinConstants.CommonKeys.AppName, out obj2))
                throw new NotSupportedException(Resources.Exception_DefaultDpapiRequiresAppNameKey);
            var str = obj2 as string;
            if (!string.IsNullOrEmpty(str))
            {
                return str;
            }
            throw new NotSupportedException(Resources.Exception_DefaultDpapiRequiresAppNameKey);
        }

        public static IDataProtectionProvider GetDataProtectionProvider(this IAppBuilder app)
        {
            object obj2;
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (!app.Properties.TryGetValue(OwinConstants.Security.DataProtectionProvider, out obj2)) return null;
            var create = obj2 as Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>>;
            return create != null ? new CallDataProtectionProvider(create) : null;
        }

        public static void SetDataProtectionProvider(this IAppBuilder app, IDataProtectionProvider dataProtectionProvider)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (dataProtectionProvider == null)
            {
                app.Properties.Remove(OwinConstants.Security.DataProtectionProvider);
            }
            else
            {
                Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>> func = delegate(string[] purposes)
                {
                    var protector = dataProtectionProvider.Create(purposes);
                    return new Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>(protector.Protect, protector.Unprotect);
                };
                app.Properties[OwinConstants.Security.DataProtectionProvider] = func;
            }
        }

        private class CallDataProtectionProvider : IDataProtectionProvider
        {
            private readonly Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>> _create;

            public CallDataProtectionProvider(Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>> create)
            {
                _create = create;
            }

            public IDataProtector Create(params string[] purposes)
            {
                var tuple = _create(purposes);
                return new CallDataProtection(tuple.Item1, tuple.Item2);
            }

            private class CallDataProtection : IDataProtector
            {
                private readonly Func<byte[], byte[]> _protect;
                private readonly Func<byte[], byte[]> _unprotect;

                public CallDataProtection(Func<byte[], byte[]> protect, Func<byte[], byte[]> unprotect)
                {
                    _protect = protect;
                    _unprotect = unprotect;
                }

                public byte[] Protect(byte[] userData)
                {
                    return _protect(userData);
                }

                public byte[] Unprotect(byte[] protectedData)
                {
                    return _unprotect(protectedData);
                }
            }
        }
    }
}
