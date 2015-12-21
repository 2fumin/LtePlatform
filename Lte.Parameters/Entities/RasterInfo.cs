using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Parameters.Entities
{
    [Table(Name = "dbo.rasterInfo")]
    public class RasterInfo
    {
        [Column(Name = "rasterNum", DbType = "Int")]
        public int? RasterNum { get; set; }

        [Column(Name = "csvFilesName4G", DbType = "VarChar(MAX)")]
        public string CsvFilesName4G { get; set; }

        [Column(Name = "csvFilesName3G", DbType = "VarChar(MAX)")]
        public string CsvFilesName3G { get; set; }

        [Column(Name = "csvFilesName2G", DbType = "VarChar(MAX)")]
        public string CsvFilesName2G { get; set; }

        [Column(Name = "coordinate0", DbType = "Char(100)")]
        public string Coordinate0 { get; set; }

        [Column(Name = "coordinate1", DbType = "Char(100)")]
        public string Coordinate1 { get; set; }

        [Column(Name = "coordinate2", DbType = "Char(100)")]
        public string Coordinate2 { get; set; }

        [Column(Name = "coordinate3", DbType = "Char(100)")]
        public string Coordinate3 { get; set; }

        [Column(Name = "area", DbType = "Char(50)")]
        public string Area { get; set; }
    }
}
