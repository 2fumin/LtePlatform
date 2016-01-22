using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Should;
using Lte.Parameters.Abstract;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.Test.MockItems.Validation
{
    [TestFixture]
    public class MockCellServiceTest
    {
        private readonly Mock<ICellRepository> _cellRepository = new Mock<ICellRepository>();

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _cellRepository.MockSixCells();
        }

        [SetUp]
        public void Setup()
        {
            _cellRepository.MockOperations();
        }

        [Test]
        public void Test_GetAllList()
        {
            var allList = _cellRepository.Object.GetAllList();
            Assert.AreEqual(allList.Count, 6);
            allList[0].IsInUse.ShouldBeTrue();
        }

        [Test]
        public void Test_GetAllInUseList()
        {
            var inuseList = _cellRepository.Object.GetAllInUseList();
            Assert.AreEqual(inuseList.Count, 6);
        }
    }
}
