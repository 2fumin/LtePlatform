using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;

namespace Lte.Parameters.Entities.Mr
{
    [AutoMapFrom(typeof(InterferenceMatrixPci))]
    public class InterferenceMatrixStat : Entity
    {
        public DateTime RecordTime { get; set; }

        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public short DestPci { get; set; }

        public int DestENodebId { get; set; }

        public byte DestSectorId { get; set; }

        public int Mod3Interferences { get; set; }

        public int Mod6Interferences { get; set; }

        public int OverInterferences6Db { get; set; }

        public int OverInterferences10Db { get; set; }

        public double InterferenceLevel { get; set; }
    }
}
