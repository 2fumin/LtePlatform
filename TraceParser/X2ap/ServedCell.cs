using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.X2ap
{
    [Serializable]
    public class ServedCell_Information
    {
        public void InitDefaults()
        {
        }

        public List<string> broadcastPLMNs { get; set; }

        public ECGI cellId { get; set; }

        public EUTRA_Mode_Info eUTRA_Mode_Info { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public long pCI { get; set; }

        public string tAC { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ServedCell_Information Decode(BitArrayInputStream input)
            {
                ServedCell_Information information = new ServedCell_Information();
                information.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.readBit();
                input.skipUnreadedBits();
                information.pCI = input.readBits(0x10);
                information.cellId = ECGI.PerDecoder.Instance.Decode(input);
                input.skipUnreadedBits();
                information.tAC = input.readOctetString(2);
                information.broadcastPLMNs = new List<string>();
                int nBits = 3;
                int num5 = input.readBits(nBits) + 1;
                for (int i = 0; i < num5; i++)
                {
                    input.skipUnreadedBits();
                    string item = input.readOctetString(3);
                    information.broadcastPLMNs.Add(item);
                }
                information.eUTRA_Mode_Info = EUTRA_Mode_Info.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    information.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num7 = input.readBits(nBits) + 1;
                    for (int j = 0; j < num7; j++)
                    {
                        ProtocolExtensionField field = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        information.iE_Extensions.Add(field);
                    }
                }
                return information;
            }
        }
    }

    [Serializable]
    public class ServedCells
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<ServedCells_Element> Decode(BitArrayInputStream input)
            {
                return new List<ServedCells_Element>();
            }
        }
    }

    [Serializable]
    public class ServedCells_Element
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public List<Neighbour_Information_Element> neighbour_Info { get; set; }

        public ServedCell_Information servedCellInfo { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ServedCells_Element Decode(BitArrayInputStream input)
            {
                int num4;
                ServedCells_Element element = new ServedCells_Element();
                element.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                element.servedCellInfo = ServedCell_Information.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    element.neighbour_Info = new List<Neighbour_Information_Element>();
                    num4 = 10;
                    int num5 = input.readBits(num4);
                    for (int i = 0; i < num5; i++)
                    {
                        Neighbour_Information_Element item = Neighbour_Information_Element.PerDecoder.Instance.Decode(input);
                        element.neighbour_Info.Add(item);
                    }
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    element.iE_Extensions = new List<ProtocolExtensionField>();
                    num4 = 0x10;
                    int num7 = input.readBits(num4) + 1;
                    for (int j = 0; j < num7; j++)
                    {
                        ProtocolExtensionField field = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        element.iE_Extensions.Add(field);
                    }
                }
                return element;
            }
        }
    }

    [Serializable]
    public class ServedCellsToActivate
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<ServedCellsToActivate_Item> Decode(BitArrayInputStream input)
            {
                return new List<ServedCellsToActivate_Item>();
            }
        }
    }

    [Serializable]
    public class ServedCellsToActivate_Item
    {
        public void InitDefaults()
        {
        }

        public ECGI ecgi { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ServedCellsToActivate_Item Decode(BitArrayInputStream input)
            {
                ServedCellsToActivate_Item item = new ServedCellsToActivate_Item();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                item.ecgi = ECGI.PerDecoder.Instance.Decode(input);
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
    public class ServedCellsToModify
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<ServedCellsToModify_Item> Decode(BitArrayInputStream input)
            {
                return new List<ServedCellsToModify_Item>();
            }
        }
    }

    [Serializable]
    public class ServedCellsToModify_Item
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public List<Neighbour_Information_Element> neighbour_Info { get; set; }

        public ECGI old_ecgi { get; set; }

        public ServedCell_Information servedCellInfo { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ServedCellsToModify_Item Decode(BitArrayInputStream input)
            {
                int num4;
                ServedCellsToModify_Item item = new ServedCellsToModify_Item();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                item.old_ecgi = ECGI.PerDecoder.Instance.Decode(input);
                item.servedCellInfo = ServedCell_Information.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    item.neighbour_Info = new List<Neighbour_Information_Element>();
                    num4 = 10;
                    int num5 = input.readBits(num4);
                    for (int i = 0; i < num5; i++)
                    {
                        Neighbour_Information_Element element = Neighbour_Information_Element.PerDecoder.Instance.Decode(input);
                        item.neighbour_Info.Add(element);
                    }
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    item.iE_Extensions = new List<ProtocolExtensionField>();
                    num4 = 0x10;
                    int num7 = input.readBits(num4) + 1;
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
    public class Neighbour_Information
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<Neighbour_Information_Element> Decode(BitArrayInputStream input)
            {
                return new List<Neighbour_Information_Element>();
            }
        }
    }

    [Serializable]
    public class Neighbour_Information_Element
    {
        public void InitDefaults()
        {
        }

        public long eARFCN { get; set; }

        public ECGI eCGI { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public long pCI { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public Neighbour_Information_Element Decode(BitArrayInputStream input)
            {
                Neighbour_Information_Element element = new Neighbour_Information_Element();
                element.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                element.eCGI = ECGI.PerDecoder.Instance.Decode(input);
                input.readBit();
                input.skipUnreadedBits();
                element.pCI = input.readBits(0x10);
                int nBits = input.readBits(1) + 1;
                input.skipUnreadedBits();
                element.eARFCN = input.readBits(nBits * 8);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    element.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        element.iE_Extensions.Add(item);
                    }
                }
                return element;
            }
        }
    }

}
