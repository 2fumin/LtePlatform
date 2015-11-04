using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class UE_EUTRA_Capability
    {
        public void InitDefaults()
        {
        }

        public AccessStratumRelease accessStratumRelease { get; set; }

        public string featureGroupIndicators { get; set; }

        public interRAT_Parameters_Type interRAT_Parameters { get; set; }

        public MeasParameters measParameters { get; set; }

        public UE_EUTRA_Capability_v920_IEs nonCriticalExtension { get; set; }

        public PDCP_Parameters pdcp_Parameters { get; set; }

        public PhyLayerParameters phyLayerParameters { get; set; }

        public RF_Parameters rf_Parameters { get; set; }

        public long ue_Category { get; set; }

        [Serializable]
        public class interRAT_Parameters_Type
        {
            public void InitDefaults()
            {
            }

            public IRAT_ParametersCDMA2000_1XRTT cdma2000_1xRTT { get; set; }

            public IRAT_ParametersCDMA2000_HRPD cdma2000_HRPD { get; set; }

            public IRAT_ParametersGERAN geran { get; set; }

            public IRAT_ParametersUTRA_FDD utraFDD { get; set; }

            public IRAT_ParametersUTRA_TDD128 utraTDD128 { get; set; }

            public IRAT_ParametersUTRA_TDD384 utraTDD384 { get; set; }

            public IRAT_ParametersUTRA_TDD768 utraTDD768 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public interRAT_Parameters_Type Decode(BitArrayInputStream input)
                {
                    interRAT_Parameters_Type type = new interRAT_Parameters_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 7);
                    if (stream.Read())
                    {
                        type.utraFDD = IRAT_ParametersUTRA_FDD.PerDecoder.Instance.Decode(input);
                    }
                    if (stream.Read())
                    {
                        type.utraTDD128 = IRAT_ParametersUTRA_TDD128.PerDecoder.Instance.Decode(input);
                    }
                    if (stream.Read())
                    {
                        type.utraTDD384 = IRAT_ParametersUTRA_TDD384.PerDecoder.Instance.Decode(input);
                    }
                    if (stream.Read())
                    {
                        type.utraTDD768 = IRAT_ParametersUTRA_TDD768.PerDecoder.Instance.Decode(input);
                    }
                    if (stream.Read())
                    {
                        type.geran = IRAT_ParametersGERAN.PerDecoder.Instance.Decode(input);
                    }
                    if (stream.Read())
                    {
                        type.cdma2000_HRPD = IRAT_ParametersCDMA2000_HRPD.PerDecoder.Instance.Decode(input);
                    }
                    if (stream.Read())
                    {
                        type.cdma2000_1xRTT = IRAT_ParametersCDMA2000_1XRTT.PerDecoder.Instance.Decode(input);
                    }
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UE_EUTRA_Capability Decode(BitArrayInputStream input)
            {
                UE_EUTRA_Capability capability = new UE_EUTRA_Capability();
                capability.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                int nBits = (input.readBit() == 0) ? 3 : 3;
                capability.accessStratumRelease = (AccessStratumRelease)input.readBits(nBits);
                capability.ue_Category = input.readBits(3) + 1;
                capability.pdcp_Parameters = PDCP_Parameters.PerDecoder.Instance.Decode(input);
                capability.phyLayerParameters = PhyLayerParameters.PerDecoder.Instance.Decode(input);
                capability.rf_Parameters = RF_Parameters.PerDecoder.Instance.Decode(input);
                capability.measParameters = MeasParameters.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    capability.featureGroupIndicators = input.readBitString(0x20);
                }
                capability.interRAT_Parameters = interRAT_Parameters_Type.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    capability.nonCriticalExtension = UE_EUTRA_Capability_v920_IEs.PerDecoder.Instance.Decode(input);
                }
                return capability;
            }
        }
    }

    [Serializable]
    public class UE_EUTRA_Capability_v1020_IEs
    {
        public void InitDefaults()
        {
        }

        public string featureGroupIndRel10_r10 { get; set; }

        public IRAT_ParametersCDMA2000_1XRTT_v1020 interRAT_ParametersCDMA2000_v1020 { get; set; }

        public IRAT_ParametersUTRA_TDD_v1020 interRAT_ParametersUTRA_TDD_v1020 { get; set; }

        public MeasParameters_v1020 measParameters_v1020 { get; set; }

        public UE_EUTRA_Capability_v1060_IEs nonCriticalExtension { get; set; }

        public PhyLayerParameters_v1020 phyLayerParameters_v1020 { get; set; }

        public RF_Parameters_v1020 rf_Parameters_v1020 { get; set; }

        public UE_BasedNetwPerfMeasParameters_r10 ue_BasedNetwPerfMeasParameters_r10 { get; set; }

        public long? ue_Category_v1020 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UE_EUTRA_Capability_v1020_IEs Decode(BitArrayInputStream input)
            {
                UE_EUTRA_Capability_v1020_IEs es = new UE_EUTRA_Capability_v1020_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 9);
                if (stream.Read())
                {
                    es.ue_Category_v1020 = input.readBits(2) + 6;
                }
                if (stream.Read())
                {
                    es.phyLayerParameters_v1020 = PhyLayerParameters_v1020.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.rf_Parameters_v1020 = RF_Parameters_v1020.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.measParameters_v1020 = MeasParameters_v1020.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.featureGroupIndRel10_r10 = input.readBitString(0x20);
                }
                if (stream.Read())
                {
                    es.interRAT_ParametersCDMA2000_v1020 = IRAT_ParametersCDMA2000_1XRTT_v1020.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.ue_BasedNetwPerfMeasParameters_r10 = UE_BasedNetwPerfMeasParameters_r10.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.interRAT_ParametersUTRA_TDD_v1020 = IRAT_ParametersUTRA_TDD_v1020.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = UE_EUTRA_Capability_v1060_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class UE_EUTRA_Capability_v1060_IEs
    {
        public void InitDefaults()
        {
        }

        public UE_EUTRA_CapabilityAddXDD_Mode_v1060 fdd_Add_UE_EUTRA_Capabilities_v1060 { get; set; }

        public UE_EUTRA_Capability_v1090_IEs nonCriticalExtension { get; set; }

        public RF_Parameters_v1060 rf_Parameters_v1060 { get; set; }

        public UE_EUTRA_CapabilityAddXDD_Mode_v1060 tdd_Add_UE_EUTRA_Capabilities_v1060 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UE_EUTRA_Capability_v1060_IEs Decode(BitArrayInputStream input)
            {
                UE_EUTRA_Capability_v1060_IEs es = new UE_EUTRA_Capability_v1060_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 4);
                if (stream.Read())
                {
                    es.fdd_Add_UE_EUTRA_Capabilities_v1060 = UE_EUTRA_CapabilityAddXDD_Mode_v1060.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.tdd_Add_UE_EUTRA_Capabilities_v1060 = UE_EUTRA_CapabilityAddXDD_Mode_v1060.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.rf_Parameters_v1060 = RF_Parameters_v1060.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = UE_EUTRA_Capability_v1090_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class UE_EUTRA_Capability_v1090_IEs
    {
        public void InitDefaults()
        {
        }

        public UE_EUTRA_Capability_v1130_IEs nonCriticalExtension { get; set; }

        public RF_Parameters_v1090 rf_Parameters_v1090 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UE_EUTRA_Capability_v1090_IEs Decode(BitArrayInputStream input)
            {
                UE_EUTRA_Capability_v1090_IEs es = new UE_EUTRA_Capability_v1090_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.rf_Parameters_v1090 = RF_Parameters_v1090.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = UE_EUTRA_Capability_v1130_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class UE_EUTRA_Capability_v10c0_IEs
    {
        public void InitDefaults()
        {
        }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        public OTDOA_PositioningCapabilities_r10 otdoa_PositioningCapabilities_r10 { get; set; }

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

            public UE_EUTRA_Capability_v10c0_IEs Decode(BitArrayInputStream input)
            {
                UE_EUTRA_Capability_v10c0_IEs es = new UE_EUTRA_Capability_v10c0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.otdoa_PositioningCapabilities_r10 = OTDOA_PositioningCapabilities_r10.PerDecoder.Instance.Decode(input);
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
    public class UE_EUTRA_Capability_v1130_IEs
    {
        public void InitDefaults()
        {
        }

        public UE_EUTRA_CapabilityAddXDD_Mode_v1130 fdd_Add_UE_EUTRA_Capabilities_v1130 { get; set; }

        public IRAT_ParametersCDMA2000_v1130 interRAT_ParametersCDMA2000_v1130 { get; set; }

        public MeasParameters_v1130 measParameters_v1130 { get; set; }

        public UE_EUTRA_Capability_v1170_IEs nonCriticalExtension { get; set; }

        public Other_Parameters_r11 otherParameters_r11 { get; set; }

        public PDCP_Parameters_v1130 pdcp_Parameters_v1130 { get; set; }

        public PhyLayerParameters_v1130 phyLayerParameters_v1130 { get; set; }

        public RF_Parameters_v1130 rf_Parameters_v1130 { get; set; }

        public UE_EUTRA_CapabilityAddXDD_Mode_v1130 tdd_Add_UE_EUTRA_Capabilities_v1130 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UE_EUTRA_Capability_v1130_IEs Decode(BitArrayInputStream input)
            {
                UE_EUTRA_Capability_v1130_IEs es = new UE_EUTRA_Capability_v1130_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 4);
                es.pdcp_Parameters_v1130 = PDCP_Parameters_v1130.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    es.phyLayerParameters_v1130 = PhyLayerParameters_v1130.PerDecoder.Instance.Decode(input);
                }
                es.rf_Parameters_v1130 = RF_Parameters_v1130.PerDecoder.Instance.Decode(input);
                es.measParameters_v1130 = MeasParameters_v1130.PerDecoder.Instance.Decode(input);
                es.interRAT_ParametersCDMA2000_v1130 = IRAT_ParametersCDMA2000_v1130.PerDecoder.Instance.Decode(input);
                es.otherParameters_r11 = Other_Parameters_r11.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    es.fdd_Add_UE_EUTRA_Capabilities_v1130 = UE_EUTRA_CapabilityAddXDD_Mode_v1130.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.tdd_Add_UE_EUTRA_Capabilities_v1130 = UE_EUTRA_CapabilityAddXDD_Mode_v1130.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = UE_EUTRA_Capability_v1170_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class UE_EUTRA_Capability_v1170_IEs
    {
        public void InitDefaults()
        {
        }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        public PhyLayerParameters_v1170 phyLayerParameters_v1170 { get; set; }

        public long? ue_Category_v1170 { get; set; }

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

            public UE_EUTRA_Capability_v1170_IEs Decode(BitArrayInputStream input)
            {
                UE_EUTRA_Capability_v1170_IEs es = new UE_EUTRA_Capability_v1170_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    es.phyLayerParameters_v1170 = PhyLayerParameters_v1170.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.ue_Category_v1170 = input.readBits(1) + 9;
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
    public class UE_EUTRA_Capability_v920_IEs
    {
        public void InitDefaults()
        {
        }

        public CSG_ProximityIndicationParameters_r9 csg_ProximityIndicationParameters_r9 { get; set; }

        public deviceType_r9_Enum? deviceType_r9 { get; set; }

        public IRAT_ParametersCDMA2000_1XRTT_v920 interRAT_ParametersCDMA2000_v920 { get; set; }

        public IRAT_ParametersGERAN_v920 interRAT_ParametersGERAN_v920 { get; set; }

        public IRAT_ParametersUTRA_v920 interRAT_ParametersUTRA_v920 { get; set; }

        public NeighCellSI_AcquisitionParameters_r9 neighCellSI_AcquisitionParameters_r9 { get; set; }

        public UE_EUTRA_Capability_v940_IEs nonCriticalExtension { get; set; }

        public PhyLayerParameters_v920 phyLayerParameters_v920 { get; set; }

        public SON_Parameters_r9 son_Parameters_r9 { get; set; }

        public enum deviceType_r9_Enum
        {
            noBenFromBatConsumpOpt
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UE_EUTRA_Capability_v920_IEs Decode(BitArrayInputStream input)
            {
                UE_EUTRA_Capability_v920_IEs es = new UE_EUTRA_Capability_v920_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 4);
                es.phyLayerParameters_v920 = PhyLayerParameters_v920.PerDecoder.Instance.Decode(input);
                es.interRAT_ParametersGERAN_v920 = IRAT_ParametersGERAN_v920.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    es.interRAT_ParametersUTRA_v920 = IRAT_ParametersUTRA_v920.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.interRAT_ParametersCDMA2000_v920 = IRAT_ParametersCDMA2000_1XRTT_v920.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    int nBits = 1;
                    es.deviceType_r9 = (deviceType_r9_Enum)input.readBits(nBits);
                }
                es.csg_ProximityIndicationParameters_r9 = CSG_ProximityIndicationParameters_r9.PerDecoder.Instance.Decode(input);
                es.neighCellSI_AcquisitionParameters_r9 = NeighCellSI_AcquisitionParameters_r9.PerDecoder.Instance.Decode(input);
                es.son_Parameters_r9 = SON_Parameters_r9.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    es.nonCriticalExtension = UE_EUTRA_Capability_v940_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class UE_EUTRA_Capability_v940_IEs
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public UE_EUTRA_Capability_v1020_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UE_EUTRA_Capability_v940_IEs Decode(BitArrayInputStream input)
            {
                UE_EUTRA_Capability_v940_IEs es = new UE_EUTRA_Capability_v940_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    es.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = UE_EUTRA_Capability_v1020_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class UE_EUTRA_Capability_v9a0_IEs
    {
        public void InitDefaults()
        {
        }

        public UE_EUTRA_CapabilityAddXDD_Mode_r9 fdd_Add_UE_EUTRA_Capabilities_r9 { get; set; }

        public string featureGroupIndRel9Add_r9 { get; set; }

        public UE_EUTRA_Capability_v9c0_IEs nonCriticalExtension { get; set; }

        public UE_EUTRA_CapabilityAddXDD_Mode_r9 tdd_Add_UE_EUTRA_Capabilities_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UE_EUTRA_Capability_v9a0_IEs Decode(BitArrayInputStream input)
            {
                UE_EUTRA_Capability_v9a0_IEs es = new UE_EUTRA_Capability_v9a0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 4);
                if (stream.Read())
                {
                    es.featureGroupIndRel9Add_r9 = input.readBitString(0x20);
                }
                if (stream.Read())
                {
                    es.fdd_Add_UE_EUTRA_Capabilities_r9 = UE_EUTRA_CapabilityAddXDD_Mode_r9.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.tdd_Add_UE_EUTRA_Capabilities_r9 = UE_EUTRA_CapabilityAddXDD_Mode_r9.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = UE_EUTRA_Capability_v9c0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class UE_EUTRA_Capability_v9c0_IEs
    {
        public void InitDefaults()
        {
        }

        public IRAT_ParametersUTRA_v9c0 interRAT_ParametersUTRA_v9c0 { get; set; }

        public UE_EUTRA_Capability_v9d0_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UE_EUTRA_Capability_v9c0_IEs Decode(BitArrayInputStream input)
            {
                UE_EUTRA_Capability_v9c0_IEs es = new UE_EUTRA_Capability_v9c0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.interRAT_ParametersUTRA_v9c0 = IRAT_ParametersUTRA_v9c0.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = UE_EUTRA_Capability_v9d0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class UE_EUTRA_Capability_v9d0_IEs
    {
        public void InitDefaults()
        {
        }

        public UE_EUTRA_Capability_v9e0_IEs nonCriticalExtension { get; set; }

        public PhyLayerParameters_v9d0 phyLayerParameters_v9d0 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UE_EUTRA_Capability_v9d0_IEs Decode(BitArrayInputStream input)
            {
                UE_EUTRA_Capability_v9d0_IEs es = new UE_EUTRA_Capability_v9d0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.phyLayerParameters_v9d0 = PhyLayerParameters_v9d0.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = UE_EUTRA_Capability_v9e0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class UE_EUTRA_Capability_v9e0_IEs
    {
        public void InitDefaults()
        {
        }

        public UE_EUTRA_Capability_v9h0_IEs nonCriticalExtension { get; set; }

        public RF_Parameters_v9e0 rf_Parameters_v9e0 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UE_EUTRA_Capability_v9e0_IEs Decode(BitArrayInputStream input)
            {
                UE_EUTRA_Capability_v9e0_IEs es = new UE_EUTRA_Capability_v9e0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.rf_Parameters_v9e0 = RF_Parameters_v9e0.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = UE_EUTRA_Capability_v9h0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class UE_EUTRA_Capability_v9h0_IEs
    {
        public void InitDefaults()
        {
        }

        public IRAT_ParametersUTRA_v9h0 interRAT_ParametersUTRA_v9h0 { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public UE_EUTRA_Capability_v10c0_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UE_EUTRA_Capability_v9h0_IEs Decode(BitArrayInputStream input)
            {
                UE_EUTRA_Capability_v9h0_IEs es = new UE_EUTRA_Capability_v9h0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    es.interRAT_ParametersUTRA_v9h0 = IRAT_ParametersUTRA_v9h0.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    es.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = UE_EUTRA_Capability_v10c0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class UE_EUTRA_CapabilityAddXDD_Mode_r9
    {
        public void InitDefaults()
        {
        }

        public string featureGroupIndicators_r9 { get; set; }

        public string featureGroupIndRel9Add_r9 { get; set; }

        public IRAT_ParametersCDMA2000_1XRTT_v920 interRAT_ParametersCDMA2000_r9 { get; set; }

        public IRAT_ParametersGERAN interRAT_ParametersGERAN_r9 { get; set; }

        public IRAT_ParametersUTRA_v920 interRAT_ParametersUTRA_r9 { get; set; }

        public NeighCellSI_AcquisitionParameters_r9 neighCellSI_AcquisitionParameters_r9 { get; set; }

        public PhyLayerParameters phyLayerParameters_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UE_EUTRA_CapabilityAddXDD_Mode_r9 Decode(BitArrayInputStream input)
            {
                UE_EUTRA_CapabilityAddXDD_Mode_r9 _r = new UE_EUTRA_CapabilityAddXDD_Mode_r9();
                _r.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 7) : new BitMaskStream(input, 7);
                if (stream.Read())
                {
                    _r.phyLayerParameters_r9 = PhyLayerParameters.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    _r.featureGroupIndicators_r9 = input.readBitString(0x20);
                }
                if (stream.Read())
                {
                    _r.featureGroupIndRel9Add_r9 = input.readBitString(0x20);
                }
                if (stream.Read())
                {
                    _r.interRAT_ParametersGERAN_r9 = IRAT_ParametersGERAN.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    _r.interRAT_ParametersUTRA_r9 = IRAT_ParametersUTRA_v920.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    _r.interRAT_ParametersCDMA2000_r9 = IRAT_ParametersCDMA2000_1XRTT_v920.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    _r.neighCellSI_AcquisitionParameters_r9 = NeighCellSI_AcquisitionParameters_r9.PerDecoder.Instance.Decode(input);
                }
                return _r;
            }
        }
    }

    [Serializable]
    public class UE_EUTRA_CapabilityAddXDD_Mode_v1060
    {
        public void InitDefaults()
        {
        }

        public string featureGroupIndRel10_v1060 { get; set; }

        public IRAT_ParametersCDMA2000_1XRTT_v1020 interRAT_ParametersCDMA2000_v1060 { get; set; }

        public IRAT_ParametersUTRA_TDD_v1020 interRAT_ParametersUTRA_TDD_v1060 { get; set; }

        public OTDOA_PositioningCapabilities_r10 otdoa_PositioningCapabilities_r10 { get; set; }

        public PhyLayerParameters_v1020 phyLayerParameters_v1060 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UE_EUTRA_CapabilityAddXDD_Mode_v1060 Decode(BitArrayInputStream input)
            {
                UE_EUTRA_CapabilityAddXDD_Mode_v1060 _v = new UE_EUTRA_CapabilityAddXDD_Mode_v1060();
                _v.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 4);
                if (stream.Read())
                {
                    _v.phyLayerParameters_v1060 = PhyLayerParameters_v1020.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    _v.featureGroupIndRel10_v1060 = input.readBitString(0x20);
                }
                if (stream.Read())
                {
                    _v.interRAT_ParametersCDMA2000_v1060 = IRAT_ParametersCDMA2000_1XRTT_v1020.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    _v.interRAT_ParametersUTRA_TDD_v1060 = IRAT_ParametersUTRA_TDD_v1020.PerDecoder.Instance.Decode(input);
                }
                if (flag)
                {
                    BitMaskStream stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        _v.otdoa_PositioningCapabilities_r10 = OTDOA_PositioningCapabilities_r10.PerDecoder.Instance.Decode(input);
                    }
                }
                return _v;
            }
        }
    }

    [Serializable]
    public class UE_EUTRA_CapabilityAddXDD_Mode_v1130
    {
        public void InitDefaults()
        {
        }

        public MeasParameters_v1130 measParameters_v1130 { get; set; }

        public Other_Parameters_r11 otherParameters_r11 { get; set; }

        public PhyLayerParameters_v1130 phyLayerParameters_v1130 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UE_EUTRA_CapabilityAddXDD_Mode_v1130 Decode(BitArrayInputStream input)
            {
                UE_EUTRA_CapabilityAddXDD_Mode_v1130 _v = new UE_EUTRA_CapabilityAddXDD_Mode_v1130();
                _v.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 3) : new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    _v.phyLayerParameters_v1130 = PhyLayerParameters_v1130.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    _v.measParameters_v1130 = MeasParameters_v1130.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    _v.otherParameters_r11 = Other_Parameters_r11.PerDecoder.Instance.Decode(input);
                }
                return _v;
            }
        }
    }

}
