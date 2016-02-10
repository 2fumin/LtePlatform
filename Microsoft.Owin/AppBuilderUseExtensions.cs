using System;
using System.Threading.Tasks;
using Microsoft.Owin.Extensions;

namespace Microsoft.Owin
{
    public static class AppBuilderUseExtensions
    {
        public static void Run(this IAppBuilder app, Func<IOwinContext, Task> handler)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }
            app.Use<UseHandlerMiddleware>(new object[] { handler });
        }

        public static IAppBuilder Use(this IAppBuilder app, Func<IOwinContext, Func<Task>, Task> handler)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }
            return app.Use<UseHandlerMiddleware>(new object[] { handler });
        }

        public static IAppBuilder Use<T>(this IAppBuilder app, params object[] args)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            return app.Use(typeof(T), args);
        }
    }
}
