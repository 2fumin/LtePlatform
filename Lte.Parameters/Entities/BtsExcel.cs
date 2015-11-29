using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;

namespace Lte.Parameters.Entities
{
    public class BtsExcel
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

        [ExcelColumn("基站编号")]
        public int BtsId { get; set; }

        [ExcelColumn("BSC编号")]
        public short BscId { get; set; }
    }
}
