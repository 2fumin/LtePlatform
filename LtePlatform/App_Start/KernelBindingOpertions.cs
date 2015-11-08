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
        }
    }
}
