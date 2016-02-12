using System;
using System.Threading;

namespace Microsoft.Owin.Host.SystemWeb
{
    internal static class OwinApplication
    {
        private static ShutdownDetector _detector;
        private static Lazy<OwinAppContext> _instance = new Lazy<OwinAppContext>(OwinBuilder.Build);

        private static ShutdownDetector InitShutdownDetector()
        {
            var detector = new ShutdownDetector();
            detector.Initialize();
            return detector;
        }

        internal static Func<OwinAppContext> Accessor
        {
            get
            {
                return () => _instance.Value;
            }
            set
            {
                _instance = new Lazy<OwinAppContext>(value);
            }
        }

        internal static OwinAppContext Instance
        {
            get
            {
                return _instance.Value;
            }
            set
            {
                _instance = new Lazy<OwinAppContext>(() => value);
            }
        }

        internal static CancellationToken ShutdownToken
            => LazyInitializer.EnsureInitialized(ref _detector, InitShutdownDetector).Token;
    }
}
