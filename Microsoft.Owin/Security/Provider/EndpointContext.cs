namespace Microsoft.Owin.Security.Provider
{
    public abstract class EndpointContext : BaseContext
    {
        protected EndpointContext(IOwinContext context) : base(context)
        {
        }

        public void RequestCompleted()
        {
            IsRequestCompleted = true;
        }

        public bool IsRequestCompleted { get; private set; }
    }

    public abstract class EndpointContext<TOptions> : BaseContext<TOptions>
    {
        protected EndpointContext(IOwinContext context, TOptions options) : base(context, options)
        {
        }

        public void RequestCompleted()
        {
            this.IsRequestCompleted = true;
        }

        public bool IsRequestCompleted { get; private set; }
    }
}


