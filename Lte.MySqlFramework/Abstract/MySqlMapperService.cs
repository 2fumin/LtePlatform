using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Regular;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Abstract
{
    public static class MySqlMapperService
    {
        public static void MapFlow()
        {
            Mapper.CreateMap<FlowHuaweiCsv, FlowHuawei>()
                .ForMember(d => d.ENodebId,
                    opt =>
                        opt.MapFrom(s => s.CellInfo.GetSplittedFields(", ")[3].GetSplittedFields('=')[1].ConvertToInt(0)))
                .ForMember(d => d.LocalCellId,
                    opt =>
                        opt.MapFrom(
                            s => s.CellInfo.GetSplittedFields(", ")[1].GetSplittedFields('=')[1].ConvertToByte(0)))
                .ForMember(d => d.PdcpDownlinkFlow,
                    opt => opt.MapFrom(s => (double) s.PdcpDownlinkFlowInByte/(1024*1024)))
                .ForMember(d => d.PdcpUplinkFlow, opt => opt.MapFrom(s => (double) s.PdcpUplinkFlowInByte/(1024*1024)))
                .ForMember(d => d.DownlinkDuration, opt => opt.MapFrom(s => (double) s.DownlinkDurationInMs/1000))
                .ForMember(d => d.UplinkDuration, opt => opt.MapFrom(s => (double) s.UplinkDurationInMs/1000))
                .ForMember(d => d.UplinkDciCceRate, opt => opt.MapFrom(s => (double) s.UplinkDciCces/s.TotalCces))
                .ForMember(d => d.DownlinkDciCceRate, opt => opt.MapFrom(s => (double) s.DownlinkDciCces/s.TotalCces))
                .ForMember(d => d.LastTtiUplinkFlow,
                    opt => opt.MapFrom(s => (double) s.LastTtiUplinkFlowInByte/(1024*1024)))
                .ForMember(d => d.ButLastUplinkDuration,
                    opt => opt.MapFrom(s => (double) s.ButLastUplinkDurationInMs/1000))
                .ForMember(d => d.LastTtiDownlinkFlow,
                    opt => opt.MapFrom(s => (double) s.LastTtiDownlinkFlowInByte/(1024*1024)))
                .ForMember(d => d.ButLastDownlinkDuration,
                    opt => opt.MapFrom(s => (double) s.ButLastDownlinkDurationInMs/1000));
        }
    }
}
