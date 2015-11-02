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

        public IEnumerable<double> Erlang2Gs { get; set; }

        public IEnumerable<double> Drop2GRates { get; set; }

        public IEnumerable<double> CallSetupRates { get; set; }

        public IEnumerable<double> Ecios { get; set; }

        public IEnumerable<double> Utility2GRates { get; set; }

        public IEnumerable<double> Flow3Gs { get; set; }

        public IEnumerable<double> Erlang3Gs { get; set; }

        public IEnumerable<double> Drop3GRates { get; set; }

        public IEnumerable<double> ConnectionRates { get; set; }

        public IEnumerable<double> Cis { get; set; }

        public IEnumerable<double> LinkBusyRates { get; set; }

        public IEnumerable<double> DownSwitchRates { get; set; }

        public IEnumerable<double> Utility3GRates { get; set; }
    }
}
