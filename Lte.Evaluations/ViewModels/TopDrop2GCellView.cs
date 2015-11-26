using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.ViewModels
{
    public class TopDrop2GCellView
    {
        public DateTime StatTime { get; set; }

        public int CdmaCellId { get; set; }

        public string CdmaName { get; set; }

        public string LteName { get; set; }

        public byte SectorId { get; set; }

        public short Frequency { get; set; }

        public int Drops { get; set; }

        public int MoAssignmentSuccess { get; set; }

        public int MtAssignmentSuccess { get; set; }

        public int TrafficAssignmentSuccess { get; set; }

        public int CallAttempts { get; set; }

        public double DropRate => (double)Drops / TrafficAssignmentSuccess;
    }
}
