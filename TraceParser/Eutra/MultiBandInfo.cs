using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class MultiBandInfo_v9e0
    {
        public void InitDefaults()
        {
        }

        public long? freqBandIndicator_v9e0 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MultiBandInfo_v9e0 Decode(BitArrayInputStream input)
            {
                MultiBandInfo_v9e0 _ve = new MultiBandInfo_v9e0();
                _ve.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    _ve.freqBandIndicator_v9e0 = input.readBits(8) + 0x41;
                }
                return _ve;
            }
        }
    }
}
