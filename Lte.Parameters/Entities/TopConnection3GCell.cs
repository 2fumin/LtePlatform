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
    public class TopConnection3GCell : Entity
    {
        public DateTime StatTime { get; set; }

        public string City { get; set; }

        public int BtsId { get; set; }

        public int CellId { get; set; }

        public byte SectorId { get; set; }

        public int WirelessDrop { get; set; }

        public int ConnectionAttempts { get; set; }

        public int ConnectionFails { get; set; }

        public double LinkBusyRate { get; set; }

        public double ConnectionRate => (double)(ConnectionAttempts - ConnectionFails) / ConnectionAttempts;

        public double DropRate => (double)WirelessDrop / (ConnectionAttempts - ConnectionFails);

        public TopConnection3GCell() { }

        public TopConnection3GCell(TopConnection3GCellExcel cellExcel)
        {
            cellExcel.CloneProperties(this);
            StatTime = cellExcel.StatDate.AddHours(cellExcel.StatHour);
            CellId = cellExcel.CellName.GetSubStringInFirstPairOfChars('[', ']').ConvertToInt(1);
        }
    }
}
