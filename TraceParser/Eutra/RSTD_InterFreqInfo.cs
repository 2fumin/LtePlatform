using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class RSTD_InterFreqInfo_r10
    {
        public void InitDefaults()
        {
        }

        public long carrierFreq_r10 { get; set; }

        public long? carrierFreq_v1090 { get; set; }

        public long measPRS_Offset_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RSTD_InterFreqInfo_r10 Decode(BitArrayInputStream input)
            {
                RSTD_InterFreqInfo_r10 _r = new RSTD_InterFreqInfo_r10();
                _r.InitDefaults();
                bool flag = input.readBit() != 0;
                _r.carrierFreq_r10 = input.readBits(0x10);
                _r.measPRS_Offset_r10 = input.readBits(6);
                if (flag)
                {
                    BitMaskStream stream = new BitMaskStream(input, 1);
                    if (stream.Read())
                    {
                        _r.carrierFreq_v1090 = input.readBits(0x12) + 0x10000;
                    }
                }
                return _r;
            }
        }
    }
}
