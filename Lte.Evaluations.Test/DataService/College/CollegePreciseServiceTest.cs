using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.MapperSerive;
using Lte.Evaluations.Test.MockItems;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities.Kpi;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.Test.DataService.College
{
    [TestFixture]
    public class CollegePreciseServiceTest
    {
        private readonly Mock<IInfrastructureRepository> _repository = new Mock<IInfrastructureRepository>();
        private readonly Mock<ICellRepository> _cellRepository = new Mock<ICellRepository>();
        private readonly Mock<IENodebRepository> _eNodebRepository = new Mock<IENodebRepository>();
        private readonly Mock<IPreciseCoverage4GRepository> _kpiRepository = new Mock<IPreciseCoverage4GRepository>();

        private CollegePreciseService _service;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            KpiMapperService.MapCellPrecise();
            _service = new CollegePreciseService(_repository.Object, _cellRepository.Object, _eNodebRepository.Object,
                _kpiRepository.Object);
            _repository.MockOperations();
            _eNodebRepository.MockOperations();
            _cellRepository.MockOperations();
            _repository.MockSixCollegeCells();
            _eNodebRepository.MockThreeENodebs();
            _cellRepository.MockSixCells();
        }

        [TestCase(1, "2015-4-1", 1, 1, 100, 50, "2015-3-5", "2015-4-5", true, true)]
        [TestCase(2, "2015-4-1", 2, 1, 100, 40, "2015-3-5", "2015-4-5", true, true)]
        [TestCase(3, "2015-4-1", 2, 2, 150, 50, "2015-3-5", "2015-4-5", true, true)]
        [TestCase(7, "2015-4-1", 2, 2, 150, 50, "2015-3-5", "2015-4-5", false, true)]
        [TestCase(3, "2015-4-1", 2, 2, 150, 50, "2015-3-5", "2015-3-25", true, false)]
        [TestCase(9, "2015-4-1", 2, 2, 150, 50, "2015-2-5", "2015-3-5", false, false)]
        public void Test_GetViews_OneRecord(int collegeId, string statTime, int cellId, byte sectorId, int totalMrs,
            int secondNeighbors, string begin, string end, bool cellMatched, bool timeMatched)
        {
            var kpis = new List<PreciseCoverage4G>
            {
                new PreciseCoverage4G
                {
                    StatTime = DateTime.Parse(statTime),
                    CellId = cellId,
                    SectorId = sectorId,
                    TotalMrs = totalMrs,
                    SecondNeighbors = secondNeighbors
                }
            };
            _kpiRepository.MockPreciseStats(kpis);
            var views = _service.GetViews("College-" + collegeId, DateTime.Parse(begin), DateTime.Parse(end));
            if (cellMatched)
            {
                Assert.AreEqual(views.Count(), 1, "Views");
                views.ElementAt(0).AssertBasicParameters(cellId, sectorId, 1.1, 20, "室外", 3.3);
                if (timeMatched)
                {
                    Assert.AreEqual(views.ElementAt(0).PreciseRate,
                        totalMrs == 0 ? 100 : 100 - (double) 100 * secondNeighbors/totalMrs, 1E-6);
                }
                else
                {
                    Assert.AreEqual(views.ElementAt(0).PreciseRate, 100.0);
                }
            }
            else
            {
                Assert.AreEqual(views.Count(), 0);
            }
        }
    }
}
