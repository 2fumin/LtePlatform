using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class LAC
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                return input.readOctetString(2);
            }
        }
    }

    [Serializable]
    public class LAI
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string lAC { get; set; }

        public string pLMNidentity { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public LAI Decode(BitArrayInputStream input)
            {
                LAI lai = new LAI();
                lai.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.skipUnreadedBits();
                lai.pLMNidentity = input.readOctetString(3);
                input.skipUnreadedBits();
                lai.lAC = input.readOctetString(2);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    lai.iE_Extensions = new List<ProtocolExtensionField>();
                    int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        lai.iE_Extensions.Add(item);
                    }
                }
                return lai;
            }
        }
    }

    [Serializable]
    public class LPPa_PDU
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                int nBits = 0;
                while (true)
                {
                    switch (input.readBit())
                    {
                        case 0:
                            nBits += input.readBits(7);
                            goto Label_0096;

                        case 1:
                            switch (input.readBit())
                            {
                                case 0:
                                    nBits += input.readBits(14);
                                    goto Label_0096;

                                case 1:
                                    input.readBits(2);
                                    nBits += input.readBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_0096:
                return input.readOctetString(nBits);
            }
        }
    }

}
