using System;
using Microsoft.Owin;

namespace Microsoft.AspNet.Identity.Owin
{
    public static class OwinContextExtensions
    {
        private const string IdentityKeyPrefix = "AspNet.Identity.Owin:";

        public static T Get<T>(this IOwinContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            return context.Get<T>(GetKey(typeof(T)));
        }

        private static string GetKey(Type t)
        {
            return (IdentityKeyPrefix + t.AssemblyQualifiedName);
        }

        public static TManager GetUserManager<TManager>(this IOwinContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            return context.Get<TManager>();
        }

        public static IOwinContext Set<T>(this IOwinContext context, T value)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            return context.Set(GetKey(typeof(T)), value);
        }
    }
}
