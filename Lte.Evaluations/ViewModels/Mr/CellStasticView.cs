using System.Collections.Generic;
using System.Linq;
using Lte.Domain.Common.Wireless;

namespace Lte.Evaluations.ViewModels.Mr
{
    public class CellStasticView
    {
        public double Mod3Count { get; set; }

        public double WeakCoverCount { get; set; }

        public double Mod6Count { get; set; }

        public double OverCoverCount { get; set; }

        public double PreciseCount { get; set; }

        public double MrCount { get; set; }

        public CellStasticView(List<ICellStastic> dateSpanStats)
        {
            Mod3Count = dateSpanStats.Average(x => x.Mod3Count);
            Mod6Count = dateSpanStats.Average(x => x.Mod6Count);
            MrCount = dateSpanStats.Average(x => x.MrCount);
            OverCoverCount = dateSpanStats.Average(x => x.OverCoverCount);
            PreciseCount = dateSpanStats.Average(x => x.PreciseCount);
            WeakCoverCount = dateSpanStats.Average(x => x.WeakCoverCount);
        }
    }
}