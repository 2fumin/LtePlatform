using AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Regular;
using Lte.Parameters.Abstract;

namespace Lte.Evaluations.ViewModels.Precise
{
    public class Precise4GSector : Precise4GView
    {
        public double Height { get; set; }

        public double Azimuth { get; set; }

        public double DownTilt { get; set; }

        public double BaiduLongtitute { get; set; }

        public double BaiduLattitute { get; set; }

        public short Pci { get; set; }

        public double RsPower { get; set; }

        public static Precise4GSector ConstructSector(Precise4GView view, ICellRepository repository)
        {
            var sector = Mapper.Map<Precise4GView, Precise4GSector>(view);
            var cell = repository.GetBySectorId(view.CellId, view.SectorId);
            if (cell == null)
            {
                sector.Height = -1;
            }
            else
            {
                cell.CloneProperties(sector);
                sector.DownTilt = cell.MTilt + cell.ETilt;
                sector.BaiduLongtitute = cell.Longtitute + GeoMath.BaiduLongtituteOffset;
                sector.BaiduLattitute = cell.Lattitute + GeoMath.BaiduLattituteOffset;
            }
            return sector;
        }
    }
}