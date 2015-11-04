using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.X2ap
{
    [Serializable]
    public class E_RAB_Item
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

            public E_RAB_Item Decode(BitArrayInputStream input)
            {
                E_RAB_Item item = new E_RAB_Item();
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
    public class E_RAB_Level_QoS_Parameters
    {
        public void InitDefaults()
        {
        }

        public AllocationAndRetentionPriority allocationAndRetentionPriority { get; set; }

        public GBR_QosInformation gbrQosInformation { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public long qCI { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RAB_Level_QoS_Parameters Decode(BitArrayInputStream input)
            {
                E_RAB_Level_QoS_Parameters parameters = new E_RAB_Level_QoS_Parameters();
                parameters.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                input.skipUnreadedBits();
                parameters.qCI = input.readBits(8);
                parameters.allocationAndRetentionPriority = AllocationAndRetentionPriority.PerDecoder.Instance.Decode(input);
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
    public class E_RAB_List
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
    public class E_RABs_Admitted_Item
    {
        public void InitDefaults()
        {
        }

        public GTPtunnelEndpoint dL_GTP_TunnelEndpoint { get; set; }

        public long e_RAB_ID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public GTPtunnelEndpoint uL_GTP_TunnelEndpoint { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABs_Admitted_Item Decode(BitArrayInputStream input)
            {
                E_RABs_Admitted_Item item = new E_RABs_Admitted_Item();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 3) : new BitMaskStream(input, 3);
                input.readBit();
                item.e_RAB_ID = input.readBits(4);
                if (stream.Read())
                {
                    item.uL_GTP_TunnelEndpoint = GTPtunnelEndpoint.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    item.dL_GTP_TunnelEndpoint = GTPtunnelEndpoint.PerDecoder.Instance.Decode(input);
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
    public class E_RABs_Admitted_List
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
    public class E_RABs_SubjectToStatusTransfer_Item
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

            public E_RABs_SubjectToStatusTransfer_Item Decode(BitArrayInputStream input)
            {
                E_RABs_SubjectToStatusTransfer_Item item = new E_RABs_SubjectToStatusTransfer_Item();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                input.readBit();
                item.e_RAB_ID = input.readBits(4);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    item.receiveStatusofULPDCPSDUs = input.readBitString(0x1000);
                }
                item.uL_COUNTvalue = COUNTvalue.PerDecoder.Instance.Decode(input);
                item.dL_COUNTvalue = COUNTvalue.PerDecoder.Instance.Decode(input);
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
    public class E_RABs_SubjectToStatusTransfer_List
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
    public class E_RABs_ToBeSetup_Item
    {
        public void InitDefaults()
        {
        }

        public DL_Forwarding? dL_Forwarding { get; set; }

        public long e_RAB_ID { get; set; }

        public E_RAB_Level_QoS_Parameters e_RAB_Level_QoS_Parameters { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public GTPtunnelEndpoint uL_GTPtunnelEndpoint { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABs_ToBeSetup_Item Decode(BitArrayInputStream input)
            {
                int num4;
                E_RABs_ToBeSetup_Item item = new E_RABs_ToBeSetup_Item();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                input.readBit();
                item.e_RAB_ID = input.readBits(4);
                item.e_RAB_Level_QoS_Parameters = E_RAB_Level_QoS_Parameters.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    num4 = 1;
                    item.dL_Forwarding = (DL_Forwarding)input.readBits(num4);
                }
                item.uL_GTPtunnelEndpoint = GTPtunnelEndpoint.PerDecoder.Instance.Decode(input);
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
    public class E_RABs_ToBeSetup_List
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
