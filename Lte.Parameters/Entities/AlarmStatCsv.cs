using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.LinqToCsv;

namespace Lte.Parameters.Entities
{
    public class AlarmStatCsv
    {
        [CsvColumn(Name = "告警级别")]
        public string AlarmLevelDescription { get; set; }

        [CsvColumn(Name = "网元")]
        public string NetworkItem { get; set; }

        [CsvColumn(Name = "网元内定位")]
        public string Position { get; set; }

        [CsvColumn(Name = "告警码")]
        public string AlarmCodeDescription { get; set; }

        [CsvColumn(Name = "发生时间")]
        public DateTime HappenTime { get; set; }

        [CsvColumn(Name = "告警类型")]
        public string AlarmCategoryDescription { get; set; }

        [CsvColumn(Name = "告警原因")]
        public string Cause { get; set; }

        [CsvColumn(Name = "附加文本")]
        public string Details { get; set; }

        [CsvColumn(Name = "告警恢复时间")]
        public DateTime RecoverTime { get; set; }

        [CsvColumn(Name = "告警对象ID", CanBeNull = true)]
        public int ObjectId { get; set; }

        [CsvColumn(Name = "站点ID(局向)", CanBeNull = true)]
        public int ENodebId { get; set; }

        [CsvColumn(Name = "告警标识", CanBeNull = true)]
        public int AlarmId { get; set; }
    }
}
