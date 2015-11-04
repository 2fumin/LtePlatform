using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.X2ap
{
    [Serializable]
    public class UL_GBR_PRB_usage
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                return input.readBits(7);
            }
        }
    }

    [Serializable]
    public class UL_HighInterferenceIndication
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                input.readBit();
                int num = input.readBits(7);
                return input.readBitString(num + 1);
            }
        }
    }

    [Serializable]
    public class UL_HighInterferenceIndicationInfo
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<UL_HighInterferenceIndicationInfo_Item> Decode(BitArrayInputStream input)
            {
                return new List<UL_HighInterferenceIndicationInfo_Item>();
            }
        }
    }

    [Serializable]
    public class UL_HighInterferenceIndicationInfo_Item
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public ECGI target_Cell_ID { get; set; }

        public string ul_interferenceindication { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UL_HighInterferenceIndicationInfo_Item Decode(BitArrayInputStream input)
            {
                UL_HighInterferenceIndicationInfo_Item item = new UL_HighInterferenceIndicationInfo_Item();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                item.target_Cell_ID = ECGI.PerDecoder.Instance.Decode(input);
                input.readBit();
                int num = input.readBits(7);
                input.skipUnreadedBits();
                item.ul_interferenceindication = input.readBitString(num + 1);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    item.iE_Extensions = new List<ProtocolExtensionField>();
                    const int nBits = 0x10;
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
    public class UL_InterferenceOverloadIndication
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<UL_InterferenceOverloadIndication_Item> Decode(BitArrayInputStream input)
            {
                return new List<UL_InterferenceOverloadIndication_Item>();
            }
        }
    }

    [Serializable]
    public class UL_non_GBR_PRB_usage
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                return input.readBits(7);
            }
        }
    }

    [Serializable]
    public class UL_Total_PRB_usage
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                return input.readBits(7);
            }
        }
    }

}
