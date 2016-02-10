using System;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.Infrastructure
{
    public abstract class AuthenticationMiddleware<TOptions> : OwinMiddleware where TOptions : AuthenticationOptions
    {
        protected AuthenticationMiddleware(OwinMiddleware next, TOptions options) : base(next)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            Options = options;
        }

        protected abstract AuthenticationHandler<TOptions> CreateHandler();

        public override async Task Invoke(IOwinContext context)
        {
            var handler = CreateHandler();
            await handler.Initialize(Options, context);
            var introduced11 = await handler.InvokeAsync();
            if (!introduced11)
            {
                await Next.Invoke(context);
            }
            await handler.TeardownAsync();
        }

        public TOptions Options { get; set; }

    }
}
