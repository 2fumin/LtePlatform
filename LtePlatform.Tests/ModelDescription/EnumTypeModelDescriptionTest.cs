using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using LtePlatform.Areas.HelpPage;
using LtePlatform.Areas.HelpPage.ModelDescriptions;
using NUnit.Framework;

namespace LtePlatform.Tests.ModelDescription
{
    [TestFixture]
    public class EnumTypeModelDescriptionTest
    {
        private ModelDescriptionGenerator generator;

        enum SimpleEnum
        {
            Option1,
            Option2
        }

        [Test]
        public void Tewt_SimpleEnum()
        {
            generator = new ModelDescriptionGenerator(new HttpConfiguration());
            var description = generator.GetOrCreateModelDescription(typeof(SimpleEnum));
            Assert.IsTrue(description is EnumTypeModelDescription);
            Assert.AreEqual(description.Name, "SimpleEnum");
            Assert.AreEqual(description.ModelType, typeof(SimpleEnum));
            Assert.IsNull(description.Documentation);
            Assert.AreEqual((description as EnumTypeModelDescription).Values.Count, 2);
            var memberValues = (description as EnumTypeModelDescription).Values;
            Assert.AreEqual(memberValues[0].Name, "Option1");
            Assert.AreEqual(memberValues[0].Value, "0");
            Assert.AreEqual(memberValues[0].Documentation, null);
            Assert.AreEqual(memberValues[1].Name, "Option2");
            Assert.AreEqual(memberValues[1].Value, "1");
            Assert.AreEqual(memberValues[1].Documentation, null);

        }

        class SimpleModelDocumentationProvider : IDocumentationProvider, IModelDocumentationProvider
        {
            public string GetDocumentation(MemberInfo member)
            {
                return "This is a simple member documentation.";
            }

            public string GetDocumentation(Type type)
            {
                return "This is a simple type documentation.";
            }

            public string GetDocumentation(HttpControllerDescriptor controllerDescriptor)
            {
                throw new NotImplementedException();
            }

            public string GetDocumentation(HttpActionDescriptor actionDescriptor)
            {
                throw new NotImplementedException();
            }

            public string GetDocumentation(HttpParameterDescriptor parameterDescriptor)
            {
                throw new NotImplementedException();
            }

            public string GetResponseDocumentation(HttpActionDescriptor actionDescriptor)
            {
                throw new NotImplementedException();
            }
        }

        private void InitializeProvider()
        {
            HttpConfiguration configuration = new HttpConfiguration();
            configuration.Services.Replace(typeof(IDocumentationProvider), new SimpleModelDocumentationProvider());
            generator = new ModelDescriptionGenerator(configuration);
        }

        [Test]
        public void Test_Configuration()
        {
            InitializeProvider();
            Assert.IsNotNull(generator.DocumentationProvider);
        }

        [Test]
        public void Test_SimpleEnum_WithSimpleModelDocumentationProvider()
        {
            InitializeProvider();
            var description = generator.GetOrCreateModelDescription(typeof(SimpleEnum));
            Assert.IsTrue(description is EnumTypeModelDescription);
            Assert.AreEqual(description.Name, "SimpleEnum");
            Assert.AreEqual(description.ModelType, typeof(SimpleEnum));
            Assert.AreEqual(description.Documentation, "This is a simple type documentation.");
        }
    }
}
