using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.X2ap
{
    [Serializable]
    public class CellBasedMDT
    {
        public void InitDefaults()
        {
        }

        public List<ECGI> cellIdListforMDT { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellBasedMDT Decode(BitArrayInputStream input)
            {
                CellBasedMDT dmdt = new CellBasedMDT();
                dmdt.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                dmdt.cellIdListforMDT = new List<ECGI>();
                int nBits = 5;
                int num5 = input.readBits(nBits) + 1;
                for (int i = 0; i < num5; i++)
                {
                    ECGI item = ECGI.PerDecoder.Instance.Decode(input);
                    dmdt.cellIdListforMDT.Add(item);
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    dmdt.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num7 = input.readBits(nBits) + 1;
                    for (int j = 0; j < num7; j++)
                    {
                        ProtocolExtensionField field = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        dmdt.iE_Extensions.Add(field);
                    }
                }
                return dmdt;
            }
        }
    }

    [Serializable]
    public class CellCapacityClassValue
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                input.readBit();
                return input.readBits(7) + 1;
            }
        }
    }

    [Serializable]
    public class CellIdListforMDT
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<ECGI> Decode(BitArrayInputStream input)
            {
                return new List<ECGI>();
            }
        }
    }

    [Serializable]
    public class CellInformation_Item
    {
        public void InitDefaults()
        {
        }

        public ECGI cell_ID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public RelativeNarrowbandTxPower relativeNarrowbandTxPower { get; set; }

        public List<UL_HighInterferenceIndicationInfo_Item> ul_HighInterferenceIndicationInfo { get; set; }

        public List<UL_InterferenceOverloadIndication_Item> ul_InterferenceOverloadIndication { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellInformation_Item Decode(BitArrayInputStream input)
            {
                int num4;
                CellInformation_Item item = new CellInformation_Item();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 4) : new BitMaskStream(input, 4);
                item.cell_ID = ECGI.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    item.ul_InterferenceOverloadIndication = new List<UL_InterferenceOverloadIndication_Item>();
                    num4 = 7;
                    int num5 = input.readBits(num4) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        num4 = (input.readBit() == 0) ? 2 : 2;
                        UL_InterferenceOverloadIndication_Item item2 = (UL_InterferenceOverloadIndication_Item)input.readBits(num4);
                        item.ul_InterferenceOverloadIndication.Add(item2);
                    }
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    item.ul_HighInterferenceIndicationInfo = new List<UL_HighInterferenceIndicationInfo_Item>();
                    num4 = 8;
                    int num7 = input.readBits(num4) + 1;
                    for (int j = 0; j < num7; j++)
                    {
                        UL_HighInterferenceIndicationInfo_Item item3 
                            = UL_HighInterferenceIndicationInfo_Item.PerDecoder.Instance.Decode(input);
                        item.ul_HighInterferenceIndicationInfo.Add(item3);
                    }
                }
                if (stream.Read())
                {
                    item.relativeNarrowbandTxPower = RelativeNarrowbandTxPower.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    item.iE_Extensions = new List<ProtocolExtensionField>();
                    num4 = 0x10;
                    int num9 = input.readBits(num4) + 1;
                    for (int k = 0; k < num9; k++)
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
    public class CellInformation_List
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
    public class AS_SecurityInformation
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string key_eNodeB_star { get; set; }

        public long nextHopChainingCount { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public AS_SecurityInformation Decode(BitArrayInputStream input)
            {
                AS_SecurityInformation information = new AS_SecurityInformation();
                information.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.skipUnreadedBits();
                information.key_eNodeB_star = input.readBitString(0x100);
                information.nextHopChainingCount = input.readBits(3);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    information.iE_Extensions = new List<ProtocolExtensionField>();
                    int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        information.iE_Extensions.Add(item);
                    }
                }
                return information;
            }
        }
    }

    [Serializable]
    public class LocationReportingInformation
    {
        public void InitDefaults()
        {
        }

        public EventType eventType { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public ReportArea reportArea { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public LocationReportingInformation Decode(BitArrayInputStream input)
            {
                LocationReportingInformation information = new LocationReportingInformation();
                information.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                int nBits = 1;
                information.eventType = (EventType)input.readBits(nBits);
                nBits = 1;
                information.reportArea = (ReportArea)input.readBits(nBits);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    information.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        information.iE_Extensions.Add(item);
                    }
                }
                return information;
            }
        }
    }

}
