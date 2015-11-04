using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class GBR_QosInformation
    {
        public void InitDefaults()
        {
        }

        public long e_RAB_GuaranteedBitrateDL { get; set; }

        public long e_RAB_GuaranteedBitrateUL { get; set; }

        public long e_RAB_MaximumBitrateDL { get; set; }

        public long e_RAB_MaximumBitrateUL { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public GBR_QosInformation Decode(BitArrayInputStream input)
            {
                GBR_QosInformation information = new GBR_QosInformation();
                information.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                int nBits = input.readBits(3) + 1;
                input.skipUnreadedBits();
                information.e_RAB_MaximumBitrateDL = input.readBits(nBits * 8);
                nBits = input.readBits(3) + 1;
                input.skipUnreadedBits();
                information.e_RAB_MaximumBitrateUL = input.readBits(nBits * 8);
                nBits = input.readBits(3) + 1;
                input.skipUnreadedBits();
                information.e_RAB_GuaranteedBitrateDL = input.readBits(nBits * 8);
                nBits = input.readBits(3) + 1;
                input.skipUnreadedBits();
                information.e_RAB_GuaranteedBitrateUL = input.readBits(nBits * 8);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    information.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        information.iE_Extensions.Add(item);
                    }
                }
                return information;
            }
        }
    }

    [Serializable]
    public class QCI
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                return input.readBits(8);
            }
        }
    }

}
