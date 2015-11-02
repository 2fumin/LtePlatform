using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.ViewModels
{
    public class CdmaRegionStatTrend
    {
        public IEnumerable<DateTime> StatDates { get; set; }

        public List<string> RegionList { get; set; } 

        public IEnumerable<List<double>> Erlang2Gs { get; set; }

        public IEnumerable<List<double>> Drop2GRates { get; set; }

        public IEnumerable<List<double>> CallSetupRates { get; set; }

        public IEnumerable<List<double>> Ecios { get; set; }

        public IEnumerable<List<double>> Utility2GRates { get; set; }

        public IEnumerable<List<double>> Flow3Gs { get; set; }

        public IEnumerable<List<double>> Erlang3Gs { get; set; }

        public IEnumerable<List<double>> Drop3GRates { get; set; }

        public IEnumerable<List<double>> ConnectionRates { get; set; }

        public IEnumerable<List<double>> Cis { get; set; }

        public IEnumerable<List<double>> LinkBusyRates { get; set; }

        public IEnumerable<List<double>> DownSwitchRates { get; set; }

        public IEnumerable<List<double>> Utility3GRates { get; set; }
    }
}
