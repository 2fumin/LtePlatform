// Copyright 2004-2014 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Castle.Core.Test.DynamicProxy.Classes;
using Castle.Core.Test.Interceptors;
using Castle.Core.Test.InterClasses;
using Castle.DynamicProxy;
using Castle.DynamicProxy.Generators;
using Castle.DynamicProxy.Generators.Emitters;
using NUnit.Framework;
using ClassWithIndexer = Castle.Core.Test.DynamicProxy.Classes.ClassWithIndexer;

namespace Castle.Core.Test.Main
{
    using ClassWithIndexer = ClassWithIndexer;

	[TestFixture]
	public class BasicClassProxyTestCase : BasePEVerifyTestCase
	{
		[Test]
		public void ProxyForClass()
		{
			var proxy = generator.CreateClassProxy(typeof(ServiceClass), new ResultModifierInterceptor());

			Assert.IsNotNull(proxy);
			Assert.IsTrue(proxy is ServiceClass);

			var instance = (ServiceClass)proxy;

			// return value is changed by the interceptor
			Assert.AreEqual(44, instance.Sum(20, 25));

			// return value is changed by the interceptor
			Assert.AreEqual(true, instance.Valid);

			Assert.AreEqual(45, instance.Sum((byte)20, (byte)25)); // byte
			Assert.AreEqual(45L, instance.Sum(20L, 25L)); // long
			Assert.AreEqual(45, instance.Sum((short)20, (short)25)); // short
			Assert.AreEqual(45f, instance.Sum(20f, 25f)); // float
			Assert.AreEqual(45.0, instance.Sum(20.0, 25.0)); // double
			Assert.AreEqual(45, instance.Sum((ushort)20, (ushort)25)); // ushort
			Assert.AreEqual(45, instance.Sum(20, (uint)25)); // uint
			Assert.AreEqual(45, instance.Sum(20, (ulong)25)); // ulong
		}

		[Test]
		public void Caching()
		{
#pragma warning disable 219
			generator.CreateClassProxy(
			    typeof(ServiceClass), new StandardInterceptor());
			generator.CreateClassProxy(
			    typeof(ServiceClass), new StandardInterceptor());
			generator.CreateClassProxy(
			    typeof(ServiceClass), new StandardInterceptor());
			generator.CreateClassProxy( 
			    typeof(ServiceClass), new StandardInterceptor());
#pragma warning restore 219
		}

		[Test]
#if __MonoCS__
		[Ignore("Expected: Castle.DynamicProxy.Generators.GeneratorException, But was: System.ArgumentNullException")]
#endif
		public void ProxyForNonPublicClass()
		{
			// have to use a type that is not from this assembly, because it is marked as internals visible to 
			// DynamicProxy2

			var type = Type.GetType("System.AppDomainInitializerInfo, mscorlib");
			var exception = Assert.Throws<GeneratorException>(() => generator.CreateClassProxy(type, new StandardInterceptor()));
			Assert.AreEqual(
				"Can not create proxy for type System.AppDomainInitializerInfo because it is not accessible. Make it public, or internal and mark your assembly with [assembly: InternalsVisibleTo(\"DynamicProxyGenAssembly2, PublicKey=0024000004800000940000000602000000240000525341310004000001000100c547cac37abd99c8db225ef2f6c8a3602f3b3606cc9891605d02baa56104f4cfc0734aa39b93bf7852f7d9266654753cc297e7d2edfe0bac1cdcf9f717241550e0a7b191195b7667bb4f64bcb8e2121380fd1d9d46ad2d92d2d15605093924cceaf74c4861eff62abf69b9291ed0a340e113be11e6a7d3113e92484cf7045cc7\")] attribute, because assembly mscorlib is strong-named.",
				exception.Message);
		}

		[Test]
		public void ProxyForClassWithIndexer()
		{
			LogInvocationInterceptor logger = new LogInvocationInterceptor();

			object proxy = generator.CreateClassProxy(typeof(ClassWithIndexer), logger);

			Assert.IsNotNull(proxy);
			Assert.IsInstanceOf(typeof(ClassWithIndexer), proxy);

			var type = (ClassWithIndexer)proxy;

			type["name"] = 10;
			Assert.AreEqual(10, type["name"]);

			Assert.AreEqual("set_Item get_Item ", logger.LogContents);
		}

