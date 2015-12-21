using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Parameters.Entities
{
    [Table(Name = "dbo.csvFilesInfo")]
    public class CsvFilesInfo
    {
        [Column(Name = "testDate", DbType = "DateTime")]
        public DateTime? TestDate { get; set; }

        [Column(Name = "csvFileName", DbType = "Char(300)")]
        public string CsvFileName { get; set; }

        [Column(Name = "direct", DbType = "Char(300)")]
        public string Directory { get; set; }

        [Column(Name = "dataType", DbType = "Char(50)")]
        public string DataType { get; set; }

        [Column(Name = "distance", DbType = "float")]
        public double? Distance { get; set; }
    }
}
