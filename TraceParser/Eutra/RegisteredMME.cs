using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class RegisteredMME
    {
        public void InitDefaults()
        {
        }

        public string mmec { get; set; }

        public string mmegi { get; set; }

        public PLMN_Identity plmn_Identity { get; set; }

        public class PerDecoder
        {
            public static readonly RegisteredMME.PerDecoder Instance = new RegisteredMME.PerDecoder();

            public RegisteredMME Decode(BitArrayInputStream input)
            {
                RegisteredMME dmme = new RegisteredMME();
                dmme.InitDefaults();
                bool flag = false;
                BitMaskStream stream = flag ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    dmme.plmn_Identity = PLMN_Identity.PerDecoder.Instance.Decode(input);
                }
                dmme.mmegi = input.readBitString(0x10);
                dmme.mmec = input.readBitString(8);
                return dmme;
            }
        }
    }
}
