using AutoMapper;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Entities;
using Lte.Parameters.MockOperations;

namespace Lte.Evaluations.MapperSerive
{
    public class EvalutaionMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<CellExcel, Cell>();
            CoreMapperService.MapCdmaCell();
            CoreMapperService.MapCell();
            AlarmMapperService.MapAlarms();
            StatMapperService.MapCdmaRegionStat();
            
            InfrastructureMapperService.MapENodeb();
            InfrastructureMapperService.MapCdmaCell();
            InfrastructureMapperService.MapCell();
            CollegeMapperService.MapCollege3GTest();
            CollegeMapperService.MapCollege4GTest();
            CollegeMapperService.MapCollegeKpi();
            KpiMapperService.MapCdmaRegionStat();
            KpiMapperService.MapCellPrecise();
            KpiMapperService.MapDistrictPrecise();
            KpiMapperService.MapTownPrecise();
        }
    }
}
