using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Lte.Domain.Common;
using Lte.Domain.Regular;

namespace Lte.Parameters.Entities
{
    public class TopDrop2GCell : Entity
    {
        public DateTime StatTime { get; set; }

        public string City { get; set; }

        public int BtsId { get; set; }

        public int CellId { get; set; }

        public byte SectorId { get; set; }

        public short Frequency { get; set; }

        public int Drops { get; set; }

        public int MoAssignmentSuccess { get; set; }

        public int MtAssignmentSuccess { get; set; }

        public int TrafficAssignmentSuccess { get; set; }

        public int CallAttempts { get; set; }

        public double DropRate => (double)Drops / TrafficAssignmentSuccess * 100;

        public TopDrop2GCell() { }

        public TopDrop2GCell(TopDrop2GCellExcel cellExcel)
        {
            cellExcel.CloneProperties(this);
            StatTime = cellExcel.StatDate.AddHours(cellExcel.StatHour);
            CellId = cellExcel.CellName.GetSubStringInFirstPairOfChars('[', ']').ConvertToInt(1);
        }
    }
}
