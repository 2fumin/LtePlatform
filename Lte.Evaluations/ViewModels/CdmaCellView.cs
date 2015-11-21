using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Entities;
using Lte.Parameters.Abstract;
using Lte.Domain.Regular;

namespace Lte.Evaluations.ViewModels
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

        public double Height { get; set; }

        public double DownTilt { get; set; }

        public double Azimuth { get; set; }

        public double AntennaGain { get; set; }

        public string Indoor { get; set; }
        
        public double RsPower { get; set; }

        public string FrequencyList { get; set; }

        public CdmaCellView() { }

        public CdmaCellView(CdmaCell cell, IBtsRepository repository)
        {
            cell.CloneProperties(this);
            CdmaBts bts = repository.FirstOrDefault(x => x.BtsId == cell.BtsId);
            BtsName = bts == null ? "Undefined" : bts.Name;
            Indoor = cell.IsOutdoor ? "室外" : "室内";
            DownTilt = cell.ETilt + cell.MTilt;
        }
    }
}
