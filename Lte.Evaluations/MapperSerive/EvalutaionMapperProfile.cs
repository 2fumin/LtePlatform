using AutoMapper;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.MapperSerive
{
    public class EvalutaionMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<CellExcel, Cell>();
            Mapper.CreateMap<CdmaCellExcel, CdmaCell>();

            Mapper.CreateMap<ENodeb, ENodebView>();
            Mapper.CreateMap<Cell, CellView>();
            Mapper.CreateMap<CdmaCell, CdmaCellView>();
            CollegeMapperService.MapCollege3GTest();
            CollegeMapperService.MapCollege4GTest();
            CollegeMapperService.MapCollegeKpi();
        }
    }
}
