using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;

namespace Lte.Parameters.Entities
{
    /// <summary>
    /// 定义记录LTE基站的信息的Excel导出数据项
    /// </summary>
    /// <remarks>需要定义与ENodeb之间的映射关系</remarks>
    public class ENodebExcel
    {
        [ExcelColumn("eNodeBName")]
        public string Name { get; set; }

        [ExcelColumn("区域")]
        public string DistrictName { get; set; }

        [ExcelColumn("镇区")]
        public string TownName { get; set; }

        [ExcelColumn("经度")]
        public double Longtitute { get; set; }

        [ExcelColumn("纬度")]
        public double Lattitute { get; set; }

        [ExcelColumn("地址")]
        public string Address { get; set; }

        [ExcelColumn("地市")]
        public string CityName { get; set; }

        [ExcelColumn("网格")]
        public string Grid { get; set; }

        [ExcelColumn("厂家")]
        public string Factory { get; set; }

        [ExcelColumn("IP", TransformEnum.IpAddress)]
        public IpAddress Ip { get; set; } = new IpAddress("0.0.0.0");

        [ExcelColumn("eNodeB ID")]
        public int ENodebId { get; set; }

        public string IpString => Ip.AddressString;

        [ExcelColumn("网关", TransformEnum.IpAddress)]
        public IpAddress Gateway { get; set; } = new IpAddress("0.0.0.0");

        public string GatewayString => Gateway.AddressString;

        [ExcelColumn("规划编号(设计院)")]
        public string PlanNum { get; set; }

        [ExcelColumn("制式")]
        public string DivisionDuplex { get; set; } = "FDD";

        [ExcelColumn("入网日期", TransformEnum.DefaultOpenDate)]
        public DateTime OpenDate { get; set; }
    }
}
