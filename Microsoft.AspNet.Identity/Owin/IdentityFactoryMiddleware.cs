using System;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Microsoft.AspNet.Identity.Owin
{
    public class IdentityFactoryMiddleware<TResult, TOptions> : OwinMiddleware where TResult : IDisposable where TOptions : IdentityFactoryOptions<TResult>
    {
        public IdentityFactoryMiddleware(OwinMiddleware next, TOptions options) : base(next)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            if (options.Provider == null)
            {
                throw new ArgumentNullException(nameof(options.Provider));
            }
            Options = options;
        }

        public async override Task Invoke(IOwinContext context)
        {
            TResult instance = Options.Provider.Create(Options, context);
            try
            {
                context.Set<TResult>(instance);
                if (Next == null)
                {
                    return;
                }
                await Next.Invoke(context);
            }
            finally
            {
                Options.Provider.Dispose(Options, instance);
            }
        }

        public TOptions Options { get; }
        
}
}
