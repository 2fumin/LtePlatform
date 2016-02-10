using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Owin.Infrastructure
{
    internal sealed class OwinMiddlewareTransition
    {
        private readonly OwinMiddleware _next;

        public OwinMiddlewareTransition(OwinMiddleware next)
        {
            _next = next;
        }

        public Task Invoke(IDictionary<string, object> environment)
        {
            return _next.Invoke(new OwinContext(environment));
        }
    }
}
