using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Parameters.Entities.Work
{
    public enum WorkItemType : byte
    {
        Kpi2G,
        Kpi4G,
        Infrastructure4G,
        Interference4G,
        RrcConnection,
        NetworkProblem,
        Others
    }

    public enum WorkItemSubtype: short
    {
        Drop2G,
        CallSetup,
        PrbUplinkInterference,
        PrbUplinkSevereInterference,
        Rssi,
        DataMaintainence,
        ErabDrop,
        ErabConnection,
        RrcConnection,
        PreciseRate,
        UplinkInterference,
        UplinkSevereInterference,
        Others
    }
}
