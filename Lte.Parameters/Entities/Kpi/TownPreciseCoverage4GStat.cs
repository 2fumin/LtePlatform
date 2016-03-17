using System;
using Abp.Domain.Entities;

namespace Lte.Parameters.Entities.Kpi
{
    public class TownPreciseCoverage4GStat : Entity
    {
        public DateTime StatTime { get; set; }

        public int TownId { get; set; }

        public int TotalMrs { get; set; }

        public int ThirdNeighbors { get; set; }

        public int SecondNeighbors { get; set; }

        public int FirstNeighbors { get; set; }
    }
}
