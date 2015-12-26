using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace Lte.Parameters.Entities.Work
{
    public class WorkItem : Entity
    {
        public string SerialNumber { get; set; }

        public WorkItemType Type { get; set; }

        public WorkItemSubtype Subtype { get; set; }

        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public DateTime BeginTime { get; set; }

        public DateTime Deadline { get; set; }

        public short RepeatTimes { get; set; }

        public short RejectTimes { get; set; }

        public string StaffName { get; set; }

        public DateTime? FeedbackTime { get; set; }

        public DateTime? FinishTime { get; set; }

        public WorkItemCause Cause { get; set; }

        public WorkItemState State { get; set; }

        public string Comments { get; set; }

        public string FeedbackContents { get; set; }
    }
}
