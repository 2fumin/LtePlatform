using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Parameters.Entities
{
    [Table("csvFilesInfo")]
    public class CsvFilesInfo
    {
        [Column("testDate")]
        public DateTime? TestDate { get; set; }

        [Column("csvFileName")]
        public string csvFileName { get; set; }

        [Column("direct")]
        public string Directory { get; set; }

        [Column("dataType")]
        public string DataType { get; set; }

        [Column("distance")]
        public double? Distance { get; set; }
    }
}
