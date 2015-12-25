using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common;

namespace Lte.Domain.Regular
{
    public static class AlarmTypeQueries
    {
        private static readonly Tuple<AlarmType, string>[] alarmTypeDescriptionList =
        {
            new Tuple<AlarmType, string>(AlarmType.CeNotEnough, "CE不足"),
            new Tuple<AlarmType, string>(AlarmType.StarUnlocked, "锁星问题"),
            new Tuple<AlarmType, string>(AlarmType.TrunkProblem, "传输问题"),
            new Tuple<AlarmType, string>(AlarmType.RssiProblem, "RSSI问题"),
            new Tuple<AlarmType, string>(AlarmType.CellDown, "小区退服"),
            new Tuple<AlarmType, string>(AlarmType.VswrProblem, "驻波比问题"),
            new Tuple<AlarmType, string>(AlarmType.VswrLte, "天馈驻波比异常(198098465)"),
            new Tuple<AlarmType, string>(AlarmType.Unimportant, "不影响业务问题"),
            new Tuple<AlarmType, string>(AlarmType.LinkBroken, "网元断链告警(198099803)"),
            new Tuple<AlarmType, string>(AlarmType.X2Broken, "X2断链告警(198094421)"),
            new Tuple<AlarmType, string>(AlarmType.X2UserPlane, "X2用户面路径不可用(198094467)"),
            new Tuple<AlarmType, string>(AlarmType.S1Broken, "S1断链告警(198094420)"),
            new Tuple<AlarmType, string>(AlarmType.S1UserPlane, "S1用户面路径不可用(198094466)"),
            new Tuple<AlarmType, string>(AlarmType.EthernetBroken, "以太网物理连接断(198098252)"),
            new Tuple<AlarmType, string>(AlarmType.LteCellDown, "LTE小区退出服务(198094419)"),
            new Tuple<AlarmType, string>(AlarmType.LteCellError, "小区关断告警(198094461)"),
            new Tuple<AlarmType, string>(AlarmType.SuperCellDown, "超级小区CP退出服务(198094440)"),
            new Tuple<AlarmType, string>(AlarmType.ENodebDown, "基站退出服务(198094422)"),
            new Tuple<AlarmType, string>(AlarmType.GnssStar, "GNSS接收机搜星故障(198096837)"),
            new Tuple<AlarmType, string>(AlarmType.GnssFeed, "GNSS天馈链路故障(198096836)"),
            new Tuple<AlarmType, string>(AlarmType.PaDeactivate, "PA去使能(198098440)"),
            new Tuple<AlarmType, string>(AlarmType.RruBroken, "RRU链路断(198097605)"),
            new Tuple<AlarmType, string>(AlarmType.RxChannel, "RX通道异常(198098469)"),
            new Tuple<AlarmType, string>(AlarmType.SntpFail, "SNTP对时失败(198092014)"),
            new Tuple<AlarmType, string>(AlarmType.VersionError, "版本包故障(198097567)"),
            new Tuple<AlarmType, string>(AlarmType.InitializationError, "初始化失败(198092070)"),
            new Tuple<AlarmType, string>(AlarmType.BoardInexist, "单板不在位(198092072)"),
            new Tuple<AlarmType, string>(AlarmType.BoardInitialize, "单板处于初始化状态(198092348)"),
            new Tuple<AlarmType, string>(AlarmType.BoardPowerDown, "单板电源关断(198092057)"),
            new Tuple<AlarmType, string>(AlarmType.BoardCommunication, "单板通讯链路断(198097060)"),
            new Tuple<AlarmType, string>(AlarmType.BoardSoftId, "找不到单板软件标识(198092397)"),
            new Tuple<AlarmType, string>(AlarmType.FiberReceiver, "光口接收链路故障(198098319)"),
            new Tuple<AlarmType, string>(AlarmType.FiberModule, "光模块不可用(198098318)"),
            new Tuple<AlarmType, string>(AlarmType.BbuInitialize, "基带单元处于初始化状态(198097050)"),
            new Tuple<AlarmType, string>(AlarmType.Temperature, "温度异常(198097061)"),
            new Tuple<AlarmType, string>(AlarmType.FanTemperature, "进风口温度异常(198092042)"),
            new Tuple<AlarmType, string>(AlarmType.NoClock, "没有可用的空口时钟源(198092217)"),
            new Tuple<AlarmType, string>(AlarmType.InnerError, "内部故障(198098467)"),
            new Tuple<AlarmType, string>(AlarmType.SoftwareAbnormal, "软件运行异常(198097604)"),
            new Tuple<AlarmType, string>(AlarmType.ApparatusPowerDown, "设备掉电(198092295)"),
            new Tuple<AlarmType, string>(AlarmType.InputVolte, "输入电压异常(198092053)"),
            new Tuple<AlarmType, string>(AlarmType.OuterApparatus, "外部扩展设备故障(198098468)"),
            new Tuple<AlarmType, string>(AlarmType.ParametersConfiguation, "网元不支持配置的参数(198097510)"),
            new Tuple<AlarmType, string>(AlarmType.BadPerformance, "性能门限越界(1513)"),
            new Tuple<AlarmType, string>(AlarmType.Others, "其他告警"),
            new Tuple<AlarmType, string>(AlarmType.DatabaseDelay, "性能数据入库延迟(15010001)")
        };

