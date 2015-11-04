using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.ViewModels
{
    public class CdmaRegionStatDetails
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

        public CdmaRegionStatDetails(CdmaRegionStatTrend trend)
        {
            StatDates = trend.StatDates;
            RegionList = trend.RegionList;
            ImportDetails(trend.ViewList);
        }

        private void ImportDetails(List<IEnumerable<CdmaRegionStatView>> views)
        {
            this.CallSetupRates = views.Select(x => x.Select(v => v.CallSetupRate)).ToList();
            this.Cis = views.Select(x => x.Select(v => v.Ci)).ToList();
            this.ConnectionRates = views.Select(x => x.Select(v => v.ConnectionRate)).ToList();
            this.DownSwitchRates = views.Select(x => x.Select(v => v.DownSwitchRate)).ToList();
            this.Drop2GRates = views.Select(x => x.Select(v => v.Drop2GRate)).ToList();
            this.Drop3GRates = views.Select(x => x.Select(v => v.Drop3GRate)).ToList();
            this.Ecios = views.Select(x => x.Select(v => v.Ecio)).ToList();
            this.Erlang2Gs = views.Select(x => x.Select(v => v.ErlangIncludingSwitch)).ToList();
            this.Erlang3Gs = views.Select(x => x.Select(v => v.Erlang3G)).ToList();
            this.Flow3Gs = views.Select(x => x.Select(v => v.Flow)).ToList();
            this.LinkBusyRates = views.Select(x => x.Select(v => v.LinkBusyRate)).ToList();
            this.Utility2GRates = views.Select(x => x.Select(v => v.Utility2GRate)).ToList();
            this.Utility3GRates = views.Select(x => x.Select(v => v.Utility3GRate)).ToList();
        }
    }
}
