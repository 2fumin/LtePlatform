using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Owin.Extensions
{
    /// <summary>
    /// Represents a middleware for executing in-line function middleware.
    /// </summary>
    public class UseHandlerMiddleware
    {
        private readonly Func<IOwinContext, Task> _handler;
        private readonly Func<IDictionary<string, object>, Task> _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="UseHandlerMiddleware" /> class.
        /// </summary>
        /// <param name="next">The pointer to next middleware.</param>
        /// <param name="handler">A function that handles the request or calls the given next function.</param>
        public UseHandlerMiddleware(Func<IDictionary<string, object>, Task> next, Func<IOwinContext, Task> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }
            _next = next;
            _handler = handler;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UseHandlerMiddleware" /> class.
        /// </summary>
        /// <param name="next">The pointer to next middleware.</param>
        /// <param name="handler">A function that handles all requests.</param>
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

        /// <summary>
        /// Invokes the handler for processing the request.
        /// </summary>
        /// <param name="environment">The OWIN context.</param>
        /// <returns>The <see cref="Task" /> object that represents the request operation.</returns>
        public Task Invoke(IDictionary<string, object> environment)
        {
            IOwinContext arg = new OwinContext(environment);
            return _handler(arg);
        }
    }
}
