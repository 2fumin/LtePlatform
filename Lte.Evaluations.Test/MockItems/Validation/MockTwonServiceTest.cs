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
    public class MockTwonServiceTest
    {
        private readonly Mock<ITownRepository> _townRepository = new Mock<ITownRepository>();

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _townRepository.MockSixTowns();
            _townRepository.MockOpertion();
        }

        [Test]
        public void Test_QueryTown()
        {
            var item = _townRepository.Object.QueryTown("city-1", "district-1", "town-1");
            Assert.IsNotNull(item);
        }
    }
}
