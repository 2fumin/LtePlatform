using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.X2ap
{
    [Serializable]
    public class UE_ContextInformation
    {
        public void InitDefaults()
        {
        }

        public AS_SecurityInformation aS_SecurityInformation { get; set; }

        public List<ProtocolIE_Field> e_RABs_ToBeSetup_List { get; set; }

        public HandoverRestrictionList handoverRestrictionList { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public LocationReportingInformation locationReportingInformation { get; set; }

        public long mME_UE_S1AP_ID { get; set; }

        public string rRC_Context { get; set; }

        public long? subscriberProfileIDforRFP { get; set; }

        public UEAggregateMaximumBitRate uEaggregateMaximumBitRate { get; set; }

        public UESecurityCapabilities uESecurityCapabilities { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UE_ContextInformation Decode(BitArrayInputStream input)
            {
                UE_ContextInformation information = new UE_ContextInformation();
                information.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 4) : new BitMaskStream(input, 4);
                int num4 = input.readBits(2) + 1;
                input.skipUnreadedBits();
                information.mME_UE_S1AP_ID = input.readBits(num4 * 8);
                information.uESecurityCapabilities = UESecurityCapabilities.PerDecoder.Instance.Decode(input);
                information.aS_SecurityInformation = AS_SecurityInformation.PerDecoder.Instance.Decode(input);
                information.uEaggregateMaximumBitRate = UEAggregateMaximumBitRate.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    information.subscriberProfileIDforRFP = input.readBits(8) + 1;
                }
                input.skipUnreadedBits();
                information.e_RABs_ToBeSetup_List = new List<ProtocolIE_Field>();
                num4 = 8;
                int num5 = input.readBits(num4) + 1;
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    information.e_RABs_ToBeSetup_List.Add(item);
                }
                input.skipUnreadedBits();
                int nBits = 0;
                while (true)
                {
                    switch (input.readBit())
                    {
                        case 0:
                            nBits += input.readBits(7);
                            goto Label_01B1;

                        case 1:
                            switch (input.readBit())
                            {
                                case 0:
                                    nBits += input.readBits(14);
                                    goto Label_01B1;

                                case 1:
                                    input.readBits(2);
                                    nBits += input.readBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_01B1:
                information.rRC_Context = input.readOctetString(nBits);
                if (stream.Read())
                {
                    information.handoverRestrictionList = HandoverRestrictionList.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    information.locationReportingInformation 
                        = LocationReportingInformation.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    information.iE_Extensions = new List<ProtocolExtensionField>();
                    num4 = 0x10;
                    int num7 = input.readBits(num4) + 1;
                    for (int j = 0; j < num7; j++)
                    {
                        ProtocolExtensionField field2 = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        information.iE_Extensions.Add(field2);
                    }
                }
                return information;
            }
        }
    }

    [Serializable]
    public class UE_HistoryInformation
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<LastVisitedCell_Item> Decode(BitArrayInputStream input)
            {
                return new List<LastVisitedCell_Item>();
            }
        }
    }

    [Serializable]
    public class UE_HistoryInformationFromTheUE
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                int nBits = 0;
                while (true)
                {
                    switch (input.readBit())
                    {
                        case 0:
                            nBits += input.readBits(7);
                            goto Label_0096;

                        case 1:
                            switch (input.readBit())
                            {
                                case 0:
                                    nBits += input.readBits(14);
                                    goto Label_0096;

                                case 1:
                                    input.readBits(2);
                                    nBits += input.readBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_0096:
                return input.readOctetString(nBits);
            }
        }
    }

    [Serializable]
    public class UE_RLF_Report_Container
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                int nBits = 0;
                while (true)
                {
                    switch (input.readBit())
                    {
                        case 0:
                            nBits += input.readBits(7);
                            goto Label_0096;

                        case 1:
                            switch (input.readBit())
                            {
                                case 0:
                                    nBits += input.readBits(14);
                                    goto Label_0096;

                                case 1:
                                    input.readBits(2);
                                    nBits += input.readBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_0096:
                return input.readOctetString(nBits);
            }
        }
    }

    [Serializable]
    public class UE_S1AP_ID
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                int num2 = input.readBits(2) + 1;
                input.skipUnreadedBits();
                return input.readBits(num2 * 8);
            }
        }
    }

    [Serializable]
    public class UE_X2AP_ID
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                return input.readBits(0x10);
            }
        }
    }

    [Serializable]
    public class UEAggregateMaximumBitRate
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public long uEaggregateMaximumBitRateDownlink { get; set; }

        public long uEaggregateMaximumBitRateUplink { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UEAggregateMaximumBitRate Decode(BitArrayInputStream input)
            {
                UEAggregateMaximumBitRate rate = new UEAggregateMaximumBitRate();
                rate.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                int nBits = input.readBits(3) + 1;
                input.skipUnreadedBits();
                rate.uEaggregateMaximumBitRateDownlink = input.readBits(nBits * 8);
                nBits = input.readBits(3) + 1;
                input.skipUnreadedBits();
                rate.uEaggregateMaximumBitRateUplink = input.readBits(nBits * 8);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    rate.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        rate.iE_Extensions.Add(item);
                    }
                }
                return rate;
            }
        }
    }

    [Serializable]
    public class UEContextRelease
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UEContextRelease Decode(BitArrayInputStream input)
            {
                UEContextRelease release = new UEContextRelease();
                release.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                release.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    release.protocolIEs.Add(item);
                }
                return release;
            }
        }
    }

    [Serializable]
    public class UESecurityCapabilities
    {
        public void InitDefaults()
        {
        }

        public string encryptionAlgorithms { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string integrityProtectionAlgorithms { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UESecurityCapabilities Decode(BitArrayInputStream input)
            {
                UESecurityCapabilities capabilities = new UESecurityCapabilities();
                capabilities.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.readBit();
                capabilities.encryptionAlgorithms = input.readBitString(0x10);
                input.readBit();
                capabilities.integrityProtectionAlgorithms = input.readBitString(0x10);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    capabilities.iE_Extensions = new List<ProtocolExtensionField>();
                    int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        capabilities.iE_Extensions.Add(item);
                    }
                }
                return capabilities;
            }
        }
    }

}
