using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class RACH_ConfigCommon
    {
        public void InitDefaults()
        {
        }

        public long maxHARQ_Msg3Tx { get; set; }

        public PowerRampingParameters powerRampingParameters { get; set; }

        public preambleInfo_Type preambleInfo { get; set; }

        public ra_SupervisionInfo_Type ra_SupervisionInfo { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RACH_ConfigCommon Decode(BitArrayInputStream input)
            {
                RACH_ConfigCommon common = new RACH_ConfigCommon();
                common.InitDefaults();
                input.readBit();
                common.preambleInfo = preambleInfo_Type.PerDecoder.Instance.Decode(input);
                common.powerRampingParameters = PowerRampingParameters.PerDecoder.Instance.Decode(input);
                common.ra_SupervisionInfo = ra_SupervisionInfo_Type.PerDecoder.Instance.Decode(input);
                common.maxHARQ_Msg3Tx = input.readBits(3) + 1;
                return common;
            }
        }

        [Serializable]
        public class preambleInfo_Type
        {
            public void InitDefaults()
            {
            }

            public numberOfRA_Preambles_Enum numberOfRA_Preambles { get; set; }

            public preamblesGroupAConfig_Type preamblesGroupAConfig { get; set; }

            public enum numberOfRA_Preambles_Enum
            {
                n4,
                n8,
                n12,
                n16,
                n20,
                n24,
                n28,
                n32,
                n36,
                n40,
                n44,
                n48,
                n52,
                n56,
                n60,
                n64
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public preambleInfo_Type Decode(BitArrayInputStream input)
                {
                    preambleInfo_Type type = new preambleInfo_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 1);
                    const int nBits = 4;
                    type.numberOfRA_Preambles = (numberOfRA_Preambles_Enum)input.readBits(nBits);
                    if (stream.Read())
                    {
                        type.preamblesGroupAConfig = preamblesGroupAConfig_Type.PerDecoder.Instance.Decode(input);
                    }
                    return type;
                }
            }

            [Serializable]
            public class preamblesGroupAConfig_Type
            {
                public void InitDefaults()
                {
                }

                public messagePowerOffsetGroupB_Enum messagePowerOffsetGroupB { get; set; }

                public messageSizeGroupA_Enum messageSizeGroupA { get; set; }

                public sizeOfRA_PreamblesGroupA_Enum sizeOfRA_PreamblesGroupA { get; set; }

                public enum messagePowerOffsetGroupB_Enum
                {
                    minusinfinity,
                    dB0,
                    dB5,
                    dB8,
                    dB10,
                    dB12,
                    dB15,
                    dB18
                }

                public enum messageSizeGroupA_Enum
                {
                    b56,
                    b144,
                    b208,
                    b256
                }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public preamblesGroupAConfig_Type Decode(BitArrayInputStream input)
                    {
                        preamblesGroupAConfig_Type type = new preamblesGroupAConfig_Type();
                        type.InitDefaults();
                        input.readBit();
                        int nBits = 4;
                        type.sizeOfRA_PreamblesGroupA = (sizeOfRA_PreamblesGroupA_Enum)input.readBits(nBits);
                        nBits = 2;
                        type.messageSizeGroupA = (messageSizeGroupA_Enum)input.readBits(nBits);
                        nBits = 3;
                        type.messagePowerOffsetGroupB = (messagePowerOffsetGroupB_Enum)input.readBits(nBits);
                        return type;
                    }
                }

                public enum sizeOfRA_PreamblesGroupA_Enum
                {
                    n4,
                    n8,
                    n12,
                    n16,
                    n20,
                    n24,
                    n28,
                    n32,
                    n36,
                    n40,
                    n44,
                    n48,
                    n52,
                    n56,
                    n60
                }
            }
        }

        [Serializable]
        public class ra_SupervisionInfo_Type
        {
            public void InitDefaults()
            {
            }

            public mac_ContentionResolutionTimer_Enum mac_ContentionResolutionTimer { get; set; }

            public PreambleTransMax preambleTransMax { get; set; }

            public ra_ResponseWindowSize_Enum ra_ResponseWindowSize { get; set; }

            public enum mac_ContentionResolutionTimer_Enum
            {
                sf8,
                sf16,
                sf24,
                sf32,
                sf40,
                sf48,
                sf56,
                sf64
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public ra_SupervisionInfo_Type Decode(BitArrayInputStream input)
                {
                    ra_SupervisionInfo_Type type = new ra_SupervisionInfo_Type();
                    type.InitDefaults();
                    int nBits = 4;
                    type.preambleTransMax = (PreambleTransMax)input.readBits(nBits);
                    nBits = 3;
                    type.ra_ResponseWindowSize = (ra_ResponseWindowSize_Enum)input.readBits(nBits);
                    nBits = 3;
                    type.mac_ContentionResolutionTimer = (mac_ContentionResolutionTimer_Enum)input.readBits(nBits);
                    return type;
                }
            }

            public enum ra_ResponseWindowSize_Enum
            {
                sf2,
                sf3,
                sf4,
                sf5,
                sf6,
                sf7,
                sf8,
                sf10
            }
        }
    }

    [Serializable]
    public class RACH_ConfigCommonSCell_r11
    {
        public void InitDefaults()
        {
        }

        public PowerRampingParameters powerRampingParameters_r11 { get; set; }

        public ra_SupervisionInfo_r11_Type ra_SupervisionInfo_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RACH_ConfigCommonSCell_r11 Decode(BitArrayInputStream input)
            {
                RACH_ConfigCommonSCell_r11 _r = new RACH_ConfigCommonSCell_r11();
                _r.InitDefaults();
                input.readBit();
                _r.powerRampingParameters_r11 = PowerRampingParameters.PerDecoder.Instance.Decode(input);
                _r.ra_SupervisionInfo_r11 = ra_SupervisionInfo_r11_Type.PerDecoder.Instance.Decode(input);
                return _r;
            }
        }

        [Serializable]
        public class ra_SupervisionInfo_r11_Type
        {
            public void InitDefaults()
            {
            }

            public PreambleTransMax preambleTransMax_r11 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public ra_SupervisionInfo_r11_Type Decode(BitArrayInputStream input)
                {
                    ra_SupervisionInfo_r11_Type type = new ra_SupervisionInfo_r11_Type();
                    type.InitDefaults();
                    const int nBits = 4;
                    type.preambleTransMax_r11 = (PreambleTransMax)input.readBits(nBits);
                    return type;
                }
            }
        }
    }

    [Serializable]
    public class RACH_ConfigDedicated
    {
        public void InitDefaults()
        {
        }

        public long ra_PRACH_MaskIndex { get; set; }

        public long ra_PreambleIndex { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RACH_ConfigDedicated Decode(BitArrayInputStream input)
            {
                RACH_ConfigDedicated dedicated = new RACH_ConfigDedicated();
                dedicated.InitDefaults();
                dedicated.ra_PreambleIndex = input.readBits(6);
                dedicated.ra_PRACH_MaskIndex = input.readBits(4);
                return dedicated;
            }
        }
    }

    [Serializable]
    public class NonContiguousUL_RA_WithinCC_r10
    {
        public void InitDefaults()
        {
        }

        public nonContiguousUL_RA_WithinCC_Info_r10_Enum? nonContiguousUL_RA_WithinCC_Info_r10 { get; set; }

        public enum nonContiguousUL_RA_WithinCC_Info_r10_Enum
        {
            supported
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public NonContiguousUL_RA_WithinCC_r10 Decode(BitArrayInputStream input)
            {
                NonContiguousUL_RA_WithinCC_r10 _r = new NonContiguousUL_RA_WithinCC_r10();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    const int nBits = 1;
                    _r.nonContiguousUL_RA_WithinCC_Info_r10 = (nonContiguousUL_RA_WithinCC_Info_r10_Enum)input.readBits(nBits);
                }
                return _r;
            }
        }
    }

}
