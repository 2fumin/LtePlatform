using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using LtePlatform.Areas.HelpPage.ModelDescriptions;
using NUnit.Framework;

namespace LtePlatform.Tests.ModelDescription
{
    [TestFixture]
    public class EnumTypeModelDocTest
    {
        private ModelDescriptionGenerator generator;

        [AttributeUsage(AttributeTargets.Enum)]
        class EnumDocAttribute : Attribute
        {
            public string Documentation { get; }

            public EnumDocAttribute(string doc)
            {
                Documentation = doc;
            }
        }

        [AttributeUsage(AttributeTargets.Field)]
        class EnumMemberDocAttribute : Attribute
        {
            public string Documentation { get; }

            public EnumMemberDocAttribute(string doc)
            {
                Documentation = doc;
            }
        }

        class EnumDocumentationProvider : IDocumentationProvider, IModelDocumentationProvider
        {
            public string GetDocumentation(MemberInfo member)
            {
                var attr = member.GetCustomAttributes<EnumMemberDocAttribute>().FirstOrDefault();
                return attr != null ? attr.Documentation : "";
            }

            public string GetDocumentation(Type type)
            {
                var attr = type.GetCustomAttributes<EnumDocAttribute>().FirstOrDefault();
                return attr != null ? attr.Documentation : "";
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

        [EnumDoc("This is an enum with simple type.")]
        enum SimpleEnum
        {
            [EnumMemberDoc("This is option1")]
            Option1,

            [EnumMemberDoc("This is option2")]
            Option2
        }

        private void InitializeProvider(IDocumentationProvider provider)
        {
            HttpConfiguration configuration = new HttpConfiguration();
            configuration.Services.Replace(typeof(IDocumentationProvider), provider);
            generator = new ModelDescriptionGenerator(configuration);
        }

        [Test]
        public void Test_SimpleEnum_WithSimpleModelDocumentationProvider()
        {
            InitializeProvider(new EnumDocumentationProvider());
            var description = generator.GetOrCreateModelDescription(typeof(SimpleEnum));
            Assert.IsTrue(description is EnumTypeModelDescription);
            Assert.AreEqual(description.Name, "SimpleEnum");
            Assert.AreEqual(description.ModelType, typeof(SimpleEnum));
            Assert.AreEqual(description.Documentation, "This is an enum with simple type.");
            Assert.AreEqual((description as EnumTypeModelDescription).Values.Count, 2);
            var memberValues = (description as EnumTypeModelDescription).Values;
            Assert.AreEqual(memberValues[0].Name, "Option1");
            Assert.AreEqual(memberValues[0].Value, "0");
            Assert.AreEqual(memberValues[0].Documentation, "This is option1");
            Assert.AreEqual(memberValues[1].Name, "Option2");
            Assert.AreEqual(memberValues[1].Value, "1");
            Assert.AreEqual(memberValues[1].Documentation, "This is option2");
        }
    }
}
