// Copyright 2004-2011 Castle Project - http://www.castleproject.org/
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
using Castle.DynamicProxy.Generators.Emitters;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using NUnit.Framework;

namespace Castle.Core.Test.Main
{
    [TestFixture]
	public class ClassEmitterTestCase : BasePEVerifyTestCase
	{
		[Test]
		public void AutomaticDefaultConstructorGeneration()
		{
			var emitter = new ClassEmitter(generator.ProxyBuilder.ModuleScope, "Foo", typeof (object), Type.EmptyTypes);
			var t = emitter.BuildType();
			Activator.CreateInstance(t);
		}

		[Test]
		public void AutomaticDefaultConstructorGenerationWithClosedGenericType()
		{
			var emitter = new ClassEmitter(generator.ProxyBuilder.ModuleScope, "Foo", typeof (List<object>),
			                                        Type.EmptyTypes);
			var t = emitter.BuildType();
			Activator.CreateInstance(t);
		}

		[Test]
		public void StaticMethodArguments()
		{
			var emitter = new ClassEmitter(generator.ProxyBuilder.ModuleScope, "Foo", typeof (List<object>),
			                                        Type.EmptyTypes);
			var methodEmitter = emitter.CreateMethod("StaticMethod", MethodAttributes.Public | MethodAttributes.Static,
			                                                   typeof (string), typeof (string));
			methodEmitter.CodeBuilder.AddStatement(new ReturnStatement(methodEmitter.Arguments[0]));
			var t = emitter.BuildType();
			Assert.AreEqual("five", t.GetMethod("StaticMethod").Invoke(null, new object[] {"five"}));
		}

		[Test]
		public void InstanceMethodArguments()
		{
			var emitter = new ClassEmitter(generator.ProxyBuilder.ModuleScope, "Foo", typeof (List<object>),
			                                        Type.EmptyTypes);
			var methodEmitter = emitter.CreateMethod("InstanceMethod", MethodAttributes.Public,
			                                                   typeof (string), typeof (string));
			methodEmitter.CodeBuilder.AddStatement(new ReturnStatement(methodEmitter.Arguments[0]));
			var t = emitter.BuildType();
			var instance = Activator.CreateInstance(t);
			Assert.AreEqual("six", t.GetMethod("InstanceMethod").Invoke(instance, new object[] {"six"}));
		}

#if !SILVERLIGHT
		[Test]
		public void ForceUnsignedFalseWithSignedTypes()
		{
			const bool shouldBeSigned = true;
			var emitter = new ClassEmitter(generator.ProxyBuilder.ModuleScope, "Foo", typeof (object), Type.EmptyTypes,
			                                        TypeAttributes.Public, false);
			var t = emitter.BuildType();
			Assert.AreEqual(shouldBeSigned, t.Assembly.IsAssemblySigned());
		}
#endif

		[Test]
		public void ForceUnsignedTrueWithSignedTypes()
		{
			var emitter = new ClassEmitter(generator.ProxyBuilder.ModuleScope, "Foo", typeof (object), Type.EmptyTypes,
			                                        TypeAttributes.Public, true);
			var t = emitter.BuildType();
			Assert.IsFalse(t.Assembly.IsAssemblySigned());
		}

		[Test]
		public void CreateFieldWithAttributes()
		{
			var emitter = new ClassEmitter(generator.ProxyBuilder.ModuleScope, "Foo", typeof (object), Type.EmptyTypes);
			emitter.CreateField("myField", typeof (string), FieldAttributes.FamANDAssem | FieldAttributes.InitOnly);
			var t = emitter.BuildType();
			var field = t.GetField("myField", BindingFlags.NonPublic | BindingFlags.Instance);
			Assert.IsNotNull(field);
			Assert.AreEqual(FieldAttributes.FamANDAssem | FieldAttributes.InitOnly, field.Attributes);
		}

		[Test]
		public void CreateStaticFieldWithAttributes()
		{
			var emitter = new ClassEmitter(generator.ProxyBuilder.ModuleScope, "Foo", typeof (object), Type.EmptyTypes);
			emitter.CreateStaticField("myField", typeof (string), FieldAttributes.FamANDAssem | FieldAttributes.InitOnly);
			var t = emitter.BuildType();
			var field = t.GetField("myField", BindingFlags.NonPublic | BindingFlags.Static);
			Assert.IsNotNull(field);
			Assert.AreEqual(FieldAttributes.Static | FieldAttributes.FamANDAssem | FieldAttributes.InitOnly, field.Attributes);
		}

		[Test]
		public void UsingClassEmitterForInterfaces()
		{
			var emitter = new ClassEmitter(generator.ProxyBuilder.ModuleScope, "IFoo", null, Type.EmptyTypes, 
				TypeAttributes.Interface | TypeAttributes.Abstract | TypeAttributes.Public, false);
			emitter.CreateMethod("MyMethod", MethodAttributes.Public | MethodAttributes.Abstract | MethodAttributes.Virtual,
			                     typeof(void), Type.EmptyTypes);
			var t = emitter.BuildType();
			Assert.IsTrue(t.GetTypeInfo().IsInterface);
			var method = t.GetMethod("MyMethod");
			Assert.IsNotNull(method);
		}

		[Test]
		public void NoBaseTypeForInterfaces()
		{
			DisableVerification();
			var emitter = new ClassEmitter (generator.ProxyBuilder.ModuleScope, "IFoo", null, Type.EmptyTypes,
				TypeAttributes.Interface | TypeAttributes.Abstract | TypeAttributes.Public, false);

			Assert.Throws<InvalidOperationException>(delegate {
#pragma warning disable 219
				var t = emitter.BaseType;
#pragma warning restore 219
			});
		}

		[Test]
		public void NoDefaultCtorForInterfaces()
		{
			DisableVerification();
			var emitter = new ClassEmitter(generator.ProxyBuilder.ModuleScope, "IFoo", null, Type.EmptyTypes,
				TypeAttributes.Interface | TypeAttributes.Abstract | TypeAttributes.Public, false);

			Assert.Throws<InvalidOperationException>(delegate {
				emitter.CreateDefaultConstructor();
			});
		}

		[Test]
		public void NoCustomCtorForInterfaces()
		{
			DisableVerification();
			var emitter = new ClassEmitter(generator.ProxyBuilder.ModuleScope, "IFoo", null, Type.EmptyTypes,
				TypeAttributes.Interface | TypeAttributes.Abstract | TypeAttributes.Public, false);

			Assert.Throws<InvalidOperationException>(delegate {
				emitter.CreateConstructor();
			});
		}

		[Test]
		public void NestedInterface()
		{
			var outerEmitter = new ClassEmitter(generator.ProxyBuilder.ModuleScope, "IOuter", null, Type.EmptyTypes, 
				TypeAttributes.Interface | TypeAttributes.Abstract | TypeAttributes.Public, false);
			var innerEmitter = new NestedClassEmitter(outerEmitter, "IInner", 
				TypeAttributes.Interface | TypeAttributes.Abstract | TypeAttributes.NestedPublic, null, Type.EmptyTypes);
			innerEmitter.CreateMethod("MyMethod", MethodAttributes.Public | MethodAttributes.Abstract | MethodAttributes.Virtual,
			                          typeof(void), Type.EmptyTypes);
			var inner = innerEmitter.BuildType();
			var outer = outerEmitter.BuildType();
			Assert.IsTrue(inner.GetTypeInfo().IsInterface);
			var method = inner.GetMethod("MyMethod");
			Assert.IsNotNull(method);
			Assert.AreSame(inner, outer.GetNestedType("IInner", BindingFlags.Public));
		}
	}
}