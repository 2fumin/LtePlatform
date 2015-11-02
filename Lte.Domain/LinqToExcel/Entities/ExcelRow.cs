using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.LinqToExcel.Entities
{
    public class ExcelRow : List<ExcelCell>
    {
        IDictionary<string, int> _columnIndexMapping;

        public ExcelRow() :
            this(new List<ExcelCell>(), new Dictionary<string, int>())
        { }

        public ExcelRow(IList<ExcelCell> cells, IDictionary<string, int> columnIndexMapping)
        {
            for (int i = 0; i < cells.Count; i++) Insert(i, cells[i]);
            _columnIndexMapping = columnIndexMapping;
        }

        public ExcelCell this[string columnName]
        {
            get
            {
                if (!_columnIndexMapping.ContainsKey(columnName))
                    throw new ArgumentException(string.Format(
                        "'{0}' column name does not exist. Valid column names are '{1}'",
                        columnName, string.Join("', '", _columnIndexMapping.Keys.ToArray())));
                return base[_columnIndexMapping[columnName]];
            }
        }

        public IEnumerable<string> ColumnNames
        {
            get { return _columnIndexMapping.Keys; }
        }
    }
}
