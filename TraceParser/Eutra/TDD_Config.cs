using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class TDD_Config
    {
        public void InitDefaults()
        {
        }

        public specialSubframePatterns_Enum specialSubframePatterns { get; set; }

        public subframeAssignment_Enum subframeAssignment { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public TDD_Config Decode(BitArrayInputStream input)
            {
                TDD_Config config = new TDD_Config();
                config.InitDefaults();
                int nBits = 3;
                config.subframeAssignment = (subframeAssignment_Enum)input.readBits(nBits);
                nBits = 4;
                config.specialSubframePatterns = (specialSubframePatterns_Enum)input.readBits(nBits);
                return config;
            }
        }

        public enum specialSubframePatterns_Enum
        {
            ssp0,
            ssp1,
            ssp2,
            ssp3,
            ssp4,
            ssp5,
            ssp6,
            ssp7,
            ssp8
        }

        public enum subframeAssignment_Enum
        {
            sa0,
            sa1,
            sa2,
            sa3,
            sa4,
            sa5,
            sa6
        }
    }

    [Serializable]
    public class TDD_Config_v1130
    {
        public void InitDefaults()
        {
        }

        public specialSubframePatterns_v1130_Enum specialSubframePatterns_v1130 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public TDD_Config_v1130 Decode(BitArrayInputStream input)
            {
                TDD_Config_v1130 _v = new TDD_Config_v1130();
                _v.InitDefaults();
                int nBits = 1;
                _v.specialSubframePatterns_v1130 = (specialSubframePatterns_v1130_Enum)input.readBits(nBits);
                return _v;
            }
        }

        public enum specialSubframePatterns_v1130_Enum
        {
            ssp7,
            ssp9
        }
    }

}
