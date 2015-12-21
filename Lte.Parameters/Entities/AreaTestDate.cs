using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using System.Data.Linq.Mapping;

namespace Lte.Parameters.Entities
{
    [Table(Name = "dbo.areaTestDate")]
    public class AreaTestDate
    {
        [Column(Name = "area", DbType = "Char(50)")]
        public string Area { get; set; }

        [Column(Name = "latestTestDate2G", DbType = "Char(100)")]
        public string LatestTestDate2G { get; set; }

        [Column(Name = "latestTestDate3G", DbType = "Char(100)")]
        public string LatestTestDate3G { get; set; }

        [Column(Name = "latestTestDate4G", DbType = "Char(100)")]
        public string LatestTestDate4G { get; set; }
    }
}
