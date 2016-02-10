namespace Microsoft.Owin.Security.Provider
{
    public abstract class BaseContext
    {
        protected BaseContext(IOwinContext context)
        {
            OwinContext = context;
        }

        public IOwinContext OwinContext { get; }

        public IOwinRequest Request => OwinContext.Request;

        public IOwinResponse Response => OwinContext.Response;
    }

    public abstract class BaseContext<TOptions>
    {
        protected BaseContext(IOwinContext context, TOptions options)
        {
            OwinContext = context;
            Options = options;
        }

        public TOptions Options { get; private set; }

        public IOwinContext OwinContext { get; }

        public IOwinRequest Request => OwinContext.Request;

        public IOwinResponse Response => OwinContext.Response;
    }
}
