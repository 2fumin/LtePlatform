using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin.Mapping;
using Owin;

namespace Microsoft.Owin
{
    public static class MapWhenExtensions
    {
        public static IAppBuilder MapWhen(this IAppBuilder app, Func<IOwinContext, bool> predicate, Action<IAppBuilder> configuration)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            MapWhenOptions options = new MapWhenOptions
            {
                Predicate = predicate
            };
            IAppBuilder builder = app.Use<MapWhenMiddleware>(options);
            IAppBuilder builder2 = app.New();
            configuration(builder2);
            options.Branch = (Func<IDictionary<string, object>, Task>)builder2.Build(typeof(Func<IDictionary<string, object>, Task>));
            return builder;
        }

        public static IAppBuilder MapWhenAsync(this IAppBuilder app, Func<IOwinContext, Task<bool>> predicate, Action<IAppBuilder> configuration)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            MapWhenOptions options = new MapWhenOptions
            {
                PredicateAsync = predicate
            };
            IAppBuilder builder = app.Use<MapWhenMiddleware>(options);
            IAppBuilder builder2 = app.New();
            configuration(builder2);
            options.Branch = (Func<IDictionary<string, object>, Task>)builder2.Build(typeof(Func<IDictionary<string, object>, Task>));
            return builder;
        }
    }
}
