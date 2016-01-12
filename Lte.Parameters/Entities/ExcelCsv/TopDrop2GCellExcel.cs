using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;

namespace Lte.Parameters.Entities
{
    public class TopDrop2GCellExcel
    {
        [ExcelColumn("地市")]
        public string City { get; set; }

        [ExcelColumn("日期")]
        public DateTime StatDate { get; set; }

        [ExcelColumn("时")]
        public int StatHour { get; set; }

        [ExcelColumn("站号")]
        public int BtsId { get; set; }

        [ExcelColumn("扇区")]
        public byte SectorId { get; set; }

        [ExcelColumn("载波")]
        public short Frequency { get; set; }

        [ExcelColumn("中文名")]
        public string CellName { get; set; }

        [ExcelColumn("业务信道掉话次数")]
        public int Drops { get; set; }

        [ExcelColumn("主叫业务信道分配成功次数")]
        public int MoAssignmentSuccess { get; set; }

        [ExcelColumn("被叫业务信道分配成功次数")]
        public int MtAssignmentSuccess { get; set; }

        [ExcelColumn("业务信道分配成功次数")]
        public int TrafficAssignmentSuccess { get; set; }

        [ExcelColumn("呼叫尝试总次数")]
        public int CallAttempts { get; set; }

        public static string SheetName { get; } = "掉话TOP30小区";
    }
}
