using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class HandoverPreparationInformation
    {
        public void InitDefaults()
        {
        }

        public criticalExtensions_Type criticalExtensions { get; set; }

        [Serializable]
        public class criticalExtensions_Type
        {
            public void InitDefaults()
            {
            }

            public c1_Type c1 { get; set; }

            public criticalExtensionsFuture_Type criticalExtensionsFuture { get; set; }

            [Serializable]
            public class c1_Type
            {
                public void InitDefaults()
                {
                }

                public HandoverPreparationInformation_r8_IEs handoverPreparationInformation_r8 { get; set; }

                public object spare1 { get; set; }

                public object spare2 { get; set; }

                public object spare3 { get; set; }

                public object spare4 { get; set; }

                public object spare5 { get; set; }

                public object spare6 { get; set; }

                public object spare7 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public c1_Type Decode(BitArrayInputStream input)
                    {
                        c1_Type type = new c1_Type();
                        type.InitDefaults();
                        switch (input.readBits(3))
                        {
                            case 0:
                                type.handoverPreparationInformation_r8 = HandoverPreparationInformation_r8_IEs.PerDecoder.Instance.Decode(input);
                                return type;

                            case 1:
                                return type;

                            case 2:
                                return type;

                            case 3:
                                return type;

                            case 4:
                                return type;

                            case 5:
                                return type;

                            case 6:
                                return type;

                            case 7:
                                return type;
                        }
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                }
            }

            [Serializable]
            public class criticalExtensionsFuture_Type
            {
                public void InitDefaults()
                {
                }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public criticalExtensionsFuture_Type Decode(BitArrayInputStream input)
                    {
                        criticalExtensionsFuture_Type type = new criticalExtensionsFuture_Type();
                        type.InitDefaults();
                        return type;
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public criticalExtensions_Type Decode(BitArrayInputStream input)
                {
                    criticalExtensions_Type type = new criticalExtensions_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.c1 = c1_Type.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.criticalExtensionsFuture = criticalExtensionsFuture_Type.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverPreparationInformation Decode(BitArrayInputStream input)
            {
                HandoverPreparationInformation information = new HandoverPreparationInformation();
                information.InitDefaults();
                information.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return information;
            }
        }
    }

    [Serializable]
    public class HandoverPreparationInformation_r8_IEs
    {
        public void InitDefaults()
        {
        }

        public AS_Config as_Config { get; set; }

        public AS_Context as_Context { get; set; }

        public HandoverPreparationInformation_v920_IEs nonCriticalExtension { get; set; }

        public RRM_Config rrm_Config { get; set; }

        public List<UE_CapabilityRAT_Container> ue_RadioAccessCapabilityInfo { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance =
                new PerDecoder();

            public HandoverPreparationInformation_r8_IEs Decode(BitArrayInputStream input)
            {
                HandoverPreparationInformation_r8_IEs es = new HandoverPreparationInformation_r8_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 4);
                es.ue_RadioAccessCapabilityInfo = new List<UE_CapabilityRAT_Container>();
                const int nBits = 4;
                int num3 = input.readBits(nBits);
                for (int i = 0; i < num3; i++)
                {
                    UE_CapabilityRAT_Container item = UE_CapabilityRAT_Container.PerDecoder.Instance.Decode(input);
                    es.ue_RadioAccessCapabilityInfo.Add(item);
                }
                if (stream.Read())
                {
                    es.as_Config = AS_Config.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.rrm_Config = RRM_Config.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.as_Context = AS_Context.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = HandoverPreparationInformation_v920_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class HandoverPreparationInformation_v1130_IEs
    {
        public void InitDefaults()
        {
        }

        public AS_Context_v1130 as_Context_v1130 { get; set; }

        public HandoverPreparationInformation_v12xy_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverPreparationInformation_v1130_IEs Decode(BitArrayInputStream input)
            {
                HandoverPreparationInformation_v1130_IEs es = new HandoverPreparationInformation_v1130_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.as_Context_v1130 = AS_Context_v1130.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = HandoverPreparationInformation_v12xy_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class HandoverPreparationInformation_v12xy_IEs
    {
        public void InitDefaults()
        {
        }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        public long? ue_SupportedEARFCN_r12 { get; set; }

        [Serializable]
        public class nonCriticalExtension_Type
        {
            public void InitDefaults()
            {
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public nonCriticalExtension_Type Decode(BitArrayInputStream input)
                {
                    nonCriticalExtension_Type type = new nonCriticalExtension_Type();
                    type.InitDefaults();
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverPreparationInformation_v12xy_IEs Decode(BitArrayInputStream input)
            {
                HandoverPreparationInformation_v12xy_IEs es = new HandoverPreparationInformation_v12xy_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.ue_SupportedEARFCN_r12 = input.readBits(0x12);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = nonCriticalExtension_Type.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class HandoverPreparationInformation_v920_IEs
    {
        public void InitDefaults()
        {
        }

        public HandoverPreparationInformation_v9d0_IEs nonCriticalExtension { get; set; }

        public ue_ConfigRelease_r9_Enum? ue_ConfigRelease_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverPreparationInformation_v920_IEs Decode(BitArrayInputStream input)
            {
                HandoverPreparationInformation_v920_IEs es = new HandoverPreparationInformation_v920_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    int nBits = (input.readBit() == 0) ? 3 : 3;
                    es.ue_ConfigRelease_r9 = (ue_ConfigRelease_r9_Enum)input.readBits(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = HandoverPreparationInformation_v9d0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }

        public enum ue_ConfigRelease_r9_Enum
        {
            rel9,
            rel10,
            rel11,
            spare5,
            spare4,
            spare3,
            spare2,
            spare1
        }
    }

    [Serializable]
    public class HandoverPreparationInformation_v9d0_IEs
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public HandoverPreparationInformation_v9e0_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverPreparationInformation_v9d0_IEs Decode(BitArrayInputStream input)
            {
                HandoverPreparationInformation_v9d0_IEs es = new HandoverPreparationInformation_v9d0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    es.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = HandoverPreparationInformation_v9e0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class HandoverPreparationInformation_v9e0_IEs
    {
        public void InitDefaults()
        {
        }

        public AS_Config_v9e0 as_Config_v9e0 { get; set; }

        public HandoverPreparationInformation_v1130_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverPreparationInformation_v9e0_IEs Decode(BitArrayInputStream input)
            {
                HandoverPreparationInformation_v9e0_IEs es = new HandoverPreparationInformation_v9e0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.as_Config_v9e0 = AS_Config_v9e0.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = HandoverPreparationInformation_v1130_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class ULHandoverPreparationTransfer
    {
        public void InitDefaults()
        {
        }

        public criticalExtensions_Type criticalExtensions { get; set; }

        [Serializable]
        public class criticalExtensions_Type
        {
            public void InitDefaults()
            {
            }

            public c1_Type c1 { get; set; }

            public criticalExtensionsFuture_Type criticalExtensionsFuture { get; set; }

            [Serializable]
            public class c1_Type
            {
                public void InitDefaults()
                {
                }

                public object spare1 { get; set; }

                public object spare2 { get; set; }

                public object spare3 { get; set; }

                public ULHandoverPreparationTransfer_r8_IEs ulHandoverPreparationTransfer_r8 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public c1_Type Decode(BitArrayInputStream input)
                    {
                        c1_Type type = new c1_Type();
                        type.InitDefaults();
                        switch (input.readBits(2))
                        {
                            case 0:
                                type.ulHandoverPreparationTransfer_r8 = ULHandoverPreparationTransfer_r8_IEs.PerDecoder.Instance.Decode(input);
                                return type;

                            case 1:
                                return type;

                            case 2:
                                return type;

                            case 3:
                                return type;
                        }
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                }
            }

            [Serializable]
            public class criticalExtensionsFuture_Type
            {
                public void InitDefaults()
                {
                }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public criticalExtensionsFuture_Type Decode(BitArrayInputStream input)
                    {
                        criticalExtensionsFuture_Type type = new criticalExtensionsFuture_Type();
                        type.InitDefaults();
                        return type;
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public criticalExtensions_Type Decode(BitArrayInputStream input)
                {
                    criticalExtensions_Type type = new criticalExtensions_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.c1 = c1_Type.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.criticalExtensionsFuture = criticalExtensionsFuture_Type.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ULHandoverPreparationTransfer Decode(BitArrayInputStream input)
            {
                ULHandoverPreparationTransfer transfer = new ULHandoverPreparationTransfer();
                transfer.InitDefaults();
                transfer.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return transfer;
            }
        }
    }

    [Serializable]
    public class ULHandoverPreparationTransfer_r8_IEs
    {
        public void InitDefaults()
        {
        }

        public CDMA2000_Type cdma2000_Type { get; set; }

        public string dedicatedInfo { get; set; }

        public string meid { get; set; }

        public ULHandoverPreparationTransfer_v8a0_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ULHandoverPreparationTransfer_r8_IEs Decode(BitArrayInputStream input)
            {
                ULHandoverPreparationTransfer_r8_IEs es = new ULHandoverPreparationTransfer_r8_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                const int num2 = 1;
                es.cdma2000_Type = (CDMA2000_Type)input.readBits(num2);
                if (stream.Read())
                {
                    es.meid = input.readBitString(0x38);
                }
                int nBits = input.readBits(8);
                es.dedicatedInfo = input.readOctetString(nBits);
                if (stream.Read())
                {
                    es.nonCriticalExtension = ULHandoverPreparationTransfer_v8a0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class ULHandoverPreparationTransfer_v8a0_IEs
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        [Serializable]
        public class nonCriticalExtension_Type
        {
            public void InitDefaults()
            {
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public nonCriticalExtension_Type Decode(BitArrayInputStream input)
                {
                    nonCriticalExtension_Type type = new nonCriticalExtension_Type();
                    type.InitDefaults();
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ULHandoverPreparationTransfer_v8a0_IEs Decode(BitArrayInputStream input)
            {
                ULHandoverPreparationTransfer_v8a0_IEs es = new ULHandoverPreparationTransfer_v8a0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    es.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = nonCriticalExtension_Type.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

}
