using AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.ViewModels.Basic
{
    public class CdmaCellView
    {
        public string BtsName { get; set; }

        public int BtsId { get; set; } = -1;

        public byte SectorId { get; set; } = 31;

        public string CellType { get; set; } = "DO";

        public int Frequency { get; set; } = 0;

        public int CellId { get; set; }

        public string Lac { get; set; }

        public short Pn { get; set; }

        public double Longtitute { get; set; }

        public double Lattitute { get; set; }

        public double BaiduLongtitute => Longtitute + GeoMath.BaiduLongtituteOffset;

        public double BaiduLattitute => Lattitute + GeoMath.BaiduLattituteOffset;

        public double Height { get; set; }

        public double DownTilt { get; set; }

        public double Azimuth { get; set; }

        public double AntennaGain { get; set; }

        public string Indoor { get; set; }
        
        public double RsPower { get; set; }

        public string FrequencyList { get; set; }

        public static CdmaCellView ConstructView(CdmaCell cell, IBtsRepository repository)
        {
            var view = Mapper.Map<CdmaCell, CdmaCellView>(cell);
            var bts = repository.GetByBtsId(cell.BtsId);
            view.BtsName = bts?.Name;
            return view;
        }
    }
}
