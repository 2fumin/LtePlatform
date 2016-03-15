using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.ViewModels.Basic
{
    public class DistrictStat
    {
        public string District { get; set; }

        public int TotalLteENodebs { get; set; }

        public int TotalLteCells { get; set; }

        public int TotalCdmaBts { get; set; }

        public int TotalCdmaCells { get; set; }
    }

    public class TownStat
    {
        public string Town { get; set; }

        public int TotalLteENodebs { get; set; }

        public int TotalLteCells { get; set; }

        public int TotalCdmaBts { get; set; }

        public int TotalCdmaCells { get; set; }
    }
}
