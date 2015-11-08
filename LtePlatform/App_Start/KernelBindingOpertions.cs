using Lte.Evaluations.DataService;
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

            ninjectKernel.Bind<CdmaRegionStatService>().ToSelf();

            ninjectKernel.Bind<CollegeStatService>().ToSelf();
        }
    }
}
