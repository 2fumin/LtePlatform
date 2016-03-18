using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.ViewModels.Switch
{
    public class CellIntraFreqHoView
    {
        public int ENodebId { get; set; }

        public int SectorId { get; set; }

        public int Hysteresis { get; set; }

        public int TimeToTrigger { get; set; }

        public int A3Offset { get; set; }
    }
}
