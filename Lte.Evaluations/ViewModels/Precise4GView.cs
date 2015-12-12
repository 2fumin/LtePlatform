using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Regular;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.ViewModels
{
    public class Precise4GView
    {
        public int CellId { get; set; }

        public byte SectorId { get; set; }

        public int TotalMrs { get; set; }

        public int SecondNeighbors { get; set; }

        public double FirstRate { get; set; }

        public double SecondRate { get; set; }

        public double ThirdRate { get; set; }

        public string ENodebName { get; set; } = "未导入基站";

        public int TopDates { get; set; }

        public static Precise4GView ConstructView(PreciseCoverage4G stat, IENodebRepository repository)
        {
            var view = Mapper.Map<PreciseCoverage4G, Precise4GView>(stat);
            var eNodeb = repository.GetByENodebId(stat.CellId);
            view.ENodebName = eNodeb?.Name;
            return view;
        }
    }

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
            if (cell == null) return sector;
            cell.CloneProperties(sector);
            sector.DownTilt = cell.MTilt + cell.ETilt;
            sector.BaiduLongtitute = cell.Longtitute + GeoMath.BaiduLongtituteOffset;
            sector.BaiduLattitute = cell.Lattitute + GeoMath.BaiduLattituteOffset;
            return sector;
        }
    }
}
