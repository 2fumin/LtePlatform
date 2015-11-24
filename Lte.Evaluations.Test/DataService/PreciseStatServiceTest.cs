using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;
using Lte.Evaluations.DataService;
using Lte.Evaluations.MapperSerive;
using Lte.Evaluations.Test.MockItems;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.Test.DataService
{
    [TestFixture]
    public class PreciseStatServiceTest
    {
        private readonly Mock<IPreciseCoverage4GRepository> _repository = new Mock<IPreciseCoverage4GRepository>();
        private readonly Mock<IENodebRepository> _eNodebRepository = new Mock<IENodebRepository>();
        private PreciseStatService _service;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            KpiMapperService.MapPreciseStat();
            _service = new PreciseStatService(_repository.Object, _eNodebRepository.Object);
            _repository.MockOperations();
            _eNodebRepository.MockOperations();
            _eNodebRepository.MockThreeENodebs();
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
            var views = _service.GetTopCountViews(DateTime.Parse("2014-12-30"), DateTime.Parse("2015-1-4"), topCount, 0);
            Assert.AreEqual(views.Count(), 0);
        }
    }
}
