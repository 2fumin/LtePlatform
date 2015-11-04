using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class UplinkPowerControlCommon
    {
        public void InitDefaults()
        {
        }

        public alpha_Enum alpha { get; set; }

        public DeltaFList_PUCCH deltaFList_PUCCH { get; set; }

        public long deltaPreambleMsg3 { get; set; }

        public long p0_NominalPUCCH { get; set; }

        public long p0_NominalPUSCH { get; set; }

        public enum alpha_Enum
        {
            al0,
            al04,
            al05,
            al06,
            al07,
            al08,
            al09,
            al1
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UplinkPowerControlCommon Decode(BitArrayInputStream input)
            {
                UplinkPowerControlCommon common = new UplinkPowerControlCommon();
                common.InitDefaults();
                common.p0_NominalPUSCH = input.readBits(8) + -126;
                int nBits = 3;
                common.alpha = (alpha_Enum)input.readBits(nBits);
                common.p0_NominalPUCCH = input.readBits(5) + -127;
                common.deltaFList_PUCCH = DeltaFList_PUCCH.PerDecoder.Instance.Decode(input);
                common.deltaPreambleMsg3 = input.readBits(3) + -1;
                return common;
            }
        }
    }

    [Serializable]
    public class UplinkPowerControlCommon_v1020
    {
        public void InitDefaults()
        {
        }

        public deltaF_PUCCH_Format1bCS_r10_Enum deltaF_PUCCH_Format1bCS_r10 { get; set; }

        public deltaF_PUCCH_Format3_r10_Enum deltaF_PUCCH_Format3_r10 { get; set; }

        public enum deltaF_PUCCH_Format1bCS_r10_Enum
        {
            deltaF1,
            deltaF2,
            spare2,
            spare1
        }

        public enum deltaF_PUCCH_Format3_r10_Enum
        {
            deltaF_1,
            deltaF0,
            deltaF1,
            deltaF2,
            deltaF3,
            deltaF4,
            deltaF5,
            deltaF6
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UplinkPowerControlCommon_v1020 Decode(BitArrayInputStream input)
            {
                UplinkPowerControlCommon_v1020 _v = new UplinkPowerControlCommon_v1020();
                _v.InitDefaults();
                int nBits = 3;
                _v.deltaF_PUCCH_Format3_r10 = (deltaF_PUCCH_Format3_r10_Enum)input.readBits(nBits);
                nBits = 2;
                _v.deltaF_PUCCH_Format1bCS_r10 = (deltaF_PUCCH_Format1bCS_r10_Enum)input.readBits(nBits);
                return _v;
            }
        }
    }

    [Serializable]
    public class UplinkPowerControlCommonSCell_r10
    {
        public void InitDefaults()
        {
        }

        public alpha_r10_Enum alpha_r10 { get; set; }

        public long p0_NominalPUSCH_r10 { get; set; }

        public enum alpha_r10_Enum
        {
            al0,
            al04,
            al05,
            al06,
            al07,
            al08,
            al09,
            al1
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UplinkPowerControlCommonSCell_r10 Decode(BitArrayInputStream input)
            {
                UplinkPowerControlCommonSCell_r10 _r = new UplinkPowerControlCommonSCell_r10();
                _r.InitDefaults();
                _r.p0_NominalPUSCH_r10 = input.readBits(8) + -126;
                int nBits = 3;
                _r.alpha_r10 = (alpha_r10_Enum)input.readBits(nBits);
                return _r;
            }
        }
    }

    [Serializable]
    public class UplinkPowerControlCommonSCell_v1130
    {
        public void InitDefaults()
        {
        }

        public long deltaPreambleMsg3_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UplinkPowerControlCommonSCell_v1130 Decode(BitArrayInputStream input)
            {
                UplinkPowerControlCommonSCell_v1130 _v = new UplinkPowerControlCommonSCell_v1130();
                _v.InitDefaults();
                _v.deltaPreambleMsg3_r11 = input.readBits(3) + -1;
                return _v;
            }
        }
    }

    [Serializable]
    public class UplinkPowerControlDedicated
    {
        public void InitDefaults()
        {
            filterCoefficient = FilterCoefficient.fc4;
        }

        public bool accumulationEnabled { get; set; }

        public deltaMCS_Enabled_Enum deltaMCS_Enabled { get; set; }

        public FilterCoefficient filterCoefficient { get; set; }

        public long p0_UE_PUCCH { get; set; }

        public long p0_UE_PUSCH { get; set; }

        public long pSRS_Offset { get; set; }

        public enum deltaMCS_Enabled_Enum
        {
            en0,
            en1
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UplinkPowerControlDedicated Decode(BitArrayInputStream input)
            {
                UplinkPowerControlDedicated dedicated = new UplinkPowerControlDedicated();
                dedicated.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                dedicated.p0_UE_PUSCH = input.readBits(4) + -8;
                int nBits = 1;
                dedicated.deltaMCS_Enabled = (deltaMCS_Enabled_Enum)input.readBits(nBits);
                dedicated.accumulationEnabled = input.readBit() == 1;
                dedicated.p0_UE_PUCCH = input.readBits(4) + -8;
                dedicated.pSRS_Offset = input.readBits(4);
                if (stream.Read())
                {
                    nBits = (input.readBit() == 0) ? 4 : 4;
                    dedicated.filterCoefficient = (FilterCoefficient)input.readBits(nBits);
                }
                return dedicated;
            }
        }
    }

    [Serializable]
    public class UplinkPowerControlDedicated_v1020
    {
        public void InitDefaults()
        {
        }

        public DeltaTxD_OffsetListPUCCH_r10 deltaTxD_OffsetListPUCCH_r10 { get; set; }

        public long? pSRS_OffsetAp_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UplinkPowerControlDedicated_v1020 Decode(BitArrayInputStream input)
            {
                UplinkPowerControlDedicated_v1020 _v = new UplinkPowerControlDedicated_v1020();
                _v.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    _v.deltaTxD_OffsetListPUCCH_r10 = DeltaTxD_OffsetListPUCCH_r10.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    _v.pSRS_OffsetAp_r10 = input.readBits(4);
                }
                return _v;
            }
        }
    }

    [Serializable]
    public class UplinkPowerControlDedicated_v1130
    {
        public void InitDefaults()
        {
        }

        public DeltaTxD_OffsetListPUCCH_v1130 deltaTxD_OffsetListPUCCH_v1130 { get; set; }

        public long? pSRS_Offset_v1130 { get; set; }

        public long? pSRS_OffsetAp_v1130 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UplinkPowerControlDedicated_v1130 Decode(BitArrayInputStream input)
            {
                UplinkPowerControlDedicated_v1130 _v = new UplinkPowerControlDedicated_v1130();
                _v.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    _v.pSRS_Offset_v1130 = input.readBits(4) + 0x10;
                }
                if (stream.Read())
                {
                    _v.pSRS_OffsetAp_v1130 = input.readBits(4) + 0x10;
                }
                if (stream.Read())
                {
                    _v.deltaTxD_OffsetListPUCCH_v1130 = DeltaTxD_OffsetListPUCCH_v1130.PerDecoder.Instance.Decode(input);
                }
                return _v;
            }
        }
    }

    [Serializable]
    public class UplinkPowerControlDedicatedSCell_r10
    {
        public void InitDefaults()
        {
            filterCoefficient_r10 = FilterCoefficient.fc4;
        }

        public bool accumulationEnabled_r10 { get; set; }

        public deltaMCS_Enabled_r10_Enum deltaMCS_Enabled_r10 { get; set; }

        public FilterCoefficient filterCoefficient_r10 { get; set; }

        public long p0_UE_PUSCH_r10 { get; set; }

        public pathlossReferenceLinking_r10_Enum pathlossReferenceLinking_r10 { get; set; }

        public long pSRS_Offset_r10 { get; set; }

        public long? pSRS_OffsetAp_r10 { get; set; }

        public enum deltaMCS_Enabled_r10_Enum
        {
            en0,
            en1
        }

        public enum pathlossReferenceLinking_r10_Enum
        {
            pCell,
            sCell
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UplinkPowerControlDedicatedSCell_r10 Decode(BitArrayInputStream input)
            {
                UplinkPowerControlDedicatedSCell_r10 _r = new UplinkPowerControlDedicatedSCell_r10();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                BitMaskStream stream2 = new BitMaskStream(input, 1);
                _r.p0_UE_PUSCH_r10 = input.readBits(4) + -8;
                int nBits = 1;
                _r.deltaMCS_Enabled_r10 = (deltaMCS_Enabled_r10_Enum)input.readBits(nBits);
                _r.accumulationEnabled_r10 = input.readBit() == 1;
                _r.pSRS_Offset_r10 = input.readBits(4);
                if (stream2.Read())
                {
                    _r.pSRS_OffsetAp_r10 = input.readBits(4);
                }
                if (stream.Read())
                {
                    nBits = (input.readBit() == 0) ? 4 : 4;
                    _r.filterCoefficient_r10 = (FilterCoefficient)input.readBits(nBits);
                }
                nBits = 1;
                _r.pathlossReferenceLinking_r10 = (pathlossReferenceLinking_r10_Enum)input.readBits(nBits);
                return _r;
            }
        }
    }

}
