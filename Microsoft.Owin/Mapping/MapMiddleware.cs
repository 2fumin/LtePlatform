using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Owin.Mapping
{
    public class MapMiddleware
    {
        private readonly Func<IDictionary<string, object>, Task> _next;
        private readonly MapOptions _options;

        public MapMiddleware(Func<IDictionary<string, object>, Task> next, MapOptions options)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _next = next;
            _options = options;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            PathString remainingPath;
            IOwinContext context = new OwinContext(environment);
            var path = context.Request.Path;
            if (path.StartsWithSegments(_options.PathMatch, out remainingPath))
            {
                var pathBase = context.Request.PathBase;
                context.Request.PathBase = pathBase + _options.PathMatch;
                context.Request.Path = remainingPath;
                await _options.Branch(environment);
                context.Request.PathBase = pathBase;
                context.Request.Path = path;
            }
            else
            {
                await _next(environment);
            }
        }
    }
}
