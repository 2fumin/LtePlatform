using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Lte.Parameters.MockOperations;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.Test.MockItems
{
    public static class MockItemsService
    {
        public static void MockQueryRegions(this Mock<IRegionRepository> repository,
            List<OptimizeRegion> regions)
        {
            repository.MockQueryItems(regions.AsQueryable());
        }

        public static void MockCdmaRegionStats(this Mock<ICdmaRegionStatRepository> repository,
            List<CdmaRegionStat> stats)
        {
            repository.MockQueryItems(stats.AsQueryable());
        }
    }
}
