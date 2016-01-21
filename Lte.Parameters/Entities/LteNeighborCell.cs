using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using System.Runtime.Serialization;
using Lte.Domain.Common;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities.ExcelCsv;

namespace Lte.Parameters.Entities
{
    [Table("dbo.LteNeighborCells")]
    [KnownType(typeof(NearestPciCell))]
    [TypeDoc("LTE邻区关系定义")]
    public class LteNeighborCell : Entity
    {
        [MemberDoc("小区编号（对于LTE来说就是基站编号）")]
        public int CellId { get; set; }

        [MemberDoc("扇区编号")]
        public byte SectorId { get; set; }

        [MemberDoc("邻区小区编号")]
        public int NearestCellId { get; set; }

        [MemberDoc("邻区扇区编号")]
        public byte NearestSectorId { get; set; }
    }

    [Table("dbo.LteNeighborCells")]
    public class NearestPciCell : LteNeighborCell
    {
        public short Pci { get; set; }
        
        public int TotalTimes { get; set; }

        public static NearestPciCell ConstructCell(NeighborCellHwCsv info, ICellRepository cellRepository)
        {
            var fields = info.CellRelation.GetSplittedFields();
            var cellId = fields[13].ConvertToInt(0);
            var sectorId = fields[5].ConvertToByte(0);
            var neighborCellId = fields[11].ConvertToInt(0);
            var neighborSectorId = fields[3].ConvertToByte(0);
            var neiborCell = neighborCellId > 10000 ? cellRepository.GetBySectorId(neighborCellId, neighborSectorId) : null;
            return new NearestPciCell
            {
                CellId = cellId,
                SectorId = (neighborSectorId > 30 && sectorId < 30) ? (byte)(sectorId + 48) : sectorId,
                NearestCellId = neighborCellId,
                NearestSectorId = neighborSectorId,
                Pci = neiborCell?.Pci ?? -1,
                TotalTimes = info.TotalTimes
            };
        }

        public static NearestPciCell ConstructCell(NeighborCellZteCsv info, ICellRepository cellRepository)
        {
            var fields = info.NeighborRelation.GetSplittedFields(':');
            var neighborCellId = fields[3].ConvertToInt(0);
            var neighborSectorId = fields[4].ConvertToByte(0);
            var neiborCell = neighborCellId > 10000 ? cellRepository.GetBySectorId(neighborCellId, neighborSectorId) : null;
            return new NearestPciCell
            {
                CellId = info.ENodebId,
                SectorId = info.SectorId,
                NearestCellId = neighborCellId,
                NearestSectorId = neighborSectorId,
                Pci = neiborCell?.Pci ?? (short) -1,
                TotalTimes = info.IntraSystemTimes + info.InterSystemTimes
            };
        }
    }
}