		[Test]
		public void Can_proxy_class_with_ctor_having_params_array()
		{
			generator.CreateClassProxy(typeof(HasCtorWithParamsStrings), new object[] { new string[0] });
		}


#if !SILVERLIGHT
		[Test]
		public void ClassWithDifferentAccessLevelOnProperties()
		{
			LogInvocationInterceptor logger = new LogInvocationInterceptor();

			object proxy = generator.CreateClassProxy(typeof(DiffAccessLevelOnProperties), logger);

			Assert.IsNotNull(proxy);
			Assert.IsInstanceOf(typeof(DiffAccessLevelOnProperties), proxy);

			var type = (DiffAccessLevelOnProperties)proxy;

			type.SetProperties();

			Assert.AreEqual("10 11 12 13 name", type.ToString());
		}

#endif

		[Test]
		public void GetPropertyByReflectionTest()
		{
			var proxy = generator.CreateClassProxy(
				typeof(ServiceClass), new StandardInterceptor());

			try
			{
				Assert.IsFalse((bool)proxy.GetType().GetProperty("Valid").GetValue(proxy, null),
							   "check reflected property is true");
			}
			catch (AmbiguousMatchException)
			{
				// this exception is acceptible if the current runtime doesn't
				// have .NET 2.0 SP1 installed
				// we'd try to grab a method info that in in .NET 2.0 SP1, and if it's
				// not present then we'd ignore that exception
			    var newDefinePropertyMethodInfo = typeof (TypeBuilder).GetMethod("DefineProperty", new[]
			    {
			        typeof (string),
			        typeof (
			            PropertyAttributes),
			        typeof (
			            CallingConventions),
			        typeof (Type),
			        typeof (Type[]),
			        typeof (Type[]),
			        typeof (Type[]),
			        typeof (Type[][]),
			        typeof (Type[][])
			    });

				var net20SP1IsInstalled = newDefinePropertyMethodInfo != null;

				if (net20SP1IsInstalled)
					throw;
			}
		}

		[Test]
		public void ClassWithInheritance()
		{
			LogInvocationInterceptor logger = new LogInvocationInterceptor();

			object proxy = generator.CreateClassProxy(typeof(ExtendedServiceClass), logger);

			Assert.IsNotNull(proxy);

			var extended = (ExtendedServiceClass)proxy;

			extended.Sum2(1, 2);
			extended.Sum(1, 2);

			Assert.AreEqual("Sum2 Sum ", logger.LogContents);
		}

		[Test]
		public void ProxyForNestedClass()
		{
			var proxy = generator.CreateClassProxy(typeof(ServiceClass.InernalClass), new[] { typeof(IDisposable) });
			Assert.IsNotNull(proxy);
			Assert.IsTrue(proxy is ServiceClass.InernalClass);
		}

		[Test]
		public void ProxyForClassWithInterfaces()
		{
			var proxy = generator.CreateClassProxy(typeof(ServiceClass), new[] { typeof(IDisposable) },
													  new ResultModifierInterceptor());

			Assert.IsNotNull(proxy);
			Assert.IsTrue(typeof(ServiceClass).IsAssignableFrom(proxy.GetType()));
			Assert.IsTrue(typeof(IDisposable).IsAssignableFrom(proxy.GetType()));

			var inter = (ServiceClass)proxy;

			Assert.AreEqual(44, inter.Sum(20, 25));
			Assert.AreEqual(true, inter.Valid);

			try
			{
				var disp = (IDisposable)proxy;
				disp.Dispose();

				Assert.Fail("Expected exception as Dispose has no implementation");
			}
			catch (NotImplementedException ex)
			{
				Assert.AreEqual(
					"This is a DynamicProxy2 error: The interceptor attempted to 'Proceed' for method 'Void Dispose()' which has no target. " +
					"When calling method without target there is no implementation to 'proceed' to and it is the responsibility of the interceptor " +
					"to mimic the implementation (set return value, out arguments etc)",
					ex.Message);
			}
		}

		[Test]
		public void ProxyForCharReturnType()
		{
			LogInvocationInterceptor logger = new LogInvocationInterceptor();
			object proxy = generator.CreateClassProxy(typeof(ClassWithCharRetType), logger);
			Assert.IsNotNull(proxy);
		    Assert.AreEqual('c', ClassWithCharRetType.DoSomething());
		}

