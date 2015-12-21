using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;

namespace Lte.Parameters.Entities
{
    [Table(Name = "dbo.rasterInfo")]
    public class RasterInfo
    {
        [System.Data.Linq.Mapping.Column(Name = "rasterNum", DbType = "Int")]
        public int? RasterNum { get; set; }

        [System.Data.Linq.Mapping.Column(Name = "csvFilesName4G", DbType = "VarChar(MAX)")]
        public string CsvFilesName4G { get; set; }

        [System.Data.Linq.Mapping.Column(Name = "csvFilesName3G", DbType = "VarChar(MAX)")]
        public string CsvFilesName3G { get; set; }

        [System.Data.Linq.Mapping.Column(Name = "csvFilesName2G", DbType = "VarChar(MAX)")]
        public string CsvFilesName2G { get; set; }

        [System.Data.Linq.Mapping.Column(Name = "coordinate0", DbType = "Char(100)")]
        public string Coordinate0 { get; set; }

        [System.Data.Linq.Mapping.Column(Name = "coordinate1", DbType = "Char(100)")]
        public string Coordinate1 { get; set; }

        [System.Data.Linq.Mapping.Column(Name = "coordinate2", DbType = "Char(100)")]
        public string Coordinate2 { get; set; }

        [System.Data.Linq.Mapping.Column(Name = "coordinate3", DbType = "Char(100)")]
        public string Coordinate3 { get; set; }

        [System.Data.Linq.Mapping.Column(Name = "area", DbType = "Char(50)")]
        public string Area { get; set; }

        public double WestLongtitute =>
            !string.IsNullOrEmpty(Coordinate0)
                ? Coordinate0.Split(',')[0].ConvertToDouble(0)
                : (!string.IsNullOrEmpty(Coordinate1) ? Coordinate1.Split(',')[0].ConvertToDouble(0) : 0);

        public double EastLongtitute =>
            !string.IsNullOrEmpty(Coordinate2)
                ? Coordinate2.Split(',')[0].ConvertToDouble(0)
                : (!string.IsNullOrEmpty(Coordinate3) ? Coordinate3.Split(',')[0].ConvertToDouble(0) : 0);

        public double SouthLattitute =>
            !string.IsNullOrEmpty(Coordinate1)
                ? Coordinate1.Split(',')[1].ConvertToDouble(0)
                : (!string.IsNullOrEmpty(Coordinate2) ? Coordinate2.Split(',')[1].ConvertToDouble(0) : 0);

        public double NorthLattitute =>
            !string.IsNullOrEmpty(Coordinate0)
                ? Coordinate0.Split(',')[1].ConvertToDouble(0)
                : (!string.IsNullOrEmpty(Coordinate3) ? Coordinate3.Split(',')[1].ConvertToDouble(0) : 0);
    }
}
