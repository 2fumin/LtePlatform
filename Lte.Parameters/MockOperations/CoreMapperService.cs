using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Domain.Common.Wireless;
using Lte.Parameters.Entities;

namespace Lte.Parameters.MockOperations
{
    public static class CoreMapperService
    {
        public static void MapCdmaCell()
        {
            Mapper.CreateMap<CdmaCellExcel, CdmaCell>()
                .ForMember(d => d.Frequency, opt => opt.Ignore())
                .ForMember(d => d.IsOutdoor, opt => opt.MapFrom(s => s.IsIndoor.Trim() == "否"));
        }

        public static void MapCell()
        {
            Mapper.CreateMap<CellExcel, Cell>()
                .ForMember(d => d.AntennaPorts, opt => opt.MapFrom(s => s.TransmitReceive.GetAntennaPortsConfig()))
                .ForMember(d => d.IsOutdoor, opt => opt.MapFrom(s => s.IsIndoor.Trim() == "否"));
        }

        public static void MapIndoorDistribution()
        {
            Mapper.CreateMap<IndoorDistributionExcel, IndoorDistribution>();
        }

        public static void MapENodeb()
        {
            Mapper.CreateMap<ENodebExcel, ENodeb>()
                .ForMember(d => d.IsFdd,
                    opt => opt.MapFrom(s => s.DivisionDuplex.IndexOf("FDD", StringComparison.Ordinal) >= 0))
                .ForMember(d => d.Gateway, opt => opt.MapFrom(s => s.Gateway.AddressValue))
                .ForMember(d => d.SubIp, opt => opt.MapFrom(s => s.Ip.IpByte4));
        }

        public static void MapBts()
        {
            Mapper.CreateMap<BtsExcel, CdmaBts>();
        }

        public static void MapDtItems()
        {
            Mapper.CreateMap<FileRecord2G, FileRecordCoverage2G>()
                .ForMember(d => d.Longtitute, opt => opt.MapFrom(s => s.Longtitute ?? -1))
                .ForMember(d => d.Lattitute, opt => opt.MapFrom(s => s.Lattitute ?? -1))
                .ForMember(d => d.Ecio, opt => opt.MapFrom(s => s.Ecio ?? 0))
                .ForMember(d => d.RxAgc, opt => opt.MapFrom(s => s.RxAgc ?? 0))
                .ForMember(d => d.TxPower, opt => opt.MapFrom(s => s.TxPower ?? 0));
        }
    }
}
