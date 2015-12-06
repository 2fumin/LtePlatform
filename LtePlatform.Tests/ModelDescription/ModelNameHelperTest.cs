using System;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LtePlatform.Areas.HelpPage.ModelDescriptions;
using NUnit.Framework;

namespace LtePlatform.Tests.ModelDescription
{
    [TestFixture]
    public class ModelNameHelperTest
    {
        [ModelName("MyModelName")]
        class ClassWithModelName
        {
             public int Member { get; set; }
        }

        [Test]
        public void Test_WithModelNameCase()
        {
            var name = ModelNameHelper.GetModelName(typeof (ClassWithModelName));
            Assert.AreEqual(name, "MyModelName");
        }

        class PlainType
        {
            public int Member { get; set; }
        }

        [Test]
        public void Test_PlainType()
        {
            var name = ModelNameHelper.GetModelName(typeof (PlainType));
            Assert.AreEqual(name, "PlainType");
        }

        interface IPlainInterface
        {
             int Member { get; set; }
        }

        [Test]
        public void Test_PlainInterface()
        {
            var name = ModelNameHelper.GetModelName(typeof (IPlainInterface));
            Assert.AreEqual(name, "IPlainInterface");
        }

        interface ITypeInterface<T>
        {
            T Member { get; set; }
        }

        [Test]
        public void Test_IntInterface()
        {
            var name = ModelNameHelper.GetModelName(typeof (ITypeInterface<int>));
            Assert.AreEqual(name, "ITypeInterfaceOfInt32");
        }

        [Test]
        public void Test_DoubleInterface()
        {
            var name = ModelNameHelper.GetModelName(typeof(ITypeInterface<double>));
            Assert.AreEqual(name, "ITypeInterfaceOfDouble");
        }

        [Test]
        public void Test_CascadeInterface()
        {
            var name = ModelNameHelper.GetModelName(typeof (ITypeInterface<ITypeInterface<int>>));
            Assert.AreEqual(name, "ITypeInterfaceOfITypeInterfaceOfInt32");
        }

        interface IDoubleParameters<T1, T2>
        {
            T1 Member1 { get; set; }
            T2 Member2 { get; set; }
        }

        [Test]
        public void Test_DoubleParameters_Interface()
        {
            var name = ModelNameHelper.GetModelName(typeof (IDoubleParameters<int, double>));
            Assert.AreEqual(name, "IDoubleParametersOfInt32AndDouble");
        }
    }
}
