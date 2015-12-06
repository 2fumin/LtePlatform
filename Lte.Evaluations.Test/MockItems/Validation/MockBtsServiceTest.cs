using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.Test.MockItems.Validation
{
    [TestFixture]
    public class MockBtsServiceTest
    {
        private readonly Mock<IBtsRepository> btsRepository = new Mock<IBtsRepository>();

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            btsRepository.MockOperation();
        }

        [SetUp]
        public void Setup()
        {
            btsRepository.MockThreeBtss();
        }

        [TestCase("kdjowi")]
        [TestCase("kjow3iu4et09wi")]
        [TestCase("kokwu43982ui")]
        public void TestUpdateFirstItem(string name)
        {
            btsRepository.Object.GetByBtsId(1).Name = name;
            Assert.AreEqual(btsRepository.Object.GetByBtsId(1).Name, name);
        }
    }
}
