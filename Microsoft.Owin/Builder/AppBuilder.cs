using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Owin.Infrastructure;

namespace Microsoft.Owin.Builder
{
    public class AppBuilder : IAppBuilder
    {
        private readonly IDictionary<Tuple<Type, Type>, Delegate> _conversions;
        private readonly IList<Tuple<Type, Delegate, object[]>> _middleware;
        private readonly IDictionary<string, object> _properties;
        private static readonly Func<IDictionary<string, object>, Task> NotFound = new NotFound().Invoke;

        public AppBuilder()
        {
            _properties = new Dictionary<string, object>();
            _conversions = new Dictionary<Tuple<Type, Type>, Delegate>();
            _middleware = new List<Tuple<Type, Delegate, object[]>>();
            _properties[OwinConstants.Builder.AddSignatureConversion] = new Action<Delegate>(AddSignatureConversion);
            _properties[OwinConstants.Builder.DefaultApp] = NotFound;
            SignatureConversions.AddConversions(this);
        }

        internal AppBuilder(IDictionary<Tuple<Type, Type>, Delegate> conversions, IDictionary<string, object> properties)
        {
            _properties = properties;
            _conversions = conversions;
            _middleware = new List<Tuple<Type, Delegate, object[]>>();
        }

        private void AddSignatureConversion(Delegate conversion)
        {
            if (conversion == null)
            {
                throw new ArgumentNullException(nameof(conversion));
            }
            var parameterType = GetParameterType(conversion);
            if (parameterType == null)
            {
                throw new ArgumentException(Resources.Exception_ConversionTakesOneParameter, nameof(conversion));
            }
            var tuple = Tuple.Create(conversion.Method.ReturnType, parameterType);
            _conversions[tuple] = conversion;
        }

        public object Build(Type returnType)
        {
            return BuildInternal(returnType);
        }

        private object BuildInternal(Type signature)
        {
            object notFound;
            if (!_properties.TryGetValue(OwinConstants.Builder.DefaultApp, out notFound))
            {
                notFound = NotFound;
            }
            foreach (var tuple in _middleware.Reverse())
            {
                var type = tuple.Item1;
                var delegate2 = tuple.Item2;
                var second = tuple.Item3;
                notFound = Convert(type, notFound);
                var args = new[] { notFound }.Concat(second).ToArray();
                notFound = delegate2.DynamicInvoke(args);
                notFound = Convert(type, notFound);
            }
            return Convert(signature, notFound);
        }

        private object Convert(Type signature, object app)
        {
            if (app == null)
            {
                return null;
            }
            var obj2 = ConvertOneHop(signature, app);
            if (obj2 != null)
            {
                return obj2;
            }
            var obj3 = ConvertMultiHop(signature, app);
            if (obj3 == null)
            {
                throw new ArgumentException(
                    string.Format(CultureInfo.CurrentCulture, Resources.Exception_NoConversionExists, app.GetType(),
                        signature), nameof(signature));
            }
            return obj3;
        }

        private object ConvertMultiHop(Type signature, object app)
        {
            return (from pair in _conversions
                let obj2 = ConvertOneHop(pair.Key.Item2, app)
                where obj2 != null
                select pair.Value.DynamicInvoke(obj2)
                into obj3
                where obj3 != null
                select ConvertOneHop(signature, obj3)).FirstOrDefault(obj4 => obj4 != null);
        }

        private object ConvertOneHop(Type signature, object app)
        {
            if (signature.IsInstanceOfType(app))
            {
                return app;
            }
            if (!typeof (Delegate).IsAssignableFrom(signature))
                return (from pair in _conversions
                    let c = pair.Key.Item1
                    where pair.Key.Item2.IsInstanceOfType(app) && signature.IsAssignableFrom(c)
                    select pair.Value.DynamicInvoke(app)).FirstOrDefault();
            var delegate2 = ToMemberDelegate(signature, app);
            if (delegate2 != null)
            {
                return delegate2;
            }
            return (from pair in _conversions
                let c = pair.Key.Item1
                where pair.Key.Item2.IsInstanceOfType(app) && signature.IsAssignableFrom(c)
                select pair.Value.DynamicInvoke(app)).FirstOrDefault();
        }

        private static Type GetParameterType(Delegate function)
        {
            var parameters = function.Method.GetParameters();
            if (parameters.Length < 1)
            {
                return null;
            }
            return parameters[0].ParameterType;
        }

        public IAppBuilder New()
        {
            return new AppBuilder(_conversions, _properties);
        }

        private static bool TestArgForParameter(Type parameterType, object arg)
        {
            return (((arg == null) && !parameterType.IsValueType) || parameterType.IsInstanceOfType(arg));
        }

