using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
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
    public class UE_S1AP_ID_pair
    {
        public void InitDefaults()
        {
        }

        public long eNB_UE_S1AP_ID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public long mME_UE_S1AP_ID { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UE_S1AP_ID_pair Decode(BitArrayInputStream input)
            {
                UE_S1AP_ID_pair _pair = new UE_S1AP_ID_pair();
                _pair.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                int nBits = input.readBits(2) + 1;
                input.skipUnreadedBits();
                _pair.mME_UE_S1AP_ID = input.readBits(nBits * 8);
                nBits = input.readBits(2) + 1;
                input.skipUnreadedBits();
                _pair.eNB_UE_S1AP_ID = input.readBits(nBits * 8);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    _pair.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        _pair.iE_Extensions.Add(item);
                    }
                }
                return _pair;
            }
        }
    }

    [Serializable]
    public class UE_S1AP_IDs
    {
        public void InitDefaults()
        {
        }

        public long mME_UE_S1AP_ID { get; set; }

        public UE_S1AP_ID_pair uE_S1AP_ID_pair { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UE_S1AP_IDs Decode(BitArrayInputStream input)
            {
                UE_S1AP_IDs ds = new UE_S1AP_IDs();
                ds.InitDefaults();
                input.readBit();
                switch (input.readBits(1))
                {
                    case 0:
                        ds.uE_S1AP_ID_pair = UE_S1AP_ID_pair.PerDecoder.Instance.Decode(input);
                        return ds;

                    case 1:
                        {
                            int num4 = input.readBits(2) + 1;
                            input.skipUnreadedBits();
                            ds.mME_UE_S1AP_ID = input.readBits(num4 * 8);
                            return ds;
                        }
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

    [Serializable]
    public class UEAggregateMaximumBitrate
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public long uEaggregateMaximumBitRateDL { get; set; }

        public long uEaggregateMaximumBitRateUL { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UEAggregateMaximumBitrate Decode(BitArrayInputStream input)
            {
                UEAggregateMaximumBitrate bitrate = new UEAggregateMaximumBitrate();
                bitrate.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                int nBits = input.readBits(3) + 1;
                input.skipUnreadedBits();
                bitrate.uEaggregateMaximumBitRateDL = input.readBits(nBits * 8);
                nBits = input.readBits(3) + 1;
                input.skipUnreadedBits();
                bitrate.uEaggregateMaximumBitRateUL = input.readBits(nBits * 8);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    bitrate.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        bitrate.iE_Extensions.Add(item);
                    }
                }
                return bitrate;
            }
        }
    }

    [Serializable]
    public class UECapabilityInfoIndication
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UECapabilityInfoIndication Decode(BitArrayInputStream input)
            {
                UECapabilityInfoIndication indication = new UECapabilityInfoIndication();
                indication.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                indication.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    indication.protocolIEs.Add(item);
                }
                return indication;
            }
        }
    }

    [Serializable]
    public class UEIdentityIndexValue
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                return input.readBitString(10);
            }
        }
    }

    [Serializable]
    public class UEPagingID
    {
        public void InitDefaults()
        {
        }

        public string iMSI { get; set; }

        public S_TMSI s_TMSI { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UEPagingID Decode(BitArrayInputStream input)
            {
                UEPagingID gid = new UEPagingID();
                gid.InitDefaults();
                input.readBit();
                switch (input.readBits(1))
                {
                    case 0:
                        gid.s_TMSI = S_TMSI.PerDecoder.Instance.Decode(input);
                        return gid;

                    case 1:
                        int num = input.readBits(3);
                        input.skipUnreadedBits();
                        gid.iMSI = input.readOctetString(num + 3);
                        return gid;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

    [Serializable]
    public class UERadioCapability
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
                capabilities.encryptionAlgorithms = input.readBitString(0x10);
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

    [Serializable]
    public class MSClassmark2
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
    public class MSClassmark3
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
    public class SubscriberProfileIDforRFP
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                return input.readBits(8) + 1;
            }
        }
    }

}
