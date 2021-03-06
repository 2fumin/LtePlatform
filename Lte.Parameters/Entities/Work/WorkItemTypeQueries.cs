﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Parameters.Entities.Work
{
    public static class WorkItemTypeQueries
    {
        private static readonly Tuple<WorkItemType, string>[] WorkItemTypeList =
        {
            new Tuple<WorkItemType, string>(WorkItemType.Infrastructure4G, "4G基础数据"),
            new Tuple<WorkItemType, string>(WorkItemType.Interference4G, "4G干扰故障"),
            new Tuple<WorkItemType, string>(WorkItemType.Kpi2G, "2G性能故障"),
            new Tuple<WorkItemType, string>(WorkItemType.Kpi4G, "4G性能故障"),
            new Tuple<WorkItemType, string>(WorkItemType.NetworkProblem, "网元故障"),
            new Tuple<WorkItemType, string>(WorkItemType.RrcConnection, "RRC连接成功率恶化"),
            new Tuple<WorkItemType, string>(WorkItemType.Others, "其他类型"),
            new Tuple<WorkItemType, string>(WorkItemType.DailyTask, "日常网优作业计划"),
            new Tuple<WorkItemType, string>(WorkItemType.DailyReport, "日报"),
            new Tuple<WorkItemType, string>(WorkItemType.Yilutong, "翼路通"),
            new Tuple<WorkItemType, string>(WorkItemType.KeySite, "省-集团测试保障-关键站点清单收集"),
            new Tuple<WorkItemType, string>(WorkItemType.SelfConstruction, "自建工单")
        };

        public static string GetWorkItemTypeDescription(this WorkItemType type)
        {
            var tuple = WorkItemTypeList.FirstOrDefault(x => x.Item1 == type);
            return tuple != null ? tuple.Item2 : "其他类型";
        }

        public static WorkItemType GetWorkItemType(this string description)
        {
            var tuple = WorkItemTypeList.FirstOrDefault(x => x.Item2 == description);
            return tuple?.Item1 ?? WorkItemType.Others;
        }

        private static readonly Tuple<WorkItemSubtype, string>[] WorkItemSubtypeList =
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
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.Others, "其他类型"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.AutomaticDt, "自动路测系统管理"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.ResourceOptimize, "资源调优管理"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.ProjectOptimization, "专题专项优化"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.CommunicationSustain, "重大通信保障"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.OptimizationWorkItem, "优化工单处理"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.KpiAlarm, "性能监控预警"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.RectifyDemand, "网优整改需求管理"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.NetworkPlan, "网络规划选址"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.SpecialData, "特殊数据更新"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.Dispossessed, "逼迁应急优化"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.ParameterCheck, "参数核查优化"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.ClusterRf, "簇射频优化"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.CoverageEvaluation, "覆盖系统评估"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.InterferenceCheck, "干扰排查整治"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.EngineeringOptimization, "工程优化管理"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.PlanDemandLibrary, "规划需求库管理"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.EngineeringParameters, "基站工参维护"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.MarketSustain, "市场支撑保障"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.CapacityEvaluation, "容量系统评估"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.CustomerComplain, "客户投诉处理"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.WeeklyAnalysis, "每周质量分析"),
            new Tuple<WorkItemSubtype, string>(WorkItemSubtype.DailyTest, "日常测试管理")
        };

        public static string GetWorkItemSubtypeDescription(this WorkItemSubtype type)
        {
            var tuple = WorkItemSubtypeList.FirstOrDefault(x => x.Item1 == type);
            return tuple != null ? tuple.Item2 : "其他类型";
        }

        public static WorkItemSubtype GetWorkItemSubtype(this string description)
        {
            var tuple = WorkItemSubtypeList.FirstOrDefault(x => x.Item2 == description);
            return tuple?.Item1 ?? WorkItemSubtype.Others;
        }

        private static readonly Tuple<WorkItemState, string>[] WorkItemStateList =
        {
            new Tuple<WorkItemState, string>(WorkItemState.Processing, "待处理"),
            new Tuple<WorkItemState, string>(WorkItemState.Processed, "待归档"),
            new Tuple<WorkItemState, string>(WorkItemState.Finished, "已归档"),
            new Tuple<WorkItemState, string>(WorkItemState.ToBeSigned, "待签单")
        };

        public static string GetWorkItemStateDescription(this WorkItemState state)
        {
            var tuple = WorkItemStateList.FirstOrDefault(x => x.Item1 == state);
            return tuple != null ? tuple.Item2 : "已归档";
        }

        public static WorkItemState GetState(this string description)
        {
            var tuple = WorkItemStateList.FirstOrDefault(x => x.Item2 == description);
            return tuple?.Item1 ?? WorkItemState.Finished;
        }

        private static readonly Tuple<WorkItemCause, string>[] WorkItemCauseList =
        {
            new Tuple<WorkItemCause, string>(WorkItemCause.Antenna, "天线问题"),
            new Tuple<WorkItemCause, string>(WorkItemCause.AntennaFeedline, "天馈器件异常"),
            new Tuple<WorkItemCause, string>(WorkItemCause.ApplianceProblem, "设备故障"),
            new Tuple<WorkItemCause, string>(WorkItemCause.FeedAppliance, "馈线链接器件问题"),
            new Tuple<WorkItemCause, string>(WorkItemCause.HardSwitch, "硬切换问题"),
            new Tuple<WorkItemCause, string>(WorkItemCause.ImproperPower, "功率不合理"),
            new Tuple<WorkItemCause, string>(WorkItemCause.IndoorDistribution, "室分器件异常"),
            new Tuple<WorkItemCause, string>(WorkItemCause.InterferenceCoverage, "干扰覆盖问题"),
            new Tuple<WorkItemCause, string>(WorkItemCause.InvisibleAlarm, "主设备隐性故障"),
            new Tuple<WorkItemCause, string>(WorkItemCause.Jamming, "拥塞"),
            new Tuple<WorkItemCause, string>(WorkItemCause.MainAlarm, "主设备障碍告警"),
            new Tuple<WorkItemCause, string>(WorkItemCause.NeighborCell, "邻区漏配"),
            new Tuple<WorkItemCause, string>(WorkItemCause.Others, "其他"),
            new Tuple<WorkItemCause, string>(WorkItemCause.Others, "其它"),
            new Tuple<WorkItemCause, string>(WorkItemCause.Others, "其它原因"),
            new Tuple<WorkItemCause, string>(WorkItemCause.OuterInterference, "外界干扰"),
            new Tuple<WorkItemCause, string>(WorkItemCause.OuterInterference, "网络外部干扰"),
            new Tuple<WorkItemCause, string>(WorkItemCause.OverCoverage, "越区覆盖"),
            new Tuple<WorkItemCause, string>(WorkItemCause.OverCoverage, "越区覆盖问题"),
            new Tuple<WorkItemCause, string>(WorkItemCause.Overload, "负荷过载"),
            new Tuple<WorkItemCause, string>(WorkItemCause.PagingChannelBusy, "寻呼信道负荷高"),
            new Tuple<WorkItemCause, string>(WorkItemCause.ParameterConfig, "参数配置错误"),
            new Tuple<WorkItemCause, string>(WorkItemCause.PilotPolution, "导频污染"),
            new Tuple<WorkItemCause, string>(WorkItemCause.ResouceJamming, "资源拥塞"),
            new Tuple<WorkItemCause, string>(WorkItemCause.Rssi, "RSSI异常"),
            new Tuple<WorkItemCause, string>(WorkItemCause.TrunkProblem, "传输故障"),
            new Tuple<WorkItemCause, string>(WorkItemCause.WeakCoverage, "弱覆盖"),
            new Tuple<WorkItemCause, string>(WorkItemCause.WeakCoverage, "弱覆盖问题"),
            new Tuple<WorkItemCause, string>(WorkItemCause.WrongDownTilt, "下倾角错误")
        };

        public static string GetWorkItemCauseDescription(this WorkItemCause cause)
        {
            var tuple = WorkItemCauseList.FirstOrDefault(x => x.Item1 == cause);
            return tuple != null ? tuple.Item2 : "其它原因";
        }

        public static WorkItemCause GetWorkItemCause(this string description)
        {
            var tuple = WorkItemCauseList.FirstOrDefault(x => x.Item2 == description);
            return tuple?.Item1 ?? WorkItemCause.Others;
        }
    }
}
