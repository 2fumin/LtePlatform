using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.X2ap
{
    [Serializable]
    public class ForbiddenLACs
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<string> Decode(BitArrayInputStream input)
            {
                return new List<string>();
            }
        }
    }

    [Serializable]
    public class ForbiddenLAs
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<ForbiddenLAs_Item> Decode(BitArrayInputStream input)
            {
                return new List<ForbiddenLAs_Item>();
            }
        }
    }

    [Serializable]
    public class ForbiddenLAs_Item
    {
        public void InitDefaults()
        {
        }

        public List<string> forbiddenLACs { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string pLMN_Identity { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ForbiddenLAs_Item Decode(BitArrayInputStream input)
            {
                ForbiddenLAs_Item item = new ForbiddenLAs_Item();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.skipUnreadedBits();
                item.pLMN_Identity = input.readOctetString(3);
                item.forbiddenLACs = new List<string>();
                int nBits = 12;
                int num5 = input.readBits(nBits) + 1;
                for (int i = 0; i < num5; i++)
                {
                    input.skipUnreadedBits();
                    string str = input.readOctetString(2);
                    item.forbiddenLACs.Add(str);
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    item.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num7 = input.readBits(nBits) + 1;
                    for (int j = 0; j < num7; j++)
                    {
                        ProtocolExtensionField field = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        item.iE_Extensions.Add(field);
                    }
                }
                return item;
            }
        }
    }

    [Serializable]
    public class ForbiddenTACs
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<string> Decode(BitArrayInputStream input)
            {
                return new List<string>();
            }
        }
    }

    [Serializable]
    public class ForbiddenTAs
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<ForbiddenTAs_Item> Decode(BitArrayInputStream input)
            {
                return new List<ForbiddenTAs_Item>();
            }
        }
    }

    [Serializable]
    public class ForbiddenTAs_Item
    {
        public void InitDefaults()
        {
        }

        public List<string> forbiddenTACs { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string pLMN_Identity { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ForbiddenTAs_Item Decode(BitArrayInputStream input)
            {
                ForbiddenTAs_Item item = new ForbiddenTAs_Item();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.skipUnreadedBits();
                item.pLMN_Identity = input.readOctetString(3);
                item.forbiddenTACs = new List<string>();
                int nBits = 12;
                int num5 = input.readBits(nBits) + 1;
                for (int i = 0; i < num5; i++)
                {
                    input.skipUnreadedBits();
                    string str = input.readOctetString(2);
                    item.forbiddenTACs.Add(str);
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    item.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num7 = input.readBits(nBits) + 1;
                    for (int j = 0; j < num7; j++)
                    {
                        ProtocolExtensionField field = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        item.iE_Extensions.Add(field);
                    }
                }
                return item;
            }
        }
    }

}
