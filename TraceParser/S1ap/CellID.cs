using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class CellID_Broadcast
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<CellID_Broadcast_Item> Decode(BitArrayInputStream input)
            {
                return new List<CellID_Broadcast_Item>();
            }
        }
    }

    [Serializable]
    public class CellID_Broadcast_Item
    {
        public void InitDefaults()
        {
        }

        public EUTRAN_CGI eCGI { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellID_Broadcast_Item Decode(BitArrayInputStream input)
            {
                CellID_Broadcast_Item item = new CellID_Broadcast_Item();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                item.eCGI = EUTRAN_CGI.PerDecoder.Instance.Decode(input);
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
    public class CellID_Cancelled
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<CellID_Cancelled_Item> Decode(BitArrayInputStream input)
            {
                return new List<CellID_Cancelled_Item>();
            }
        }
    }

    [Serializable]
    public class CellID_Cancelled_Item
    {
        public void InitDefaults()
        {
        }

        public EUTRAN_CGI eCGI { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public long numberOfBroadcasts { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellID_Cancelled_Item Decode(BitArrayInputStream input)
            {
                CellID_Cancelled_Item item = new CellID_Cancelled_Item();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                item.eCGI = EUTRAN_CGI.PerDecoder.Instance.Decode(input);
                int nBits = input.readBits(1) + 1;
                input.skipUnreadedBits();
                item.numberOfBroadcasts = input.readBits(nBits * 8);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    item.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
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
    public class GERAN_Cell_ID
    {
        public void InitDefaults()
        {
        }

        public string cI { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public LAI lAI { get; set; }

        public string rAC { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public GERAN_Cell_ID Decode(BitArrayInputStream input)
            {
                GERAN_Cell_ID l_id = new GERAN_Cell_ID();
                l_id.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                l_id.lAI = LAI.PerDecoder.Decode(input);
                input.skipUnreadedBits();
                l_id.rAC = input.readOctetString(1);
                input.skipUnreadedBits();
                l_id.cI = input.readOctetString(2);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    l_id.iE_Extensions = new List<ProtocolExtensionField>();
                    int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        l_id.iE_Extensions.Add(item);
                    }
                }
                return l_id;
            }
        }
    }

}
