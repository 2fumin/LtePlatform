using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.LinqToCsv;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;

namespace Lte.MySqlFramework.Entities
{
    public class FlowHuaweiCsv
    {
        [CsvColumn(Name = "开始时间")]
        public DateTime StatTime { get; set; }

        [CsvColumn(Name = "小区")]
        public string CellInfo { get; set; }
        
        [CsvColumn(Name = "小区PDCP层所发送的下行数据的总吞吐量 (比特)")]
        public long PdcpDownlinkFlowInByte { get; set; }

        [CsvColumn(Name = "小区PDCP层所接收到的上行数据的总吞吐量 (比特)")]
        public long PdcpUplinkFlowInByte { get; set; }

        [CsvColumn(Name = "小区内的平均用户数 (无)")]
        public double AverageUsers { get; set; }

        [CsvColumn(Name = "小区内的最大用户数 (无)")]
        public int MaxUsers { get; set; }

        [CsvColumn(Name = "平均激活用户数 (无)")]
        public double AverageActiveUsers { get; set; }

        [CsvColumn(Name = "最大激活用户数 (无)")]
        public int MaxActiveUsers { get; set; }

        [CsvColumn(Name = "上行平均激活用户数 (无)")]
        public double UplinkAverageUsers { get; set; }

        [CsvColumn(Name = "上行最大激活用户数 (无)")]
        public int UplinkMaxUsers { get; set; }

        [CsvColumn(Name = "下行平均激活用户数 (无)")]
        public double DownlinkAverageUsers { get; set; }

        [CsvColumn(Name = "下行最大激活用户数 (无)")]
        public int DownlinkMaxUsers { get; set; }

        [CsvColumn(Name = "小区下行有数据传输总时长(1ms精度) (毫秒)")]
        public int DownlinkDurationInMs { get; set; }

        [CsvColumn(Name = "小区上行有数据传输总时长(1ms精度) (毫秒)")]
        public int UplinkDurationInMs { get; set; }

        [CsvColumn(Name = "小区Uu接口寻呼用户个数 (无)")]
        public string PagingUsersString { get; set; }

        [CsvColumn(Name = "下行Physical Resource Block被使用的平均个数 (无)")]
        public double DownlinkAveragePrbs { get; set; }

        [CsvColumn(Name = "下行PDSCH DRB的Physical Resource Block被使用的平均个数 (无)")]
        public double DownlinkDrbPbs { get; set; }

        [CsvColumn(Name = "上行Physical Resource Block被使用的平均个数 (无)")]
        public double UplinkAveragePrbs { get; set; }

        [CsvColumn(Name = "上行PUSCH DRB的Physical Resource Block被使用的平均个数 (无)")]
        public double UplinkDrbPbs { get; set; }

        [CsvColumn(Name = "小区接收到属于Group A的Preamble消息次数 (无)")]
        public int GroupAPreambles { get; set; }

        [CsvColumn(Name = "小区接收到属于Group B的Preamble消息的次数 (无)")]
        public int GroupBPreambles { get; set; }

        [CsvColumn(Name = "小区接收到专用前导消息的次数 (无)")]
        public int DedicatedPreambles { get; set; }

        [CsvColumn(Name = "统计周期内上行DCI所使用的PDCCH CCE个数 (无)")]
        public long UplinkDciCces { get; set; }

        [CsvColumn(Name = "统计周期内下行DCI所使用的PDCCH CCE个数 (无)")]
        public long DownlinkDciCces { get; set; }

        [CsvColumn(Name = "统计周期内可用的PDCCH CCE的个数 (无)")]
        public long TotalCces { get; set; }

        [CsvColumn(Name = "PUCCH的PRB资源分配的平均值 (无)")]
        public string PucchPrbsString { get; set; }

        [CsvColumn(Name = "使UE缓存为空的最后一个TTI所传的上行PDCP吞吐量 (比特)")]
        public long LastTtiUplinkFlowInByte { get; set; }

        [CsvColumn(Name = "扣除使UE缓存为空的最后一个TTI之后的上行数传时长 (毫秒)")]
        public int ButLastUplinkDurationInMs { get; set; }

        [CsvColumn(Name = "使缓存为空的最后一个TTI所传的下行PDCP吞吐量 (比特)")]
        public long LastTtiDownlinkFlowInByte { get; set; }

        [CsvColumn(Name = "扣除使下行缓存为空的最后一个TTI之后的数传时长 (毫秒)")]
        public int ButLastDownlinkDurationInMs { get; set; }

        public static List<FlowHuaweiCsv> ReadFlowHuaweiCsvs(StreamReader reader)
        {
            return CsvContext.Read<FlowHuaweiCsv>(reader, CsvFileDescription.CommaDescription).ToList();
        }
    }
}
