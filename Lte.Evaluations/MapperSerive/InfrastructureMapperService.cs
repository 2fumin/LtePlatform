using AutoMapper;
using Lte.Evaluations.ViewModels.Basic;
using Lte.Evaluations.ViewModels.Mr;
using Lte.Evaluations.ViewModels.Switch;
using Lte.Parameters.Entities.Basic;
using Lte.Parameters.Entities.Neighbor;
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

            Mapper.CreateMap<EUtranRelationZte, NeighborCellMongo>()
                .ForMember(d => d.CellId, opt => opt.MapFrom(s => s.eNodeB_Id))
                .ForMember(d => d.IsAnrCreated, opt => opt.MapFrom(s => s.isAnrCreated == 1))
                .ForMember(d => d.HandoffAllowed, opt => opt.MapFrom(s => s.isHOAllowed == 1))
                .ForMember(d => d.RemovedAllowed, opt => opt.MapFrom(s => s.isRemoveAllowed == 1))
                .ForMember(d => d.CellPriority, opt => opt.MapFrom(s => s.nCelPriority));

            Mapper.CreateMap<ExternalEUtranCellFDDZte, NeighborCellMongo>()
                .ForMember(d => d.CellId, opt => opt.MapFrom(s => s.eNodeB_Id))
                .ForMember(d => d.NeighborCellId, opt => opt.MapFrom(s => s.eNBId))
                .ForMember(d => d.NeighborSectorId, opt => opt.MapFrom(s => s.cellLocalId))
                .ForMember(d => d.NeighborPci, opt => opt.MapFrom(s => s.pci))
                .ForMember(d => d.NeighborCellName, opt => opt.MapFrom(s => s.userLabel));

            Mapper.CreateMap<EutranIntraFreqNCell, NeighborCellMongo>()
                .ForMember(d => d.CellId, opt => opt.MapFrom(s => s.eNodeB_Id))
                .ForMember(d => d.NeighborCellId, opt => opt.MapFrom(s => s.eNodeBId))
                .ForMember(d => d.NeighborSectorId, opt => opt.MapFrom(s => s.CellId))
                .ForMember(d => d.NeighborCellName, opt => opt.MapFrom(s => s.NeighbourCellName))
                .ForMember(d => d.IsAnrCreated, opt => opt.MapFrom(s => s.AnrFlag > 0))
                .ForMember(d => d.HandoffAllowed, opt => opt.MapFrom(s => s.NoHoFlag == 0))
                .ForMember(d => d.RemovedAllowed, opt => opt.MapFrom(s => s.NoRmvFlag == 0))
                .ForMember(d => d.CellPriority, opt => opt.MapFrom(s => s.CellMeasPriority));
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

            Mapper.CreateMap<UeEUtranMeasurementZte, ENodebIntraFreqHoView>()
                .ForMember(d=>d.ENodebId, opt=>opt.MapFrom(s=>s.eNodeB_Id))
                .ForMember(d=>d.ReportInterval, opt=>opt.MapFrom(s=>s.reportInterval))
                .ForMember(d => d.ReportAmount, opt => opt.MapFrom(s => s.reportAmount))
                .ForMember(d => d.MaxReportCellNum, opt => opt.MapFrom(s => s.maxReportCellNum))
                .ForMember(d => d.TriggerQuantity, opt => opt.MapFrom(s => s.triggerQuantity))
                .ForMember(d => d.ReportQuantity, opt => opt.MapFrom(s => s.reportQuantity));

            Mapper.CreateMap<IntraFreqHoGroup, CellIntraFreqHoView>()
                .ForMember(d => d.ENodebId, opt => opt.MapFrom(s => s.eNodeB_Id))
                .ForMember(d => d.Hysteresis, opt => opt.MapFrom(s => s.IntraFreqHoA3Hyst))
                .ForMember(d => d.TimeToTrigger, opt => opt.MapFrom(s => s.IntraFreqHoA3TimeToTrig))
                .ForMember(d => d.A3Offset, opt => opt.MapFrom(s => s.IntraFreqHoA3Offset));
            
            Mapper.CreateMap<UeEUtranMeasurementZte, CellIntraFreqHoView>()
                .ForMember(d => d.ENodebId, opt => opt.MapFrom(s => s.eNodeB_Id))
                .ForMember(d => d.Hysteresis, opt => opt.MapFrom(s => s.hysteresis * 2))
                .ForMember(d => d.TimeToTrigger, opt => opt.MapFrom(s => s.timeToTrigger))
                .ForMember(d => d.A3Offset, opt => opt.MapFrom(s => s.a3Offset * 2));
            
            Mapper.CreateMap<IntraRatHoComm, ENodebInterFreqHoView>()
                .ForMember(d => d.ENodebId, opt => opt.MapFrom(s => s.eNodeB_Id))
                .ForMember(d => d.InterFreqHoA4RprtQuan, opt => opt.MapFrom(s => s.InterFreqHoA4RprtQuan))
                .ForMember(d => d.InterFreqHoA4TrigQuan, opt => opt.MapFrom(s => s.InterFreqHoA4TrigQuan))
                .ForMember(d => d.InterFreqHoA1TrigQuan, opt => opt.MapFrom(s => s.InterFreqHoA1A2TrigQuan))
                .ForMember(d => d.InterFreqHoA2TrigQuan, opt => opt.MapFrom(s => s.InterFreqHoA1A2TrigQuan))
                .ForMember(d => d.A3InterFreqHoA1TrigQuan, opt => opt.MapFrom(s => s.A3InterFreqHoA1A2TrigQuan))
                .ForMember(d => d.A3InterFreqHoA2TrigQuan, opt => opt.MapFrom(s => s.A3InterFreqHoA1A2TrigQuan))
                .ForMember(d => d.InterFreqHoRprtInterval, opt => opt.MapFrom(s => s.InterFreqHoRprtInterval));

            Mapper.CreateMap<UeEUtranMeasurementZte, InterFreqEventA1>()
                .ForMember(d => d.Hysteresis, opt => opt.MapFrom(s => s.hysteresis))
                .ForMember(d => d.TimeToTrigger, opt => opt.MapFrom(s => s.timeToTrigger))
                .ForMember(d => d.ThresholdOfRsrp, opt => opt.MapFrom(s => s.thresholdOfRSRP))
                .ForMember(d => d.ThresholdOfRsrq, opt => opt.MapFrom(s => s.thresholdOfRSRQ));

            Mapper.CreateMap<UeEUtranMeasurementZte, InterFreqEventA2>()
                .ForMember(d => d.Hysteresis, opt => opt.MapFrom(s => s.hysteresis))
                .ForMember(d => d.TimeToTrigger, opt => opt.MapFrom(s => s.timeToTrigger))
                .ForMember(d => d.ThresholdOfRsrp, opt => opt.MapFrom(s => s.thresholdOfRSRP))
                .ForMember(d => d.ThresholdOfRsrq, opt => opt.MapFrom(s => s.thresholdOfRSRQ));

            Mapper.CreateMap<UeEUtranMeasurementZte, InterFreqEventA3>()
                .ForMember(d => d.Hysteresis, opt => opt.MapFrom(s => s.hysteresis))
                .ForMember(d => d.TimeToTrigger, opt => opt.MapFrom(s => s.timeToTrigger))
                .ForMember(d => d.A3Offset, opt => opt.MapFrom(s => s.a3Offset));

            Mapper.CreateMap<UeEUtranMeasurementZte, InterFreqEventA4>()
                .ForMember(d => d.Hysteresis, opt => opt.MapFrom(s => s.hysteresis))
                .ForMember(d => d.TimeToTrigger, opt => opt.MapFrom(s => s.timeToTrigger))
                .ForMember(d => d.ThresholdOfRsrp, opt => opt.MapFrom(s => s.thresholdOfRSRP))
                .ForMember(d => d.ThresholdOfRsrq, opt => opt.MapFrom(s => s.thresholdOfRSRQ));

            Mapper.CreateMap<UeEUtranMeasurementZte, InterFreqEventA5>()
                .ForMember(d => d.Hysteresis, opt => opt.MapFrom(s => s.hysteresis))
                .ForMember(d => d.TimeToTrigger, opt => opt.MapFrom(s => s.timeToTrigger))
                .ForMember(d => d.ThresholdOfRsrp, opt => opt.MapFrom(s => s.thresholdOfRSRP))
                .ForMember(d => d.ThresholdOfRsrq, opt => opt.MapFrom(s => s.thresholdOfRSRQ))
                .ForMember(d => d.Threshold2OfRsrp, opt => opt.MapFrom(s => s.a5Threshold2OfRSRP))
                .ForMember(d => d.Threshold2OfRsrq, opt => opt.MapFrom(s => s.a5Threshold2OfRSRQ));

            Mapper.CreateMap<InterFreqHoGroup, InterFreqEventA1>()
                .ForMember(d => d.Hysteresis, opt => opt.MapFrom(s => s.InterFreqHoA1A2Hyst))
                .ForMember(d => d.TimeToTrigger, opt => opt.MapFrom(s => s.InterFreqHoA1A2TimeToTrig));

            Mapper.CreateMap<InterFreqHoGroup, InterFreqEventA2>()
                .ForMember(d => d.Hysteresis, opt => opt.MapFrom(s => s.InterFreqHoA1A2Hyst))
                .ForMember(d => d.TimeToTrigger, opt => opt.MapFrom(s => s.InterFreqHoA1A2TimeToTrig));

            Mapper.CreateMap<InterFreqHoGroup, InterFreqEventA3>()
                .ForMember(d => d.A3Offset, opt => opt.MapFrom(s => s.InterFreqHoA3Offset));

            Mapper.CreateMap<InterFreqHoGroup, InterFreqEventA4>()
                .ForMember(d => d.Hysteresis, opt => opt.MapFrom(s => s.InterFreqHoA4Hyst))
                .ForMember(d => d.TimeToTrigger, opt => opt.MapFrom(s => s.InterFreqHoA4TimeToTrig))
                .ForMember(d => d.ThresholdOfRsrp, opt => opt.MapFrom(s => s.InterFreqHoA4ThdRsrp))
                .ForMember(d => d.ThresholdOfRsrq, opt => opt.MapFrom(s => s.InterFreqHoA4ThdRsrq));

            Mapper.CreateMap<InterFreqHoGroup, InterFreqEventA5>()
                .ForMember(d => d.Hysteresis, opt => opt.MapFrom(s => s.InterFreqHoA4Hyst))
                .ForMember(d => d.TimeToTrigger, opt => opt.MapFrom(s => s.InterFreqHoA4TimeToTrig))
                .ForMember(d => d.ThresholdOfRsrp, opt => opt.MapFrom(s => s.InterFreqHoA4ThdRsrp))
                .ForMember(d => d.ThresholdOfRsrq, opt => opt.MapFrom(s => s.InterFreqHoA4ThdRsrq))
                .ForMember(d => d.Threshold2OfRsrp, opt => opt.MapFrom(s => s.InterFreqHoA5Thd1Rsrp))
                .ForMember(d => d.Threshold2OfRsrq, opt => opt.MapFrom(s => s.InterFreqHoA5Thd1Rsrq));
            
        }
    }
}
