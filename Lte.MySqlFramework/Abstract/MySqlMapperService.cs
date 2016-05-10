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
                .ForMember(d => d.PagingUsers, opt => opt.MapFrom(s => s.PagingUsersString.ConvertToInt(0)))
                .ForMember(d => d.UplinkDciCceRate,
                    opt => opt.MapFrom(s => s.TotalCces == 0 ? 0 : (double) s.UplinkDciCces/s.TotalCces))
                .ForMember(d => d.DownlinkDciCceRate,
                    opt => opt.MapFrom(s => s.TotalCces == 0 ? 0 : (double) s.DownlinkDciCces/s.TotalCces))
                .ForMember(d => d.PucchPrbs, opt => opt.MapFrom(s => s.PucchPrbsString.ConvertToDouble(0)))
                .ForMember(d => d.LastTtiUplinkFlow,
                    opt => opt.MapFrom(s => (double) s.LastTtiUplinkFlowInByte/(1024*1024)))
                .ForMember(d => d.ButLastUplinkDuration,
                    opt => opt.MapFrom(s => (double) s.ButLastUplinkDurationInMs/1000))
                .ForMember(d => d.LastTtiDownlinkFlow,
                    opt => opt.MapFrom(s => (double) s.LastTtiDownlinkFlowInByte/(1024*1024)))
                .ForMember(d => d.ButLastDownlinkDuration,
                    opt => opt.MapFrom(s => (double) s.ButLastDownlinkDurationInMs/1000));

            Mapper.CreateMap<FlowZteCsv, FlowZte>()
                .ForMember(d => d.UplindPdcpFlow, opt => opt.MapFrom(s => s.UplindPdcpFlowInMByte*8))
                .ForMember(d => d.DownlinkPdcpFlow, opt => opt.MapFrom(s => s.DownlinkPdcpFlowInMByte*8))
                .ForMember(d => d.Qci8UplinkIpThroughput,
                    opt =>
                        opt.MapFrom(
                            s =>
                                s.Qci8UplinkIpThroughputHigh.ConvertToInt(0) +
                                s.Qci8UplinkIpThroughputLow.Replace(",", "").ConvertToDouble(0)/1024))
                .ForMember(d => d.Qci8UplinkIpDuration,
                    opt => opt.MapFrom(s => s.Qci8UplinkIpThroughputDuration.ConvertToDouble(0)/1000))
                .ForMember(d => d.Qci9UplinkIpThroughput,
                    opt =>
                        opt.MapFrom(
                            s =>
                                s.Qci9UplinkIpThroughputHigh.ConvertToInt(0) +
                                s.Qci9UplinkIpThroughputLow.Replace(",", "").ConvertToDouble(0)/1024))
                .ForMember(d => d.Qci9UplinkIpDuration,
                    opt => opt.MapFrom(s => s.Qci9UplinkIpThroughputDuration.ConvertToDouble(0)/1000))
                .ForMember(d => d.Qci8DownlinkIpThroughput,
                    opt =>
                        opt.MapFrom(
                            s =>
                                s.Qci8DownlinkIpThroughputHigh.ConvertToInt(0) +
                                s.Qci8DownlinkIpThroughputLow.Replace(",", "").ConvertToDouble(0)/1024))
                .ForMember(d => d.Qci8DownlinkIpDuration,
                    opt => opt.MapFrom(s => s.Qci8DownlinkIpThroughputDuration.ConvertToDouble(0)/1000))
                .ForMember(d => d.Qci9DownlinkIpThroughput,
                    opt =>
                        opt.MapFrom(
                            s =>
                                s.Qci9DownlinkIpThroughputHigh.ConvertToInt(0) +
                                s.Qci9DownlinkIpThroughputLow.Replace(",", "").ConvertToDouble(0)/1024))
                .ForMember(d => d.Qci9DownlinkIpDuration,
                    opt => opt.MapFrom(s => s.Qci9DownlinkIpThroughputDuration.ConvertToDouble(0)/1000));
        }
    }
}
