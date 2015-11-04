using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class P_C_AndCBSR_r11
    {
        public void InitDefaults()
        {
        }

        public string codebookSubsetRestriction_r11 { get; set; }

        public long p_C_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public P_C_AndCBSR_r11 Decode(BitArrayInputStream input)
            {
                P_C_AndCBSR_r11 _r = new P_C_AndCBSR_r11();
                _r.InitDefaults();
                _r.p_C_r11 = input.readBits(5) + -8;
                int nBits = input.readBits(8);
                _r.codebookSubsetRestriction_r11 = input.readBitString(nBits);
                return _r;
            }
        }
    }
}
