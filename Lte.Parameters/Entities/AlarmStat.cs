using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;

namespace Lte.Parameters.Entities
{
    [TypeDoc("告警统计实体（在Sqlserver数据库中）")]
    public class AlarmStat : Entity
    {
        [MemberDoc("基站编号")]
        public int ENodebId { get; set; }

        [MemberDoc("扇区编号")]
        public byte SectorId { get; set; }

        [MemberDoc("告警编号")]
        public int AlarmId { get; set; }

        [MemberDoc("发生时间")]
        public DateTime HappenTime { get; set; }

        [MemberDoc("恢复时间")]
        public DateTime RecoverTime { get; set; }

        [MemberDoc("告警等级")]
        public AlarmLevel AlarmLevel { get; set; }

        [MemberDoc("告警分类")]
        public AlarmCategory AlarmCategory { get; set; }

        [MemberDoc("告警类型")]
        public AlarmType AlarmType { get; set; }

        [MemberDoc("详细信息")]
        public string Details { get; set; }
        
    }
}
