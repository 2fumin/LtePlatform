using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;

namespace Lte.Evaluations.ViewModels
{
    [TypeDoc("告警信息视图")]
    public class AlarmView
    {
        [MemberDoc("基站编号")]
        public int ENodebId { get; set; }

        [MemberDoc("告警定位")]
        public string Position { get; set; }

        [MemberDoc("发生时间")]
        public DateTime HappenTime { get; set; }

        [MemberDoc("发生时间字符串")]
        public string HappenTimeString => HappenTime.ToShortDateString();

        [MemberDoc("持续时间（分钟）")]
        public double Duration { get; set; }

        [MemberDoc("告警等级")]
        public string AlarmLevelDescription { get; set; }

        [MemberDoc("告警类型")]
        public string AlarmCategoryDescription { get; set; }

        [MemberDoc("告警分类")]
        public string AlarmTypeDescription { get; set; }

        [MemberDoc("详细描述")]
        public string Details { get; set; }
    }

    public class AlarmHistory
    {
        public string DateString { get; set; }

        public int Alarms { get; set; }
    }
}
