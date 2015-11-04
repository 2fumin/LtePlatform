using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class E_RAB_ID
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                input.readBit();
                return input.readBits(4);
            }
        }
    }

    [Serializable]
    public class E_RABAdmittedItem
    {
        public void InitDefaults()
        {
        }

        public string dL_gTP_TEID { get; set; }

        public string dL_transportLayerAddress { get; set; }

        public long e_RAB_ID { get; set; }

        public string gTP_TEID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string transportLayerAddress { get; set; }

        public string uL_GTP_TEID { get; set; }

        public string uL_TransportLayerAddress { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABAdmittedItem Decode(BitArrayInputStream input)
            {
                E_RABAdmittedItem item = new E_RABAdmittedItem();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 5) : new BitMaskStream(input, 5);
                input.readBit();
                item.e_RAB_ID = input.readBits(4);
                input.readBit();
                int num = input.readBits(8);
                input.skipUnreadedBits();
                item.transportLayerAddress = input.readBitString(num + 1);
                input.skipUnreadedBits();
                item.gTP_TEID = input.readOctetString(4);
                if (stream.Read())
                {
                    input.readBit();
                    num = input.readBits(8);
                    input.skipUnreadedBits();
                    item.dL_transportLayerAddress = input.readBitString(num + 1);
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    item.dL_gTP_TEID = input.readOctetString(4);
                }
                if (stream.Read())
                {
                    input.readBit();
                    num = input.readBits(8);
                    input.skipUnreadedBits();
                    item.uL_TransportLayerAddress = input.readBitString(num + 1);
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    item.uL_GTP_TEID = input.readOctetString(4);
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
    public class E_RABDataForwardingItem
    {
        public void InitDefaults()
        {
        }

        public string dL_gTP_TEID { get; set; }

        public string dL_transportLayerAddress { get; set; }

        public long e_RAB_ID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string uL_GTP_TEID { get; set; }

        public string uL_TransportLayerAddress { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABDataForwardingItem Decode(BitArrayInputStream input)
            {
                int num;
                E_RABDataForwardingItem item = new E_RABDataForwardingItem();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 5) : new BitMaskStream(input, 5);
                input.readBit();
                item.e_RAB_ID = input.readBits(4);
                if (stream.Read())
                {
                    input.readBit();
                    num = input.readBits(8);
                    input.skipUnreadedBits();
                    item.dL_transportLayerAddress = input.readBitString(num + 1);
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    item.dL_gTP_TEID = input.readOctetString(4);
                }
                if (stream.Read())
                {
                    input.readBit();
                    num = input.readBits(8);
                    input.skipUnreadedBits();
                    item.uL_TransportLayerAddress = input.readBitString(num + 1);
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    item.uL_GTP_TEID = input.readOctetString(4);
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
    public class E_RABFailedToSetupItemHOReqAck
    {
        public void InitDefaults()
        {
        }

        public Cause cause { get; set; }

        public long e_RAB_ID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABFailedToSetupItemHOReqAck Decode(BitArrayInputStream input)
            {
                E_RABFailedToSetupItemHOReqAck ack = new E_RABFailedToSetupItemHOReqAck();
                ack.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.readBit();
                ack.e_RAB_ID = input.readBits(4);
                ack.cause = Cause.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    ack.iE_Extensions = new List<ProtocolExtensionField>();
                    int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        ack.iE_Extensions.Add(item);
                    }
                }
                return ack;
            }
        }
    }

    [Serializable]
    public class E_RABInformationList
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
    public class E_RABInformationListItem
    {
        public void InitDefaults()
        {
        }

        public DL_Forwarding? dL_Forwarding { get; set; }

        public long e_RAB_ID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABInformationListItem Decode(BitArrayInputStream input)
            {
                int num4;
                E_RABInformationListItem item = new E_RABInformationListItem();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                input.readBit();
                item.e_RAB_ID = input.readBits(4);
                if (stream.Read())
                {
                    num4 = 1;
                    item.dL_Forwarding = (DL_Forwarding)input.readBits(num4);
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    item.iE_Extensions = new List<ProtocolExtensionField>();
                    num4 = 0x10;
                    int num5 = input.readBits(num4) + 1;
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
    public class E_RABItem
    {
        public void InitDefaults()
        {
        }

        public Cause cause { get; set; }

        public long e_RAB_ID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABItem Decode(BitArrayInputStream input)
            {
                E_RABItem item = new E_RABItem();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.readBit();
                item.e_RAB_ID = input.readBits(4);
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
    public class E_RABLevelQoSParameters
    {
        public void InitDefaults()
        {
        }

        public AllocationAndRetentionPriority allocationRetentionPriority { get; set; }

        public GBR_QosInformation gbrQosInformation { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public long qCI { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABLevelQoSParameters Decode(BitArrayInputStream input)
            {
                E_RABLevelQoSParameters parameters = new E_RABLevelQoSParameters();
                parameters.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                input.skipUnreadedBits();
                parameters.qCI = input.readBits(8);
                parameters.allocationRetentionPriority = AllocationAndRetentionPriority.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    parameters.gbrQosInformation = GBR_QosInformation.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    parameters.iE_Extensions = new List<ProtocolExtensionField>();
                    int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        parameters.iE_Extensions.Add(item);
                    }
                }
                return parameters;
            }
        }
    }

    [Serializable]
    public class E_RABList
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
}
