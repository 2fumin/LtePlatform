using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class BCCH_Config
    {
        public void InitDefaults()
        {
        }

        public modificationPeriodCoeff_Enum modificationPeriodCoeff { get; set; }

        public enum modificationPeriodCoeff_Enum
        {
            n2,
            n4,
            n8,
            n16
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public BCCH_Config Decode(BitArrayInputStream input)
            {
                var config = new BCCH_Config();
                config.InitDefaults();
                const int nBits = 2;
                config.modificationPeriodCoeff = (modificationPeriodCoeff_Enum)input.readBits(nBits);
                return config;
            }
        }
    }
}