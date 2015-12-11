using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Test.Interceptors;
using Castle.DynamicProxy;
using NUnit.Framework;

namespace Castle.Core.Test.Junior.Interceptors
{
    [TestFixture]
    public class WithCallbackInterceptorTest
    {
        [Test]
        public void Test_Delegate_SimpleInvocationConstructor()
        {
            var delegateInvocation = new SimpleInvocation(null, null, null);
            var interceptor = new WithCallbackInterceptor(delegate (IInvocation invocation)
            {
                invocation.Arguments[0] = 5;
                invocation.Arguments[1] = "aaa";
                invocation.Arguments[3] = "bbb";
            });

            interceptor.Intercept(delegateInvocation);
            Assert.AreEqual(delegateInvocation.Arguments[0], 5);
        }

        [Test]
        public void Test_CreateClassProxy()
        {
            var interceptor = new WithCallbackInterceptor(delegate (IInvocation invocation)
            {
                invocation.Arguments[0] = 5;
                invocation.Arguments[1] = "aaa";
                invocation.Arguments[3] = "bbb";
            });
            var generator = new ProxyGenerator(new PersistentProxyBuilder());
            var proxy = (MyClass)generator.CreateClassProxy(typeof(MyClass), interceptor);
            Assert.IsNotNull(proxy);

        }

        public class SimpleInvocation : AbstractInvocation
        {
            public SimpleInvocation(object proxy, IInterceptor[] interceptors, MethodInfo proxiedMethod)
                : base(proxy, interceptors, proxiedMethod, new object[4])
            {
            }

            public override object InvocationTarget { get; }
            public override Type TargetType { get; }
            public override MethodInfo MethodInvocationTarget { get; }
            protected override void InvokeMethodOnTarget()
            {
                throw new NotImplementedException();
            }
        }

        public class MyClass
        {
             
        }
    }
}
