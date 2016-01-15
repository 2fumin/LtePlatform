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
using Lte.Parameters.Entities.ExcelCsv;
using Lte.Parameters.Entities.Work;

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

        public static void MapWorkItem()
        {
            Mapper.CreateMap<WorkItemExcel, WorkItem>()
                .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Title.Split('_')[1].GetWorkItemType()))
                .ForMember(d => d.Subtype, opt => opt.MapFrom(s => s.Title.Split('_')[2].GetWorkItemSubtype()))
                .ForMember(d => d.Cause, opt => opt.MapFrom(s => s.CauseDescription.GetWorkItemCause()))
                .ForMember(d => d.State, opt => opt.MapFrom(s => s.StateDescription.GetState()));
        }

        public static void MapInterferenceMatrix()
        {
            Mapper.CreateMap<InterferenceMatrixCsv, InterferenceMatrixPci>()
                .ForMember(d => d.ENodebId, opt => opt.MapFrom(s => s.CellRelation.Split('_')[0].ConvertToInt(0)))
                .ForMember(d => d.SourcePci,
                    opt => opt.MapFrom(s => s.CellRelation.GetSplittedFields('_')[1].ConvertToShort(0)))
                .ForMember(d => d.DestPci,
                    opt => opt.MapFrom(s => s.CellRelation.GetSplittedFields('_')[2].ConvertToShort(0)))
                .ForMember(d => d.Frequency,
                    opt => opt.MapFrom(s => s.CellRelation.GetSplittedFields('_')[3].ConvertToInt(100)));
            Mapper.CreateMap<InterferenceMatrixPci, InterferenceMatrixStat>();
        }
    }
}
