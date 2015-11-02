using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.LinqToExcel.Entities
{
    public class ExcelRowNoHeader : List<ExcelCell>
    {
        public ExcelRowNoHeader(IEnumerable<ExcelCell> cells)
        {
            AddRange(cells);
        }
    }
}
