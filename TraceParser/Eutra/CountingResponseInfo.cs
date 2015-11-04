using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CountingResponseInfo_r10
    {
        public void InitDefaults()
        {
        }

        public long countingResponseService_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CountingResponseInfo_r10 Decode(BitArrayInputStream input)
            {
                CountingResponseInfo_r10 _r = new CountingResponseInfo_r10();
                _r.InitDefaults();
                input.readBit();
                _r.countingResponseService_r10 = input.readBits(4);
                return _r;
            }
        }
    }
}