		[Test]
		public void ProxyForClassWithConstructors()
		{
			var proxy = generator.CreateClassProxy(
				typeof(ClassWithConstructors), new object[] {"name"}, new StandardInterceptor());

			Assert.IsNotNull(proxy);
			var classProxy = (ClassWithConstructors)proxy;
			Assert.AreEqual("name", classProxy.Name);

			proxy = generator.CreateClassProxy(typeof(ClassWithConstructors), new object[] {"name", 10},
			                                   new StandardInterceptor());

			Assert.IsNotNull(proxy);
			classProxy = (ClassWithConstructors)proxy;
			Assert.AreEqual("name", classProxy.Name);
			Assert.AreEqual(10, classProxy.X);
		}

		/// <summary>
		/// See http://support.castleproject.org/browse/DYNPROXY-43
		/// </summary>
		[Test]
		public void MethodParamNamesAreReplicated()
		{
			OutRefParamsTestCase.MyClass mc = generator.CreateClassProxy<OutRefParamsTestCase.MyClass>(new StandardInterceptor());
			var methodParams = GetMyTestMethodParams(mc.GetType());
			Assert.AreEqual("myParam", methodParams[0].Name);
		}

		[Test]
		public void ProducesInvocationsThatCantChangeTarget()
		{
			AssertCannotChangeTargetInterceptor invocationChecker = new AssertCannotChangeTargetInterceptor();
			object proxy = generator.CreateClassProxy(typeof(ClassWithCharRetType), invocationChecker);
			Assert.IsNotNull(proxy);
		    var x = ClassWithCharRetType.DoSomething();
			Assert.AreEqual('c', x);
		}

		[Test]
		public void ProxyTypeWithMultiDimentionalArrayAsParameters()
		{
			LogInvocationInterceptor log = new LogInvocationInterceptor();

			ClassWithMultiDimentionalArray proxy =
				generator.CreateClassProxy<ClassWithMultiDimentionalArray>(log);

			var x = new int[1,2];

			proxy.Do(new[] {1});
			proxy.Do2(x);
			proxy.Do3(new[] {"1", "2"});

			Assert.AreEqual("Do Do2 Do3 ", log.LogContents);
		}

		private ParameterInfo[] GetMyTestMethodParams(Type type)
		{
			var methodInfo = type.GetMethod("MyTestMethod");
			return methodInfo.GetParameters();
		}

#if !SILVERLIGHT
		[Test]
		public void ProxyForBaseTypeFromSignedAssembly()
		{
			const bool shouldBeSigned = true;
			var t = typeof(List<object>);
			Assert.IsTrue(StrongNameUtil.IsAssemblySigned(t.Assembly));
			var proxy = generator.CreateClassProxy(t, new StandardInterceptor());
			Assert.AreEqual(shouldBeSigned, StrongNameUtil.IsAssemblySigned(proxy.GetType().Assembly));
		}

		[Test]
		public void ProxyForBaseTypeAndInterfaceFromSignedAssembly()
		{
			const bool shouldBeSigned = true;
			var t1 = typeof(List<object>);
			var t2 = typeof(IServiceProvider);
			Assert.IsTrue(StrongNameUtil.IsAssemblySigned(t1.Assembly));
			Assert.IsTrue(StrongNameUtil.IsAssemblySigned(t2.Assembly));
			var proxy = generator.CreateClassProxy(t1, new[] { t2 }, new StandardInterceptor());
			Assert.AreEqual(shouldBeSigned, StrongNameUtil.IsAssemblySigned(proxy.GetType().Assembly));
		}
#endif

#if SILVERLIGHT // Silverlight test runner treats Assert.Ignore as failed test :/
		[Ignore]
#endif
		[Test]
		public void ProxyForBaseTypeFromUnsignedAssembly()
		{
			if(TestAssemblySigned())
			{
				Assert.Ignore("To get this running, the Tests project must not be signed.");
			}
			var t = typeof (OutRefParamsTestCase.MyClass);
			Assert.IsFalse(StrongNameUtil.IsAssemblySigned(t.Assembly));
			var proxy = generator.CreateClassProxy(t, new StandardInterceptor());
			Assert.IsFalse(StrongNameUtil.IsAssemblySigned(proxy.GetType().Assembly));
		}

