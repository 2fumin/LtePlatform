using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;

namespace Lte.Parameters.Entities
{
    public class IndoorDistributionExcel
    {
        [ExcelColumn("室分名称")]
        public string Name { get; set; }

        [ExcelColumn("覆盖范围")]
        public string Range { get; set; }

        [ExcelColumn("施主基站")]
        public string SourceName { get; set; }

        [ExcelColumn("信源种类")]
        public string SourceType { get; set; }

        [ExcelColumn("经度")]
        public double Longtitute { get; set; }

        [ExcelColumn("纬度")]
        public double Lattitute { get; set; }
    }
}
