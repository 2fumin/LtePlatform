using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Regular;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.MapperSerive
{
    public static class KpiMapperService
    {
        public static void MapCdmaRegionStat()
        {
            Mapper.CreateMap<CdmaRegionStat, CdmaRegionStatView>();
        }

        public static void MapDistrictPrecise()
        {
            Mapper.CreateMap<TownPreciseView, DistrictPreciseView>();
        }

        public static void MapTownPrecise()
        {
            Mapper.CreateMap<TownPreciseCoverage4GStat, TownPreciseView>();
        }

        public static void MapPreciseStat()
        {
            Mapper.CreateMap<PreciseCoverage4G, Precise4GView>();
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
    }
}
