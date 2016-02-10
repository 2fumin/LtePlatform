﻿namespace Microsoft.Owin.Security.Provider
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
}
