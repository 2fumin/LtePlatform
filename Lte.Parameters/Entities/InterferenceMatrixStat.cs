using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace Lte.Parameters.Entities
{
    public class InterferenceMatrixStat : Entity
    {
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
