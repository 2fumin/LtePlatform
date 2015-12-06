using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;

namespace Lte.Parameters.Entities
{
    /// <summary>
    /// 定义记录CDMA基站信息的Excel导出数据项
    /// </summary>
    /// <remarks>需要定义与CdmaBts之间的映射关系。</remarks>
    public class BtsExcel
    {
        [ExcelColumn("基站名称")]
        public string Name { get; set; }

        [ExcelColumn("行政区域")]
        public string DistrictName { get; set; }

        [ExcelColumn("所属镇区")]
        public string TownName { get; set; }

        [ExcelColumn("经度")]
        public double Longtitute { get; set; }

        [ExcelColumn("纬度")]
        public double Lattitute { get; set; }

        [ExcelColumn("地址")]
        public string Address { get; set; }

        [ExcelColumn("基站编号")]
        public int BtsId { get; set; }

        [ExcelColumn("BSC编号")]
        public short BscId { get; set; }
    }
}
