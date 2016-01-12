using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;

namespace Lte.Parameters.Entities.Work
{
    [TypeDoc("存储于数据库的工单信息")]
    public class WorkItem : Entity
    {
        [MemberDoc("工单编号")]
        public string SerialNumber { get; set; }

        [MemberDoc("工单类型")]
        public WorkItemType Type { get; set; }

        [MemberDoc("工单子类型")]
        public WorkItemSubtype Subtype { get; set; }

        [MemberDoc("基站编号")]
        public int ENodebId { get; set; }

        [MemberDoc("扇区编号")]
        public byte SectorId { get; set; }

        [MemberDoc("派单时间")]
        public DateTime BeginTime { get; set; }

        [MemberDoc("回单期限")]
        public DateTime Deadline { get; set; }

        [MemberDoc("重复次数")]
        public short RepeatTimes { get; set; }

        [MemberDoc("驳回次数")]
        public short RejectTimes { get; set; }

        [MemberDoc("责任人")]
        public string StaffName { get; set; }

        [MemberDoc("最近反馈时间")]
        public DateTime? FeedbackTime { get; set; }

        [MemberDoc("完成时间")]
        public DateTime? FinishTime { get; set; }

        [MemberDoc("定位原因")]
        public WorkItemCause Cause { get; set; }

        [MemberDoc("工单状态")]
        public WorkItemState State { get; set; }

        [MemberDoc("省中心平台反馈信息")]
        public string Comments { get; set; }

        [MemberDoc("本平台反馈信息")]
        public string FeedbackContents { get; set; }
    }
}
