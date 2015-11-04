using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.X2ap
{
    [Serializable]
    public class RelativeNarrowbandTxPower
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public numberOfCellSpecificAntennaPorts_Enum numberOfCellSpecificAntennaPorts { get; set; }

        public long p_B { get; set; }

        public long pDCCH_InterferenceImpact { get; set; }

        public string rNTP_PerPRB { get; set; }

        public RNTP_Threshold rNTP_Threshold { get; set; }

        public enum numberOfCellSpecificAntennaPorts_Enum
        {
            one,
            two,
            four
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RelativeNarrowbandTxPower Decode(BitArrayInputStream input)
            {
                RelativeNarrowbandTxPower power = new RelativeNarrowbandTxPower();
                power.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.readBit();
                int num = input.readBits(7);
                input.skipUnreadedBits();
                power.rNTP_PerPRB = input.readBitString(num + 6);
                int nBits = (input.readBit() == 0) ? 4 : 4;
                power.rNTP_Threshold = (RNTP_Threshold)input.readBits(nBits);
                nBits = (input.readBit() == 0) ? 2 : 2;
                power.numberOfCellSpecificAntennaPorts = (numberOfCellSpecificAntennaPorts_Enum)input.readBits(nBits);
                power.p_B = input.readBits(2);
                power.pDCCH_InterferenceImpact = input.readBits(3);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    power.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        power.iE_Extensions.Add(item);
                    }
                }
                return power;
            }
        }
    }

    [Serializable]
    public class AllocationAndRetentionPriority
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public Pre_emptionCapability pre_emptionCapability { get; set; }

        public Pre_emptionVulnerability pre_emptionVulnerability { get; set; }

        public long priorityLevel { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public AllocationAndRetentionPriority Decode(BitArrayInputStream input)
            {
                AllocationAndRetentionPriority priority = new AllocationAndRetentionPriority();
                priority.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                priority.priorityLevel = input.readBits(4);
                int nBits = 1;
                priority.pre_emptionCapability = (Pre_emptionCapability)input.readBits(nBits);
                nBits = 1;
                priority.pre_emptionVulnerability = (Pre_emptionVulnerability)input.readBits(nBits);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    priority.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        priority.iE_Extensions.Add(item);
                    }
                }
                return priority;
            }
        }
    }

    [Serializable]
    public class GBR_QosInformation
    {
        public void InitDefaults()
        {
        }

        public long e_RAB_GuaranteedBitrateDL { get; set; }

        public long e_RAB_GuaranteedBitrateUL { get; set; }

        public long e_RAB_MaximumBitrateDL { get; set; }

        public long e_RAB_MaximumBitrateUL { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public GBR_QosInformation Decode(BitArrayInputStream input)
            {
                GBR_QosInformation information = new GBR_QosInformation();
                information.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                int nBits = input.readBits(3) + 1;
                input.skipUnreadedBits();
                information.e_RAB_MaximumBitrateDL = input.readBits(nBits * 8);
                nBits = input.readBits(3) + 1;
                input.skipUnreadedBits();
                information.e_RAB_MaximumBitrateUL = input.readBits(nBits * 8);
                nBits = input.readBits(3) + 1;
                input.skipUnreadedBits();
                information.e_RAB_GuaranteedBitrateDL = input.readBits(nBits * 8);
                nBits = input.readBits(3) + 1;
                input.skipUnreadedBits();
                information.e_RAB_GuaranteedBitrateUL = input.readBits(nBits * 8);
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

    [Serializable]
    public class GTPtunnelEndpoint
    {
        public void InitDefaults()
        {
        }

        public string gTP_TEID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string transportLayerAddress { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public GTPtunnelEndpoint Decode(BitArrayInputStream input)
            {
                GTPtunnelEndpoint endpoint = new GTPtunnelEndpoint();
                endpoint.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.readBit();
                int num = input.readBits(8);
                input.skipUnreadedBits();
                endpoint.transportLayerAddress = input.readBitString(num + 1);
                input.skipUnreadedBits();
                endpoint.gTP_TEID = input.readOctetString(4);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    endpoint.iE_Extensions = new List<ProtocolExtensionField>();
                    const int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        endpoint.iE_Extensions.Add(item);
                    }
                }
                return endpoint;
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
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.skipUnreadedBits();
                tvalue.pDCP_SN = input.readBits(0x10);
                int nBits = input.readBits(2) + 1;
                input.skipUnreadedBits();
                tvalue.hFN = input.readBits(nBits * 8);
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

    [Serializable]
    public class COUNTValueExtended
    {
        public void InitDefaults()
        {
        }

        public long hFNModified { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public long pDCP_SNExtended { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public COUNTValueExtended Decode(BitArrayInputStream input)
            {
                COUNTValueExtended extended = new COUNTValueExtended();
                extended.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                extended.pDCP_SNExtended = input.readBits(15);
                int nBits = input.readBits(2) + 1;
                input.skipUnreadedBits();
                extended.hFNModified = input.readBits(nBits * 8);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    extended.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        extended.iE_Extensions.Add(item);
                    }
                }
                return extended;
            }
        }
    }

}
