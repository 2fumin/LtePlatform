using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Abp.EntityFramework.AutoMapper;

namespace Lte.Evaluations.MapperSerive
{
    public class PciCell
    {
        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public short Pci { get; set; }

        public int Frequency { get; set; }
    }

    public class PciCellDumpInfo
    {
        public PciCell PciCell { get; set; }

        public DateTime Begin { get; set; }

        public DateTime End { get; set; }
    }

    [AutoMapFrom(typeof(PciCell))]
    public class PciCellPair
    {
        public int ENodebId { get; set; }

        public short Pci { get; set; }
    }

    public class PciCellPairComparer : IEqualityComparer<PciCellPair>
    {
        public bool Equals(PciCellPair x, PciCellPair y)
        {
            return x.ENodebId == y.ENodebId && x.Pci == y.Pci;
        }

        public int GetHashCode(PciCellPair obj)
        {
            return obj.ENodebId*839 + obj.Pci;
        }
    }
}