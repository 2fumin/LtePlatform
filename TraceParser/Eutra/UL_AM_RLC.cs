using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class UL_AM_RLC
    {
        public void InitDefaults()
        {
        }

        public maxRetxThreshold_Enum maxRetxThreshold { get; set; }

        public PollByte pollByte { get; set; }

        public PollPDU pollPDU { get; set; }

        public T_PollRetransmit t_PollRetransmit { get; set; }

        public enum maxRetxThreshold_Enum
        {
            t1,
            t2,
            t3,
            t4,
            t6,
            t8,
            t16,
            t32
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UL_AM_RLC Decode(BitArrayInputStream input)
            {
                UL_AM_RLC ul_am_rlc = new UL_AM_RLC();
                ul_am_rlc.InitDefaults();
                int nBits = 6;
                ul_am_rlc.t_PollRetransmit = (T_PollRetransmit)input.readBits(nBits);
                nBits = 3;
                ul_am_rlc.pollPDU = (PollPDU)input.readBits(nBits);
                nBits = 4;
                ul_am_rlc.pollByte = (PollByte)input.readBits(nBits);
                nBits = 3;
                ul_am_rlc.maxRetxThreshold = (maxRetxThreshold_Enum)input.readBits(nBits);
                return ul_am_rlc;
            }
        }
    }
}