        private static Tuple<Type, Delegate, object[]> ToConstructorMiddlewareFactory(object middlewareObject, object[] args, ref Delegate middlewareDelegate)
        {
            var type = middlewareObject as Type;
            foreach (var info in type.GetConstructors())
            {
                var parameters = info.GetParameters();
                var source = (from p in parameters select p.ParameterType).ToArray<Type>();
                if ((source.Length != (args.Length + 1)) || !source.Skip(1).Zip(args, TestArgForParameter).All(x => x))
                    continue;
                var arguments = (from p in parameters select Expression.Parameter(p.ParameterType, p.Name)).ToArray<ParameterExpression>();
                var body = Expression.New(info, arguments);
                middlewareDelegate = Expression.Lambda(body, arguments).Compile();
                return Tuple.Create(parameters[0].ParameterType, middlewareDelegate, args);
            }
            throw new MissingMethodException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_NoConstructorFound, type.FullName, args.Length + 1));
        }

        private static Tuple<Type, Delegate, object[]> ToGeneratorMiddlewareFactory(object middlewareObject, object[] args)
        {
            return (from info in middlewareObject.GetType().GetMethods()
                where info.Name == Constants.Invoke
                let parameters = info.GetParameters()
                let source = (from p in parameters select p.ParameterType).ToArray<Type>()
                where
                    (source.Length == (args.Length + 1)) &&
                    source.Skip(1).Zip(args, TestArgForParameter).All(x => x)
                let delegate2 =
                    Delegate.CreateDelegate(
                        Expression.GetFuncType(source.Concat(new[] {info.ReturnType}).ToArray()), middlewareObject,
                        info)
                select Tuple.Create(parameters[0].ParameterType, delegate2, args)).FirstOrDefault();
        }

        private static Tuple<Type, Delegate, object[]> ToInstanceMiddlewareFactory(object middlewareObject, object[] args)
        {
            var methods = middlewareObject.GetType().GetMethods();
            return (from method in methods
                where method.Name == Constants.Initialize
                let infoArray2 = method.GetParameters()
                let source = (from p in infoArray2 select p.ParameterType).ToArray<Type>()
                where (source.Length == (args.Length + 1)) && source.Skip(1).Zip(args, TestArgForParameter).All(x => x)
                let func2 = (Func<object, object>) delegate(object app)
                {
                    var parameters = new[] {app}.Concat(args).ToArray();
                    method.Invoke(middlewareObject, parameters);
                    return middlewareObject;
                }
                let func = func2
                select Tuple.Create<Type, Delegate, object[]>(infoArray2[0].ParameterType, func, new object[0])).FirstOrDefault();
        }

        private static Delegate ToMemberDelegate(Type signature, object app)
        {
            var method = signature.GetMethod(Constants.Invoke);
            var parameters = method.GetParameters();
            return (from info2 in app.GetType().GetMethods()
                where info2.Name == Constants.Invoke
                let first = info2.GetParameters()
                where
                    (first.Length == parameters.Length) &&
                    (first.Zip(parameters,
                        (methodParameter, signatureParameter) =>
                            methodParameter.ParameterType.IsAssignableFrom(signatureParameter.ParameterType))
                        .All(compatible => compatible) && method.ReturnType.IsAssignableFrom(info2.ReturnType))
                select Delegate.CreateDelegate(signature, app, info2)).FirstOrDefault();
        }

        private static Tuple<Type, Delegate, object[]> ToMiddlewareFactory(object middlewareObject, object[] args)
        {
            if (middlewareObject == null)
            {
                throw new ArgumentNullException(nameof(middlewareObject));
            }
            var delegate2 = middlewareObject as Delegate;
            if (delegate2 != null)
            {
                return Tuple.Create(GetParameterType(delegate2), delegate2, args);
            }
            var tuple = ToInstanceMiddlewareFactory(middlewareObject, args);
            if (tuple != null)
            {
                return tuple;
            }
            tuple = ToGeneratorMiddlewareFactory(middlewareObject, args);
            if (tuple != null)
            {
                return tuple;
            }
            if (!(middlewareObject is Type))
            {
                throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture,
                    Resources.Exception_MiddlewareNotSupported, middlewareObject.GetType().FullName));
            }
            return ToConstructorMiddlewareFactory(middlewareObject, args, ref delegate2);
        }

        public IAppBuilder Use(object middleware, params object[] args)
        {
            _middleware.Add(ToMiddlewareFactory(middleware, args));
            return this;
        }

        public IDictionary<string, object> Properties => _properties;
    }
}
