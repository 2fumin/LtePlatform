using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using AutoMapper;
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

        public static TopConnection3GCell ConstructStat(TopConnection3GCellExcel cellExcel)
        {
            return Mapper.Map<TopConnection3GCellExcel, TopConnection3GCell>(cellExcel);
        }
    }
}
