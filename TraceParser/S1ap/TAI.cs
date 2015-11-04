using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class TAI
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string pLMNidentity { get; set; }

        public string tAC { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public TAI Decode(BitArrayInputStream input)
            {
                TAI tai = new TAI();
                tai.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.skipUnreadedBits();
                tai.pLMNidentity = input.readOctetString(3);
                input.skipUnreadedBits();
                tai.tAC = input.readOctetString(2);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    tai.iE_Extensions = new List<ProtocolExtensionField>();
                    int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        tai.iE_Extensions.Add(item);
                    }
                }
                return tai;
            }
        }
    }

    [Serializable]
    public class TAI_Broadcast
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<TAI_Broadcast_Item> Decode(BitArrayInputStream input)
            {
                return new List<TAI_Broadcast_Item>();
            }
        }
    }

    [Serializable]
    public class TAI_Broadcast_Item
    {
        public void InitDefaults()
        {
        }

        public List<CompletedCellinTAI_Item> completedCellinTAI { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public TAI tAI { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public TAI_Broadcast_Item Decode(BitArrayInputStream input)
            {
                TAI_Broadcast_Item item = new TAI_Broadcast_Item();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                item.tAI = TAI.PerDecoder.Instance.Decode(input);
                input.skipUnreadedBits();
                item.completedCellinTAI = new List<CompletedCellinTAI_Item>();
                int nBits = 0x10;
                int num5 = input.readBits(nBits) + 1;
                for (int i = 0; i < num5; i++)
                {
                    CompletedCellinTAI_Item item2 = CompletedCellinTAI_Item.PerDecoder.Instance.Decode(input);
                    item.completedCellinTAI.Add(item2);
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
    public class TAI_Cancelled
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<TAI_Cancelled_Item> Decode(BitArrayInputStream input)
            {
                return new List<TAI_Cancelled_Item>();
            }
        }
    }

    [Serializable]
    public class TAI_Cancelled_Item
    {
        public void InitDefaults()
        {
        }

        public List<CancelledCellinTAI_Item> cancelledCellinTAI { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public TAI tAI { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public TAI_Cancelled_Item Decode(BitArrayInputStream input)
            {
                TAI_Cancelled_Item item = new TAI_Cancelled_Item();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                item.tAI = TAI.PerDecoder.Instance.Decode(input);
                input.skipUnreadedBits();
                item.cancelledCellinTAI = new List<CancelledCellinTAI_Item>();
                int nBits = 0x10;
                int num5 = input.readBits(nBits) + 1;
                for (int i = 0; i < num5; i++)
                {
                    CancelledCellinTAI_Item item2 = CancelledCellinTAI_Item.PerDecoder.Instance.Decode(input);
                    item.cancelledCellinTAI.Add(item2);
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
    public class TAIItem
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public TAI tAI { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public TAIItem Decode(BitArrayInputStream input)
            {
                TAIItem item = new TAIItem();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                item.tAI = TAI.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    item.iE_Extensions = new List<ProtocolExtensionField>();
                    int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
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
    public class TAIList
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<ProtocolIE_Field> Decode(BitArrayInputStream input)
            {
                return new List<ProtocolIE_Field>();
            }
        }
    }

    [Serializable]
    public class TAIListforWarning
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<TAI> Decode(BitArrayInputStream input)
            {
                return new List<TAI>();
            }
        }
    }

    [Serializable]
    public class SupportedTAs
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<SupportedTAs_Item> Decode(BitArrayInputStream input)
            {
                return new List<SupportedTAs_Item>();
            }
        }
    }

    [Serializable]
    public class SupportedTAs_Item
    {
        public void InitDefaults()
        {
        }

        public List<string> broadcastPLMNs { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string tAC { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SupportedTAs_Item Decode(BitArrayInputStream input)
            {
                SupportedTAs_Item item = new SupportedTAs_Item();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.skipUnreadedBits();
                item.tAC = input.readOctetString(2);
                item.broadcastPLMNs = new List<string>();
                int nBits = 3;
                int num5 = input.readBits(nBits) + 1;
                for (int i = 0; i < num5; i++)
                {
                    input.skipUnreadedBits();
                    string str = input.readOctetString(3);
                    item.broadcastPLMNs.Add(str);
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
