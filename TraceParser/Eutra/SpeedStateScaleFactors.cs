using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SpeedStateScaleFactors
    {
        public void InitDefaults()
        {
        }

        public sf_High_Enum sf_High { get; set; }

        public sf_Medium_Enum sf_Medium { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SpeedStateScaleFactors Decode(BitArrayInputStream input)
            {
                SpeedStateScaleFactors factors = new SpeedStateScaleFactors();
                factors.InitDefaults();
                int nBits = 2;
                factors.sf_Medium = (sf_Medium_Enum)input.readBits(nBits);
                nBits = 2;
                factors.sf_High = (sf_High_Enum)input.readBits(nBits);
                return factors;
            }
        }

        public enum sf_High_Enum
        {
            oDot25,
            oDot5,
            oDot75,
            lDot0
        }

        public enum sf_Medium_Enum
        {
            oDot25,
            oDot5,
            oDot75,
            lDot0
        }
    }
}