        public static string GetAlarmTypeDescription(this AlarmType type)
        {
            var tuple = alarmTypeDescriptionList.FirstOrDefault(x => x.Item1 == type);
            return (tuple != null) ? tuple.Item2 : type.GetAlarmTypeHuawei();
        }

        public static AlarmType GetAlarmType(this string description)
        {
            var tuple = alarmTypeDescriptionList.FirstOrDefault(x => x.Item2 == description);
            return (tuple != null) ? tuple.Item1 : AlarmType.Others;
        }

        private static readonly Tuple<AlarmType, string>[] alarmTypeHuaweiList =
        {
            new Tuple<AlarmType, string>(AlarmType.PciCrack, "小区PCI冲突告警"),
            new Tuple<AlarmType, string>(AlarmType.FiberModule, "BBU光模块收发异常告警"),
            new Tuple<AlarmType, string>(AlarmType.RruBroken, "射频单元硬件故障告警"),
            new Tuple<AlarmType, string>(AlarmType.RruRtwp, "射频单元接收通道RTWP/RSSI过低告警"),
            new Tuple<AlarmType, string>(AlarmType.BadPerformance, "小区服务能力下降告警"),
            new Tuple<AlarmType, string>(AlarmType.BbuCpriLost, "BBU CPRI光模块/电接口不在位告警"),
            new Tuple<AlarmType, string>(AlarmType.BbuCpriInterface, "BBU CPRI光模块/电接口不在位告警"),
            new Tuple<AlarmType, string>(AlarmType.RssiProblem, "RSSI值过高告警"),
            new Tuple<AlarmType, string>(AlarmType.S1UserPlane, "S1接口故障告警"),
            new Tuple<AlarmType, string>(AlarmType.S1Broken, "SCTP链路故障告警"),
            new Tuple<AlarmType, string>(AlarmType.X2UserPlane, "X2接口故障告警"),
            new Tuple<AlarmType, string>(AlarmType.FiberReceiver, "传输光接口异常告警"),
            new Tuple<AlarmType, string>(AlarmType.EletricAntenna, "电调天线未校准告警"),
            new Tuple<AlarmType, string>(AlarmType.S1Broken, "基站控制面传输中断告警"),
            new Tuple<AlarmType, string>(AlarmType.SoftwareAbnormal, "配置数据不一致告警"),
            new Tuple<AlarmType, string>(AlarmType.InnerError, "任务执行失败告警"),
            new Tuple<AlarmType, string>(AlarmType.RfAld, "射频单元ALD电流异常告警"),
            new Tuple<AlarmType, string>(AlarmType.RruCpriInterface, "射频单元CPRI接口异常告警"),
            new Tuple<AlarmType, string>(AlarmType.RruInterfacePerformance, "射频单元光接口性能恶化告警"),
            new Tuple<AlarmType, string>(AlarmType.RruPowerDown, "射频单元交流掉电告警"),
            new Tuple<AlarmType, string>(AlarmType.RruRtwpUnbalance, "射频单元接收通道RTWP/RSSI不平衡告警"),
            new Tuple<AlarmType, string>(AlarmType.RruClock, "射频单元时钟异常告警"),
            new Tuple<AlarmType, string>(AlarmType.RruOmcLink, "射频单元维护链路异常告警"),
            new Tuple<AlarmType, string>(AlarmType.VswrProblem, "射频单元驻波告警"),
            new Tuple<AlarmType, string>(AlarmType.SntpFail, "时间同步失败告警"),
            new Tuple<AlarmType, string>(AlarmType.ClockReference, "时钟参考源异常告警"),
            new Tuple<AlarmType, string>(AlarmType.Database, "数据库占用率过高告警(提示)"),
            new Tuple<AlarmType, string>(AlarmType.Database, "数据库占用率过高告警(次要)"),
            new Tuple<AlarmType, string>(AlarmType.AntennaLink, "天线设备维护链路异常告警"),
            new Tuple<AlarmType, string>(AlarmType.LinkBroken, "网元连接中断"),
            new Tuple<AlarmType, string>(AlarmType.NoClock, "系统时钟不可用告警"),
            new Tuple<AlarmType, string>(AlarmType.CellDown, "小区不可用告警"),
            new Tuple<AlarmType, string>(AlarmType.StarUnlocked, "星卡天线故障告警"),
            new Tuple<AlarmType, string>(AlarmType.DatabaseDelay, "性能数据库剩余空间不足"),
            new Tuple<AlarmType, string>(AlarmType.EthernetBroken, "以太网链路故障告警"),
            new Tuple<AlarmType, string>(AlarmType.TrunkProblem, "用户面承载链路故障告警"),
            new Tuple<AlarmType, string>(AlarmType.UserPlane, "用户面承载链路故障告警"),
            new Tuple<AlarmType, string>(AlarmType.RemoteOmc, "远程维护通道故障告警"),
            new Tuple<AlarmType, string>(AlarmType.LoginError, "登录尝试次数达到最大值告警"),
            new Tuple<AlarmType, string>(AlarmType.OuterApparatus, "网管服务异常退出告警"),
            new Tuple<AlarmType, string>(AlarmType.AnalogLoad, "小区模拟负载启动告警"),
        };

