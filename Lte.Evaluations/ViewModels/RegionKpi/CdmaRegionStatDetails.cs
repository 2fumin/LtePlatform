using System.Collections.Generic;
using System.Linq;

namespace Lte.Evaluations.ViewModels.RegionKpi
{
    public class CdmaRegionStatDetails
    {
        public IEnumerable<string> StatDates { get; set; }

        public List<string> RegionList { get; set; }

        public Dictionary<string, List<IEnumerable<double>>> KpiDetails { get; } =
            new Dictionary<string, List<IEnumerable<double>>>();

        public CdmaRegionStatDetails(CdmaRegionStatTrend trend)
        {
            StatDates = trend.StatDates;
            RegionList = trend.RegionList;
            ImportDetails(trend.ViewList);
        }

        public static readonly List<string> KpiOptions = new List<string>
        {
            "2G呼建(%)",
            "C/I优良率(%)",
            "3G连接(%)",
            "3G切2G流量比(MB)",
            "掉话率(%)",
            "掉线率(%)",
            "Ec/Io优良率(%)",
            "2G全天话务量(Erl)",
            "3G全天话务量(Erl)",
            "全天流量(GB)",
            "反向链路繁忙率(%)",
            "2G利用率(%)",
            "3G利用率(%)"
        };

        private void ImportDetails(List<IEnumerable<CdmaRegionStatView>> views)
        {
            KpiDetails.Add(KpiOptions[0], views.Select(x => x.Select(v => v.CallSetupRate * 100)).ToList());
            KpiDetails.Add(KpiOptions[1], views.Select(x => x.Select(v => v.Ci * 100)).ToList());
            KpiDetails.Add(KpiOptions[2], views.Select(x => x.Select(v => v.ConnectionRate * 100)).ToList());
            KpiDetails.Add(KpiOptions[3], views.Select(x => x.Select(v => v.DownSwitchRate * 100)).ToList());
            KpiDetails.Add(KpiOptions[4], views.Select(x => x.Select(v => v.Drop2GRate * 100)).ToList());
            KpiDetails.Add(KpiOptions[5], views.Select(x => x.Select(v => v.Drop3GRate * 100)).ToList());
            KpiDetails.Add(KpiOptions[6], views.Select(x => x.Select(v => v.Ecio * 100)).ToList());
            KpiDetails.Add(KpiOptions[7], views.Select(x => x.Select(v => v.ErlangIncludingSwitch)).ToList());
            KpiDetails.Add(KpiOptions[8], views.Select(x => x.Select(v => v.Erlang3G)).ToList());
            KpiDetails.Add(KpiOptions[9], views.Select(x => x.Select(v => v.Flow / 1024)).ToList());
            KpiDetails.Add(KpiOptions[10], views.Select(x => x.Select(v => v.LinkBusyRate * 100)).ToList());
            KpiDetails.Add(KpiOptions[11], views.Select(x => x.Select(v => v.Utility2GRate * 100)).ToList());
            KpiDetails.Add(KpiOptions[12], views.Select(x => x.Select(v => v.Utility3GRate)).ToList());
        }
    }
}
