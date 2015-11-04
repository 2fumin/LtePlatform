using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class EmergencyAreaID
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                return input.readOctetString(3);
            }
        }
    }

    [Serializable]
    public class EmergencyAreaID_Broadcast
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<EmergencyAreaID_Broadcast_Item> Decode(BitArrayInputStream input)
            {
                return new List<EmergencyAreaID_Broadcast_Item>();
            }
        }
    }

    [Serializable]
    public class EmergencyAreaID_Broadcast_Item
    {
        public void InitDefaults()
        {
        }

        public List<CompletedCellinEAI_Item> completedCellinEAI { get; set; }

        public string emergencyAreaID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public EmergencyAreaID_Broadcast_Item Decode(BitArrayInputStream input)
            {
                EmergencyAreaID_Broadcast_Item item = new EmergencyAreaID_Broadcast_Item();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.skipUnreadedBits();
                item.emergencyAreaID = input.readOctetString(3);
                input.skipUnreadedBits();
                item.completedCellinEAI = new List<CompletedCellinEAI_Item>();
                int nBits = 0x10;
                int num5 = input.readBits(nBits) + 1;
                for (int i = 0; i < num5; i++)
                {
                    CompletedCellinEAI_Item item2 = CompletedCellinEAI_Item.PerDecoder.Instance.Decode(input);
                    item.completedCellinEAI.Add(item2);
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
    public class EmergencyAreaID_Cancelled
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<EmergencyAreaID_Cancelled_Item> Decode(BitArrayInputStream input)
            {
                return new List<EmergencyAreaID_Cancelled_Item>();
            }
        }
    }

    [Serializable]
    public class EmergencyAreaID_Cancelled_Item
    {
        public void InitDefaults()
        {
        }

        public List<CancelledCellinEAI_Item> cancelledCellinEAI { get; set; }

        public string emergencyAreaID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public EmergencyAreaID_Cancelled_Item Decode(BitArrayInputStream input)
            {
                EmergencyAreaID_Cancelled_Item item = new EmergencyAreaID_Cancelled_Item();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.skipUnreadedBits();
                item.emergencyAreaID = input.readOctetString(3);
                input.skipUnreadedBits();
                item.cancelledCellinEAI = new List<CancelledCellinEAI_Item>();
                int nBits = 0x10;
                int num5 = input.readBits(nBits) + 1;
                for (int i = 0; i < num5; i++)
                {
                    CancelledCellinEAI_Item item2 = CancelledCellinEAI_Item.PerDecoder.Instance.Decode(input);
                    item.cancelledCellinEAI.Add(item2);
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
    public class EmergencyAreaIDList
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
