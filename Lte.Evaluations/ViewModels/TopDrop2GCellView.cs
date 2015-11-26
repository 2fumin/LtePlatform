using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.ViewModels
{
    public class TopDrop2GCellView
    {
        [Display(Name = "")]
        public int CdmaCellId { get; set; }

        [Display(Name = "CDMA基站名称")]
        public string CdmaName { get; set; }

        [Display(Name = "LTE基站名称")]
        public string LteName { get; set; }

        [Display(Name = "扇区编号")]
        public byte SectorId { get; set; }

        [Display(Name = "频点")]
        public short Frequency { get; set; }

        [Display(Name = "掉话次数")]
        public int Drops { get; set; }

        public int MoAssignmentSuccess { get; set; }

        public int MtAssignmentSuccess { get; set; }

        public int TrafficAssignmentSuccess { get; set; }

        public int CallAttempts { get; set; }

        [Display(Name = "掉话率")]
        public double DropRate => (double)Drops / TrafficAssignmentSuccess;
    }
}
