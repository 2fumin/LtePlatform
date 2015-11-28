using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Regular;
using Lte.Parameters.Abstract;

namespace Lte.Parameters.Entities
{
    public class TopDrop2GCell : Entity, IBtsIdQuery
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

        public static TopDrop2GCell ConstructStat(TopDrop2GCellExcel cellExcel)
        {
            return Mapper.Map<TopDrop2GCellExcel, TopDrop2GCell>(cellExcel);
        }
    }
}
