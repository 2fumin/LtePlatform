using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin.Builder;

namespace Microsoft.Owin.Infrastructure
{
    public static class SignatureConversions
    {
        public static void AddConversions(IAppBuilder app)
        {
            app.AddSignatureConversion(new Func<Func<IDictionary<string, object>, Task>, OwinMiddleware>(Conversion1));
            app.AddSignatureConversion(new Func<OwinMiddleware, Func<IDictionary<string, object>, Task>>(Conversion2));
        }

        private static OwinMiddleware Conversion1(Func<IDictionary<string, object>, Task> next)
        {
            return new AppFuncTransition(next);
        }

        private static Func<IDictionary<string, object>, Task> Conversion2(OwinMiddleware next)
        {
            return new OwinMiddlewareTransition(next).Invoke;
        }
    }
}
