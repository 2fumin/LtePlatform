using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Owin.Mapping
{
    public class MapWhenMiddleware
    {
        private readonly Func<IDictionary<string, object>, Task> _next;
        private readonly MapWhenOptions _options;

        public MapWhenMiddleware(Func<IDictionary<string, object>, Task> next, MapWhenOptions options)
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
            IOwinContext arg = new OwinContext(environment);
            if (_options.Predicate != null)
            {
                if (_options.Predicate(arg))
                {
                    await _options.Branch(environment);
                }
                else
                {
                    await _next(environment);
                }
            }
            else
            {
                bool introduced13 = await _options.PredicateAsync(arg);
                if (introduced13)
                {
                    await _options.Branch(environment);
                }
                else
                {
                    await _next(environment);
                }
            }
        }

    }
}
