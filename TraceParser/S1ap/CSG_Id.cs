using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class CSG_Id
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                return input.readBitString(0x1b);
            }
        }
    }

    [Serializable]
    public class CSG_IdList
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<CSG_IdList_Item> Decode(BitArrayInputStream input)
            {
                return new List<CSG_IdList_Item>();
            }
        }
    }

    [Serializable]
    public class CSG_IdList_Item
    {
        public void InitDefaults()
        {
        }

        public string cSG_Id { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CSG_IdList_Item Decode(BitArrayInputStream input)
            {
                CSG_IdList_Item item = new CSG_IdList_Item();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                item.cSG_Id = input.readBitString(0x1b);
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
