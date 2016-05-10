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
    public class FlowZteCsv
    {
        [CsvColumn(Name = "开始时间")]
        public DateTime StatTime { get; set; }

        [CsvColumn(Name = "网元")]
        public int ENodebId { get; set; }

        [CsvColumn(Name = "小区")]
        public byte SectorId { get; set; }

        [CsvColumn(Name = "最大RRC连接用户数_1")]
        public int MaxRrcUsers { get; set; }

        [CsvColumn(Name = "上行平均激活用户数_1")]
        public double UplinkAverageActiveUsers { get; set; }

        [CsvColumn(Name = "下行平均激活用户数_1")]
        public double DownlinkAverageActiveUsers { get; set; }

        [CsvColumn(Name = "平均RRC连接用户数_1")]
        public double AverageRrcUsers { get; set; }

        [CsvColumn(Name = "平均激活用户数_1")]
        public double AverageActiveUsers { get; set; }

        [CsvColumn(Name = "最大激活用户数_1")]
        public int MaxActiveUsers { get; set; }

        [CsvColumn(Name = "小区PDCP接收上行数据的总时长(s)")]
        public int PdcpUplinkDuration { get; set; }

        [CsvColumn(Name = "小区PDCP发送下行数据的总时长(s)")]
        public int PdcpDownlinkDuration { get; set; }

        [CsvColumn(Name = "小区上行PDCP层流量（MB）")]
        public double UplindPdcpFlowInMByte { get; set; }

        [CsvColumn(Name = "小区下行PDCP层流量（MB）")]
        public double DownlinkPdcpFlowInMByte { get; set; }

        [CsvColumn(Name = "QCI8小区上行IP Throughput数据量高(兆比特)")]
        public string Qci8UplinkIpThroughputHigh { get; set; }

        [CsvColumn(Name = "QCI8小区上行IP Throughput数据量低(千比特)")]
        public string Qci8UplinkIpThroughputLow { get; set; }

        [CsvColumn(Name = "QCI8小区上行IP Throughput数据传输时间(毫秒)")]
        public string Qci8UplinkIpThroughputDuration { get; set; }

        [CsvColumn(Name = "QCI9小区上行IP Throughput数据量高(兆比特)")]
        public string Qci9UplinkIpThroughputHigh { get; set; }

        [CsvColumn(Name = "QCI9小区上行IP Throughput数据量低(千比特)")]
        public string Qci9UplinkIpThroughputLow { get; set; }

        [CsvColumn(Name = "QCI9小区上行IP Throughput数据传输时间(毫秒)")]
        public string Qci9UplinkIpThroughputDuration { get; set; }

        [CsvColumn(Name = "QCI8小区下行IP Throughput数据量高(兆比特)")]
        public string Qci8DownlinkIpThroughputHigh { get; set; }

        [CsvColumn(Name = "QCI8小区下行IP Throughput数据量低(千比特)")]
        public string Qci8DownlinkIpThroughputLow { get; set; }

        [CsvColumn(Name = "QCI8小区下行IP Throughput数据传输时间(毫秒)")]
        public string Qci8DownlinkIpThroughputDuration { get; set; }

        [CsvColumn(Name = "QCI9小区下行IP Throughput数据量高(兆比特)")]
        public string Qci9DownlinkIpThroughputHigh { get; set; }

        [CsvColumn(Name = "QCI9小区下行IP Throughput数据量低(千比特)")]
        public string Qci9DownlinkIpThroughputLow { get; set; }

        [CsvColumn(Name = "QCI9小区下行IP Throughput数据传输时间(毫秒)")]
        public string Qci9DownlinkIpThroughputDuration { get; set; }

        public static IEnumerable<FlowZteCsv> ReadFlowZteCsvs(StreamReader reader)
        {
            return
                CsvContext.Read<FlowZteCsv>(reader, CsvFileDescription.CommaDescription)
                    .ToList()
                    .Where(x => !string.IsNullOrEmpty(x.Qci8DownlinkIpThroughputDuration));
        }
    }
}
