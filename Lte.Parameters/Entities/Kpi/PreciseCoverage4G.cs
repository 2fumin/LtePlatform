using System;
using Abp.Domain.Entities;
using AutoMapper;

namespace Lte.Parameters.Entities.Kpi
{
    public class PreciseCoverage4G : Entity
    {
        public DateTime StatTime { get; set; }

        public string DateString => StatTime.ToShortDateString();

        public int CellId { get; set; }

        public byte SectorId { get; set; }

        public int TotalMrs { get; set; }

        public int ThirdNeighbors { get; set; }

        public int SecondNeighbors { get; set; }

        public int FirstNeighbors { get; set; }

        public double FirstRate => 100 * (double)FirstNeighbors / TotalMrs;

        public double SecondRate => 100 * (double)SecondNeighbors / TotalMrs;

        public double ThirdRate => 100 * (double)ThirdNeighbors / TotalMrs;

        public PreciseCoverage4G() { }

        public static PreciseCoverage4G ConstructStat(PreciseCoverage4GCsv info)
        {
            return Mapper.Map<PreciseCoverage4GCsv, PreciseCoverage4G>(info);
        }
    }
}
