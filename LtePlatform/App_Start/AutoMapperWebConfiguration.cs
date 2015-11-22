using AutoMapper;
using Lte.Evaluations;
using Lte.Evaluations.MapperSerive;

namespace LtePlatform
{
    public static class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new EvalutaionMapperProfile());
            });
        }
    }
}
