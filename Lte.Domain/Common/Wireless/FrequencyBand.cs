using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Common.Wireless
{
    public enum FrequencyBand : byte
    {
        Fdd2100 = 0,
        Fdd1800 = 1,
        Tdd2600 = 2
    }

    public enum FrequencyBandType
    {
        Downlink2100,

        Uplink2100,

        Downlink1800,

        Uplink1800,

        Tdd2600,

        Undefined
    }

    internal class FrequencyBandDef
    {
        public FrequencyBandType FrequencyBandType { get; set; }

        public int FcnStart { get; set; }

        public int FcnEnd { get; set; }

        public double FrequencyStart { get; set; }

        public double FrequencyEnd { get; set; }
    }
}
