using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class OTDOA_PositioningCapabilities_r10
    {
        public void InitDefaults()
        {
        }

        public interFreqRSTD_Measurement_r10_Enum? interFreqRSTD_Measurement_r10 { get; set; }

        public otdoa_UE_Assisted_r10_Enum otdoa_UE_Assisted_r10 { get; set; }

        public enum interFreqRSTD_Measurement_r10_Enum
        {
            supported
        }

        public enum otdoa_UE_Assisted_r10_Enum
        {
            supported
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public OTDOA_PositioningCapabilities_r10 Decode(BitArrayInputStream input)
            {
                OTDOA_PositioningCapabilities_r10 _r = new OTDOA_PositioningCapabilities_r10();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                int nBits = 1;
                _r.otdoa_UE_Assisted_r10 = (otdoa_UE_Assisted_r10_Enum)input.readBits(nBits);
                if (stream.Read())
                {
                    nBits = 1;
                    _r.interFreqRSTD_Measurement_r10 = (interFreqRSTD_Measurement_r10_Enum)input.readBits(nBits);
                }
                return _r;
            }
        }
    }
}
