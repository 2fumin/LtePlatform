using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.X2ap
{
    [Serializable]
    public class CompleteFailureCauseInformation_Item
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

            public CompleteFailureCauseInformation_Item Decode(BitArrayInputStream input)
            {
                CompleteFailureCauseInformation_Item item = new CompleteFailureCauseInformation_Item();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                item.cell_ID = ECGI.PerDecoder.Instance.Decode(input);
                item.measurementFailureCause_List = new List<ProtocolIE_Field>();
                int nBits = 5;
                int num5 = input.readBits(nBits) + 1;
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field field = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    item.measurementFailureCause_List.Add(field);
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    item.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num7 = input.readBits(nBits) + 1;
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
    public class CompleteFailureCauseInformation_List
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
    public class CompositeAvailableCapacity
    {
        public void InitDefaults()
        {
        }

        public long capacityValue { get; set; }

        public long? cellCapacityClassValue { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CompositeAvailableCapacity Decode(BitArrayInputStream input)
            {
                CompositeAvailableCapacity capacity = new CompositeAvailableCapacity();
                capacity.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    input.readBit();
                    capacity.cellCapacityClassValue = input.readBits(7) + 1;
                }
                capacity.capacityValue = input.readBits(7);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    capacity.iE_Extensions = new List<ProtocolExtensionField>();
                    const int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        capacity.iE_Extensions.Add(item);
                    }
                }
                return capacity;
            }
        }
    }

    [Serializable]
    public class CompositeAvailableCapacityGroup
    {
        public void InitDefaults()
        {
        }

        public CompositeAvailableCapacity dL_CompositeAvailableCapacity { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public CompositeAvailableCapacity uL_CompositeAvailableCapacity { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CompositeAvailableCapacityGroup Decode(BitArrayInputStream input)
            {
                CompositeAvailableCapacityGroup group = new CompositeAvailableCapacityGroup();
                group.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                group.dL_CompositeAvailableCapacity = CompositeAvailableCapacity.PerDecoder.Instance.Decode(input);
                group.uL_CompositeAvailableCapacity = CompositeAvailableCapacity.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    group.iE_Extensions = new List<ProtocolExtensionField>();
                    const int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        group.iE_Extensions.Add(item);
                    }
                }
                return group;
            }
        }
    }

    [Serializable]
    public class Cause
    {
        public void InitDefaults()
        {
        }

        public CauseMisc misc { get; set; }

        public CauseProtocol protocol { get; set; }

        public CauseRadioNetwork radioNetwork { get; set; }

        public CauseTransport transport { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public Cause Decode(BitArrayInputStream input)
            {
                int num4;
                Cause cause = new Cause();
                cause.InitDefaults();
                input.readBit();
                switch (input.readBits(2))
                {
                    case 0:
                        num4 = (input.readBit() == 0) ? 5 : 5;
                        cause.radioNetwork = (CauseRadioNetwork)input.readBits(num4);
                        return cause;

                    case 1:
                        num4 = 1;
                        cause.transport = (CauseTransport)input.readBits(num4);
                        return cause;

                    case 2:
                        num4 = (input.readBit() == 0) ? 3 : 3;
                        cause.protocol = (CauseProtocol)input.readBits(num4);
                        return cause;

                    case 3:
                        num4 = (input.readBit() == 0) ? 3 : 3;
                        cause.misc = (CauseMisc)input.readBits(num4);
                        return cause;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

}
