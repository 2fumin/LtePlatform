using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class PHICH_Config
    {
        public void InitDefaults()
        {
        }

        public phich_Duration_Enum phich_Duration { get; set; }

        public phich_Resource_Enum phich_Resource { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PHICH_Config Decode(BitArrayInputStream input)
            {
                PHICH_Config config = new PHICH_Config();
                config.InitDefaults();
                int nBits = 1;
                config.phich_Duration = (phich_Duration_Enum)input.readBits(nBits);
                nBits = 2;
                config.phich_Resource = (phich_Resource_Enum)input.readBits(nBits);
                return config;
            }
        }

        public enum phich_Duration_Enum
        {
            normal,
            extended
        }

        public enum phich_Resource_Enum
        {
            oneSixth,
            half,
            one,
            two
        }
    }
}
