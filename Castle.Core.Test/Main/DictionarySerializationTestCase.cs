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

#define FEATURE_SERIALIZATION

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Castle.Core.Test.DynamicProxy.Classes;
using Castle.DynamicProxy;
using NUnit.Framework;

#if FEATURE_SERIALIZATION

namespace Castle.Core.Test.Main
{
    [TestFixture]
	public class DictionarySerializationTestCase
	{
		[Test]
		public void NullReferenceProxyDeserializationTest()
		{
			var generator = new ProxyGenerator();
			var theInstances =
				new Dictionary<ClassOverridingEqualsAndGetHashCode, string>();
			var c =
				(ClassOverridingEqualsAndGetHashCode)generator.CreateClassProxy(typeof(ClassOverridingEqualsAndGetHashCode));
			c.Id = Guid.NewGuid();
			c.Name = DateTime.Now.ToString("yyyyMMddHHmmss");
			theInstances.Add(c, c.Name);
			var theInstancesBis =
				SerializeAndDeserialize(theInstances);

			Assert.IsNotNull(theInstancesBis);
			Assert.AreEqual(theInstances.Count, theInstancesBis.Count);
		}

		[Test]
		public void DictionaryDeserializationWithoutProxyTest()
		{
			var theInstances =
				new Dictionary<ClassOverridingEqualsAndGetHashCode, string>();

			for (var i = 0; i < 50; i++)
			{
			    var c = new ClassOverridingEqualsAndGetHashCode
			    {
			        Id = Guid.NewGuid(),
			        Name = DateTime.Now.ToString("yyyyMMddHHmmss")
			    };
			    theInstances.Add(c, c.Name);
			}

#pragma warning disable 219
			var theInstancesBis =
				SerializeAndDeserialize(theInstances);
#pragma warning restore 219
		}

		[Test]
		public void DictionaryDeserializationWithProxyTest()
		{
			var generator = new ProxyGenerator();
			var theInstances =
				new Dictionary<ClassOverridingEqualsAndGetHashCode, string>();

			for (var i = 0; i < 50; i++)
			{
				var c =
					(ClassOverridingEqualsAndGetHashCode)generator.CreateClassProxy(typeof(ClassOverridingEqualsAndGetHashCode));
				c.Id = Guid.NewGuid();
				c.Name = DateTime.Now.ToString("yyyyMMddHHmmss");
				theInstances.Add(c, c.Name);
			}

#pragma warning disable 219
			var theInstancesBis =
				SerializeAndDeserialize(theInstances);
#pragma warning restore 219
		}

		[Test]
		public void BasicSerializationProxyTest()
		{
			var generator = new ProxyGenerator();
			var c =
				(ClassOverridingEqualsAndGetHashCode)generator.CreateClassProxy(typeof(ClassOverridingEqualsAndGetHashCode));
			c.Id = Guid.NewGuid();
			c.Name = DateTime.Now.ToString("yyyyMMddHHmmss");

			var c2 = SerializeAndDeserialize(c);
			Assert.IsNotNull(c2);
			Assert.AreEqual(c.Id, c2.Id);
			Assert.AreEqual(c.Name, c2.Name);
		}

		public static T SerializeAndDeserialize<T>(T proxy)
		{
			var stream = new MemoryStream();
			var formatter = new BinaryFormatter();
			formatter.Serialize(stream, proxy);
			stream.Position = 0;
			return (T) formatter.Deserialize(stream);
		}
	}
}

#endif