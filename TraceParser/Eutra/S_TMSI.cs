using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class S_TMSI
    {
        public void InitDefaults()
        {
        }

        public string m_TMSI { get; set; }

        public string mmec { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public S_TMSI Decode(BitArrayInputStream input)
            {
                S_TMSI s_tmsi = new S_TMSI();
                s_tmsi.InitDefaults();
                s_tmsi.mmec = input.readBitString(8);
                s_tmsi.m_TMSI = input.readBitString(0x20);
                return s_tmsi;
            }
        }
    }
}
