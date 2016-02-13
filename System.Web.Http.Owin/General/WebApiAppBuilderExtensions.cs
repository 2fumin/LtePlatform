using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Hosting;
using System.Web.Http.Owin;

namespace Owin
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class WebApiAppBuilderExtensions
    {
        private static readonly IHostBufferPolicySelector _defaultBufferPolicySelector = new OwinBufferPolicySelector();

        private static HttpMessageHandlerOptions CreateOptions(IAppBuilder builder, HttpServer server, 
            HttpConfiguration configuration)
        {
            var services = configuration.Services;
            var selector = services.GetHostBufferPolicySelector() ?? _defaultBufferPolicySelector;
            var logger = ExceptionServices.GetLogger(services);
            var handler = ExceptionServices.GetHandler(services);
            return new HttpMessageHandlerOptions
            {
                MessageHandler = server,
                BufferPolicySelector = selector,
                ExceptionLogger = logger,
                ExceptionHandler = handler,
                AppDisposing = builder.GetOnAppDisposingProperty()
            };
        }

        internal static CancellationToken GetOnAppDisposingProperty(this IAppBuilder builder)
        {
            object obj2;
            var properties = builder.Properties;
            if (properties == null)
            {
                return CancellationToken.None;
            }
            if (!properties.TryGetValue("host.OnAppDisposing", out obj2))
            {
                return CancellationToken.None;
            }
            var nullable = obj2 as CancellationToken?;
            if (!nullable.HasValue)
            {
                return CancellationToken.None;
            }
            return nullable.Value;
        }

        private static IAppBuilder UseMessageHandler(this IAppBuilder builder, HttpMessageHandlerOptions options)
        {
            return builder.Use(typeof(HttpMessageHandlerAdapter), options);
        }

        public static IAppBuilder UseWebApi(this IAppBuilder builder, HttpConfiguration configuration)
        {
            IAppBuilder builder2;
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            var server = new HttpServer(configuration);
            try
            {
                var options = CreateOptions(builder, server, configuration);
                builder2 = builder.UseMessageHandler(options);
            }
            catch
            {
                server.Dispose();
                throw;
            }
            return builder2;
        }

        public static IAppBuilder UseWebApi(this IAppBuilder builder, HttpServer httpServer)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (httpServer == null)
            {
                throw new ArgumentNullException(nameof(httpServer));
            }
            var configuration = httpServer.Configuration;
            var options = CreateOptions(builder, httpServer, configuration);
            return builder.UseMessageHandler(options);
        }
    }
}
