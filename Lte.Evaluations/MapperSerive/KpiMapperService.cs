using AutoMapper;
using Lte.Domain.Common.Wireless;
using Lte.Evaluations.MapperSerive.Kpi;
using Lte.Evaluations.ViewModels;
using Lte.Evaluations.ViewModels.Kpi;
using Lte.Evaluations.ViewModels.Precise;
using Lte.Evaluations.ViewModels.RegionKpi;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;
using Lte.Parameters.Entities.Work;

namespace Lte.Evaluations.MapperSerive
{
    public static class KpiMapperService
    {
        public static void MapCdmaRegionStat()
        {
            Mapper.CreateMap<CdmaRegionStat, CdmaRegionStatView>();
        }

        public static void MapTownPrecise()
        {
            Mapper.CreateMap<TownPreciseCoverage4GStat, TownPreciseView>();
            Mapper.CreateMap<TownPreciseView, TownPreciseCoverage4GStat>();
        }

        public static void MapPreciseStat()
        {
            Mapper.CreateMap<PreciseCoverage4G, Precise4GView>();
            Mapper.CreateMap<Precise4GView, Precise4GSector>();
        }

        public static void MapCellPrecise()
        {
            Mapper.CreateMap<Cell, CellPreciseKpiView>()
                .ForMember(d => d.Indoor, opt => opt.MapFrom(s => s.IsOutdoor ? "室外" : "室内"))
                .ForMember(d => d.DownTilt, opt => opt.MapFrom(s => s.ETilt + s.MTilt))
                .ForMember(d => d.PreciseRate, opt => opt.MapFrom(s => 100.0));
        }

        public static void MapAlarmStat()
        {
            Mapper.CreateMap<AlarmStat, AlarmView>()
                .ForMember(d => d.Position,
                    opt =>
                        opt.MapFrom(
                            s =>
                                s.SectorId == 255 || s.AlarmCategory == AlarmCategory.Huawei
                                    ? "基站级"
                                    : "Cell-" + s.SectorId))
                .ForMember(d => d.Duration, opt => opt.MapFrom(s => (s.RecoverTime - s.HappenTime).TotalMinutes))
                .ForMember(d => d.AlarmLevelDescription,
                    opt => opt.MapFrom(s => s.AlarmLevel.GetAlarmLevelDescription()))
                .ForMember(d => d.AlarmCategoryDescription,
                    opt => opt.MapFrom(s => s.AlarmCategory.GetAlarmCategoryDescription()))
                .ForMember(d => d.AlarmTypeDescription, opt => opt.MapFrom(s => s.AlarmType.GetAlarmTypeDescription()));
        }

        public static void MapTopKpi()
        {
            Mapper.CreateMap<TopDrop2GCell, TopDrop2GCellView>();
            Mapper.CreateMap<TopCellContainer<TopDrop2GCell>, TopDrop2GCellViewContainer>()
                .ForMember(d => d.TopDrop2GCellView,
                    opt => opt.MapFrom(s => Mapper.Map<TopDrop2GCell, TopDrop2GCellView>(s.TopCell)));
            Mapper.CreateMap<TopConnection3GCell, TopConnection3GCellView>();
            Mapper.CreateMap<TopCellContainer<TopConnection3GCell>, TopConnection3GCellViewContainer>()
                .ForMember(d => d.TopConnection3GCellView,
                    opt => opt.MapFrom(s => Mapper.Map<TopConnection3GCell, TopConnection3GCellView>(s.TopCell)));
        }

        public static void MapTopDrop2GTrend()
        {
            Mapper.CreateMap<TopDrop2GTrend, TopDrop2GTrendView>();
            Mapper.CreateMap<TopCellContainer<TopDrop2GTrend>, TopDrop2GTrendViewContainer>()
                .ForMember(d => d.TopDrop2GTrendView,
                    opt => opt.MapFrom(s => Mapper.Map<TopDrop2GTrend, TopDrop2GTrendView>(s.TopCell)))
                .ForMember(d => d.CellName, opt => opt.MapFrom(s => s.CdmaName + "-" + s.TopCell.SectorId))
                .ForMember(d => d.ENodebName, opt => opt.MapFrom(s => s.LteName));
        }

        public static void MapWorkItem()
        {
            Mapper.CreateMap<WorkItem, WorkItemView>()
                .ForMember(d => d.WorkItemCause, opt => opt.MapFrom(s => s.Cause.GetWorkItemCauseDescription()))
                .ForMember(d => d.WorkItemState, opt => opt.MapFrom(s => s.State.GetWorkItemStateDescription()))
                .ForMember(d => d.WorkItemType, opt => opt.MapFrom(s => s.Type.GetWorkItemTypeDescription()))
                .ForMember(d => d.WorkItemSubType, opt => opt.MapFrom(s => s.Subtype.GetWorkItemSubtypeDescription()));
        }
    }
}
