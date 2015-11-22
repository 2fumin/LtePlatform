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
    public class CellView
    {
        public string ENodebName { get; private set; }

        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public int Frequency { get; set; }

        public byte BandClass { get; set; }

        public short Pci { get; set; }

        public short Prach { get; set; }

        public double RsPower { get; set; }

        public int Tac { get; set; }

        public double Height { get; set; }

        public double Azimuth { get; set; }

        public string Indoor { get; set; }

        public double DownTilt { get; set; }

        public double AntennaGain { get; set; }

        public CellView() { }

        public CellView(Cell cell, IENodebRepository repository)
        {
            cell.CloneProperties(this);
            var eNodeb = repository.FirstOrDefault(x => x.ENodebId == cell.ENodebId);
            ENodebName = eNodeb == null ? "Undefined" : eNodeb.Name;
            Indoor = cell.IsOutdoor ? "室外" : "室内";
            DownTilt = cell.ETilt + cell.MTilt;
        }
    }
}
