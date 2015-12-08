using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Domain.Regular;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.MapperSerive
{
    public static class ParametersDumpMapperService
    {
        public static void MapFromENodebContainerService()
        {
            Mapper.CreateMap<ENodebExcelWithTownIdContainer, ENodebWithTownIdContainer>()
                .ForMember(d => d.ENodeb, opt => opt.MapFrom(s => Mapper.Map<ENodebExcel, ENodeb>(s.ENodebExcel)));
        }

        public static void MapFromBtsContainerService()
        {
            Mapper.CreateMap<BtsExcelWithTownIdContainer, BtsWithTownIdContainer>()
                .ForMember(d => d.CdmaBts, opt => opt.MapFrom(s => Mapper.Map<BtsExcel, CdmaBts>(s.BtsExcel)));
        }

        public static void MapENodebBtsIdService()
        {
            Mapper.CreateMap<CellExcel, ENodebBtsIdPair>()
                .ForMember(d => d.BtsId, opt => opt.MapFrom(s =>
                    s.ShareCdmaInfo.Split('_').Length > 2 ? s.ShareCdmaInfo.Split('_')[1].ConvertToInt(-1) : -1));
        }
    }
}
