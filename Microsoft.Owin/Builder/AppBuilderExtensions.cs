using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Owin.Builder
{
    public static class AppBuilderExtensions
    {
        public static void AddSignatureConversion(this IAppBuilder builder, Delegate conversion)
        {
            object obj2;
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (!builder.Properties.TryGetValue(OwinConstants.Builder.AddSignatureConversion, out obj2))
                throw new MissingMethodException(builder.GetType().FullName, Constants.BuilderAddConversion);
            var action = obj2 as Action<Delegate>;
            if (action == null) throw new MissingMethodException(builder.GetType().FullName, Constants.BuilderAddConversion);
            action(conversion);
        }

        public static void AddSignatureConversion<T1, T2>(this IAppBuilder builder, Func<T1, T2> conversion)
        {
            builder.AddSignatureConversion(conversion);
        }

        public static Func<IDictionary<string, object>, Task> Build(this IAppBuilder builder)
        {
            return builder.Build<Func<IDictionary<string, object>, Task>>();
        }

        public static TApp Build<TApp>(this IAppBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            return (TApp)builder.Build(typeof(TApp));
        }
    }
}
