using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;

namespace Lte.Domain.Test.LinqToExcel.ColumnFormat
{
    internal class IntegerColumnClass
    {
        public string StringColumn { get; set; }

        public int IntegerColumn { get; set; }
    }

    internal class IntegerClassWithColumnAnnotation
    {
        [ExcelColumn("String Column")]
        public string StringColumn { get; set; }

        [ExcelColumn("Integer Column")]
        public int IntegerColumn { get; set; }
    }
}
