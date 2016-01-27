using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.DataService
{
    public class InterferenceMatrixView
    {
        public short DestPci { get; set; }

        public int DestENodebId { get; set; }

        public byte DestSectorId { get; set; }

        public string NeighborCellName { get; set; }

        public double Mod3Interferences { get; set; }

        public double Mod6Interferences { get; set; }

        public double OverInterferences6Db { get; set; }

        public double OverInterferences10Db { get; set; }

        public double InterferenceLevel { get; set; }
    }
}
