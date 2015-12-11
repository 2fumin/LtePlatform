using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.DataService.Dump;
using Lte.Parameters.Abstract;
using Lte.Parameters.Concrete;
using Ninject;

namespace LtePlatform
{
    public static class KernelBindingOpertions
    {
        public static void AddBindings(this IKernel ninjectKernel)
        {
            ninjectKernel.Bind<ITownRepository>().To<EFTownRepository>();

            ninjectKernel.Bind<ICdmaRegionStatRepository>().To<EFCdmaRegionStatRepository>();

            ninjectKernel.Bind<IRegionRepository>().To<EFRegionRepository>();

            ninjectKernel.Bind<ICollegeRepository>().To<EFCollegeRepository>();

            ninjectKernel.Bind<IInfrastructureRepository>().To<EFInfrastructureRepository>();

            ninjectKernel.Bind<IAlarmRepository>().To<EFAlameRepository>();

            ninjectKernel.Bind<ICollege3GTestRepository>().To<EFCollege3GTestRepository>();

            ninjectKernel.Bind<ICollege4GTestRepository>().To<EFCollege4GTestRepository>();

            ninjectKernel.Bind<ICollegeKpiRepository>().To<EFCollegeKpiRepository>();

            ninjectKernel.Bind<IENodebRepository>().To<EFENodebRepository>();

            ninjectKernel.Bind<IBtsRepository>().To<EFBtsRepository>();

            ninjectKernel.Bind<ICellRepository>().To<EFCellRepository>();

            ninjectKernel.Bind<ICdmaCellRepository>().To<EFCdmaCellRepository>();

            ninjectKernel.Bind<IIndoorDistributioinRepository>().To<EFIndoorDistributionRepository>();

            ninjectKernel.Bind<IPreciseCoverage4GRepository>().To<EFPreciseCoverage4GRepository>();

            ninjectKernel.Bind<ITopDrop2GCellRepository>().To<EFTopDrop2GCellRepository>();

            ninjectKernel.Bind<ITopConnection3GRepository>().To<EFTopConnection3GRepository>();

            ninjectKernel.Bind<ITownPreciseCoverage4GStatRepository>().To<EFTownPreciseCoverage4GStatRepository>();

            ninjectKernel.Bind<CdmaRegionStatService>().ToSelf();

            ninjectKernel.Bind<CollegeStatService>().ToSelf();

            ninjectKernel.Bind<ENodebQueryService>().ToSelf();

            ninjectKernel.Bind<BtsQueryService>().ToSelf();

            ninjectKernel.Bind<CollegeENodebService>().ToSelf();

            ninjectKernel.Bind<CollegeBtssService>().ToSelf();

            ninjectKernel.Bind<CellService>().ToSelf();

            ninjectKernel.Bind<CdmaCellService>().ToSelf();

            ninjectKernel.Bind<College3GTestService>().ToSelf();

            ninjectKernel.Bind<College4GTestService>().ToSelf();

            ninjectKernel.Bind<CollegeDistributionService>().ToSelf();

            ninjectKernel.Bind<CollegeKpiService>().ToSelf();

            ninjectKernel.Bind<CollegePreciseService>().ToSelf();

            ninjectKernel.Bind<CollegeCdmaCellsService>().ToSelf();

            ninjectKernel.Bind<CollegeCellsService>().ToSelf();
            
            ninjectKernel.Bind<PreciseStatService>().ToSelf();

            ninjectKernel.Bind<TownQueryService>().ToSelf();

            ninjectKernel.Bind<KpiImportService>().ToSelf();

            ninjectKernel.Bind<PreciseRegionStatService>().ToSelf();

            ninjectKernel.Bind<AlarmsService>().ToSelf();

            ninjectKernel.Bind<TopDrop2GService>().ToSelf();

            ninjectKernel.Bind<BasicImportService>().ToSelf();

            ninjectKernel.Bind<ENodebDumpService>().ToSelf();

            ninjectKernel.Bind<BtsDumpService>().ToSelf();

            ninjectKernel.Bind<CellDumpService>().ToSelf();

            ninjectKernel.Bind<CdmaCellDumpService>().ToSelf();
        }
    }
}
