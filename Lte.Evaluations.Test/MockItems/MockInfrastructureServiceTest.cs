using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.Test.MockItems
{
    [TestFixture]
    public class MockInfrastructureServiceTest
    {
        private readonly Mock<IInfrastructureRepository> _repository = new Mock<IInfrastructureRepository>();

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _repository.MockThreeCollegeENodebs();
            _repository.MockOperations();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Test_GetENodebIds(int id)
        {
            var ids = _repository.Object.GetENodebIds("College-" + id);
            Assert.AreEqual(ids.Count(), 1);
            Assert.AreEqual(ids.ElementAt(0), id);
        }
    }
}
