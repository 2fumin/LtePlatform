using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Entities;

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
                                s.Tac + "; ENodebId: " + s.ENodebId));
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
                                s.Pn + "; Frequency List: " + s.FrequencyList + "; BtsId: " + s.BtsId));
        }

        public static void MapDtViews()
        {
            Mapper.CreateMap<RasterInfo, RasterInfoView>()
                .ForMember(d => d.RasterNum, opt => opt.MapFrom(s => s.RasterNum ?? -1))
                .ForMember(d => d.CsvFilesName2Gs,
                    opt =>
                        opt.MapFrom(
                            s =>
                                string.IsNullOrEmpty(s.CsvFilesName2G)
                                    ? new List<string>()
                                    : s.CsvFilesName2G.Split(';').ToList()))
                .ForMember(d => d.CsvFilesName3Gs,
                    opt =>
                        opt.MapFrom(
                            s =>
                                string.IsNullOrEmpty(s.CsvFilesName3G)
                                    ? new List<string>()
                                    : s.CsvFilesName3G.Split(';').ToList()))
                .ForMember(d => d.CsvFilesName4Gs,
                    opt =>
                        opt.MapFrom(
                            s =>
                                string.IsNullOrEmpty(s.CsvFilesName4G)
                                    ? new List<string>()
                                    : s.CsvFilesName4G.Split(';').ToList()));
        }
    }
}
