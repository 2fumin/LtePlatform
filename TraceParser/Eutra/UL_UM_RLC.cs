using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class UL_UM_RLC
    {
        public void InitDefaults()
        {
        }

        public SN_FieldLength sn_FieldLength { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UL_UM_RLC Decode(BitArrayInputStream input)
            {
                UL_UM_RLC ul_um_rlc = new UL_UM_RLC();
                ul_um_rlc.InitDefaults();
                int nBits = 1;
                ul_um_rlc.sn_FieldLength = (SN_FieldLength)input.readBits(nBits);
                return ul_um_rlc;
            }
        }
    }
}
