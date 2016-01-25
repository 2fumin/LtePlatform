using AutoMapper;
using Lte.Evaluations.ViewModels;
using Lte.Evaluations.ViewModels.Basic;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;

namespace Lte.Evaluations.MapperSerive
{
    public static class InfrastructureMapperService
    {
        public static void MapCdmaCell()
        {
            Mapper.CreateMap<CdmaCell, CdmaCellView>()
                .ForMember(d => d.Indoor, opt => opt.MapFrom(s => s.IsOutdoor ? "室外" : "室内"))
                .ForMember(d => d.DownTilt, opt => opt.MapFrom(s => s.ETilt + s.MTilt));
            Mapper.CreateMap<CdmaCell, CdmaCompoundCellView>()
                .ForMember(d => d.Indoor, opt => opt.MapFrom(s => s.IsOutdoor ? "室外" : "室内"))
                .ForMember(d => d.DownTilt, opt => opt.MapFrom(s => s.ETilt + s.MTilt));
        }

        public static void MapCell()
        {
            Mapper.CreateMap<Cell, CellView>()
                .ForMember(d => d.Indoor, opt => opt.MapFrom(s => s.IsOutdoor ? "室外" : "室内"))
                .ForMember(d => d.DownTilt, opt => opt.MapFrom(s => s.ETilt + s.MTilt));
            Mapper.CreateMap<Cell, PciCell>();
            Mapper.CreateMap<NearestPciCell, NearestPciCellView>();
        }
    }
}
