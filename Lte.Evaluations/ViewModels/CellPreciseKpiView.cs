using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.ViewModels
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

        public CellPreciseKpiView()
        {
        }

        public CellPreciseKpiView(Cell cell, IENodebRepository repository)
        {
            cell.CloneProperties(this);
            ENodeb eNodeb = repository.FirstOrDefault(x => x.ENodebId == cell.ENodebId);
            ENodebName = eNodeb == null ? "Undefined" : eNodeb.Name;
            Indoor = cell.IsOutdoor ? "室外" : "室内";
            DownTilt = cell.ETilt + cell.MTilt;
            PreciseRate = 100;
        }

        public void UpdateKpi(IPreciseCoverage4GRepository repository, DateTime begin, DateTime end)
        {
            var query = repository.GetAll().Where(x => x.StatTime >= begin && x.StatTime < end
                                                       && x.CellId == ENodebId && x.SectorId == SectorId).ToList();
            if (query.Count > 0)
            {
                PreciseRate = 100 - (double)query.Sum(x => x.SecondNeighbors) / query.Sum(x => x.TotalMrs) * 100;
            }
        }
    }
}
