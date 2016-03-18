using System;
using System.Linq;
using Lte.Parameters.Abstract.Kpi;
using Moq;

namespace Lte.Evaluations.Test.MockItems
{
    public static class MockPreciseRegionStatService
    {
        public static void MockOperation(this Mock<ITownPreciseCoverage4GStatRepository> repository)
        {
            repository.Setup(x => x.GetAllList(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns<DateTime, DateTime>(
                    (begin, end) =>
                        repository.Object.GetAll().Where(x => x.StatTime >= begin && x.StatTime < end).ToList());
        }
    }
}
