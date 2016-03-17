using Lte.Parameters.Abstract.Basic;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.Test.MockItems.Validation
{
    [TestFixture]
    public class MockBtsServiceTest
    {
        private readonly Mock<IBtsRepository> _btsRepository = new Mock<IBtsRepository>();

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _btsRepository.MockOperation();
        }

        [SetUp]
        public void Setup()
        {
            _btsRepository.MockThreeBtss();
        }

        [TestCase("kdjowi")]
        [TestCase("kjow3iu4et09wi")]
        [TestCase("kokwu43982ui")]
        public void TestUpdateFirstItem(string name)
        {
            _btsRepository.Object.GetByBtsId(1).Name = name;
            Assert.AreEqual(_btsRepository.Object.GetByBtsId(1).Name, name);
        }
    }
}
