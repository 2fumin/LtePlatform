using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class Bearers_SubjectToStatusTransfer_Item
    {
        public void InitDefaults()
        {
        }

        public COUNTvalue dL_COUNTvalue { get; set; }

        public long e_RAB_ID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string receiveStatusofULPDCPSDUs { get; set; }

        public COUNTvalue uL_COUNTvalue { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public Bearers_SubjectToStatusTransfer_Item Decode(BitArrayInputStream input)
            {
                Bearers_SubjectToStatusTransfer_Item item = new Bearers_SubjectToStatusTransfer_Item();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                input.readBit();
                item.e_RAB_ID = input.readBits(4);
                item.uL_COUNTvalue = COUNTvalue.PerDecoder.Instance.Decode(input);
                item.dL_COUNTvalue = COUNTvalue.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    item.receiveStatusofULPDCPSDUs = input.readBitString(0x1000);
                }
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
    public class Bearers_SubjectToStatusTransferList
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
    public class COUNTvalue
    {
        public void InitDefaults()
        {
        }

        public long hFN { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public long pDCP_SN { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public COUNTvalue Decode(BitArrayInputStream input)
            {
                COUNTvalue tvalue = new COUNTvalue();
                tvalue.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0)
                    ? new BitMaskStream(input, 1)
                    : new BitMaskStream(input, 1);
                input.skipUnreadedBits();
                tvalue.pDCP_SN = input.readBits(0x10);
                int nBits = input.readBits(2) + 1;
                input.skipUnreadedBits();
                tvalue.hFN = input.readBits(nBits*8);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    tvalue.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        tvalue.iE_Extensions.Add(item);
                    }
                }
                return tvalue;
            }
        }
    }

}
