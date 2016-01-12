using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;

namespace Lte.Evaluations.ViewModels
{
    [TypeDoc("TOP掉话小区视图")]
    public class TopDrop2GCellView
    {
        [MemberDoc("统计日期")]
        public DateTime StatTime { get; set; }

        [MemberDoc("小区编号")]
        public int CellId { get; set; }

        [MemberDoc("CDMA基站名称")]
        public string CdmaName { get; set; }

        [MemberDoc("LTE基站名称")]
        public string LteName { get; set; }

        [MemberDoc("扇区编号")]
        public byte SectorId { get; set; }

        [MemberDoc("频点")]
        public short Frequency { get; set; }

        [MemberDoc("掉话次数")]
        public int Drops { get; set; }

        [MemberDoc("主叫分配成功数")]
        public int MoAssignmentSuccess { get; set; }

        [MemberDoc("被叫分配成功数")]
        public int MtAssignmentSuccess { get; set; }

        [MemberDoc("业务信道分配成功数")]
        public int TrafficAssignmentSuccess { get; set; }

        [MemberDoc("呼叫尝试次数")]
        public int CallAttempts { get; set; }

        [MemberDoc("掉话率")]
        public double DropRate => TrafficAssignmentSuccess == 0 ? 0 : (double) Drops/TrafficAssignmentSuccess;
    }
}
