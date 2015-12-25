using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Parameters.Entities.Work
{
    public static class WorkItemTypeQueries
    {
        private static readonly Tuple<WorkItemType, string>[] workItemTypeList =
        {
            new Tuple<WorkItemType, string>(WorkItemType.Infrastructure4G, "4G基础数据"),
            new Tuple<WorkItemType, string>(WorkItemType.Interference4G, "4G干扰故障"),
            new Tuple<WorkItemType, string>(WorkItemType.Kpi2G, "2G性能故障"),
            new Tuple<WorkItemType, string>(WorkItemType.Kpi4G, "4G性能故障"),
            new Tuple<WorkItemType, string>(WorkItemType.NetworkProblem, "网元故障"),
            new Tuple<WorkItemType, string>(WorkItemType.RrcConnection, "RRC连接成功率恶化"),
            new Tuple<WorkItemType, string>(WorkItemType.Others, "其他类型")
        };

        public static string GetWorkItemTypeDescription(this WorkItemType type)
        {
            var tuple = workItemTypeList.FirstOrDefault(x => x.Item1 == type);
            return tuple != null ? tuple.Item2 : "其他类型";
        }

        public static WorkItemType GetWorkItemType(this string description)
        {
            var tuple = workItemTypeList.FirstOrDefault(x => x.Item2 == description);
            return tuple != null ? tuple.Item1 : WorkItemType.Others;
        }

        private static readonly Tuple<WorkItemSubtype, string>[] workItemSubtypeList =
        {
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.CallSetup, "小区级呼叫建立成功率异常"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.DataMaintainence, "数据维护"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.Drop2G, "小区级掉话率异常"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.ErabConnection, "小区级E-RAB建立成功率异常"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.ErabDrop, "小区级E-RAB掉线率异常"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.PrbUplinkInterference, "PRB上行控制信道干扰"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.PrbUplinkSevereInterference, "PRB上行控制信道严重干扰"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.PreciseRate, "小区级精确覆盖率异常"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.RrcConnection, "小区级RRC连接成功率恶化"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.Rssi, "RSSI故障"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.UplinkInterference, "小区级上行干扰"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.UplinkSevereInterference, "小区级上行严重干扰"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.Others, "其他类型")
        };

        public static string GetWorkItemSubtypeDescription(this WorkItemSubtype type)
        {
            var tuple = workItemSubtypeList.FirstOrDefault(x => x.Item1 == type);
            return tuple != null ? tuple.Item2 : "其他类型";
        }

        public static WorkItemSubtype GetSubtype(this string description)
        {
            var tuple = workItemSubtypeList.FirstOrDefault(x => x.Item2 == description);
            return tuple != null ? tuple.Item1 : WorkItemSubtype.Others;
        }
    }
}
