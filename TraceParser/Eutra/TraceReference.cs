using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class TraceReference_r10
    {
        public void InitDefaults()
        {
        }

        public PLMN_Identity plmn_Identity_r10 { get; set; }

        public string traceId_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public TraceReference_r10 Decode(BitArrayInputStream input)
            {
                TraceReference_r10 _r = new TraceReference_r10();
                _r.InitDefaults();
                _r.plmn_Identity_r10 = PLMN_Identity.PerDecoder.Instance.Decode(input);
                _r.traceId_r10 = input.readOctetString(3);
                return _r;
            }
        }
    }
}
