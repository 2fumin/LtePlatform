// Copyright 2004-2010 Castle Project - http://www.castleproject.org/
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
using Castle.DynamicProxy.Generators;
using NUnit.Framework;

namespace Castle.Core.Test.Main
{
    [TestFixture]
	public class MethodFinderTestCase
	{
		private static void AssertArraysAreEqualUnsorted(IReadOnlyCollection<object> expected, IReadOnlyCollection<object> actual)
		{
			Assert.AreEqual(expected.Count, actual.Count);
			var actualAsList = new List<object>(actual);
			foreach (var expectedElement in expected)
			{
				Assert.Contains(expectedElement, actualAsList);
				actualAsList.Remove(expectedElement);
				// need to remove the element after it has been found to guarantee that duplicate elements are handled correctly
			}
		}

		[Test]
		public void AssertArrayAreEqualUnsorted()
		{
			AssertArraysAreEqualUnsorted(new object[0], new object[0]);
			AssertArraysAreEqualUnsorted(new object[] { null }, new object[] { null });
			AssertArraysAreEqualUnsorted(new object[] { null, "one", null }, new object[] { null, null, "one" });
			AssertArraysAreEqualUnsorted(new object[] { null, "one", null }, new object[] { "one", null, null });

			try
			{
				AssertArraysAreEqualUnsorted(new object[] { null, "one", null }, new object[] { "one", "one", null });
				Assert.Fail();
			}
#if FEATURE_XUNITNET
			catch (Xunit.Sdk.XunitException)
#else
			catch (AssertionException)
#endif
			{
				// ok
			}
			try
			{
				AssertArraysAreEqualUnsorted(new object[] { null, "one" }, new object[] { "one", null, null });
				Assert.Fail();
			}
#if FEATURE_XUNITNET
			catch (Xunit.Sdk.XunitException)
#else
			catch (AssertionException)
#endif
			{
				// ok
			}
			try
			{
				AssertArraysAreEqualUnsorted(new object[] { null, "one", null }, new object[] { "one", null });
				Assert.Fail();
			}
#if FEATURE_XUNITNET
			catch (Xunit.Sdk.XunitException)
#else
			catch (AssertionException)
#endif
			{
				// ok
			}
		}

		[Test]
		public void GetMethodsForPublic()
		{
			var methods =
				MethodFinder.GetAllInstanceMethods(typeof(object), BindingFlags.Instance | BindingFlags.Public);
			var realMethods = typeof(object).GetMethods(BindingFlags.Instance | BindingFlags.Public);
			AssertArraysAreEqualUnsorted(realMethods, methods);
		}

		[Test]
		public void GetMethodsForNonPublic()
		{
			var methods =
				MethodFinder.GetAllInstanceMethods(typeof(object), BindingFlags.Instance | BindingFlags.NonPublic);
			var realMethods = typeof(object).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
			AssertArraysAreEqualUnsorted(realMethods, methods);
		}

		[Test]
		public void GetMethodsForPublicAndNonPublic()
		{
			var methods =
				MethodFinder.GetAllInstanceMethods(typeof(object),
												   BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			var realMethods =
				typeof(object).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			AssertArraysAreEqualUnsorted(realMethods, methods);
		}

		[Test]
		public void GetMethodsThrowsOnStatic()
		{
			Assert.Throws<ArgumentException>(() =>
				MethodFinder.GetAllInstanceMethods(typeof(object),
					BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
			);
		}

		[Test]
		public void GetMethodsThrowsOnOtherFlags()
		{
			Assert.Throws<ArgumentException>(() =>
				MethodFinder.GetAllInstanceMethods(typeof(object),
					BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly)
			);
		}
	}
}
