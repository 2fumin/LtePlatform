using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Lte.Domain.Common.Wireless;

namespace Lte.Parameters.Entities
{
    public class Cell : Entity
    {
        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public int Frequency { get; set; }

        public byte BandClass { get; set; }

        public short Pci { get; set; }

        public short Prach { get; set; }

        public double RsPower { get; set; }

        public bool IsOutdoor { get; set; }

        public int Tac { get; set; }

        public double Longtitute { get; set; }

        public double Lattitute { get; set; }

        public double Height { get; set; }

        public double Azimuth { get; set; }
        
        public double MTilt { get; set; }
        
        public double ETilt { get; set; }
        
        public double AntennaGain { get; set; }

        public AntennaPortsConfigure AntennaPorts { get; set; }
    }
}
