using AutoMapper;
using Lte.Evaluations.ViewModels.Basic;
using Lte.Evaluations.ViewModels.Mr;
using Lte.Evaluations.ViewModels.Switch;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;
using Lte.Parameters.Entities.Switch;

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

        public static void MapHoParametersService()
        {
            Mapper.CreateMap<IntraRatHoComm, ENodebIntraFreqHoView>()
                .ForMember(d => d.ENodebId, opt => opt.MapFrom(s => s.eNodeB_Id))
                .ForMember(d => d.ReportInterval, opt => opt.MapFrom(s => s.IntraFreqHoRprtInterval))
                .ForMember(d => d.ReportAmount, opt => opt.MapFrom(s => s.IntraFreqHoRprtInterval))
                .ForMember(d => d.MaxReportCellNum, opt => opt.MapFrom(s => s.IntraRatHoMaxRprtCell))
                .ForMember(d => d.TriggerQuantity, opt => opt.MapFrom(s => s.IntraFreqHoA3TrigQuan))
                .ForMember(d => d.ReportQuantity, opt => opt.MapFrom(s => s.IntraFreqHoA3RprtQuan));
        }
    }
}
