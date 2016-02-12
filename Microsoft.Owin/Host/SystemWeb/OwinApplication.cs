using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin.Host.SystemWeb
{
    using System;
    using System.Threading;

    internal static class OwinApplication
    {
        private static ShutdownDetector _detector;
        private static Lazy<OwinAppContext> _instance = new Lazy<OwinAppContext>(new Func<OwinAppContext>(OwinBuilder.Build));

        private static ShutdownDetector InitShutdownDetector()
        {
            ShutdownDetector detector = new ShutdownDetector();
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
        {
            get
            {
                return LazyInitializer.EnsureInitialized<ShutdownDetector>(ref _detector, new Func<ShutdownDetector>(OwinApplication.InitShutdownDetector)).Token;
            }
        }
    }
}
