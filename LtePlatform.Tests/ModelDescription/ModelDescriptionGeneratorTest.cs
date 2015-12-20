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

        [Test]
        public void Test_GetOrCreateModelDescription_byteType()
        {
            var description = generator.GetOrCreateModelDescription(typeof(byte));
            Assert.IsTrue(description is SimpleTypeModelDescription);
            Assert.AreEqual(description.Name, "Byte");
            Assert.AreEqual(description.ModelType, typeof(byte));
            Assert.AreEqual(description.Documentation, "byte");
        }

        [Test]
        public void Test_GetOrCreateModelDescription_charType()
        {
            var description = generator.GetOrCreateModelDescription(typeof(char));
            Assert.IsTrue(description is SimpleTypeModelDescription);
            Assert.AreEqual(description.Name, "Char");
            Assert.AreEqual(description.ModelType, typeof(char));
            Assert.AreEqual(description.Documentation, "character");
        }

        [Test]
        public void Test_GetOrCreateModelDescription_sbyteType()
        {
            var description = generator.GetOrCreateModelDescription(typeof(sbyte));
            Assert.IsTrue(description is SimpleTypeModelDescription);
            Assert.AreEqual(description.Name, "SByte");
            Assert.AreEqual(description.ModelType, typeof(sbyte));
            Assert.AreEqual(description.Documentation, "signed byte");
        }

        [Test]
        public void Test_GetOrCreateModelDescription_UriType()
        {
            var description = generator.GetOrCreateModelDescription(typeof(Uri));
            Assert.IsTrue(description is SimpleTypeModelDescription);
            Assert.AreEqual(description.Name, "Uri");
            Assert.AreEqual(description.ModelType, typeof(Uri));
            Assert.AreEqual(description.Documentation, "URI");
        }

        [Test]
        public void Test_GetOrCreateModelDescription_floatType()
        {
            var description = generator.GetOrCreateModelDescription(typeof(float));
            Assert.IsTrue(description is SimpleTypeModelDescription);
            Assert.AreEqual(description.Name, "Single");
            Assert.AreEqual(description.ModelType, typeof(float));
            Assert.AreEqual(description.Documentation, "decimal number");
        }

        [Test]
        public void Test_GetOrCreateModelDescription_doubleType()
        {
            var description = generator.GetOrCreateModelDescription(typeof(double));
            Assert.IsTrue(description is SimpleTypeModelDescription);
            Assert.AreEqual(description.Name, "Double");
            Assert.AreEqual(description.ModelType, typeof(double));
            Assert.AreEqual(description.Documentation, "decimal number");
        }

        [Test]
        public void Test_GetOrCreateModelDescription_decimalType()
        {
            var description = generator.GetOrCreateModelDescription(typeof(decimal));
            Assert.IsTrue(description is SimpleTypeModelDescription);
            Assert.AreEqual(description.Name, "Decimal");
            Assert.AreEqual(description.ModelType, typeof(decimal));
            Assert.AreEqual(description.Documentation, "decimal number");
        }

        [Test]
        public void Test_GetOrCreateModelDescription_stringType()
        {
            var description = generator.GetOrCreateModelDescription(typeof(string));
            Assert.IsTrue(description is SimpleTypeModelDescription);
            Assert.AreEqual(description.Name, "String");
            Assert.AreEqual(description.ModelType, typeof(string));
            Assert.AreEqual(description.Documentation, "string");
        }

        [Test]
        public void Test_GetOrCreateModelDescription_GuidType()
        {
            var description = generator.GetOrCreateModelDescription(typeof(Guid));
            Assert.IsTrue(description is SimpleTypeModelDescription);
            Assert.AreEqual(description.Name, "Guid");
            Assert.AreEqual(description.ModelType, typeof(Guid));
            Assert.AreEqual(description.Documentation, "globally unique identifier");
        }

        [Test]
        public void Test_GetOrCreateModelDescription_TimeSpan()
        {
            var description = generator.GetOrCreateModelDescription(typeof(TimeSpan));
            Assert.IsTrue(description is SimpleTypeModelDescription);
            Assert.AreEqual(description.Name, "TimeSpan");
            Assert.AreEqual(description.ModelType, typeof(TimeSpan));
            Assert.AreEqual(description.Documentation, "time interval");
        }

        [Test]
        public void Test_GetOrCreateModelDescription_DateTime()
        {
            var description = generator.GetOrCreateModelDescription(typeof(DateTime));
            Assert.IsTrue(description is SimpleTypeModelDescription);
            Assert.AreEqual(description.Name, "DateTime");
            Assert.AreEqual(description.ModelType, typeof(DateTime));
            Assert.AreEqual(description.Documentation, "date");
        }

        [Test]
        public void Test_GetOrCreateModelDescription_DateTimeOffset()
        {
            var description = generator.GetOrCreateModelDescription(typeof(DateTimeOffset));
            Assert.IsTrue(description is SimpleTypeModelDescription);
            Assert.AreEqual(description.Name, "DateTimeOffset");
            Assert.AreEqual(description.ModelType, typeof(DateTimeOffset));
            Assert.AreEqual(description.Documentation, "date");
        }

        [Test]
        public void Test_GetOrCreateModelDescription_boolType()
        {
            var description = generator.GetOrCreateModelDescription(typeof(bool));
            Assert.IsTrue(description is SimpleTypeModelDescription);
            Assert.AreEqual(description.Name, "Boolean");
            Assert.AreEqual(description.ModelType, typeof(bool));
            Assert.AreEqual(description.Documentation, "boolean");
        }
    }
}
