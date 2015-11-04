using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class PowerRampingParameters
    {
        public void InitDefaults()
        {
        }

        public powerRampingStep_Enum powerRampingStep { get; set; }

        public preambleInitialReceivedTargetPower_Enum preambleInitialReceivedTargetPower { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PowerRampingParameters Decode(BitArrayInputStream input)
            {
                PowerRampingParameters parameters = new PowerRampingParameters();
                parameters.InitDefaults();
                int nBits = 2;
                parameters.powerRampingStep = (powerRampingStep_Enum)input.readBits(nBits);
                nBits = 4;
                parameters.preambleInitialReceivedTargetPower 
                    = (preambleInitialReceivedTargetPower_Enum)input.readBits(nBits);
                return parameters;
            }
        }

        public enum powerRampingStep_Enum
        {
            dB0,
            dB2,
            dB4,
            dB6
        }

        public enum preambleInitialReceivedTargetPower_Enum
        {
            dBm_120,
            dBm_118,
            dBm_116,
            dBm_114,
            dBm_112,
            dBm_110,
            dBm_108,
            dBm_106,
            dBm_104,
            dBm_102,
            dBm_100,
            dBm_98,
            dBm_96,
            dBm_94,
            dBm_92,
            dBm_90
        }
    }
}
