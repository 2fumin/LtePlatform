using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.ViewModels
{
    public class PreciseRegionDateView
    {
        public string StatDate { get; set; }

        public IEnumerable<DistrictPreciseView> DistrictPreciseViews { get; set; } 

        public IEnumerable<TownPreciseView> TownPreciseViews { get; set; } 
    }
}
