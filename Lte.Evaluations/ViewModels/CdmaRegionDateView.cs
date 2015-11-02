using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.ViewModels
{
    public class CdmaRegionDateView
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<CdmaRegionStatView> StatViews { get; set; }
    }
}
