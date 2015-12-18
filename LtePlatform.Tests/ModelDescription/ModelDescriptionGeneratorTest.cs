using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using LtePlatform.Areas.HelpPage.ModelDescriptions;
using NUnit.Framework;

namespace LtePlatform.Tests.ModelDescription
{
    [TestFixture]
    public class ModelDescriptionGeneratorTest
    {
        private ModelDescriptionGenerator generator;

        [SetUp]
        public void Setup()
        {
            generator = new ModelDescriptionGenerator(new HttpConfiguration());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_GetOrCreateModelDescription_NullType()
        {
            var description = generator.GetOrCreateModelDescription(null);
            Assert.IsNotNull(description);
        }

        [Test]
        public void Test_GetOrCreateModelDescription_shortType()
        {
            var description = generator.GetOrCreateModelDescription(typeof (short));
            Assert.IsTrue(description is SimpleTypeModelDescription);
            Assert.AreEqual(description.Name, "Int16");
            Assert.AreEqual(description.ModelType, typeof(short));
            Assert.AreEqual(description.Documentation, "integer");
        }

        [Test]
        public void Test_GetOrCreateModelDescription_intType()
        {
            var description = generator.GetOrCreateModelDescription(typeof(int));
            Assert.IsTrue(description is SimpleTypeModelDescription);
            Assert.AreEqual(description.Name, "Int32");
            Assert.AreEqual(description.ModelType, typeof(int));
            Assert.AreEqual(description.Documentation, "integer");
        }

        [Test]
        public void Test_GetOrCreateModelDescription_longType()
        {
            var description = generator.GetOrCreateModelDescription(typeof(long));
            Assert.IsTrue(description is SimpleTypeModelDescription);
            Assert.AreEqual(description.Name, "Int64");
            Assert.AreEqual(description.ModelType, typeof(long));
            Assert.AreEqual(description.Documentation, "integer");
        }

        [Test]
        public void Test_GetOrCreateModelDescription_ushortType()
        {
            var description = generator.GetOrCreateModelDescription(typeof(ushort));
            Assert.IsTrue(description is SimpleTypeModelDescription);
            Assert.AreEqual(description.Name, "UInt16");
            Assert.AreEqual(description.ModelType, typeof(ushort));
            Assert.AreEqual(description.Documentation, "unsigned integer");
        }

        [Test]
        public void Test_GetOrCreateModelDescription_uintType()
        {
            var description = generator.GetOrCreateModelDescription(typeof(uint));
            Assert.IsTrue(description is SimpleTypeModelDescription);
            Assert.AreEqual(description.Name, "UInt32");
            Assert.AreEqual(description.ModelType, typeof(uint));
            Assert.AreEqual(description.Documentation, "unsigned integer");
        }

        [Test]
        public void Test_GetOrCreateModelDescription_ulongType()
        {
            var description = generator.GetOrCreateModelDescription(typeof(ulong));
            Assert.IsTrue(description is SimpleTypeModelDescription);
            Assert.AreEqual(description.Name, "UInt64");
            Assert.AreEqual(description.ModelType, typeof(ulong));
            Assert.AreEqual(description.Documentation, "unsigned integer");
        }
    }
}
