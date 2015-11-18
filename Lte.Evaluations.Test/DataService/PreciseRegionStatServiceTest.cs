using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Evaluations.Test.MockItems;
using Lte.Evaluations.Test.TestService;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.Test.DataService
{
    [TestFixture]
    public class PreciseRegionStatServiceTest
    {
        private readonly Mock<ITownRepository> _townRepository = new Mock<ITownRepository>(); 
        private readonly Mock<ITownPreciseCoverage4GStatRepository> _statRepository =
            new Mock<ITownPreciseCoverage4GStatRepository>();

        private PreciseRegionStatTestService _testService;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            _statRepository.MockOperation();
            _townRepository.MockOpertion();
        }

        [SetUp]
        public void SetUp()
        {
            _townRepository.MockQueryTowns(new List<Town>
            {
                new Town {Id = 1, CityName = "city", DistrictName = "district1", TownName = "town1"},
                new Town {Id = 2, CityName = "city", DistrictName = "district2", TownName = "town2"},
                new Town {Id = 3, CityName = "city", DistrictName = "district2", TownName = "town3"}
            });
            _testService = new PreciseRegionStatTestService(_townRepository, _statRepository);
        }

        [TestCase("2015-5-1", 1, "2015-4-1", 10, 11, 12, 13)]
        [TestCase("2015-6-2", 2, "2015-4-20", 15, 21, 72, 44)]
        [TestCase("2015-6-2", 3, "2015-5-20", 15, 38, 41, 5)]
        public void TestQueryLastDateStat_Normal(string initialDate, int townId, string statDate, int totalMrs,
            int firstNeighbors, int secondNeighbors, int thirdNeighbors)
        {
            _testService.ImportPreciseRecord(townId, statDate, totalMrs, firstNeighbors, secondNeighbors, thirdNeighbors);
            var result = _testService.QueryLastDateStat(initialDate, "city");
            Assert.IsNotNull(result);
            Assert.AreEqual(DateTime.Parse(result.StatDate), DateTime.Parse(statDate));
            Assert.AreEqual(result.TownPreciseViews.Count(), 1);
            PreciseViewTestService.AssertEqual(result.TownPreciseViews.ElementAt(0), new TownPreciseView
            {
                City = "city",
                District = "district" + (townId == 1 ? 1 : 2),
                Town = "town" + townId,
                FirstNeighbors = firstNeighbors,
                SecondNeighbors = secondNeighbors,
                ThirdNeighbors = thirdNeighbors,
                TotalMrs = totalMrs
            });
            Assert.AreEqual(result.DistrictPreciseViews.Count(), 1);
            PreciseViewTestService.AssertEqual(result.DistrictPreciseViews.ElementAt(0), new DistrictPreciseView
            {
                City = "city",
                District = "district" + (townId == 1 ? 1 : 2),
                FirstNeighbors = firstNeighbors,
                SecondNeighbors = secondNeighbors,
                TotalMrs = totalMrs
            });
        }
    }
}
