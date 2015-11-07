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

        public Dictionary<string, List<IEnumerable<double>>> KpiDetails { get; set; }

        public CdmaRegionStatDetails(CdmaRegionStatTrend trend)
        {
            StatDates = trend.StatDates;
            RegionList = trend.RegionList;
            ImportDetails(trend.ViewList);
        }

        private void ImportDetails(List<IEnumerable<CdmaRegionStatView>> views)
        {
            KpiDetails.Add("2G呼建", views.Select(x => x.Select(v => v.CallSetupRate)).ToList());
            KpiDetails.Add("C/I优良率", views.Select(x => x.Select(v => v.Ci)).ToList());
            KpiDetails.Add("3G连接", views.Select(x => x.Select(v => v.ConnectionRate)).ToList());
            KpiDetails.Add("3G切2G流量比", views.Select(x => x.Select(v => v.DownSwitchRate)).ToList());
            KpiDetails.Add("掉话率", views.Select(x => x.Select(v => v.Drop2GRate)).ToList());
            KpiDetails.Add("掉线率", views.Select(x => x.Select(v => v.Drop3GRate)).ToList());
            KpiDetails.Add("Ec/Io优良率", views.Select(x => x.Select(v => v.Ecio)).ToList());
            KpiDetails.Add("2G全天话务量", views.Select(x => x.Select(v => v.ErlangIncludingSwitch)).ToList());
            KpiDetails.Add("3G全天话务量", views.Select(x => x.Select(v => v.Erlang3G)).ToList());
            KpiDetails.Add("全天流量(GB)", views.Select(x => x.Select(v => v.Flow)).ToList());
            KpiDetails.Add("反向链路繁忙率", views.Select(x => x.Select(v => v.LinkBusyRate)).ToList());
            KpiDetails.Add("2G利用率", views.Select(x => x.Select(v => v.Utility2GRate)).ToList());
            KpiDetails.Add("3G利用率", views.Select(x => x.Select(v => v.Utility3GRate)).ToList());
        }
    }
}
