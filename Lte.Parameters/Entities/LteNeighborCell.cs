using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace Lte.Parameters.Entities
{
    [Table("dbo.LteNeighborCells")]
    public class LteNeighborCell : Entity
    {
        public int CellId { get; set; }

        public byte SectorId { get; set; }

        public int NearestCellId { get; set; }

        public byte NearestSectorId { get; set; }
    }

    [Table("dbo.LteNeighborCells")]
    public class NearestPciCell : LteNeighborCell
    {
        public short Pci { get; set; }

    }
}
