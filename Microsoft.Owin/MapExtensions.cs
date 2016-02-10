using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin.Mapping;
using Microsoft.Owin.Properties;

namespace Microsoft.Owin
{
    public static class MapExtensions
    {
        public static IAppBuilder Map(this IAppBuilder app, PathString pathMatch, Action<IAppBuilder> configuration)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            if (pathMatch.HasValue && pathMatch.Value.EndsWith("/", StringComparison.Ordinal))
            {
                throw new ArgumentException(Resources.Exception_PathMustNotEndWithSlash, nameof(pathMatch));
            }
            var options = new MapOptions
            {
                PathMatch = pathMatch
            };
            var builder = app.Use<MapMiddleware>(options);
            var builder2 = app.New();
            configuration(builder2);
            options.Branch = (Func<IDictionary<string, object>, Task>)builder2.Build(typeof(Func<IDictionary<string, object>, Task>));
            return builder;
        }

        public static IAppBuilder Map(this IAppBuilder app, string pathMatch, Action<IAppBuilder> configuration)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (pathMatch == null)
            {
                throw new ArgumentNullException(nameof(pathMatch));
            }
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            if (!string.IsNullOrEmpty(pathMatch) && pathMatch.EndsWith("/", StringComparison.Ordinal))
            {
                throw new ArgumentException(Resources.Exception_PathMustNotEndWithSlash, nameof(pathMatch));
            }
            return app.Map(new PathString(pathMatch), configuration);
        }
    }
}
