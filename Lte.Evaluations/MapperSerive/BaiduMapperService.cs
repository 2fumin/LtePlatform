using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Evaluations.ViewModels;

namespace Lte.Evaluations.MapperSerive
{
    public static class BaiduMapperService
    {
        public static void MapCellView()
        {
            Mapper.CreateMap<CellView, SectorView>()
                .ForMember(d => d.CellName, opt => opt.MapFrom(s => s.ENodebName + "-" + s.SectorId))
                .ForMember(d => d.OtherInfos,
                    opt =>
                        opt.MapFrom(
                            s =>
                                "PCI: " + s.Pci + "; PRACH: " + s.Prach + "; RS Power(dBm): " + s.RsPower + "; TAC: " +
                                s.Tac));
        }

        public static void MapCdmaCellView()
        {
            Mapper.CreateMap<CdmaCellView, SectorView>()
                .ForMember(d => d.CellName, opt => opt.MapFrom(s => s.BtsName + "-" + s.SectorId))
                .ForMember(d => d.OtherInfos,
                    opt =>
                        opt.MapFrom(
                            s =>
                                "Cell Type: " + s.CellType + "; Cell ID: " + s.CellId + "; LAC: " + s.Lac + "; PN: " +
                                s.Pn + "; Frequency List: " + s.FrequencyList));
        }
    }
}
