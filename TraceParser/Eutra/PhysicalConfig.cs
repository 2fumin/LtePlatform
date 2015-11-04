using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class PhysicalConfigDedicated
    {
        public void InitDefaults()
        {
        }

        public additionalSpectrumEmissionCA_r10_Type additionalSpectrumEmissionCA_r10 { get; set; }

        public antennaInfo_Type antennaInfo { get; set; }

        public antennaInfo_r10_Type antennaInfo_r10 { get; set; }

        public AntennaInfoDedicated_v12xx antennaInfo_v12xx { get; set; }

        public AntennaInfoDedicated_v920 antennaInfo_v920 { get; set; }

        public AntennaInfoUL_r10 antennaInfoUL_r10 { get; set; }

        public bool? cif_Presence_r10 { get; set; }

        public CQI_ReportConfig cqi_ReportConfig { get; set; }

        public CQI_ReportConfig_r10 cqi_ReportConfig_r10 { get; set; }

        public CQI_ReportConfig_v1130 cqi_ReportConfig_v1130 { get; set; }

        public CQI_ReportConfig_v920 cqi_ReportConfig_v920 { get; set; }

        public CSI_RS_Config_r10 csi_RS_Config_r10 { get; set; }

        public List<CSI_RS_ConfigNZP_r11> csi_RS_ConfigNZPToAddModList_r11 { get; set; }

        public List<long> csi_RS_ConfigNZPToReleaseList_r11 { get; set; }

        public List<CSI_RS_ConfigZP_r11> csi_RS_ConfigZPToAddModList_r11 { get; set; }

        public List<long> csi_RS_ConfigZPToReleaseList_r11 { get; set; }

        public EPDCCH_Config_r11 epdcch_Config_r11 { get; set; }

        public PDSCH_ConfigDedicated pdsch_ConfigDedicated { get; set; }

        public PDSCH_ConfigDedicated_v1130 pdsch_ConfigDedicated_v1130 { get; set; }

        public PUCCH_ConfigDedicated pucch_ConfigDedicated { get; set; }

        public PUCCH_ConfigDedicated_v1020 pucch_ConfigDedicated_v1020 { get; set; }

        public PUCCH_ConfigDedicated_v1130 pucch_ConfigDedicated_v1130 { get; set; }

        public PUSCH_ConfigDedicated pusch_ConfigDedicated { get; set; }

        public PUSCH_ConfigDedicated_v1020 pusch_ConfigDedicated_v1020 { get; set; }

        public PUSCH_ConfigDedicated_v1130 pusch_ConfigDedicated_v1130 { get; set; }

        public SchedulingRequestConfig schedulingRequestConfig { get; set; }

        public SchedulingRequestConfig_v1020 schedulingRequestConfig_v1020 { get; set; }

        public SoundingRS_UL_ConfigDedicated soundingRS_UL_ConfigDedicated { get; set; }

        public SoundingRS_UL_ConfigDedicated_v1020 soundingRS_UL_ConfigDedicated_v1020 { get; set; }

        public SoundingRS_UL_ConfigDedicatedAperiodic_r10 soundingRS_UL_ConfigDedicatedAperiodic_r10 { get; set; }

        public TPC_PDCCH_Config tpc_PDCCH_ConfigPUCCH { get; set; }

        public TPC_PDCCH_Config tpc_PDCCH_ConfigPUSCH { get; set; }

        public UplinkPowerControlDedicated uplinkPowerControlDedicated { get; set; }

        public UplinkPowerControlDedicated_v1020 uplinkPowerControlDedicated_v1020 { get; set; }

        public UplinkPowerControlDedicated_v1130 uplinkPowerControlDedicated_v1130 { get; set; }

        [Serializable]
        public class additionalSpectrumEmissionCA_r10_Type
        {
            public void InitDefaults()
            {
            }

            public object release { get; set; }

            public setup_Type setup { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public additionalSpectrumEmissionCA_r10_Type Decode(BitArrayInputStream input)
                {
                    additionalSpectrumEmissionCA_r10_Type type = new additionalSpectrumEmissionCA_r10_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            return type;

                        case 1:
                            type.setup = setup_Type.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }

            [Serializable]
            public class setup_Type
            {
                public void InitDefaults()
                {
                }

                public long additionalSpectrumEmissionPCell_r10 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public setup_Type Decode(BitArrayInputStream input)
                    {
                        setup_Type type = new setup_Type();
                        type.InitDefaults();
                        type.additionalSpectrumEmissionPCell_r10 = input.readBits(5) + 1;
                        return type;
                    }
                }
            }
        }

        [Serializable]
        public class antennaInfo_r10_Type
        {
            public void InitDefaults()
            {
            }

            public object defaultValue { get; set; }

            public AntennaInfoDedicated_r10 explicitValue_r10 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public antennaInfo_r10_Type Decode(BitArrayInputStream input)
                {
                    antennaInfo_r10_Type type = new antennaInfo_r10_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.explicitValue_r10 = AntennaInfoDedicated_r10.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        [Serializable]
        public class antennaInfo_Type
        {
            public void InitDefaults()
            {
            }

            public object defaultValue { get; set; }

            public AntennaInfoDedicated explicitValue { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public antennaInfo_Type Decode(BitArrayInputStream input)
                {
                    antennaInfo_Type type = new antennaInfo_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.explicitValue = AntennaInfoDedicated.PerDecoder.Instance.Decode(input);
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

            public PhysicalConfigDedicated Decode(BitArrayInputStream input)
            {
                BitMaskStream stream2;
                PhysicalConfigDedicated dedicated = new PhysicalConfigDedicated();
                dedicated.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 10);
                if (stream.Read())
                {
                    dedicated.pdsch_ConfigDedicated = PDSCH_ConfigDedicated.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    dedicated.pucch_ConfigDedicated = PUCCH_ConfigDedicated.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    dedicated.pusch_ConfigDedicated = PUSCH_ConfigDedicated.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    dedicated.uplinkPowerControlDedicated = UplinkPowerControlDedicated.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    dedicated.tpc_PDCCH_ConfigPUCCH = TPC_PDCCH_Config.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    dedicated.tpc_PDCCH_ConfigPUSCH = TPC_PDCCH_Config.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    dedicated.cqi_ReportConfig = CQI_ReportConfig.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    dedicated.soundingRS_UL_ConfigDedicated = SoundingRS_UL_ConfigDedicated.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    dedicated.antennaInfo = antennaInfo_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    dedicated.schedulingRequestConfig = SchedulingRequestConfig.PerDecoder.Instance.Decode(input);
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 2);
                    if (stream2.Read())
                    {
                        dedicated.cqi_ReportConfig_v920 = CQI_ReportConfig_v920.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        dedicated.antennaInfo_v920 = AntennaInfoDedicated_v920.PerDecoder.Instance.Decode(input);
                    }
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 11);
                    if (stream2.Read())
                    {
                        dedicated.antennaInfo_r10 = antennaInfo_r10_Type.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        dedicated.antennaInfoUL_r10 = AntennaInfoUL_r10.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        dedicated.cif_Presence_r10 = input.readBit() == 1;
                    }
                    if (stream2.Read())
                    {
                        dedicated.cqi_ReportConfig_r10 = CQI_ReportConfig_r10.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        dedicated.csi_RS_Config_r10 = CSI_RS_Config_r10.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        dedicated.pucch_ConfigDedicated_v1020 = PUCCH_ConfigDedicated_v1020.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        dedicated.pusch_ConfigDedicated_v1020 = PUSCH_ConfigDedicated_v1020.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        dedicated.schedulingRequestConfig_v1020 = SchedulingRequestConfig_v1020.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        dedicated.soundingRS_UL_ConfigDedicated_v1020 = SoundingRS_UL_ConfigDedicated_v1020.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        dedicated.soundingRS_UL_ConfigDedicatedAperiodic_r10 = SoundingRS_UL_ConfigDedicatedAperiodic_r10.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        dedicated.uplinkPowerControlDedicated_v1020 = UplinkPowerControlDedicated_v1020.PerDecoder.Instance.Decode(input);
                    }
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        dedicated.additionalSpectrumEmissionCA_r10 = additionalSpectrumEmissionCA_r10_Type.PerDecoder.Instance.Decode(input);
                    }
                }
                if (flag)
                {
                    int num2;
                    stream2 = new BitMaskStream(input, 10);
                    if (stream2.Read())
                    {
                        dedicated.csi_RS_ConfigNZPToReleaseList_r11 = new List<long>();
                        num2 = 2;
                        int num3 = input.readBits(num2) + 1;
                        for (int i = 0; i < num3; i++)
                        {
                            long item = input.readBits(2) + 1;
                            dedicated.csi_RS_ConfigNZPToReleaseList_r11.Add(item);
                        }
                    }
                    if (stream2.Read())
                    {
                        dedicated.csi_RS_ConfigNZPToAddModList_r11 = new List<CSI_RS_ConfigNZP_r11>();
                        num2 = 2;
                        int num6 = input.readBits(num2) + 1;
                        for (int j = 0; j < num6; j++)
                        {
                            CSI_RS_ConfigNZP_r11 _r = CSI_RS_ConfigNZP_r11.PerDecoder.Instance.Decode(input);
                            dedicated.csi_RS_ConfigNZPToAddModList_r11.Add(_r);
                        }
                    }
                    if (stream2.Read())
                    {
                        dedicated.csi_RS_ConfigZPToReleaseList_r11 = new List<long>();
                        num2 = 2;
                        int num8 = input.readBits(num2) + 1;
                        for (int k = 0; k < num8; k++)
                        {
                            long num10 = input.readBits(2) + 1;
                            dedicated.csi_RS_ConfigZPToReleaseList_r11.Add(num10);
                        }
                    }
                    if (stream2.Read())
                    {
                        dedicated.csi_RS_ConfigZPToAddModList_r11 = new List<CSI_RS_ConfigZP_r11>();
                        num2 = 2;
                        int num11 = input.readBits(num2) + 1;
                        for (int m = 0; m < num11; m++)
                        {
                            CSI_RS_ConfigZP_r11 _r2 = CSI_RS_ConfigZP_r11.PerDecoder.Instance.Decode(input);
                            dedicated.csi_RS_ConfigZPToAddModList_r11.Add(_r2);
                        }
                    }
                    if (stream2.Read())
                    {
                        dedicated.epdcch_Config_r11 = EPDCCH_Config_r11.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        dedicated.pdsch_ConfigDedicated_v1130 = PDSCH_ConfigDedicated_v1130.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        dedicated.cqi_ReportConfig_v1130 = CQI_ReportConfig_v1130.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        dedicated.pucch_ConfigDedicated_v1130 = PUCCH_ConfigDedicated_v1130.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        dedicated.pusch_ConfigDedicated_v1130 = PUSCH_ConfigDedicated_v1130.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        dedicated.uplinkPowerControlDedicated_v1130 = UplinkPowerControlDedicated_v1130.PerDecoder.Instance.Decode(input);
                    }
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        dedicated.antennaInfo_v12xx = AntennaInfoDedicated_v12xx.PerDecoder.Instance.Decode(input);
                    }
                }
                return dedicated;
            }
        }
    }

    [Serializable]
    public class PhysicalConfigDedicatedSCell_r10
    {
        public void InitDefaults()
        {
        }

        public AntennaInfoDedicated_v12xx antennaInfo_v12xx { get; set; }

        public CQI_ReportConfig_v1130 cqi_ReportConfig_v1130 { get; set; }

        public List<CSI_RS_ConfigNZP_r11> csi_RS_ConfigNZPToAddModList_r11 { get; set; }

        public List<long> csi_RS_ConfigNZPToReleaseList_r11 { get; set; }

        public List<CSI_RS_ConfigZP_r11> csi_RS_ConfigZPToAddModList_r11 { get; set; }

        public List<long> csi_RS_ConfigZPToReleaseList_r11 { get; set; }

        public EPDCCH_Config_r11 epdcch_Config_r11 { get; set; }

        public nonUL_Configuration_r10_Type nonUL_Configuration_r10 { get; set; }

        public PDSCH_ConfigDedicated_v1130 pdsch_ConfigDedicated_v1130 { get; set; }

        public PUSCH_ConfigDedicated_v1130 pusch_ConfigDedicated_v1130 { get; set; }

        public ul_Configuration_r10_Type ul_Configuration_r10 { get; set; }

        public UplinkPowerControlDedicated_v1130 uplinkPowerControlDedicatedSCell_v1130 { get; set; }

        [Serializable]
        public class nonUL_Configuration_r10_Type
        {
            public void InitDefaults()
            {
            }

            public AntennaInfoDedicated_r10 antennaInfo_r10 { get; set; }

            public CrossCarrierSchedulingConfig_r10 crossCarrierSchedulingConfig_r10 { get; set; }

            public CSI_RS_Config_r10 csi_RS_Config_r10 { get; set; }

            public PDSCH_ConfigDedicated pdsch_ConfigDedicated_r10 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public nonUL_Configuration_r10_Type Decode(BitArrayInputStream input)
                {
                    nonUL_Configuration_r10_Type type = new nonUL_Configuration_r10_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 4);
                    if (stream.Read())
                    {
                        type.antennaInfo_r10 = AntennaInfoDedicated_r10.PerDecoder.Instance.Decode(input);
                    }
                    if (stream.Read())
                    {
                        type.crossCarrierSchedulingConfig_r10 
                            = CrossCarrierSchedulingConfig_r10.PerDecoder.Instance.Decode(input);
                    }
                    if (stream.Read())
                    {
                        type.csi_RS_Config_r10 = CSI_RS_Config_r10.PerDecoder.Instance.Decode(input);
                    }
                    if (stream.Read())
                    {
                        type.pdsch_ConfigDedicated_r10 = PDSCH_ConfigDedicated.PerDecoder.Instance.Decode(input);
                    }
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PhysicalConfigDedicatedSCell_r10 Decode(BitArrayInputStream input)
            {
                BitMaskStream stream2;
                PhysicalConfigDedicatedSCell_r10 _r = new PhysicalConfigDedicatedSCell_r10();
                _r.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    _r.nonUL_Configuration_r10 = nonUL_Configuration_r10_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    _r.ul_Configuration_r10 = ul_Configuration_r10_Type.PerDecoder.Instance.Decode(input);
                }
                if (flag)
                {
                    int num2;
                    stream2 = new BitMaskStream(input, 9);
                    if (stream2.Read())
                    {
                        _r.csi_RS_ConfigNZPToReleaseList_r11 = new List<long>();
                        num2 = 2;
                        int num3 = input.readBits(num2) + 1;
                        for (int i = 0; i < num3; i++)
                        {
                            long item = input.readBits(2) + 1;
                            _r.csi_RS_ConfigNZPToReleaseList_r11.Add(item);
                        }
                    }
                    if (stream2.Read())
                    {
                        _r.csi_RS_ConfigNZPToAddModList_r11 = new List<CSI_RS_ConfigNZP_r11>();
                        num2 = 2;
                        int num6 = input.readBits(num2) + 1;
                        for (int j = 0; j < num6; j++)
                        {
                            CSI_RS_ConfigNZP_r11 _r2 = CSI_RS_ConfigNZP_r11.PerDecoder.Instance.Decode(input);
                            _r.csi_RS_ConfigNZPToAddModList_r11.Add(_r2);
                        }
                    }
                    if (stream2.Read())
                    {
                        _r.csi_RS_ConfigZPToReleaseList_r11 = new List<long>();
                        num2 = 2;
                        int num8 = input.readBits(num2) + 1;
                        for (int k = 0; k < num8; k++)
                        {
                            long num10 = input.readBits(2) + 1;
                            _r.csi_RS_ConfigZPToReleaseList_r11.Add(num10);
                        }
                    }
                    if (stream2.Read())
                    {
                        _r.csi_RS_ConfigZPToAddModList_r11 = new List<CSI_RS_ConfigZP_r11>();
                        num2 = 2;
                        int num11 = input.readBits(num2) + 1;
                        for (int m = 0; m < num11; m++)
                        {
                            CSI_RS_ConfigZP_r11 _r3 = CSI_RS_ConfigZP_r11.PerDecoder.Instance.Decode(input);
                            _r.csi_RS_ConfigZPToAddModList_r11.Add(_r3);
                        }
                    }
                    if (stream2.Read())
                    {
                        _r.epdcch_Config_r11 = EPDCCH_Config_r11.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        _r.pdsch_ConfigDedicated_v1130 = PDSCH_ConfigDedicated_v1130.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        _r.cqi_ReportConfig_v1130 = CQI_ReportConfig_v1130.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        _r.pusch_ConfigDedicated_v1130 = PUSCH_ConfigDedicated_v1130.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        _r.uplinkPowerControlDedicatedSCell_v1130 = UplinkPowerControlDedicated_v1130.PerDecoder.Instance.Decode(input);
                    }
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        _r.antennaInfo_v12xx = AntennaInfoDedicated_v12xx.PerDecoder.Instance.Decode(input);
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

            public AntennaInfoUL_r10 antennaInfoUL_r10 { get; set; }

            public CQI_ReportConfigSCell_r10 cqi_ReportConfigSCell_r10 { get; set; }

            public PUSCH_ConfigDedicatedSCell_r10 pusch_ConfigDedicatedSCell_r10 { get; set; }

            public SoundingRS_UL_ConfigDedicated soundingRS_UL_ConfigDedicated_r10 { get; set; }

            public SoundingRS_UL_ConfigDedicated_v1020 soundingRS_UL_ConfigDedicated_v1020 { get; set; }

            public SoundingRS_UL_ConfigDedicatedAperiodic_r10 soundingRS_UL_ConfigDedicatedAperiodic_r10 { get; set; }

            public UplinkPowerControlDedicatedSCell_r10 uplinkPowerControlDedicatedSCell_r10 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public ul_Configuration_r10_Type Decode(BitArrayInputStream input)
                {
                    ul_Configuration_r10_Type type = new ul_Configuration_r10_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 7);
                    if (stream.Read())
                    {
                        type.antennaInfoUL_r10 = AntennaInfoUL_r10.PerDecoder.Instance.Decode(input);
                    }
                    if (stream.Read())
                    {
                        type.pusch_ConfigDedicatedSCell_r10 = PUSCH_ConfigDedicatedSCell_r10.PerDecoder.Instance.Decode(input);
                    }
                    if (stream.Read())
                    {
                        type.uplinkPowerControlDedicatedSCell_r10 = UplinkPowerControlDedicatedSCell_r10.PerDecoder.Instance.Decode(input);
                    }
                    if (stream.Read())
                    {
                        type.cqi_ReportConfigSCell_r10 = CQI_ReportConfigSCell_r10.PerDecoder.Instance.Decode(input);
                    }
                    if (stream.Read())
                    {
                        type.soundingRS_UL_ConfigDedicated_r10 = SoundingRS_UL_ConfigDedicated.PerDecoder.Instance.Decode(input);
                    }
                    if (stream.Read())
                    {
                        type.soundingRS_UL_ConfigDedicated_v1020 = SoundingRS_UL_ConfigDedicated_v1020.PerDecoder.Instance.Decode(input);
                    }
                    if (stream.Read())
                    {
                        type.soundingRS_UL_ConfigDedicatedAperiodic_r10 = SoundingRS_UL_ConfigDedicatedAperiodic_r10.PerDecoder.Instance.Decode(input);
                    }
                    return type;
                }
            }
        }
    }

}
