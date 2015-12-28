using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.ViewModels
{
    public class WorkItemView
    {
        public string SerialNumber { get; set; }

        public string WorkItemType { get; set; }

        public string WorkItemSubType { get; set; }

        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Town { get; set; }

        public DateTime BeginTime { get; set; }

        public DateTime Deadline { get; set; }

        public short RepeatTimes { get; set; }

        public short RejectTimes { get; set; }

        public string StaffName { get; set; }

        public DateTime? FeedbackTime { get; set; }

        public DateTime? FinishTime { get; set; }

        public string WorkItemCause { get; set; }

        public string WorkItemState { get; set; }

        public string Comments { get; set; }

        public string FeedbackContents { get; set; }
    }
}
