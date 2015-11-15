using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Lte.Domain.Regular;

namespace Lte.Parameters.Entities
{
    public class PreciseCoverage4G : Entity
    {
        public DateTime StatTime { get; set; }

        public string DateString => StatTime.ToShortDateString();

        public int CellId { get; set; }

        public byte SectorId { get; set; }

        public int TotalMrs { get; set; }

        public int ThirdNeighbors { get; set; }

        public int SecondNeighbors { get; set; }

        public int FirstNeighbors { get; set; }

        public double FirstRate => 100 * (double)FirstNeighbors / TotalMrs;

        public double SecondRate => 100 * (double)SecondNeighbors / TotalMrs;

        public double ThirdRate => 100 * (double)ThirdNeighbors / TotalMrs;

        public PreciseCoverage4G() { }

        public PreciseCoverage4G(PreciseCoverage4GCsv info)
        {
            info.CloneProperties(this);
            ThirdNeighbors = (int)(TotalMrs * info.ThirdNeighborRate) / 100;
            SecondNeighbors = (int)(TotalMrs * info.SecondNeighborRate) / 100;
            FirstNeighbors = (int)(TotalMrs * info.FirstNeighborRate) / 100;
        }
    }
}
