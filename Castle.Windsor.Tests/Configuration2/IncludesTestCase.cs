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
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml;
using Castle.Core.Resource;
using Castle.MicroKernel;
using Castle.MicroKernel.SubSystems.Resource;
using Castle.Windsor.Configuration.Interpreters.XmlProcessor;
using Castle.Windsor.Installer;

#if !SILVERLIGHT
// we do not support xml config on SL

namespace CastleTests.Configuration2
{
	using Castle.MicroKernel.Tests.ClassComponents;
	using Castle.Windsor;
	using Castle.Windsor.Configuration.Interpreters;
	using Castle.Windsor.Tests;
	using Castle.XmlFiles;

	using NUnit.Framework;

	[TestFixture]
	public class IncludesTestCase
	{
		private IWindsorContainer container;

		[Test]
		public void AssemblyResourceAndIncludes()
		{
			container = new WindsorContainer(new XmlInterpreter(Xml.Embedded("hasResourceIncludes.xml")));

			AssertConfiguration();
		}

        [Test]
	    public void Test_Embedded()
	    {
	        var resource = Xml.Embedded("hasResourceIncludes.xml");
            Assert.IsNotNull(resource);
            Assert.AreEqual(resource.FileBasePath, AppDomain.CurrentDomain.BaseDirectory);
            var interpreter=new XmlInterpreter(resource);
            Assert.AreEqual(interpreter.EnvironmentName, null);
            var kernel=new DefaultKernel();
            interpreter.ProcessResource(resource, kernel.ConfigurationStore, kernel);
	    }

        [Test]
        public void Test_Embedded2()
        {
            var resource = Xml.Embedded("hasResourceIncludes.xml");
            var interpreter = new XmlInterpreter(resource);
            var kernel = new DefaultKernel();
            var resourceSubSystem = kernel.GetSubSystem(SubSystemConstants.ResourceKey) as IResourceSubSystem;
            var processor = new XmlProcessor(null, resourceSubSystem);
            var element = processor.Process(resource);
        }

        [Test]
        public void Test_Embedded3()
        {
            var resource = Xml.Embedded("hasResourceIncludes.xml");
            var interpreter = new XmlInterpreter(resource);
            var kernel = new DefaultKernel();
            var resourceSubSystem = kernel.GetSubSystem(SubSystemConstants.ResourceKey) as IResourceSubSystem;
            var processor = new XmlProcessor(null, resourceSubSystem);
            var assemRes = resource as AssemblyResource;
            Assert.IsNotNull(assemRes);
            var stream = assemRes.CreateStream();
        }

        [Test]
        public void Test_Embedded4()
        {
            var resource = Xml.Embedded("hasResourceIncludes.xml");
            var interpreter = new XmlInterpreter(resource);
            var kernel = new DefaultKernel();
            var resourceSubSystem = kernel.GetSubSystem(SubSystemConstants.ResourceKey) as IResourceSubSystem;
            var processor = new XmlProcessor(null, resourceSubSystem);
            var assemRes = resource as AssemblyResource;
            Assert.IsNotNull(assemRes);
            var doc = new XmlDocument();
            using (var stream = resource.GetStreamReader())
            {
                doc.Load(stream);
            }
            var engine = new DefaultXmlProcessorEngine(null, resourceSubSystem);
            engine.PushResource(resource);
            Assert.AreEqual(doc.DocumentElement.InnerText, "");
            var element = processor.Process(doc.DocumentElement);
            engine.PopResource();
        }

        [Test]
	    public void Test_Custormer_uri()
	    {
	        var name = "assembly://" + typeof (Xml).Assembly.FullName + "/XmlFiles/" + "hasResourceIncludes.xml";
            Assert.AreEqual(name, "assembly://Castle.Windsor.Tests, Version=0.0.0.0, Culture=neutral, PublicKeyToken=407dd0808d44fbdc/XmlFiles/hasResourceIncludes.xml");
            var uri = new CustomUri(name);
            Assert.AreEqual(uri.Path, "/XmlFiles/hasResourceIncludes.xml");
            var resource = new AssemblyResource(uri);
            var assemblyName = uri.Host;
            Assert.AreEqual(assemblyName, "Castle.Windsor.Tests, Version=0.0.0.0, Culture=neutral, PublicKeyToken=407dd0808d44fbdc");
            var indexOfComma = assemblyName.IndexOf(',');
            assemblyName = indexOfComma < 0 ? assemblyName : assemblyName.Substring(0, indexOfComma);
            Assert.AreEqual(assemblyName, "Castle.Windsor.Tests");
            var resourcePath = string.Format(CultureInfo.CurrentCulture, "{0}{1}", assemblyName,
                uri.Path.Replace('/', '.'));
            Assert.AreEqual(resourcePath, "Castle.Windsor.Tests.XmlFiles.hasResourceIncludes.xml");
            var assembly = Assembly.Load(assemblyName);
            Assert.IsNotNull(assembly);
            var names = assembly.GetManifestResourceNames();
            Assert.AreEqual(names.Length, 60);
            foreach (var s in names)
            {
                Console.WriteLine(s);
            }
            var nameFound = names.FirstOrDefault(x => string.Compare(resourcePath, x, StringComparison.OrdinalIgnoreCase) == 0);
            Assert.IsNotNull(nameFound);
        }

        [Test]
		public void FileResourceAndIncludes()
		{
			container = new WindsorContainer(new XmlInterpreter(Xml.File("hasFileIncludes.xml")));

			AssertConfiguration();
		}

		[Test]
		public void FileResourceAndRelativeIncludes()
		{
			container = new WindsorContainer(ConfigHelper.ResolveConfigPath("Configuration2/config_with_include_relative.xml"));

			AssertConfiguration();
		}

		[Test]
		public void FileResourceAndRelativeIncludes2()
		{
			container = new WindsorContainer(ConfigHelper.ResolveConfigPath("Configuration2/config_with_include_relative2.xml"));

			AssertConfiguration();
		}

		private void AssertConfiguration()
		{
			var store = container.Kernel.ConfigurationStore;

			Assert.AreEqual(2, store.GetFacilities().Length);
			Assert.AreEqual(2, store.GetComponents().Length);

			var config = store.GetFacilityConfiguration(typeof(NoopFacility).FullName);
			var childItem = config.Children["item"];
			Assert.IsNotNull(childItem);
			Assert.AreEqual("value", childItem.Value);

			config = store.GetFacilityConfiguration(typeof(Noop2Facility).FullName);
			Assert.IsNotNull(config);
			Assert.AreEqual("value within CDATA section", config.Value);

			config = store.GetComponentConfiguration("testidcomponent1");
			childItem = config.Children["item"];
			Assert.IsNotNull(childItem);
			Assert.AreEqual("value1", childItem.Value);

			config = store.GetComponentConfiguration("testidcomponent2");
			childItem = config.Children["item"];
			Assert.IsNotNull(childItem);
			Assert.AreEqual("value2", childItem.Value);
		}
	}
}

#endif