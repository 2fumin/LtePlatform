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

    public enum WorkItemState: byte
    {
        Processing,
        Processed,
        Finished
    }

    public enum WorkItemCause: short
    {
        Rssi,
        ParameterConfig,
        TrunkProblem,
        PilotPolution,
        Overload,
        InterferenceCoverage,
        ImproperPower,
        FeedAppliance,
        NeighborCell,
        Others,
        WeakCoverage,
        ApplianceProblem,
        IndoorDistribution,
        AntennaFeedline,
        Antenna,
        OuterInterference,
        WrongDownTilt,
        PagingChannelBusy,
        HardSwitch,
        Jamming,
        OverCoverage,
        InvisibleAlarm,
        MainAlarm,
        ResouceJamming
    }
}
