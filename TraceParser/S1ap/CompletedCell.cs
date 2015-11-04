using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class CompletedCellinEAI
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<CompletedCellinEAI_Item> Decode(BitArrayInputStream input)
            {
                return new List<CompletedCellinEAI_Item>();
            }
        }
    }

    [Serializable]
    public class CompletedCellinEAI_Item
    {
        public void InitDefaults()
        {
        }

        public EUTRAN_CGI eCGI { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CompletedCellinEAI_Item Decode(BitArrayInputStream input)
            {
                CompletedCellinEAI_Item item = new CompletedCellinEAI_Item();
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
    public class CompletedCellinTAI
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<CompletedCellinTAI_Item> Decode(BitArrayInputStream input)
            {
                return new List<CompletedCellinTAI_Item>();
            }
        }
    }

    [Serializable]
    public class CompletedCellinTAI_Item
    {
        public void InitDefaults()
        {
        }

        public EUTRAN_CGI eCGI { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CompletedCellinTAI_Item Decode(BitArrayInputStream input)
            {
                CompletedCellinTAI_Item item = new CompletedCellinTAI_Item();
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

}
