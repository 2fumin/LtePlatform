using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.ViewModels
{
    public class CdmaRegionStatTrend
    {
        public IEnumerable<string> StatDates { get; set; }

        public List<string> RegionList { get; set; } 

        public List<IEnumerable<double>> Erlang2Gs { get; set; }

        public List<IEnumerable<double>> Drop2GRates { get; set; }

        public List<IEnumerable<double>> CallSetupRates { get; set; }

        public List<IEnumerable<double>> Ecios { get; set; }

        public List<IEnumerable<double>> Utility2GRates { get; set; }

        public List<IEnumerable<double>> Flow3Gs { get; set; }

        public List<IEnumerable<double>> Erlang3Gs { get; set; }

        public List<IEnumerable<double>> Drop3GRates { get; set; }

        public List<IEnumerable<double>> ConnectionRates { get; set; }

        public List<IEnumerable<double>> Cis { get; set; }

        public List<IEnumerable<double>> LinkBusyRates { get; set; }

        public List<IEnumerable<double>> DownSwitchRates { get; set; }

        public List<IEnumerable<double>> Utility3GRates { get; set; }
    }
}
