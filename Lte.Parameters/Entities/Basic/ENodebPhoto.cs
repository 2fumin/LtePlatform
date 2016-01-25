using Abp.Domain.Entities;

namespace Lte.Parameters.Entities.Basic
{
    public class ENodebPhoto : Entity
    {
        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public short Angle { get; set; }

        public string Path { get; set; }
    }
}
