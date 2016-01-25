using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace Lte.Parameters.Entities
{
    public class ENodebPhoto : Entity
    {
        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public short Angle { get; set; }

        public string Path { get; set; }
    }
}
