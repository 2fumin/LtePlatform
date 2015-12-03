using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.MapperSerive
{
    public static class ParametersDumpMapperService
    {
        public static void MapFromENodebContainerService()
        {
            Mapper.CreateMap<ENodebExcel, ENodeb>()
                .ForMember(d => d.IsFdd,
                    opt => opt.MapFrom(s => s.DivisionDuplex.IndexOf("FDD", StringComparison.Ordinal) >= 0))
                .ForMember(d => d.Gateway, opt => opt.MapFrom(s => s.Gateway.AddressValue))
                .ForMember(d => d.SubIp, opt => opt.MapFrom(s => s.Ip.IpByte4));
            Mapper.CreateMap<ENodebExcelWithTownIdContainer, ENodebWithTownIdContainer>()
                .ForMember(d => d.ENodeb, opt => opt.MapFrom(s => Mapper.Map<ENodebExcel, ENodeb>(s.ENodebExcel)));
        }
    }
}
