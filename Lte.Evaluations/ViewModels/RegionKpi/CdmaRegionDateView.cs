using System;
using System.Collections.Generic;

namespace Lte.Evaluations.ViewModels.RegionKpi
{
    public class CdmaRegionDateView
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<CdmaRegionStatView> StatViews { get; set; }
    }
}
