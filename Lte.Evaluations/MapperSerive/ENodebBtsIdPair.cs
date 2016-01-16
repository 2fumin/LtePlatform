using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.MapperSerive
{
    public class ENodebBtsIdPair
    {
        public int ENodebId { get; set; }

        public int BtsId { get; set; }
    }

    public class PciCell
    {
        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public short Pci { get; set; }

        public int Frequency { get; set; }
    }
}
