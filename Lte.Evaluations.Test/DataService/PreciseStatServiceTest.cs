using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Evaluations.DataService.Kpi;
using Lte.Evaluations.MapperSerive;
using Lte.Evaluations.Policy;
using Lte.Evaluations.Test.MockItems;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace Lte.Evaluations.Test.DataService
{
    [TestFixture]
    public class PreciseStatServiceTest
    {
        private readonly Mock<IPreciseCoverage4GRepository> _repository = new Mock<IPreciseCoverage4GRepository>();
        private readonly Mock<IENodebRepository> _eNodebRepository = new Mock<IENodebRepository>();
        private readonly Mock<ICellRepository> _cellRepository = new Mock<ICellRepository>();
        private readonly Mock<IInfrastructureRepository> _infrastructure = new Mock<IInfrastructureRepository>(); 
        private PreciseStatService _service;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            KpiMapperService.MapPreciseStat();
            _service = new PreciseStatService(_repository.Object, _eNodebRepository.Object, _cellRepository.Object,
                _infrastructure.Object);
            _repository.MockOperations();
            _eNodebRepository.MockOperations();
            _eNodebRepository.MockThreeENodebs();
            _cellRepository.MockOperations();
            _cellRepository.MockSixCells();
            _infrastructure.MockOperations();
            _infrastructure.MockSixCollegeCdmaCells();
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(-3)]
        public void Test_GetTopCountViews_IllegalTopCounts(int topCount)
        {
            _repository.MockPreciseStats(new List<PreciseCoverage4G>
            {
                new PreciseCoverage4G
                {
                    CellId = 1,
                    SectorId = 1,
                    StatTime = DateTime.Parse("2015-1-1")
                }
            });
            var views = _service.GetTopCountViews(DateTime.Parse("2014-12-30"), DateTime.Parse("2015-1-4"), topCount, 
                OrderPreciseStatService.OrderPreciseStatPolicy.OrderBySecondRate);
            Assert.AreEqual(views.Count(), 0);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Test_GetTopCountViews_LegalTopCounts(int topCount)
        {
            _repository.MockPreciseStats(new List<PreciseCoverage4G>
            {
                new PreciseCoverage4G
                {
                    CellId = 1,
                    SectorId = 1,
                    StatTime = DateTime.Parse("2015-1-1"),
                    TotalMrs = 5000
                }
            });
            var views = _service.GetTopCountViews(DateTime.Parse("2014-12-30"), DateTime.Parse("2015-1-4"), topCount,
                OrderPreciseStatService.OrderPreciseStatPolicy.OrderBySecondRate);
            Assert.AreEqual(views.Count(), 1);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Test_GetTopCountStats(int topCount)
        {
            _repository.MockPreciseStats(new List<PreciseCoverage4G>
            {
                new PreciseCoverage4G
                {
                    CellId = 1,
                    SectorId = 1,
                    StatTime = DateTime.Parse("2015-1-1"),
                    TotalMrs = 5000
                }
            });
            var stats = _service.GetTopCountViews(DateTime.Parse("2014-12-30"), DateTime.Parse("2015-1-4"), topCount,
                OrderPreciseStatService.OrderPreciseStatPolicy.OrderBySecondRate);
            Assert.AreEqual(stats.Count(), 1);
        }

        [Test]
        public void Test_GetStats()
        {
            _repository.MockPreciseStats(new List<PreciseCoverage4G>
            {
                new PreciseCoverage4G
                {
                    CellId = 1,
                    SectorId = 1,
                    StatTime = DateTime.Parse("2015-1-1"),
                    TotalMrs = 5000
                }
            });
            var query =
                _repository.Object.GetAll()
                    .Where(
                        x =>
                            x.StatTime >= DateTime.Parse("2014-12-30") && x.StatTime < DateTime.Parse("2015-1-4") &&
                            x.TotalMrs > PreciseStatService.TotalMrsThreshold);
            Assert.AreEqual(query.Count(), 1);
        }

        [TestCase(new[] {"2015-2-1", "2015-2-2" }, new[] { "2015-2-1", "2015-2-2" }, "2015-1-31", "2015-2-3")]
        [TestCase(new[] { "2015-2-2", "2015-2-1" }, new[] { "2015-2-1", "2015-2-2" }, "2015-1-31", "2015-2-3")]
        public void Test_GetTimeSpanStats(string[] soureDates, string[] resultDates, string beginDate, string endDate)
        {
            _repository.MockPreciseStats(soureDates.Select(x=>new PreciseCoverage4G
            {
                CellId = 1,
                SectorId = 1,
                StatTime = DateTime.Parse(x)
            }).ToList());
            var stats = _service.GetTimeSpanStats(1, 1, DateTime.Parse(beginDate), DateTime.Parse(endDate));
            stats.Select(x => x.StatTime).ShouldBe(resultDates.Select(DateTime.Parse));
        }
    }
}
