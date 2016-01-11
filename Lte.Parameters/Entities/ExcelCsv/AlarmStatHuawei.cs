using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.LinqToCsv;

namespace Lte.Parameters.Entities
{
    public class AlarmStatHuawei
    {
        [CsvColumn(Name = "级别")]
        public string AlarmLevelDescription { get; set; }

        [CsvColumn(Name = "告警源")]
        public string NetworkItem { get; set; }

        [CsvColumn(Name = "MO对象")]
        public string Position { get; set; }

        [CsvColumn(Name = "名称")]
        public string AlarmCodeDescription { get; set; }

        [CsvColumn(Name = "发生时间(NT)")]
        public DateTime HappenTime { get; set; }

        [CsvColumn(Name = "用户自定义标识")]
        public string Cause { get; set; }

        [CsvColumn(Name = "定位信息")]
        public string Details { get; set; }

        [CsvColumn(Name = "清除时间(NT)")]
        public DateTime RecoverTime { get; set; }

        [CsvColumn(Name = "eNodeB ID", CanBeNull = true)]
        public string ENodebIdString { get; set; }

        [CsvColumn(Name = "告警ID", CanBeNull = true)]
        public int AlarmId { get; set; }
    }
}
