using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.X2ap
{
    [Serializable]
    public class Measurement_ID
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                input.readBit();
                input.skipUnreadedBits();
                return input.readBits(0x10) + 1;
            }
        }
    }

    [Serializable]
    public class MeasurementFailureCause_Item
    {
        public void InitDefaults()
        {
        }

        public Cause cause { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string measurementFailedReportCharacteristics { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasurementFailureCause_Item Decode(BitArrayInputStream input)
            {
                MeasurementFailureCause_Item item = new MeasurementFailureCause_Item();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                item.measurementFailedReportCharacteristics = input.readBitString(0x20);
                item.cause = Cause.PerDecoder.Instance.Decode(input);
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
    public class MeasurementFailureCause_List
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
    public class MeasurementInitiationResult_Item
    {
        public void InitDefaults()
        {
        }

        public ECGI cell_ID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public List<ProtocolIE_Field> measurementFailureCause_List { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasurementInitiationResult_Item Decode(BitArrayInputStream input)
            {
                int num4;
                MeasurementInitiationResult_Item item = new MeasurementInitiationResult_Item();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                item.cell_ID = ECGI.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    item.measurementFailureCause_List = new List<ProtocolIE_Field>();
                    num4 = 5;
                    int num5 = input.readBits(num4) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolIE_Field field = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                        item.measurementFailureCause_List.Add(field);
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
                        ProtocolExtensionField field2 = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        item.iE_Extensions.Add(field2);
                    }
                }
                return item;
            }
        }
    }

    [Serializable]
    public class MeasurementInitiationResult_List
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
    public class MeasurementsToActivate
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                return input.readBitString(8);
            }
        }
    }

    [Serializable]
    public class MeasurementThresholdA2
    {
        public void InitDefaults()
        {
        }

        public long threshold_RSRP { get; set; }

        public long threshold_RSRQ { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasurementThresholdA2 Decode(BitArrayInputStream input)
            {
                MeasurementThresholdA2 da = new MeasurementThresholdA2();
                da.InitDefaults();
                input.readBit();
                switch (input.readBits(1))
                {
                    case 0:
                        da.threshold_RSRP = input.readBits(7);
                        return da;

                    case 1:
                        da.threshold_RSRQ = input.readBits(6);
                        return da;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

    [Serializable]
    public class ReportCharacteristics
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                return input.readBitString(0x20);
            }
        }
    }

}
