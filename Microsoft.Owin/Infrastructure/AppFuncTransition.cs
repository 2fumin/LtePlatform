using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Owin.Infrastructure
{
    internal sealed class AppFuncTransition : OwinMiddleware
    {
        private readonly Func<IDictionary<string, object>, Task> _next;

        public AppFuncTransition(Func<IDictionary<string, object>, Task> next) : base(null)
        {
            this._next = next;
        }

        public override Task Invoke(IOwinContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            return this._next(context.Environment);
        }
    }
}
