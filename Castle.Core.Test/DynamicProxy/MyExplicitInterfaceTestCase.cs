using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Test.DynamicProxy.Explicit;
using Castle.Core.Test.Interceptors;
using Castle.Core.Test.Main;
using Castle.DynamicProxy;
using NUnit.Framework;

namespace Castle.Core.Test.DynamicProxy
{
    [TestFixture]
    public class MyExplicitInterfaceTestCase
    {
        private LogInvocationInterceptor interceptor;
        private MyProxyGenerator generator;

        [Test]
        public void ExplicitInterface_AsAdditionalInterfaceToProxy_OnClassProxy_WithoutBaseCalls4()
        {
            interceptor.Proceed = false;

            var proxy = (SimpleInterfaceExplicit)generator.CreateClassProxy(typeof(SimpleInterfaceExplicit),
                                                                            new[] { typeof(ISimpleInterface) },
                                                                            ProxyGenerationOptions.Default, null,
                                                                            interceptors: interceptor);

            proxy.DoVirtual();
            var result = ((ISimpleInterface)proxy).Do();
            proxy.DoVirtual();

            Assert.AreEqual(1, interceptor.Invocations.Count);

            Assert.AreEqual(0, result); // indicates that original method was not called
        }

        [Test]
        public void ExplicitInterface_AsAdditionalInterfaceToProxy_OnClassProxy_WithoutBaseCalls5()
        {
            interceptor.Proceed = false;
            generator.CheckNotGenericType(typeof(SimpleInterfaceExplicit));
            generator.CheckNotGenericTypes(new[] { typeof(ISimpleInterface) });

            var proxyType = generator.ProxyBuilder.CreateClassProxyType(typeof (SimpleInterfaceExplicit),
                new[] {typeof (ISimpleInterface)}, ProxyGenerationOptions.Default);
            Assert.AreEqual(proxyType.Name, "SimpleInterfaceExplicitProxy");
            Assert.AreEqual(proxyType.FullName, "Castle.Proxies.SimpleInterfaceExplicitProxy");
            Assert.AreEqual(proxyType.Assembly.FullName, "DynamicProxyGenAssembly2, Version=0.0.0.0, Culture=neutral, PublicKeyToken=a621a9e7e5c32e69");
            Assert.AreEqual(proxyType.AssemblyQualifiedName,
                "Castle.Proxies.SimpleInterfaceExplicitProxy, DynamicProxyGenAssembly2, Version=0.0.0.0, Culture=neutral, PublicKeyToken=a621a9e7e5c32e69");
            Assert.AreEqual(proxyType.GetCustomAttributes().Count(), 2);
            Assert.AreEqual(proxyType.GetProperties().Count(), 0);

            var arguments = MyProxyGenerator.BuildArgumentList(ProxyGenerationOptions.Default, new IInterceptor[] {interceptor});
            Assert.AreEqual(arguments.Count, 1);
            var mixins = ProxyGenerationOptions.Default.MixinData.Mixins;
            Assert.AreEqual(mixins.Count(),0);
            Assert.IsNull(ProxyGenerationOptions.Default.Selector);
            Assert.IsNotNull(arguments[0]);
            Assert.AreEqual(arguments[0], new IInterceptor[] { interceptor });
        }

        [Test]
        public void ExplicitInterface_AsAdditionalInterfaceToProxy_OnClassProxy_WithoutBaseCalls6()
        {
            interceptor.Proceed = false;
            var proxyType = generator.ProxyBuilder.CreateClassProxyType(typeof(SimpleInterfaceExplicit),
                new[] { typeof(ISimpleInterface) }, ProxyGenerationOptions.Default);
            var proxy =
                (SimpleInterfaceExplicit)
                    generator.CreateClassProxy(proxyType, new List<object> {new IInterceptor[] {interceptor}},
                        typeof (SimpleInterfaceExplicit), null);
            proxy.DoVirtual();
            var result = ((ISimpleInterface)proxy).Do();
            proxy.DoVirtual();

            Assert.AreEqual(1, interceptor.Invocations.Count);
        }

        [Test]
        public void ExplicitInterface_AsAdditionalInterfaceToProxy_OnClassProxy_WithoutBaseCalls7()
        {
            interceptor.Proceed = false;
            var proxyType = generator.ProxyBuilder.CreateClassProxyType(typeof(SimpleInterfaceExplicit),
                new[] { typeof(ISimpleInterface) }, ProxyGenerationOptions.Default);
            var proxy =
                (SimpleInterfaceExplicit)
                    Activator.CreateInstance(proxyType, new object[]{ new IInterceptor[] { interceptor } });
            Assert.IsNotNull(proxy);
            proxy.DoVirtual();
            var result = ((ISimpleInterface)proxy).Do();
            proxy.DoVirtual();

            Assert.AreEqual(1, interceptor.Invocations.Count);
            var invocation = interceptor.Invocations[0];
            Assert.IsNotNull(invocation);
            Assert.AreEqual(invocation.ToString(), "Do");
            Assert.AreEqual(result, 0);
            result= ((ISimpleInterface)proxy).Do();
            Assert.AreEqual(2, interceptor.Invocations.Count);
        }

        [Test]
        public void ExplicitInterface_AsAdditionalInterfaceToProxy_OnClassProxy_WithoutBaseCalls8()
        {
            interceptor.Proceed = false;
            var proxyType = generator.ProxyBuilder.CreateClassProxyType(typeof(SimpleInterfaceExplicit),
                new[] { typeof(SimpleInterfaceExplicit) }, ProxyGenerationOptions.Default);
            var proxy =
                (SimpleInterfaceExplicit)
                    Activator.CreateInstance(proxyType, new object[] { new IInterceptor[] { interceptor } });
            Assert.IsNotNull(proxy);
            proxy.DoVirtual();
            var result = ((ISimpleInterface)proxy).Do();
            proxy.DoVirtual();

            Assert.AreEqual(1, interceptor.Invocations.Count);
            var invocation = interceptor.Invocations[0];
            Assert.IsNotNull(invocation);
            Assert.AreEqual(invocation.ToString(), "Do");
            Assert.AreEqual(result, 0);
            result = ((ISimpleInterface)proxy).Do();
            Assert.AreEqual(2, interceptor.Invocations.Count);
        }

        class MyProxyGenerator : ProxyGenerator
        {
             public MyProxyGenerator(IProxyBuilder builer) :base(builer)
            { }

            public void CheckNotGenericType(Type type)
            {
                CheckNotGenericTypeDefinition(type);
            }

            public void CheckNotGenericTypes(IEnumerable<Type> types)
            {
                CheckNotGenericTypeDefinitions(types);
            }

            public static List<object> BuildArgumentList(ProxyGenerationOptions options, IInterceptor[] interceptors)
            {
                return BuildArgumentListForClassProxy(options, interceptors);
            }

            public object CreateClassProxy(Type proxyType, List<object> proxyArguments, Type classToProxy,
                object[] constructorArguments)
            {
                return CreateClassProxyInstance(proxyType, proxyArguments, classToProxy, constructorArguments);
            }
        }

        [SetUp]
        public void Init()
        {
            var builder = new PersistentProxyBuilder();
            generator=new MyProxyGenerator(builder);
            interceptor = new LogInvocationInterceptor();
        }
    }
}