		private bool TestAssemblySigned()
		{
			return StrongNameUtil.IsAssemblySigned(GetType().Assembly);
		}

#if SILVERLIGHT // Silverlight test runner treats Assert.Ignore as failed test :/
		[Ignore]
#endif
		[Test]
		public void ProxyForBaseTypeAndInterfaceFromUnsignedAssembly()
		{
			if(TestAssemblySigned())
			{
				Assert.Ignore("To get this running, the Tests project must not be signed.");
			}
			var t1 = typeof (OutRefParamsTestCase.MyClass);
			var t2 = typeof (IService);
			Assert.IsFalse(StrongNameUtil.IsAssemblySigned(t1.Assembly));
			Assert.IsFalse(StrongNameUtil.IsAssemblySigned(t2.Assembly));
			var proxy = generator.CreateClassProxy(t1, new[] {t2}, new StandardInterceptor());
			Assert.IsFalse(StrongNameUtil.IsAssemblySigned(proxy.GetType().Assembly));
		}

#if SILVERLIGHT // Silverlight test runner treats Assert.Ignore as failed test :/
		[Ignore]
#endif
		[Test]
		public void ProxyForBaseTypeAndInterfaceFromSignedAndUnsignedAssemblies1()
		{
			if(TestAssemblySigned())
			{
				Assert.Ignore("To get this running, the Tests project must not be signed.");
			}
			var t1 = typeof (OutRefParamsTestCase.MyClass);
			var t2 = typeof (IServiceProvider);
			Assert.IsFalse(StrongNameUtil.IsAssemblySigned(t1.Assembly));
			Assert.IsTrue(StrongNameUtil.IsAssemblySigned(t2.Assembly));
			var proxy = generator.CreateClassProxy(t1, new[] {t2}, new StandardInterceptor());
			Assert.IsFalse(StrongNameUtil.IsAssemblySigned(proxy.GetType().Assembly));
		}

#if SILVERLIGHT // Silverlight test runner treats Assert.Ignore as failed test :/
		[Ignore]
#endif
		[Test]
		public void ProxyForBaseTypeAndInterfaceFromSignedAndUnsignedAssemblies2()
		{
			if (TestAssemblySigned())
			{
				Assert.Ignore("To get this running, the Tests project must not be signed.");
			}
			var t1 = typeof (List<int>);
			var t2 = typeof (IService);
			Assert.IsTrue(StrongNameUtil.IsAssemblySigned(t1.Assembly));
			Assert.IsFalse(StrongNameUtil.IsAssemblySigned(t2.Assembly));
			var proxy = generator.CreateClassProxy(t1, new[] {t2}, new StandardInterceptor());
			Assert.IsFalse(StrongNameUtil.IsAssemblySigned(proxy.GetType().Assembly));
		}

		[Test]
		public void VirtualCallFromCtor()
		{
			var interceptor = new StandardInterceptor();
			var proxy = generator.CreateClassProxy<ClassCallingVirtualMethodFromCtor>(interceptor);
			Assert.AreEqual(7, proxy.Result);
		}

		[Test]
		public void ClassProxyShouldHaveDefaultConstructor()
		{
			object proxy = generator.CreateClassProxy<ClassWithDefaultConstructor>();
			Assert.IsNotNull(Activator.CreateInstance(proxy.GetType()));
		}

		[Test]
		public void ClassProxyShouldCallBaseClassDefaultConstructor()
		{
			object proxy = generator.CreateClassProxy<ClassWithDefaultConstructor>();
			var proxy2 = Activator.CreateInstance(proxy.GetType());
			Assert.AreEqual("Something", ((ClassWithDefaultConstructor)proxy2).SomeString);
		}
#if !SILVERLIGHT
		[Test]
		public void ClassProxyShouldHaveDefaultConstructorWhenBaseClassHasInternal()
		{
			object proxy = generator.CreateClassProxy<ClassWithInternalDefaultConstructor>();
			Assert.IsNotNull(Activator.CreateInstance(proxy.GetType()));
		}

		[Test]
		public void ClassProxyShouldCallInternalDefaultConstructor()
		{
			object proxy = generator.CreateClassProxy<ClassWithInternalDefaultConstructor>();
			var proxy2 = Activator.CreateInstance(proxy.GetType());
			Assert.AreEqual("Something", ((ClassWithInternalDefaultConstructor)proxy2).SomeString);
		}
#endif

