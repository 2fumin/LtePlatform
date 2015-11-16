using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace Lte.Parameters.Entities
{
    public class TownPreciseCoverage4GStat : Entity
    {
        public DateTime StatTime { get; set; }

        public int TownId { get; set; }

        public int TotalMrs { get; set; }

        public int ThirdNeighbors { get; set; }

        public int SecondNeighbors { get; set; }

        public int FirstNeighbors { get; set; }

        public TownPreciseCoverage4GStat() { }

        public TownPreciseCoverage4GStat(IEnumerable<PreciseCoverage4GCsv> cellExcel, int townId)
        {
            TownId = townId;
            if (!cellExcel.Any()) return;
            StatTime = cellExcel.ElementAt(0).StatTime;
            TotalMrs = cellExcel.Sum(x => x.TotalMrs);
            ThirdNeighbors = (int)cellExcel.Sum(x => x.TotalMrs * x.ThirdNeighborRate) / 100;
            SecondNeighbors = (int)cellExcel.Sum(x => x.TotalMrs * x.SecondNeighborRate) / 100;
            FirstNeighbors = (int)cellExcel.Sum(x => x.TotalMrs * x.FirstNeighborRate) / 100;
        }
    }
}
