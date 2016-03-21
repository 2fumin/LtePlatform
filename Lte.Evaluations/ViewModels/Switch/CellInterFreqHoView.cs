using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.ViewModels.Switch
{
    public class CellInterFreqHoView
    {
        public int ENodebId { get; set; }

        public int SectorId { get; set; }

        public InterFreqEventA1 InterFreqEventA1 { get; set; }

        public InterFreqEventA2 InterFreqEventA2 { get; set; }

        public InterFreqEventA3 InterFreqEventA3 { get; set; }

        public InterFreqEventA4 InterFreqEventA4 { get; set; }

        public InterFreqEventA5 InterFreqEventA5 { get; set; }
    }

    public class NeighborInterFreqHoConfig
    {
        public int Earfcn { get; set; }

        public int InterFreqHoEventType { get; set; }
    }
}
