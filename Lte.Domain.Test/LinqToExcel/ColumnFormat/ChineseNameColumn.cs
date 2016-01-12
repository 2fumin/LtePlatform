using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Test.LinqToExcel.ColumnFormat
{
    internal class ChineseNameColumn
    {
        public string ChineseColumn { get; set; }

        public double DoubleColumn { get; set; }

        public int ThirdColumn { get; set; }
    }

    internal class ChineseClassWithColumnAnnotation
    {
        [ExcelColumn("第一列")]
        public string ChineseColumn { get; set; }

        [ExcelColumn("第二列（复杂的：浮点-》")]
        public double DoubleColumn { get; set; }

        [ExcelColumn("第3列：\"待引号的\"")]
        public int ThirdColumn { get; set; }
    }
}
