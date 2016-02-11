using System;
using Microsoft.Owin;

namespace Microsoft.AspNet.Identity.Owin
{
    namespace Microsoft.AspNet.Identity.Owin
    {
        public interface IIdentityFactoryProvider<T> where T : IDisposable
        {
            T Create(IdentityFactoryOptions<T> options, IOwinContext context);

            void Dispose(IdentityFactoryOptions<T> options, T instance);
        }
    }

}
