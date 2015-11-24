using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Regular;

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

        public static AlarmStat ConstructStat(AlarmStatCsv info)
        {
            return Mapper.Map<AlarmStatCsv, AlarmStat>(info);
        }

        public static AlarmStat ConstructStat(AlarmStatHuawei info)
        {
            return Mapper.Map<AlarmStatHuawei, AlarmStat>(info);
        }
    }
}
