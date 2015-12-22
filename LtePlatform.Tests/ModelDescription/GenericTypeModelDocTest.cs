using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Lte.Domain.Regular;
using LtePlatform.Areas.HelpPage.ModelDescriptions;
using LtePlatform.Models;
using NUnit.Framework;

namespace LtePlatform.Tests.ModelDescription
{
    [TestFixture]
    public class GenericTypeModelDocTest
    {
        private ModelDescriptionGenerator generator;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            var configuration = new HttpConfiguration();
            configuration.Services.Replace(typeof(IDocumentationProvider), new DocProvider());
            generator = new ModelDescriptionGenerator(configuration);
        }

        [TypeDoc("This documentation will not be read.")]
        interface ISingleParameterFromEnumerable<T> : IEnumerable<T>
        {
            [MemberDoc("Property1")]
             int Property1 { get; set; }

            [MemberDoc("Property2")]
            T Property2 { get; set; }
        }

        [TypeDoc("Some simple class")]
        class SomeSimpleClass
        {
             
        }

        [Test]
        public void Test_SingleParameterFromEnumerable()
        {
            var description = generator.GetOrCreateModelDescription(typeof(ISingleParameterFromEnumerable<SomeSimpleClass>));
            Assert.IsTrue(description is CollectionModelDescription);
            Assert.AreEqual(description.Name, "ISingleParameterFromEnumerableOfSomeSimpleClass");
            Assert.AreEqual(description.ModelType, typeof(ISingleParameterFromEnumerable<SomeSimpleClass>));
            Assert.AreEqual(description.Documentation, null);
            Areas.HelpPage.ModelDescriptions.ModelDescription modelDescription =
                (description as CollectionModelDescription).ElementDescription;
            Assert.AreEqual(modelDescription.Name, "SomeSimpleClass");
            Assert.AreEqual(modelDescription.ModelType, typeof(SomeSimpleClass));
            Assert.AreEqual(modelDescription.Documentation, "Some simple class");
        }
    }
}
