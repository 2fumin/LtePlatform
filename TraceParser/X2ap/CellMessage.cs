using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.X2ap
{
    [Serializable]
    public class CellMeasurementResult_Item
    {
        public void InitDefaults()
        {
        }

        public ECGI cell_ID { get; set; }

        public HWLoadIndicator hWOverLoadIndicator { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public RadioResourceStatus radioResourceStatus { get; set; }

        public S1TNLLoadIndicator s1TNLOverLoadIndicator { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellMeasurementResult_Item Decode(BitArrayInputStream input)
            {
                CellMeasurementResult_Item item = new CellMeasurementResult_Item();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 4) : new BitMaskStream(input, 4);
                item.cell_ID = ECGI.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    item.hWOverLoadIndicator = HWLoadIndicator.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    item.s1TNLOverLoadIndicator = S1TNLLoadIndicator.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    item.radioResourceStatus = RadioResourceStatus.PerDecoder.Instance.Decode(input);
                }
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
    public class CellMeasurementResult_List
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
    public class CellToReport_Item
    {
        public void InitDefaults()
        {
        }

        public ECGI cell_ID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellToReport_Item Decode(BitArrayInputStream input)
            {
                CellToReport_Item item = new CellToReport_Item();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                item.cell_ID = ECGI.PerDecoder.Instance.Decode(input);
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
    public class CellToReport_List
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
    public class CellType
    {
        public void InitDefaults()
        {
        }

        public Cell_Size cell_Size { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellType Decode(BitArrayInputStream input)
            {
                CellType type = new CellType();
                type.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                int nBits = (input.readBit() == 0) ? 2 : 2;
                type.cell_Size = (Cell_Size)input.readBits(nBits);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    type.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        type.iE_Extensions.Add(item);
                    }
                }
                return type;
            }
        }
    }

}