        public static string GetAlarmTypeHuawei(this AlarmType type)
        {
            var tuple = alarmTypeHuaweiList.FirstOrDefault(x => x.Item1 == type);
            return (tuple != null) ? tuple.Item2 : "其他告警";
        }

        public static AlarmType GetAlarmHuawei(this string description)
        {
            var tuple = alarmTypeHuaweiList.FirstOrDefault(x => x.Item2 == description);
            return (tuple != null) ? tuple.Item1 : AlarmType.Others;
        }

        private static readonly Tuple<AlarmLevel, string>[] alarmLevelDescriptionList =
        {
            new Tuple<AlarmLevel, string>(AlarmLevel.Serious, "严重"),
            new Tuple<AlarmLevel, string>(AlarmLevel.Primary, "主要"),
            new Tuple<AlarmLevel, string>(AlarmLevel.Secondary, "次要"),
            new Tuple<AlarmLevel, string>(AlarmLevel.Warning, "警告"),
            new Tuple<AlarmLevel, string>(AlarmLevel.Urgent, "紧急"),
            new Tuple<AlarmLevel, string>(AlarmLevel.Important, "重要"),
            new Tuple<AlarmLevel, string>(AlarmLevel.Tips, "提示"),
        };

        public static string GetAlarmLevelDescription(this AlarmLevel level)
        {
            var tuple = alarmLevelDescriptionList.FirstOrDefault(x => x.Item1 == level);
            return (tuple != null) ? tuple.Item2 : "次要";
        }

        public static AlarmLevel GetAlarmLevel(this string description)
        {
            var tuple = alarmLevelDescriptionList.FirstOrDefault(x => x.Item2 == description);
            return (tuple != null) ? tuple.Item1 : AlarmLevel.Secondary;
        }

        private static readonly Tuple<AlarmCategory, string>[] alarmCategoryDescriptionList =
        {
            new Tuple<AlarmCategory, string>(AlarmCategory.Communication, "通信告警"),
            new Tuple<AlarmCategory, string>(AlarmCategory.Qos, "服务质量告警"),
            new Tuple<AlarmCategory, string>(AlarmCategory.ProcessError, "处理错误告警"),
            new Tuple<AlarmCategory, string>(AlarmCategory.Environment, "环境告警"),
            new Tuple<AlarmCategory, string>(AlarmCategory.Apparatus, "设备告警"),
            new Tuple<AlarmCategory, string>(AlarmCategory.Huawei, "华为告警"),
        };

        public static string GetAlarmCategoryDescription(this AlarmCategory category)
        {
            var tuple = alarmCategoryDescriptionList.FirstOrDefault(x => x.Item1 == category);
            return (tuple != null) ? tuple.Item2 : "服务质量告警";
        }

        public static AlarmCategory GetCategory(this string description)
        {
            var tuple = alarmCategoryDescriptionList.FirstOrDefault(x => x.Item2 == description);
            return (tuple != null) ? tuple.Item1 : AlarmCategory.Qos;
        }
    }
}
