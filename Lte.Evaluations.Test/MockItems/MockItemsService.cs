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

        public static void MockQueryTowns(this Mock<ITownRepository> repository,
            List<Town> towns)
        {
            repository.MockQueryItems(towns.AsQueryable());
        }

        public static void MockPreciseRegionStats(this Mock<ITownPreciseCoverage4GStatRepository> repository,
            List<TownPreciseCoverage4GStat> stats)
        {
            repository.MockQueryItems(stats.AsQueryable());
        }

        public static void MockPreciseStats(this Mock<IPreciseCoverage4GRepository> repository,
            List<PreciseCoverage4G> stats)
        {
            repository.MockQueryItems(stats.AsQueryable());
        }

        public static void MockENodebs(this Mock<IENodebRepository> repository, List<ENodeb> eNodebs)
        {
            repository.MockQueryItems(eNodebs.AsQueryable());
        }

        public static void MockBtss(this Mock<IBtsRepository> repository, List<CdmaBts> btss)
        {
            repository.MockQueryItems(btss.AsQueryable());
        }

        public static void MockCells(this Mock<ICellRepository> repository, List<Cell> cells)
        {
            repository.MockQueryItems(cells.AsQueryable());
        }
    }
}
