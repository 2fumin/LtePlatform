using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Lte.Evaluations.DataService;
using Lte.Evaluations.Test.MockItems;
using Lte.Evaluations.ViewModels.Basic;
using Lte.Parameters.Abstract;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.Test.DataService
{
    [TestFixture]
    public class BtsQueryServiceTest
    {
        private readonly Mock<ITownRepository> _townRepository = new Mock<ITownRepository>();
        private readonly Mock<IBtsRepository> _btsRepository = new Mock<IBtsRepository>();
        private BtsQueryService _service;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _service = new BtsQueryService(_townRepository.Object, _btsRepository.Object);
            _townRepository.MockSixTowns();
            _townRepository.MockOpertion();
            AutoMapperHelper.CreateMap(typeof(CdmaBtsView));
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(4, 0)]
        [TestCase(6, 0)]
        [TestCase(13, 0)]
        [TestCase(24, 0)]
        public void Test_GetByTownNames(int townId, int count)
        {
            _btsRepository.MockThreeBtss();
            _btsRepository.MockOperation();
            var btsList = _service.GetByTownNames("city-" + townId, "district-" + townId, "town-" + townId) ?? new List<CdmaBtsView>();
            Assert.AreEqual(btsList.Count(), count);
        }

        [TestCase(1, 1, new[] {1, 2, 3})]
        [TestCase(1, 2, new[] { 1, 1, 3 })]
        [TestCase(2, 1, new[] { 1, 2, 3 })]
        [TestCase(2, 3, new[] { 2, 2, 2 })]
        [TestCase(3, 1, new[] { 1, 2, 3 })]
        [TestCase(4, 0, new[] { 1, 2, 3 })]
        [TestCase(6, 0, new[] { 1, 2, 3 })]
        [TestCase(13, 0, new[] { 1, 2, 3 })]
        [TestCase(24, 0, new[] { 1, 2, 3 })]
        public void Test_GetByTownNames_TownIdAssigned(int townId, int count, int[] assighedTownIds)
        {
            _btsRepository.MockThreeBtss(assighedTownIds);
            _btsRepository.MockOperation();
            var btsList = _service.GetByTownNames("city-" + townId, "district-" + townId, "town-" + townId) ?? new List<CdmaBtsView>();
            Assert.AreEqual(btsList.Count(), count);
        }

        [TestCase("Bts", 3)]
        [TestCase("1", 1)]
        [TestCase("2", 1)]
        [TestCase("3", 1)]
        [TestCase("Address", 3)]
        public void Test_GetByGeneralName(string queryString, int count)
        {
            _btsRepository.MockThreeBtss();
            _btsRepository.MockOperation();
            var btsList = _service.GetByGeneralName(queryString);
            Assert.AreEqual(btsList.Count(), count);
        }
    }
}
