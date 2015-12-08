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

#define FEATURE_SERIALIZATION

using System;
using System.Collections.Generic;
using System.Reflection;
using Castle.Core.Test.DynamicProxy.Classes;
using Castle.Core.Test.Interceptors;
using Castle.Core.Test.InterClasses;
using Castle.DynamicProxy;
using NUnit.Framework;

namespace Castle.Core.Test.Main
{
    [TestFixture]
	public class GenerationHookTestCase : BasePEVerifyTestCase
	{
		[Test]
		public void HookIsUsedForConcreteClassProxy()
		{
			var logger = new LogInvocationInterceptor();
			var hook = new LogHook(typeof(ServiceClass), true);

			var options = new ProxyGenerationOptions(hook);

			var proxy = (ServiceClass)generator.CreateClassProxy(typeof(ServiceClass), options, logger);

			Assert.IsTrue(hook.Completed);
			Assert.AreEqual(13, hook.AskedMembers.Count, "Asked members");
			Assert.AreEqual(2, hook.NonVirtualMembers.Count, "Non-virtual members");

			proxy.Sum(1, 2);
			Assert.IsFalse(proxy.Valid);

			Assert.AreEqual("get_Valid ", logger.LogContents);
		}

		[Test]
		public void HookIsUsedForInterfaceProxy()
		{
			var logger = new LogInvocationInterceptor();
			var hook = new LogHook(typeof(IService));

			var options = new ProxyGenerationOptions(hook);

			var proxy = (IService)
			            generator.CreateInterfaceProxyWithTarget(
			            	typeof(IService), new ServiceImpl(), options, logger);

			Assert.IsTrue(hook.Completed);
			Assert.AreEqual(10, hook.AskedMembers.Count);
			Assert.AreEqual(0, hook.NonVirtualMembers.Count);

			Assert.AreEqual(3, proxy.Sum(1, 2));
			Assert.IsFalse(proxy.Valid);

			Assert.AreEqual("Sum get_Valid ", logger.LogContents);
		}

		[Test]
		public void Hook_can_NOT_see_GetType_method()
		{
			var hook = new LogHook(typeof(EmptyClass));

			generator.CreateClassProxy(typeof(EmptyClass), new ProxyGenerationOptions(hook));

			var getType = typeof(EmptyClass).GetMethod("GetType");
			CollectionAssert.DoesNotContain(hook.AskedMembers, getType);
			CollectionAssert.DoesNotContain(hook.NonVirtualMembers, getType);
		}

		[Test]
		public void Hook_can_NOT_see_MemberwiseClone_method()
		{
			var hook = new LogHook(typeof(EmptyClass));

			generator.CreateClassProxy(typeof(EmptyClass), new ProxyGenerationOptions(hook));

			var memberwiseClone = typeof(EmptyClass).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);
			CollectionAssert.DoesNotContain(hook.AskedMembers, memberwiseClone);
			CollectionAssert.DoesNotContain(hook.NonVirtualMembers, memberwiseClone);
		}

		[Test]
		public void Hook_can_see_Equals_method()
		{
			var hook = new LogHook(typeof(EmptyClass));

			generator.CreateClassProxy(typeof(EmptyClass), new ProxyGenerationOptions(hook));

			var equals = typeof(EmptyClass).GetMethod("Equals");
			CollectionAssert.Contains(hook.AskedMembers, equals);
		}

		[Test]
		public void Hook_can_see_GetHashCode_method()
		{
			var hook = new LogHook(typeof(EmptyClass));

			generator.CreateClassProxy(typeof(EmptyClass), new ProxyGenerationOptions(hook));

			var getHashCode = typeof(EmptyClass).GetMethod("GetHashCode");
			CollectionAssert.Contains(hook.AskedMembers, getHashCode);
		}

		[Test]
		public void Hook_can_see_ToString_method()
		{
			var hook = new LogHook(typeof(EmptyClass));

			generator.CreateClassProxy(typeof(EmptyClass), new ProxyGenerationOptions(hook));

			var equals = typeof(EmptyClass).GetMethod("ToString");
			CollectionAssert.Contains(hook.AskedMembers, equals);
		}
	}

#if FEATURE_SERIALIZATION
	[Serializable]
#endif
	public class LogHook : IProxyGenerationHook
	{
		private readonly Type targetTypeToAssert;
		private readonly bool screeningEnabled;

	    public LogHook(Type targetTypeToAssert, bool screeningEnabled = false)
		{
			this.targetTypeToAssert = targetTypeToAssert;
			this.screeningEnabled = screeningEnabled;
		}

		public IList<MemberInfo> NonVirtualMembers { get; } = new List<MemberInfo>();

	    public IList<MemberInfo> AskedMembers { get; } = new List<MemberInfo>();

	    public bool Completed { get; private set; }

	    public bool ShouldInterceptMethod(Type type, MethodInfo memberInfo)
		{
			Assert.AreEqual(targetTypeToAssert, type);

			AskedMembers.Add(memberInfo);

			return !screeningEnabled || !memberInfo.Name.StartsWith("Sum");
		}

		public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
		{
			Assert.AreEqual(targetTypeToAssert, type);

			NonVirtualMembers.Add(memberInfo);
		}

		public void MethodsInspected()
		{
			Completed = true;
		}
	}
}
