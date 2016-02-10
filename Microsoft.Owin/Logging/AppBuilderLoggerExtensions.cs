using System;
using System.Diagnostics;

namespace Microsoft.Owin.Logging
{
    public static class AppBuilderLoggerExtensions
    {
        public static ILogger CreateLogger<TType>(this IAppBuilder app)
        {
            return app.CreateLogger(typeof(TType));
        }

        public static ILogger CreateLogger(this IAppBuilder app, string name)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            return (app.GetLoggerFactory() ?? LoggerFactory.Default).Create(name);
        }

        public static ILogger CreateLogger(this IAppBuilder app, Type component)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }
            return app.CreateLogger(component.FullName);
        }

        public static ILoggerFactory GetLoggerFactory(this IAppBuilder app)
        {
            object obj2;
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (app.Properties.TryGetValue("server.LoggerFactory", out obj2))
            {
                Func<string, Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool>> create = obj2 as Func<string, Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool>>;
                if (create != null)
                {
                    return new WrapLoggerFactory(create);
                }
            }
            return null;
        }

        public static void SetLoggerFactory(this IAppBuilder app, ILoggerFactory loggerFactory)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            //app.Properties["server.LoggerFactory"] = name => new Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool>(loggerFactory.Create(name).WriteCore);
        }

        private class WrapLoggerFactory : ILoggerFactory
        {
            private readonly Func<string, Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool>> _create;

            public WrapLoggerFactory(Func<string, Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool>> create)
            {
                if (create == null)
                {
                    throw new ArgumentNullException(nameof(create));
                }
                this._create = create;
            }

            public ILogger Create(string name)
            {
                return new WrappingLogger(this._create(name));
            }
        }

        private class WrappingLogger : ILogger
        {
            private readonly Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool> _write;

            public WrappingLogger(Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool> write)
            {
                if (write == null)
                {
                    throw new ArgumentNullException(nameof(write));
                }
                this._write = write;
            }

            public bool WriteCore(TraceEventType eventType, int eventId, object state, Exception exception, Func<object, Exception, string> message)
            {
                return this._write(eventType, eventId, state, exception, message);
            }
        }
    }
}
