using System;
using System.Collections.Generic;

namespace Lte.Evaluations.ViewModels.RegionKpi
{
    public class PreciseRegionDateView
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictPreciseView> DistrictPreciseViews { get; set; } 

        public IEnumerable<TownPreciseView> TownPreciseViews { get; set; } 
    }
}
