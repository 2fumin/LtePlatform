using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class RadioResourceConfigCommon
    {
        public void InitDefaults()
        {
        }

        public AntennaInfoCommon antennaInfoCommon { get; set; }

        public long? p_Max { get; set; }

        public PDSCH_ConfigCommon pdsch_ConfigCommon { get; set; }

        public PHICH_Config phich_Config { get; set; }

        public PRACH_Config prach_Config { get; set; }

        public PUCCH_ConfigCommon pucch_ConfigCommon { get; set; }

        public PUSCH_ConfigCommon pusch_ConfigCommon { get; set; }

        public RACH_ConfigCommon rach_ConfigCommon { get; set; }

        public SoundingRS_UL_ConfigCommon soundingRS_UL_ConfigCommon { get; set; }

        public TDD_Config tdd_Config { get; set; }

        public TDD_Config_v1130 tdd_Config_v1130 { get; set; }

        public UL_CyclicPrefixLength ul_CyclicPrefixLength { get; set; }

        public UplinkPowerControlCommon uplinkPowerControlCommon { get; set; }

        public UplinkPowerControlCommon_v1020 uplinkPowerControlCommon_v1020 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RadioResourceConfigCommon Decode(BitArrayInputStream input)
            {
                BitMaskStream stream2;
                RadioResourceConfigCommon common = new RadioResourceConfigCommon();
                common.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 9);
                if (stream.Read())
                {
                    common.rach_ConfigCommon = RACH_ConfigCommon.PerDecoder.Instance.Decode(input);
                }
                common.prach_Config = PRACH_Config.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    common.pdsch_ConfigCommon = PDSCH_ConfigCommon.PerDecoder.Instance.Decode(input);
                }
                common.pusch_ConfigCommon = PUSCH_ConfigCommon.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    common.phich_Config = PHICH_Config.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    common.pucch_ConfigCommon = PUCCH_ConfigCommon.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    common.soundingRS_UL_ConfigCommon = SoundingRS_UL_ConfigCommon.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    common.uplinkPowerControlCommon = UplinkPowerControlCommon.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    common.antennaInfoCommon = AntennaInfoCommon.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    common.p_Max = input.readBits(6) + -30;
                }
                if (stream.Read())
                {
                    common.tdd_Config = TDD_Config.PerDecoder.Instance.Decode(input);
                }
                int nBits = 1;
                common.ul_CyclicPrefixLength = (UL_CyclicPrefixLength)input.readBits(nBits);
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        common.uplinkPowerControlCommon_v1020 = UplinkPowerControlCommon_v1020.PerDecoder.Instance.Decode(input);
                    }
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        common.tdd_Config_v1130 = TDD_Config_v1130.PerDecoder.Instance.Decode(input);
                    }
                }
                return common;
            }
        }
    }

    [Serializable]
    public class RadioResourceConfigCommonSCell_r10
    {
        public void InitDefaults()
        {
        }

        public nonUL_Configuration_r10_Type nonUL_Configuration_r10 { get; set; }

        public PRACH_Config prach_ConfigSCell_r11 { get; set; }

        public RACH_ConfigCommonSCell_r11 rach_ConfigCommonSCell_r11 { get; set; }

        public TDD_Config_v1130 tdd_Config_v1130 { get; set; }

        public long? ul_CarrierFreq_v1090 { get; set; }

        public ul_Configuration_r10_Type ul_Configuration_r10 { get; set; }

        public UplinkPowerControlCommonSCell_v1130 uplinkPowerControlCommonSCell_v1130 { get; set; }

        [Serializable]
        public class nonUL_Configuration_r10_Type
        {
            public void InitDefaults()
            {
            }

            public AntennaInfoCommon antennaInfoCommon_r10 { get; set; }

            public dl_Bandwidth_r10_Enum dl_Bandwidth_r10 { get; set; }

            public List<MBSFN_SubframeConfig> mbsfn_SubframeConfigList_r10 { get; set; }

            public PDSCH_ConfigCommon pdsch_ConfigCommon_r10 { get; set; }

            public PHICH_Config phich_Config_r10 { get; set; }

            public TDD_Config tdd_Config_r10 { get; set; }

            public enum dl_Bandwidth_r10_Enum
            {
                n6,
                n15,
                n25,
                n50,
                n75,
                n100
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public nonUL_Configuration_r10_Type Decode(BitArrayInputStream input)
                {
                    nonUL_Configuration_r10_Type type = new nonUL_Configuration_r10_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 2);
                    int nBits = 3;
                    type.dl_Bandwidth_r10 = (dl_Bandwidth_r10_Enum)input.readBits(nBits);
                    type.antennaInfoCommon_r10 = AntennaInfoCommon.PerDecoder.Instance.Decode(input);
                    if (stream.Read())
                    {
                        type.mbsfn_SubframeConfigList_r10 = new List<MBSFN_SubframeConfig>();
                        nBits = 3;
                        int num3 = input.readBits(nBits) + 1;
                        for (int i = 0; i < num3; i++)
                        {
                            MBSFN_SubframeConfig item = MBSFN_SubframeConfig.PerDecoder.Instance.Decode(input);
                            type.mbsfn_SubframeConfigList_r10.Add(item);
                        }
                    }
                    type.phich_Config_r10 = PHICH_Config.PerDecoder.Instance.Decode(input);
                    type.pdsch_ConfigCommon_r10 = PDSCH_ConfigCommon.PerDecoder.Instance.Decode(input);
                    if (stream.Read())
                    {
                        type.tdd_Config_r10 = TDD_Config.PerDecoder.Instance.Decode(input);
                    }
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RadioResourceConfigCommonSCell_r10 Decode(BitArrayInputStream input)
            {
                BitMaskStream stream2;
                RadioResourceConfigCommonSCell_r10 _r = new RadioResourceConfigCommonSCell_r10();
                _r.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 1);
                _r.nonUL_Configuration_r10 = nonUL_Configuration_r10_Type.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    _r.ul_Configuration_r10 = ul_Configuration_r10_Type.PerDecoder.Instance.Decode(input);
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        _r.ul_CarrierFreq_v1090 = input.readBits(0x12) + 0x10000;
                    }
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 4);
                    if (stream2.Read())
                    {
                        _r.rach_ConfigCommonSCell_r11 = RACH_ConfigCommonSCell_r11.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        _r.prach_ConfigSCell_r11 = PRACH_Config.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        _r.tdd_Config_v1130 = TDD_Config_v1130.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        _r.uplinkPowerControlCommonSCell_v1130 = UplinkPowerControlCommonSCell_v1130.PerDecoder.Instance.Decode(input);
                    }
                }
                return _r;
            }
        }

        [Serializable]
        public class ul_Configuration_r10_Type
        {
            public void InitDefaults()
            {
            }

            public long? p_Max_r10 { get; set; }

            public PRACH_ConfigSCell_r10 prach_ConfigSCell_r10 { get; set; }

            public PUSCH_ConfigCommon pusch_ConfigCommon_r10 { get; set; }

            public SoundingRS_UL_ConfigCommon soundingRS_UL_ConfigCommon_r10 { get; set; }

            public UL_CyclicPrefixLength ul_CyclicPrefixLength_r10 { get; set; }

            public ul_FreqInfo_r10_Type ul_FreqInfo_r10 { get; set; }

            public UplinkPowerControlCommonSCell_r10 uplinkPowerControlCommonSCell_r10 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public ul_Configuration_r10_Type Decode(BitArrayInputStream input)
                {
                    ul_Configuration_r10_Type type = new ul_Configuration_r10_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 2);
                    type.ul_FreqInfo_r10 = ul_FreqInfo_r10_Type.PerDecoder.Instance.Decode(input);
                    if (stream.Read())
                    {
                        type.p_Max_r10 = input.readBits(6) + -30;
                    }
                    type.uplinkPowerControlCommonSCell_r10 = UplinkPowerControlCommonSCell_r10.PerDecoder.Instance.Decode(input);
                    type.soundingRS_UL_ConfigCommon_r10 = SoundingRS_UL_ConfigCommon.PerDecoder.Instance.Decode(input);
                    int nBits = 1;
                    type.ul_CyclicPrefixLength_r10 = (UL_CyclicPrefixLength)input.readBits(nBits);
                    if (stream.Read())
                    {
                        type.prach_ConfigSCell_r10 = PRACH_ConfigSCell_r10.PerDecoder.Instance.Decode(input);
                    }
                    type.pusch_ConfigCommon_r10 = PUSCH_ConfigCommon.PerDecoder.Instance.Decode(input);
                    return type;
                }
            }

            [Serializable]
            public class ul_FreqInfo_r10_Type
            {
                public void InitDefaults()
                {
                }

                public long additionalSpectrumEmissionSCell_r10 { get; set; }

                public ul_Bandwidth_r10_Enum? ul_Bandwidth_r10 { get; set; }

                public long? ul_CarrierFreq_r10 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public ul_FreqInfo_r10_Type Decode(BitArrayInputStream input)
                    {
                        ul_FreqInfo_r10_Type type = new ul_FreqInfo_r10_Type();
                        type.InitDefaults();
                        BitMaskStream stream = new BitMaskStream(input, 2);
                        if (stream.Read())
                        {
                            type.ul_CarrierFreq_r10 = input.readBits(0x10);
                        }
                        if (stream.Read())
                        {
                            int nBits = 3;
                            type.ul_Bandwidth_r10 = (ul_Bandwidth_r10_Enum)input.readBits(nBits);
                        }
                        type.additionalSpectrumEmissionSCell_r10 = input.readBits(5) + 1;
                        return type;
                    }
                }

                public enum ul_Bandwidth_r10_Enum
                {
                    n6,
                    n15,
                    n25,
                    n50,
                    n75,
                    n100
                }
            }
        }
    }

    [Serializable]
    public class RadioResourceConfigCommonSIB
    {
        public void InitDefaults()
        {
        }

        public BCCH_Config bcch_Config { get; set; }

        public PCCH_Config pcch_Config { get; set; }

        public PDSCH_ConfigCommon pdsch_ConfigCommon { get; set; }

        public PRACH_ConfigSIB prach_Config { get; set; }

        public PUCCH_ConfigCommon pucch_ConfigCommon { get; set; }

        public PUSCH_ConfigCommon pusch_ConfigCommon { get; set; }

        public RACH_ConfigCommon rach_ConfigCommon { get; set; }

        public SoundingRS_UL_ConfigCommon soundingRS_UL_ConfigCommon { get; set; }

        public UL_CyclicPrefixLength ul_CyclicPrefixLength { get; set; }

        public UplinkPowerControlCommon uplinkPowerControlCommon { get; set; }

        public UplinkPowerControlCommon_v1020 uplinkPowerControlCommon_v1020 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RadioResourceConfigCommonSIB Decode(BitArrayInputStream input)
            {
                RadioResourceConfigCommonSIB nsib = new RadioResourceConfigCommonSIB();
                nsib.InitDefaults();
                bool flag = input.readBit() != 0;
                nsib.rach_ConfigCommon = RACH_ConfigCommon.PerDecoder.Instance.Decode(input);
                nsib.bcch_Config = BCCH_Config.PerDecoder.Instance.Decode(input);
                nsib.pcch_Config = PCCH_Config.PerDecoder.Instance.Decode(input);
                nsib.prach_Config = PRACH_ConfigSIB.PerDecoder.Instance.Decode(input);
                nsib.pdsch_ConfigCommon = PDSCH_ConfigCommon.PerDecoder.Instance.Decode(input);
                nsib.pusch_ConfigCommon = PUSCH_ConfigCommon.PerDecoder.Instance.Decode(input);
                nsib.pucch_ConfigCommon = PUCCH_ConfigCommon.PerDecoder.Instance.Decode(input);
                nsib.soundingRS_UL_ConfigCommon = SoundingRS_UL_ConfigCommon.PerDecoder.Instance.Decode(input);
                nsib.uplinkPowerControlCommon = UplinkPowerControlCommon.PerDecoder.Instance.Decode(input);
                int nBits = 1;
                nsib.ul_CyclicPrefixLength = (UL_CyclicPrefixLength)input.readBits(nBits);
                if (flag)
                {
                    BitMaskStream stream = new BitMaskStream(input, 1);
                    if (stream.Read())
                    {
                        nsib.uplinkPowerControlCommon_v1020 = UplinkPowerControlCommon_v1020.PerDecoder.Instance.Decode(input);
                    }
                }
                return nsib;
            }
        }
    }

    [Serializable]
    public class RadioResourceConfigDedicated
    {
        public void InitDefaults()
        {
        }

        public List<DRB_ToAddMod> drb_ToAddModList { get; set; }

        public List<long> drb_ToReleaseList { get; set; }

        public mac_MainConfig_Type mac_MainConfig { get; set; }

        public MeasSubframePatternPCell_r10 measSubframePatternPCell_r10 { get; set; }

        public NeighCellsCRS_Info_r11 neighCellsCRS_Info_r11 { get; set; }

        public PhysicalConfigDedicated physicalConfigDedicated { get; set; }

        public RLF_TimersAndConstants_r9 rlf_TimersAndConstants_r9 { get; set; }

        public SPS_Config sps_Config { get; set; }

        public List<SRB_ToAddMod> srb_ToAddModList { get; set; }

        [Serializable]
        public class mac_MainConfig_Type
        {
            public void InitDefaults()
            {
            }

            public object defaultValue { get; set; }

            public MAC_MainConfig explicitValue { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public mac_MainConfig_Type Decode(BitArrayInputStream input)
                {
                    mac_MainConfig_Type type = new mac_MainConfig_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.explicitValue = MAC_MainConfig.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RadioResourceConfigDedicated Decode(BitArrayInputStream input)
            {
                int num2;
                BitMaskStream stream2;
                RadioResourceConfigDedicated dedicated = new RadioResourceConfigDedicated();
                dedicated.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 6);
                if (stream.Read())
                {
                    dedicated.srb_ToAddModList = new List<SRB_ToAddMod>();
                    num2 = 1;
                    int num3 = input.readBits(num2) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        SRB_ToAddMod item = SRB_ToAddMod.PerDecoder.Instance.Decode(input);
                        dedicated.srb_ToAddModList.Add(item);
                    }
                }
                if (stream.Read())
                {
                    dedicated.drb_ToAddModList = new List<DRB_ToAddMod>();
                    num2 = 4;
                    int num5 = input.readBits(num2) + 1;
                    for (int j = 0; j < num5; j++)
                    {
                        DRB_ToAddMod mod2 = DRB_ToAddMod.PerDecoder.Instance.Decode(input);
                        dedicated.drb_ToAddModList.Add(mod2);
                    }
                }
                if (stream.Read())
                {
                    dedicated.drb_ToReleaseList = new List<long>();
                    num2 = 4;
                    int num7 = input.readBits(num2) + 1;
                    for (int k = 0; k < num7; k++)
                    {
                        long num9 = input.readBits(5) + 1;
                        dedicated.drb_ToReleaseList.Add(num9);
                    }
                }
                if (stream.Read())
                {
                    dedicated.mac_MainConfig = mac_MainConfig_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    dedicated.sps_Config = SPS_Config.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    dedicated.physicalConfigDedicated = PhysicalConfigDedicated.PerDecoder.Instance.Decode(input);
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        dedicated.rlf_TimersAndConstants_r9 = RLF_TimersAndConstants_r9.PerDecoder.Instance.Decode(input);
                    }
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        dedicated.measSubframePatternPCell_r10 = MeasSubframePatternPCell_r10.PerDecoder.Instance.Decode(input);
                    }
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        dedicated.neighCellsCRS_Info_r11 = NeighCellsCRS_Info_r11.PerDecoder.Instance.Decode(input);
                    }
                }
                return dedicated;
            }
        }
    }

    [Serializable]
    public class RadioResourceConfigDedicatedSCell_r10
    {
        public void InitDefaults()
        {
        }

        public MAC_MainConfigSCell_r11 mac_MainConfigSCell_r11 { get; set; }

        public PhysicalConfigDedicatedSCell_r10 physicalConfigDedicatedSCell_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RadioResourceConfigDedicatedSCell_r10 Decode(BitArrayInputStream input)
            {
                RadioResourceConfigDedicatedSCell_r10 _r = new RadioResourceConfigDedicatedSCell_r10();
                _r.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    _r.physicalConfigDedicatedSCell_r10 = PhysicalConfigDedicatedSCell_r10.PerDecoder.Instance.Decode(input);
                }
                if (flag)
                {
                    BitMaskStream stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        _r.mac_MainConfigSCell_r11 = MAC_MainConfigSCell_r11.PerDecoder.Instance.Decode(input);
                    }
                }
                return _r;
            }
        }
    }

}
