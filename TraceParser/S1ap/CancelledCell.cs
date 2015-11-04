using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class CancelledCellinEAI
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<CancelledCellinEAI_Item> Decode(BitArrayInputStream input)
            {
                return new List<CancelledCellinEAI_Item>();
            }
        }
    }

    [Serializable]
    public class CancelledCellinEAI_Item
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

            public CancelledCellinEAI_Item Decode(BitArrayInputStream input)
            {
                CancelledCellinEAI_Item item = new CancelledCellinEAI_Item();
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
    public class CancelledCellinTAI
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<CancelledCellinTAI_Item> Decode(BitArrayInputStream input)
            {
                return new List<CancelledCellinTAI_Item>();
            }
        }
    }

    [Serializable]
    public class CancelledCellinTAI_Item
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

            public CancelledCellinTAI_Item Decode(BitArrayInputStream input)
            {
                CancelledCellinTAI_Item item = new CancelledCellinTAI_Item();
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

}
