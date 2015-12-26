using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;

namespace Lte.Parameters.Entities.Work
{
    public class WorkItemExcel
    {
        [ExcelColumn("工单编号")]
        public string SerialNumber { get; set; }

        [ExcelColumn("工单标题")]
        public string Title { get; set; }

        [ExcelColumn("基站编号")]
        public int ENodebId { get; set; }

        [ExcelColumn("小区编号")]
        public byte SectorId { get; set; }

        [ExcelColumn("派单时间")]
        public DateTime BeginTime { get; set; }

        [ExcelColumn("最迟结单时间")]
        public DateTime Deadline { get; set; }

        [ExcelColumn("重复派单数")]
        public byte RepeatTimes { get; set; }

        [ExcelColumn("退回次数")]
        public byte RejectTimes { get; set; }

        [ExcelColumn("处理人")]
        public string StaffName { get; set; }

        [ExcelColumn("回单时间")]
        public DateTime? FeedbackTime { get; set; }

        [ExcelColumn("结单时间")]
        public DateTime? FinishTime { get; set; }

        [ExcelColumn("故障原因")]
        public string CauseDescription { get; set; }

        [ExcelColumn("工单状态")]
        public string StateDescription { get; set; }

        [ExcelColumn("备注")]
        public string Comments { get; set; }
    }
}
