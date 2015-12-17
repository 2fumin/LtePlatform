using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.UI.Inputs;
using AutoMapper;
using Lte.Parameters.Entities;
using Lte.Domain.Common;
using Lte.Domain.Regular;

namespace Lte.Parameters.MockOperations
{
    public static class StatMapperService
    {
        public static void MapCdmaRegionStat()
        {
            Mapper.CreateMap<CdmaRegionStatExcel, CdmaRegionStat>();
        }

        public static void MapPreciseCoverage()
        {
            Mapper.CreateMap<PreciseCoverage4GCsv, PreciseCoverage4G>()
                .ForMember(d => d.ThirdNeighbors, opt => opt.MapFrom(s => (int) (s.TotalMrs*s.ThirdNeighborRate)/100))
                .ForMember(d => d.SecondNeighbors, opt => opt.MapFrom(s => (int) (s.TotalMrs*s.SecondNeighborRate)/100))
                .ForMember(d => d.FirstNeighbors, opt => opt.MapFrom(s => (int) (s.TotalMrs*s.FirstNeighborRate)/100));
            Mapper.CreateMap<PreciseCoverage4G, TownPreciseCoverage4GStat>();
        }

        public static void MapTopConnection3G()
        {
            Mapper.CreateMap<TopConnection3GCellExcel, TopConnection3GCell>()
                .ForMember(d => d.StatTime, opt => opt.MapFrom(s => s.StatDate.AddHours(s.StatHour)))
                .ForMember(d => d.CellId,
                    opt => opt.MapFrom(s => s.CellName.GetSubStringInFirstPairOfChars('[', ']').ConvertToInt(1)));
        }

        public static void MapTopDrop2G()
        {
            Mapper.CreateMap<TopDrop2GCellExcel, TopDrop2GCell>()
                .ForMember(d => d.StatTime, opt => opt.MapFrom(s => s.StatDate.AddHours(s.StatHour)))
                .ForMember(d => d.CellId,
                    opt => opt.MapFrom(s => s.CellName.GetSubStringInFirstPairOfChars('[', ']').ConvertToInt(1)));
        }
    }
}
