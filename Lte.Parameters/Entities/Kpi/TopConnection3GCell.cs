using System;
using Abp.Domain.Entities;
using AutoMapper;
using Lte.Parameters.Abstract;

namespace Lte.Parameters.Entities.Kpi
{
    public class TopConnection3GCell : Entity, IBtsIdQuery
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
        
        public static TopConnection3GCell ConstructStat(TopConnection3GCellExcel cellExcel)
        {
            return Mapper.Map<TopConnection3GCellExcel, TopConnection3GCell>(cellExcel);
        }
    }
}
