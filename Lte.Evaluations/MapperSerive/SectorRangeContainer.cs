using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.MapperSerive
{
    public class SectorRangeContainer
    {
        public double West { get; set; }

        public double East { get; set; }

        public double South { get; set; }

        public double North { get; set; }

        public IEnumerable<CellIdPair> ExcludedCells { get; set; } 
    }

    public class CellIdPair
    {
        public int CellId { get; set; }

        public byte SectorId { get; set; }
    }
}
