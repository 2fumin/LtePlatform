using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CountingRequestInfo_r10
    {
        public void InitDefaults()
        {
        }

        public TMGI_r9 tmgi_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CountingRequestInfo_r10 Decode(BitArrayInputStream input)
            {
                CountingRequestInfo_r10 _r = new CountingRequestInfo_r10();
                _r.InitDefaults();
                input.readBit();
                _r.tmgi_r10 = TMGI_r9.PerDecoder.Instance.Decode(input);
                return _r;
            }
        }
    }
}
