using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Parameters.Entities
{
    [Table("areaTestDate")]
    public class AreaTestDate
    {
        [Column("area")]
        [MaxLength(50)]
        public string Area { get; set; }

        [Column("latestTestDate2G")]
        [MaxLength(100)]
        public string LatestTestDate2G { get; set; }

        [Column("latestTestDate3G")]
        [MaxLength(100)]
        public string LatestTestDate3G { get; set; }

        [Column("latestTestDate4G")]
        [MaxLength(100)]
        public string LatestTestDate4G { get; set; }
    }
}
