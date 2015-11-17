using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Moq;

namespace Lte.Evaluations.Test.MockItems
{
    public static class MockCdmaRegionStatService
    {
        public static void MockOperation(this Mock<ICdmaRegionStatRepository> repository)
        {
            repository.Setup(x => x.GetByDateSpan(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns<DateTime, DateTime>(
                    (begin, end) =>
                        repository.Object.GetAll().Where(x => x.StatDate >= begin && x.StatDate < end).ToList());
        }
    }
}
