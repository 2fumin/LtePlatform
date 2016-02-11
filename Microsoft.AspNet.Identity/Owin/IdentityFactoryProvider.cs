using System;
using Microsoft.AspNet.Identity.Owin.Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Microsoft.AspNet.Identity.Owin
{
    public class IdentityFactoryProvider<T> : IIdentityFactoryProvider<T> where T : class, IDisposable
    {
        public IdentityFactoryProvider()
        {
            OnDispose = delegate {
            };
            OnCreate = (options, context) => default(T);
        }

        public virtual T Create(IdentityFactoryOptions<T> options, IOwinContext context)
        {
            return OnCreate(options, context);
        }

        public virtual void Dispose(IdentityFactoryOptions<T> options, T instance)
        {
            OnDispose(options, instance);
        }

        public Func<IdentityFactoryOptions<T>, IOwinContext, T> OnCreate { get; set; }

        public Action<IdentityFactoryOptions<T>, T> OnDispose { get; set; }
    }
}
