using System;
using System.Linq;
using AutoMapper;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities.Basic;

namespace Lte.Evaluations.ViewModels.Precise
{
    public class CellPreciseKpiView
    {
        public string ENodebName { get; private set; }

        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public int Frequency { get; set; }

        public double RsPower { get; set; }

        public double Height { get; set; }

        public double Azimuth { get; set; }

        public string Indoor { get; set; }

        public double DownTilt { get; set; }

        public double AntennaGain { get; set; }

        public double PreciseRate { get; set; }

        public static CellPreciseKpiView ConstructView(Cell cell, IENodebRepository repository)
        {
            var view = Mapper.Map<Cell, CellPreciseKpiView>(cell);
            var eNodeb = repository.GetByENodebId(cell.ENodebId);
            view.ENodebName = eNodeb?.Name;
            return view;
        }

        public void UpdateKpi(IPreciseCoverage4GRepository repository, DateTime begin, DateTime end)
        {
            var query = repository.GetAll().Where(x => x.StatTime >= begin && x.StatTime < end
                                                       && x.CellId == ENodebId && x.SectorId == SectorId).ToList();
            if (query.Count > 0)
            {
                var sum = query.Sum(x => x.TotalMrs);
                PreciseRate = sum == 0 ? 100 : 100 - (double)query.Sum(x => x.SecondNeighbors) / sum * 100;
            }
        }
    }
}
