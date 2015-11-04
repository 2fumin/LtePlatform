using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class ServedGroupIDs
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
    public class ServedGUMMEIs
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<ServedGUMMEIsItem> Decode(BitArrayInputStream input)
            {
                return new List<ServedGUMMEIsItem>();
            }
        }
    }

    [Serializable]
    public class ServedGUMMEIsItem
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public List<string> servedGroupIDs { get; set; }

        public List<string> servedMMECs { get; set; }

        public List<string> servedPLMNs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ServedGUMMEIsItem Decode(BitArrayInputStream input)
            {
                ServedGUMMEIsItem item = new ServedGUMMEIsItem();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                item.servedPLMNs = new List<string>();
                int nBits = 5;
                int num5 = input.readBits(nBits) + 1;
                for (int i = 0; i < num5; i++)
                {
                    input.skipUnreadedBits();
                    string str = input.readOctetString(3);
                    item.servedPLMNs.Add(str);
                }
                input.skipUnreadedBits();
                item.servedGroupIDs = new List<string>();
                nBits = 0x10;
                int num7 = input.readBits(nBits) + 1;
                for (int j = 0; j < num7; j++)
                {
                    input.skipUnreadedBits();
                    string str2 = input.readOctetString(2);
                    item.servedGroupIDs.Add(str2);
                }
                input.skipUnreadedBits();
                item.servedMMECs = new List<string>();
                nBits = 8;
                int num9 = input.readBits(nBits) + 1;
                for (int k = 0; k < num9; k++)
                {
                    input.skipUnreadedBits();
                    string str3 = input.readOctetString(1);
                    item.servedMMECs.Add(str3);
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    item.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num11 = input.readBits(nBits) + 1;
                    for (int m = 0; m < num11; m++)
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
    public class ServedMMECs
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
    public class ServedPLMNs
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

}
