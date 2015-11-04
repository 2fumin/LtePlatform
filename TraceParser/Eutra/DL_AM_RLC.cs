using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class DL_AM_RLC
    {
        public void InitDefaults()
        {
        }

        public T_Reordering t_Reordering { get; set; }

        public T_StatusProhibit t_StatusProhibit { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public DL_AM_RLC Decode(BitArrayInputStream input)
            {
                DL_AM_RLC dl_am_rlc = new DL_AM_RLC();
                dl_am_rlc.InitDefaults();
                int nBits = 5;
                dl_am_rlc.t_Reordering = (T_Reordering)input.readBits(nBits);
                nBits = 6;
                dl_am_rlc.t_StatusProhibit = (T_StatusProhibit)input.readBits(nBits);
                return dl_am_rlc;
            }
        }
    }
}
