using System.Collections.Generic;

namespace Lte.Evaluations.ViewModels.RegionKpi
{
    public class CdmaRegionDateView
    {
        public string StatDate { get; set; }

        public IEnumerable<CdmaRegionStatView> StatViews { get; set; }
    }
}
