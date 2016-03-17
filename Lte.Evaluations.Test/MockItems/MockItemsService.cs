using System.Collections.Generic;
using System.Linq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;
using Lte.Parameters.Entities.Kpi;
using Lte.Parameters.MockOperations;
using Moq;

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

        public static void MockInfrastructures(this Mock<IInfrastructureRepository> repository,
            List<InfrastructureInfo> infos)
        {
            repository.MockQueryItems(infos.AsQueryable());
        }

        public static void MockDistributions(this Mock<IIndoorDistributioinRepository> repository,
            List<IndoorDistribution> distributions)
        {
            repository.MockQueryItems(distributions.AsQueryable());
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

        public static void MockTopDrop2GCells(this Mock<ITopDrop2GCellRepository> repository, List<TopDrop2GCell> stats)
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

        public static void MockCdmaCells(this Mock<ICdmaCellRepository> repository, List<CdmaCell> cells)
        {
            repository.MockQueryItems(cells.AsQueryable());
        }

        public static void MockAlarms(this Mock<IAlarmRepository> repository, List<AlarmStat> alarms)
        {
            repository.MockQueryItems(alarms.AsQueryable());
        }

        public static void MockTowns(this Mock<ITownRepository> repository, List<Town> towns)
        {
            repository.MockQueryItems(towns.AsQueryable());
        }
    }
}
