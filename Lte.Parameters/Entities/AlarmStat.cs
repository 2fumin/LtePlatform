using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Lte.Domain.Common;

namespace Lte.Parameters.Entities
{
    public class AlarmStat : Entity
    {
        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public int AlarmId { get; set; }

        public DateTime HappenTime { get; set; }

        public DateTime RecoverTime { get; set; }

        public AlarmLevel AlarmLevel { get; set; }

        public AlarmCategory AlarmCategory { get; set; }

        public AlarmType AlarmType { get; set; }

        public string Details { get; set; }
    }
}
