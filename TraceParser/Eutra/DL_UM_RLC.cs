using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class DL_UM_RLC
    {
        public void InitDefaults()
        {
        }

        public SN_FieldLength sn_FieldLength { get; set; }

        public T_Reordering t_Reordering { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public DL_UM_RLC Decode(BitArrayInputStream input)
            {
                DL_UM_RLC dl_um_rlc = new DL_UM_RLC();
                dl_um_rlc.InitDefaults();
                int nBits = 1;
                dl_um_rlc.sn_FieldLength = (SN_FieldLength)input.readBits(nBits);
                nBits = 5;
                dl_um_rlc.t_Reordering = (T_Reordering)input.readBits(nBits);
                return dl_um_rlc;
            }
        }
    }
}
