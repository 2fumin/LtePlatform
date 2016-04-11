using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Entities.Basic;
using Lte.Parameters.Entities.Channel;
using Lte.Parameters.Entities.Neighbor;

namespace Lte.Evaluations.ViewModels.Channel
{
    public class CellPower
    {
        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public double RsPower { get; set; }

        public int Pa { get; set; }

        public int Pb { get; set; }

        public CellPower(EUtranCellFDDZte cellFdd, PowerControlDLZte pcDl)
        {
            ENodebId = cellFdd.eNodeB_Id;
            RsPower = cellFdd.cellReferenceSignalPower;
            Pb = cellFdd.pb;
            Pa = pcDl.paForDTCH;
        }
    }
}