		[Test]
		public void ClassProxyShouldHaveDefaultConstructorWhenBaseClassHasProtected()
		{
			object proxy = generator.CreateClassProxy<ClassWithProtectedDefaultConstructor>();
			Assert.IsNotNull(Activator.CreateInstance(proxy.GetType()));
		}

		[Test]
		public void ClassProxyShouldCallProtectedDefaultConstructor()
		{
			object proxy = generator.CreateClassProxy<ClassWithProtectedDefaultConstructor>();
			var proxy2 = Activator.CreateInstance(proxy.GetType());
			Assert.AreEqual("Something", ((ClassWithProtectedDefaultConstructor)proxy2).SomeString);
		}

		[Test]
		public void ClassImplementingInterfaceVitrually()
		{
			var @class = typeof (ClassWithVirtualInterface);
			var @interface = typeof(ISimpleInterface);
			var baseMethod = @class.GetMethod("Do");
			var interceptor = new SetReturnValueInterceptor(123);
			var proxy = generator.CreateClassProxy(@class, new[] {@interface}, interceptor);
			var mapping = proxy.GetType().GetInterfaceMap(@interface);

			Assert.AreEqual(mapping.TargetMethods[0].GetBaseDefinition(), baseMethod);

			Assert.AreEqual(123, (proxy as ClassWithVirtualInterface).Do());
			Assert.AreEqual(123, (proxy as ISimpleInterface).Do());
		}


		[Test]
		public void ClassImplementingInterfacePropertyVirtuallyWithInterface()
		{
			generator.CreateClassProxy(typeof (VirtuallyImplementsInterfaceWithProperty), new[] {typeof (IHasProperty)});
		}

		[Test]
		public void ClassImplementingInterfacePropertyVirtuallyWithoutInterface()
		{
			generator.CreateClassProxy(typeof(VirtuallyImplementsInterfaceWithProperty));
		}

		[Test]
		public void ClassImplementingInterfaceEventVirtuallyWithInterface()
		{
			var proxy = generator.CreateClassProxy(typeof (VirtuallyImplementsInterfaceWithEvent), new[] {typeof (IHasEvent)});
			(proxy as VirtuallyImplementsInterfaceWithEvent).MyEvent += null;
			(proxy as IHasEvent).MyEvent += null;
		}

		[Test]
		public void ClassImplementingInterfaceEventVirtuallyWithoutInterface()
		{
			var proxy = generator.CreateClassProxy(typeof(VirtuallyImplementsInterfaceWithEvent));
			(proxy as VirtuallyImplementsInterfaceWithEvent).MyEvent += null;
			(proxy as IHasEvent).MyEvent += null;
		}

		[Test]
		public void Finalizer_is_not_proxied()
		{
			var proxy = generator.CreateClassProxy<HasFinalizer>();

			var finalize = proxy.GetType().GetMethod("Finalize", BindingFlags.Instance | BindingFlags.NonPublic);

			Assert.IsNotNull(finalize);
			Assert.AreEqual(typeof(HasFinalizer), finalize.DeclaringType, "Finalizer shouldn't have been proxied");
		}

		[Test]
		public void Finalize_method_is_proxied_even_though_its_not_the_best_idea_ever()
		{
			var proxy = generator.CreateClassProxy<HasFinalizeMethod>();

			var finalize = proxy.GetType().GetMethod("Finalize", BindingFlags.Instance | BindingFlags.NonPublic);

			Assert.IsNotNull(finalize);
			Assert.AreNotEqual(typeof(HasFinalizeMethod), finalize.DeclaringType, "Finalize method is not a finalizer and should have been proxied");
		}

		public class ResultModifierInterceptor : StandardInterceptor
		{
			protected override void PostProceed(IInvocation invocation)
			{
				var returnValue = invocation.ReturnValue;

				if (returnValue != null && returnValue.GetType() == typeof(int))
				{
					var value = (int)returnValue;

					invocation.ReturnValue = --value;
				}
				if (returnValue != null && returnValue.GetType() == typeof(bool))
				{
					invocation.ReturnValue = true;
				}
			}
		}
	}
}