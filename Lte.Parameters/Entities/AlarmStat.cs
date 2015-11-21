using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
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

        public AlarmStat() { }

        public AlarmStat(AlarmStatCsv info)
        {
            info.CloneProperties(this);
            AlarmLevel = info.AlarmLevelDescription.GetAlarmLevel();
            AlarmCategory = info.AlarmCategoryDescription.GetCategory();
            AlarmType = info.AlarmCodeDescription.GetAlarmType();
            SectorId = info.ObjectId > 255 ? (byte)255 : (byte)info.ObjectId;
        }

        public AlarmStat(AlarmStatHuawei info)
        {
            info.CloneProperties(this);
            AlarmLevel = info.AlarmLevelDescription.GetAlarmLevel();
            AlarmCategory = AlarmCategory.Huawei;
            AlarmType = info.AlarmCodeDescription.GetAlarmHuawei();
            ENodebId = info.ENodebIdString.ConvertToInt(0);
        }
    }
}
