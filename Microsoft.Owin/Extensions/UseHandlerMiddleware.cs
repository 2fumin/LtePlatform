using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Owin.Extensions
{
    public class UseHandlerMiddleware
    {
        private readonly Func<IOwinContext, Task> _handler;
        private readonly Func<IDictionary<string, object>, Task> _next;

        public UseHandlerMiddleware(Func<IDictionary<string, object>, Task> next, Func<IOwinContext, Task> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }
            _next = next;
            _handler = handler;
        }

        public UseHandlerMiddleware(Func<IDictionary<string, object>, Task> next, Func<IOwinContext, Func<Task>, Task> handler)
        {
            Func<IOwinContext, Task> func = null;
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }
            _next = next;
            func = context => handler(context, () => _next(context.Environment));
            _handler = func;
        }

        public Task Invoke(IDictionary<string, object> environment)
        {
            IOwinContext arg = new OwinContext(environment);
            return _handler(arg);
        }
    }
}
