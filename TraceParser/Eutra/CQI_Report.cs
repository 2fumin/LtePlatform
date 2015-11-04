using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CQI_ReportConfig
    {
        public void InitDefaults()
        {
        }

        public CQI_ReportModeAperiodic? cqi_ReportModeAperiodic { get; set; }

        public CQI_ReportPeriodic cqi_ReportPeriodic { get; set; }

        public long nomPDSCH_RS_EPRE_Offset { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CQI_ReportConfig Decode(BitArrayInputStream input)
            {
                CQI_ReportConfig config = new CQI_ReportConfig();
                config.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    int nBits = 3;
                    config.cqi_ReportModeAperiodic = (CQI_ReportModeAperiodic)input.readBits(nBits);
                }
                config.nomPDSCH_RS_EPRE_Offset = input.readBits(3) + -1;
                if (stream.Read())
                {
                    config.cqi_ReportPeriodic = CQI_ReportPeriodic.PerDecoder.Instance.Decode(input);
                }
                return config;
            }
        }
    }

    [Serializable]
    public class CQI_ReportConfig_r10
    {
        public void InitDefaults()
        {
        }

        public CQI_ReportAperiodic_r10 cqi_ReportAperiodic_r10 { get; set; }

        public CQI_ReportPeriodic_r10 cqi_ReportPeriodic_r10 { get; set; }

        public csi_SubframePatternConfig_r10_Type csi_SubframePatternConfig_r10 { get; set; }

        public long nomPDSCH_RS_EPRE_Offset { get; set; }

        public pmi_RI_Report_r9_Enum? pmi_RI_Report_r9 { get; set; }

        [Serializable]
        public class csi_SubframePatternConfig_r10_Type
        {
            public void InitDefaults()
            {
            }

            public object release { get; set; }

            public setup_Type setup { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public csi_SubframePatternConfig_r10_Type Decode(BitArrayInputStream input)
                {
                    csi_SubframePatternConfig_r10_Type type = new csi_SubframePatternConfig_r10_Type();
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

                public MeasSubframePattern_r10 csi_MeasSubframeSet1_r10 { get; set; }

                public MeasSubframePattern_r10 csi_MeasSubframeSet2_r10 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public setup_Type Decode(BitArrayInputStream input)
                    {
                        setup_Type type = new setup_Type();
                        type.InitDefaults();
                        type.csi_MeasSubframeSet1_r10 = MeasSubframePattern_r10.PerDecoder.Instance.Decode(input);
                        type.csi_MeasSubframeSet2_r10 = MeasSubframePattern_r10.PerDecoder.Instance.Decode(input);
                        return type;
                    }
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CQI_ReportConfig_r10 Decode(BitArrayInputStream input)
            {
                CQI_ReportConfig_r10 _r = new CQI_ReportConfig_r10();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 4);
                if (stream.Read())
                {
                    _r.cqi_ReportAperiodic_r10 = CQI_ReportAperiodic_r10.PerDecoder.Instance.Decode(input);
                }
                _r.nomPDSCH_RS_EPRE_Offset = input.readBits(3) + -1;
                if (stream.Read())
                {
                    _r.cqi_ReportPeriodic_r10 = CQI_ReportPeriodic_r10.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    const int nBits = 1;
                    _r.pmi_RI_Report_r9 = (pmi_RI_Report_r9_Enum)input.readBits(nBits);
                }
                if (stream.Read())
                {
                    _r.csi_SubframePatternConfig_r10 = csi_SubframePatternConfig_r10_Type.PerDecoder.Instance.Decode(input);
                }
                return _r;
            }
        }

        public enum pmi_RI_Report_r9_Enum
        {
            setup
        }
    }

    [Serializable]
    public class CQI_ReportConfig_v1130
    {
        public void InitDefaults()
        {
        }

        public CQI_ReportBoth_r11 cqi_ReportBoth_r11 { get; set; }

        public CQI_ReportPeriodic_v1130 cqi_ReportPeriodic_v1130 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CQI_ReportConfig_v1130 Decode(BitArrayInputStream input)
            {
                CQI_ReportConfig_v1130 _v = new CQI_ReportConfig_v1130();
                _v.InitDefaults();
                _v.cqi_ReportPeriodic_v1130 = CQI_ReportPeriodic_v1130.PerDecoder.Instance.Decode(input);
                _v.cqi_ReportBoth_r11 = CQI_ReportBoth_r11.PerDecoder.Instance.Decode(input);
                return _v;
            }
        }
    }

    [Serializable]
    public class CQI_ReportConfig_v920
    {
        public void InitDefaults()
        {
        }

        public cqi_Mask_r9_Enum? cqi_Mask_r9 { get; set; }

        public pmi_RI_Report_r9_Enum? pmi_RI_Report_r9 { get; set; }

        public enum cqi_Mask_r9_Enum
        {
            setup
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CQI_ReportConfig_v920 Decode(BitArrayInputStream input)
            {
                int num2;
                CQI_ReportConfig_v920 _v = new CQI_ReportConfig_v920();
                _v.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    num2 = 1;
                    _v.cqi_Mask_r9 = (cqi_Mask_r9_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _v.pmi_RI_Report_r9 = (pmi_RI_Report_r9_Enum)input.readBits(num2);
                }
                return _v;
            }
        }

        public enum pmi_RI_Report_r9_Enum
        {
            setup
        }
    }

    [Serializable]
    public class CQI_ReportConfigSCell_r10
    {
        public void InitDefaults()
        {
        }

        public CQI_ReportModeAperiodic? cqi_ReportModeAperiodic_r10 { get; set; }

        public CQI_ReportPeriodic_r10 cqi_ReportPeriodicSCell_r10 { get; set; }

        public long nomPDSCH_RS_EPRE_Offset_r10 { get; set; }

        public pmi_RI_Report_r10_Enum? pmi_RI_Report_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CQI_ReportConfigSCell_r10 Decode(BitArrayInputStream input)
            {
                int num2;
                CQI_ReportConfigSCell_r10 _r = new CQI_ReportConfigSCell_r10();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    num2 = 3;
                    _r.cqi_ReportModeAperiodic_r10 = (CQI_ReportModeAperiodic)input.readBits(num2);
                }
                _r.nomPDSCH_RS_EPRE_Offset_r10 = input.readBits(3) + -1;
                if (stream.Read())
                {
                    _r.cqi_ReportPeriodicSCell_r10 = CQI_ReportPeriodic_r10.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _r.pmi_RI_Report_r10 = (pmi_RI_Report_r10_Enum)input.readBits(num2);
                }
                return _r;
            }
        }

        public enum pmi_RI_Report_r10_Enum
        {
            setup
        }
    }

    [Serializable]
    public class CQI_ReportAperiodic_r10
    {
        public void InitDefaults()
        {
        }

        public object release { get; set; }

        public setup_Type setup { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CQI_ReportAperiodic_r10 Decode(BitArrayInputStream input)
            {
                CQI_ReportAperiodic_r10 _r = new CQI_ReportAperiodic_r10();
                _r.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        return _r;

                    case 1:
                        _r.setup = setup_Type.PerDecoder.Instance.Decode(input);
                        return _r;
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

            public aperiodicCSI_Trigger_r10_Type aperiodicCSI_Trigger_r10 { get; set; }

            public CQI_ReportModeAperiodic cqi_ReportModeAperiodic_r10 { get; set; }

            [Serializable]
            public class aperiodicCSI_Trigger_r10_Type
            {
                public void InitDefaults()
                {
                }

                public string trigger1_r10 { get; set; }

                public string trigger2_r10 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public aperiodicCSI_Trigger_r10_Type Decode(BitArrayInputStream input)
                    {
                        aperiodicCSI_Trigger_r10_Type type = new aperiodicCSI_Trigger_r10_Type();
                        type.InitDefaults();
                        type.trigger1_r10 = input.readBitString(8);
                        type.trigger2_r10 = input.readBitString(8);
                        return type;
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public setup_Type Decode(BitArrayInputStream input)
                {
                    setup_Type type = new setup_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 1);
                    const int nBits = 3;
                    type.cqi_ReportModeAperiodic_r10 = (CQI_ReportModeAperiodic)input.readBits(nBits);
                    if (stream.Read())
                    {
                        type.aperiodicCSI_Trigger_r10 = aperiodicCSI_Trigger_r10_Type.PerDecoder.Instance.Decode(input);
                    }
                    return type;
                }
            }
        }
    }

    [Serializable]
    public class CQI_ReportAperiodicProc_r11
    {
        public void InitDefaults()
        {
        }

        public CQI_ReportModeAperiodic cqi_ReportModeAperiodic_r11 { get; set; }

        public bool trigger01_r11 { get; set; }

        public bool trigger10_r11 { get; set; }

        public bool trigger11_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CQI_ReportAperiodicProc_r11 Decode(BitArrayInputStream input)
            {
                CQI_ReportAperiodicProc_r11 _r = new CQI_ReportAperiodicProc_r11();
                _r.InitDefaults();
                int nBits = 3;
                _r.cqi_ReportModeAperiodic_r11 = (CQI_ReportModeAperiodic)input.readBits(nBits);
                _r.trigger01_r11 = input.readBit() == 1;
                _r.trigger10_r11 = input.readBit() == 1;
                _r.trigger11_r11 = input.readBit() == 1;
                return _r;
            }
        }
    }

    [Serializable]
    public class CQI_ReportBoth_r11
    {
        public void InitDefaults()
        {
        }

        public List<CSI_IM_Config_r11> csi_IM_ConfigToAddModList_r11 { get; set; }

        public List<long> csi_IM_ConfigToReleaseList_r11 { get; set; }

        public List<CSI_Process_r11> csi_ProcessToAddModList_r11 { get; set; }

        public List<long> csi_ProcessToReleaseList_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CQI_ReportBoth_r11 Decode(BitArrayInputStream input)
            {
                int num2;
                CQI_ReportBoth_r11 _r = new CQI_ReportBoth_r11();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 4);
                if (stream.Read())
                {
                    _r.csi_IM_ConfigToReleaseList_r11 = new List<long>();
                    num2 = 2;
                    int num3 = input.readBits(num2) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        long item = input.readBits(2) + 1;
                        _r.csi_IM_ConfigToReleaseList_r11.Add(item);
                    }
                }
                if (stream.Read())
                {
                    _r.csi_IM_ConfigToAddModList_r11 = new List<CSI_IM_Config_r11>();
                    num2 = 2;
                    int num6 = input.readBits(num2) + 1;
                    for (int j = 0; j < num6; j++)
                    {
                        CSI_IM_Config_r11 _r2 = CSI_IM_Config_r11.PerDecoder.Instance.Decode(input);
                        _r.csi_IM_ConfigToAddModList_r11.Add(_r2);
                    }
                }
                if (stream.Read())
                {
                    _r.csi_ProcessToReleaseList_r11 = new List<long>();
                    num2 = 2;
                    int num8 = input.readBits(num2) + 1;
                    for (int k = 0; k < num8; k++)
                    {
                        long num10 = input.readBits(2) + 1;
                        _r.csi_ProcessToReleaseList_r11.Add(num10);
                    }
                }
                if (stream.Read())
                {
                    _r.csi_ProcessToAddModList_r11 = new List<CSI_Process_r11>();
                    num2 = 2;
                    int num11 = input.readBits(num2) + 1;
                    for (int m = 0; m < num11; m++)
                    {
                        CSI_Process_r11 _r3 = CSI_Process_r11.PerDecoder.Instance.Decode(input);
                        _r.csi_ProcessToAddModList_r11.Add(_r3);
                    }
                }
                return _r;
            }
        }
    }

    [Serializable]
    public class CQI_ReportBothProc_r11
    {
        public void InitDefaults()
        {
        }

        public pmi_RI_Report_r11_Enum? pmi_RI_Report_r11 { get; set; }

        public long? ri_Ref_CSI_ProcessId_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CQI_ReportBothProc_r11 Decode(BitArrayInputStream input)
            {
                CQI_ReportBothProc_r11 _r = new CQI_ReportBothProc_r11();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    _r.ri_Ref_CSI_ProcessId_r11 = input.readBits(2) + 1;
                }
                if (stream.Read())
                {
                    int nBits = 1;
                    _r.pmi_RI_Report_r11 = (pmi_RI_Report_r11_Enum)input.readBits(nBits);
                }
                return _r;
            }
        }

        public enum pmi_RI_Report_r11_Enum
        {
            setup
        }
    }

    [Serializable]
    public class CQI_ReportPeriodic
    {
        public void InitDefaults()
        {
        }

        public object release { get; set; }

        public setup_Type setup { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CQI_ReportPeriodic Decode(BitArrayInputStream input)
            {
                CQI_ReportPeriodic periodic = new CQI_ReportPeriodic();
                periodic.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        return periodic;

                    case 1:
                        periodic.setup = setup_Type.PerDecoder.Instance.Decode(input);
                        return periodic;
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

            public cqi_FormatIndicatorPeriodic_Type cqi_FormatIndicatorPeriodic { get; set; }

            public long cqi_pmi_ConfigIndex { get; set; }

            public long cqi_PUCCH_ResourceIndex { get; set; }

            public long? ri_ConfigIndex { get; set; }

            public bool simultaneousAckNackAndCQI { get; set; }

            [Serializable]
            public class cqi_FormatIndicatorPeriodic_Type
            {
                public void InitDefaults()
                {
                }

                public subbandCQI_Type subbandCQI { get; set; }

                public object widebandCQI { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public cqi_FormatIndicatorPeriodic_Type Decode(BitArrayInputStream input)
                    {
                        cqi_FormatIndicatorPeriodic_Type type = new cqi_FormatIndicatorPeriodic_Type();
                        type.InitDefaults();
                        switch (input.readBits(1))
                        {
                            case 0:
                                return type;

                            case 1:
                                type.subbandCQI = subbandCQI_Type.PerDecoder.Instance.Decode(input);
                                return type;
                        }
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                }

                [Serializable]
                public class subbandCQI_Type
                {
                    public void InitDefaults()
                    {
                    }

                    public long k { get; set; }

                    public class PerDecoder
                    {
                        public static readonly PerDecoder Instance = new PerDecoder();

                        public subbandCQI_Type Decode(BitArrayInputStream input)
                        {
                            subbandCQI_Type type = new subbandCQI_Type();
                            type.InitDefaults();
                            type.k = input.readBits(2) + 1;
                            return type;
                        }
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public setup_Type Decode(BitArrayInputStream input)
                {
                    setup_Type type = new setup_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 1);
                    type.cqi_PUCCH_ResourceIndex = input.readBits(11);
                    type.cqi_pmi_ConfigIndex = input.readBits(10);
                    type.cqi_FormatIndicatorPeriodic = cqi_FormatIndicatorPeriodic_Type.PerDecoder.Instance.Decode(input);
                    if (stream.Read())
                    {
                        type.ri_ConfigIndex = input.readBits(10);
                    }
                    type.simultaneousAckNackAndCQI = input.readBit() == 1;
                    return type;
                }
            }
        }
    }

    [Serializable]
    public class CQI_ReportPeriodic_r10
    {
        public void InitDefaults()
        {
        }

        public object release { get; set; }

        public setup_Type setup { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CQI_ReportPeriodic_r10 Decode(BitArrayInputStream input)
            {
                CQI_ReportPeriodic_r10 _r = new CQI_ReportPeriodic_r10();
                _r.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        return _r;

                    case 1:
                        _r.setup = setup_Type.PerDecoder.Instance.Decode(input);
                        return _r;
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

            public cqi_FormatIndicatorPeriodic_r10_Type cqi_FormatIndicatorPeriodic_r10 { get; set; }

            public cqi_Mask_r9_Enum? cqi_Mask_r9 { get; set; }

            public long cqi_pmi_ConfigIndex { get; set; }

            public long cqi_PUCCH_ResourceIndex_r10 { get; set; }

            public long? cqi_PUCCH_ResourceIndexP1_r10 { get; set; }

            public csi_ConfigIndex_r10_Type csi_ConfigIndex_r10 { get; set; }

            public long? ri_ConfigIndex { get; set; }

            public bool simultaneousAckNackAndCQI { get; set; }

            [Serializable]
            public class cqi_FormatIndicatorPeriodic_r10_Type
            {
                public void InitDefaults()
                {
                }

                public subbandCQI_r10_Type subbandCQI_r10 { get; set; }

                public widebandCQI_r10_Type widebandCQI_r10 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public cqi_FormatIndicatorPeriodic_r10_Type Decode(BitArrayInputStream input)
                    {
                        cqi_FormatIndicatorPeriodic_r10_Type type = new cqi_FormatIndicatorPeriodic_r10_Type();
                        type.InitDefaults();
                        switch (input.readBits(1))
                        {
                            case 0:
                                type.widebandCQI_r10 = widebandCQI_r10_Type.PerDecoder.Instance.Decode(input);
                                return type;

                            case 1:
                                type.subbandCQI_r10 = subbandCQI_r10_Type.PerDecoder.Instance.Decode(input);
                                return type;
                        }
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                }

                [Serializable]
                public class subbandCQI_r10_Type
                {
                    public void InitDefaults()
                    {
                    }

                    public long k { get; set; }

                    public periodicityFactor_r10_Enum periodicityFactor_r10 { get; set; }

                    public class PerDecoder
                    {
                        public static readonly PerDecoder Instance = new PerDecoder();

                        public subbandCQI_r10_Type Decode(BitArrayInputStream input)
                        {
                            subbandCQI_r10_Type type = new subbandCQI_r10_Type();
                            type.InitDefaults();
                            type.k = input.readBits(2) + 1;
                            int nBits = 1;
                            type.periodicityFactor_r10 = (periodicityFactor_r10_Enum)input.readBits(nBits);
                            return type;
                        }
                    }

                    public enum periodicityFactor_r10_Enum
                    {
                        n2,
                        n4
                    }
                }

                [Serializable]
                public class widebandCQI_r10_Type
                {
                    public void InitDefaults()
                    {
                    }

                    public csi_ReportMode_r10_Enum? csi_ReportMode_r10 { get; set; }

                    public enum csi_ReportMode_r10_Enum
                    {
                        submode1,
                        submode2
                    }

                    public class PerDecoder
                    {
                        public static readonly PerDecoder Instance = new PerDecoder();

                        public widebandCQI_r10_Type Decode(BitArrayInputStream input)
                        {
                            widebandCQI_r10_Type type = new widebandCQI_r10_Type();
                            type.InitDefaults();
                            BitMaskStream stream = new BitMaskStream(input, 1);
                            if (stream.Read())
                            {
                                const int nBits = 1;
                                type.csi_ReportMode_r10 = (csi_ReportMode_r10_Enum)input.readBits(nBits);
                            }
                            return type;
                        }
                    }
                }
            }

            public enum cqi_Mask_r9_Enum
            {
                setup
            }

            [Serializable]
            public class csi_ConfigIndex_r10_Type
            {
                public void InitDefaults()
                {
                }

                public object release { get; set; }

                public setup_Type setup { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public csi_ConfigIndex_r10_Type Decode(BitArrayInputStream input)
                    {
                        csi_ConfigIndex_r10_Type type = new csi_ConfigIndex_r10_Type();
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

                    public long cqi_pmi_ConfigIndex2_r10 { get; set; }

                    public long? ri_ConfigIndex2_r10 { get; set; }

                    public class PerDecoder
                    {
                        public static readonly PerDecoder Instance = new PerDecoder();

                        public setup_Type Decode(BitArrayInputStream input)
                        {
                            setup_Type type = new setup_Type();
                            type.InitDefaults();
                            BitMaskStream stream = new BitMaskStream(input, 1);
                            type.cqi_pmi_ConfigIndex2_r10 = input.readBits(10);
                            if (stream.Read())
                            {
                                type.ri_ConfigIndex2_r10 = input.readBits(10);
                            }
                            return type;
                        }
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public setup_Type Decode(BitArrayInputStream input)
                {
                    setup_Type type = new setup_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 4);
                    type.cqi_PUCCH_ResourceIndex_r10 = input.readBits(11);
                    if (stream.Read())
                    {
                        type.cqi_PUCCH_ResourceIndexP1_r10 = input.readBits(11);
                    }
                    type.cqi_pmi_ConfigIndex = input.readBits(10);
                    type.cqi_FormatIndicatorPeriodic_r10 = cqi_FormatIndicatorPeriodic_r10_Type.PerDecoder.Instance.Decode(input);
                    if (stream.Read())
                    {
                        type.ri_ConfigIndex = input.readBits(10);
                    }
                    type.simultaneousAckNackAndCQI = input.readBit() == 1;
                    if (stream.Read())
                    {
                        int nBits = 1;
                        type.cqi_Mask_r9 = (cqi_Mask_r9_Enum)input.readBits(nBits);
                    }
                    if (stream.Read())
                    {
                        type.csi_ConfigIndex_r10 = csi_ConfigIndex_r10_Type.PerDecoder.Instance.Decode(input);
                    }
                    return type;
                }
            }
        }
    }

    [Serializable]
    public class CQI_ReportPeriodic_v1130
    {
        public void InitDefaults()
        {
        }

        public List<CQI_ReportPeriodicProcExt_r11> cqi_ReportPeriodicProcExtToAddModList_r11 { get; set; }

        public List<long> cqi_ReportPeriodicProcExtToReleaseList_r11 { get; set; }

        public simultaneousAckNackAndCQI_Format3_r11_Enum? simultaneousAckNackAndCQI_Format3_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CQI_ReportPeriodic_v1130 Decode(BitArrayInputStream input)
            {
                int num2;
                CQI_ReportPeriodic_v1130 _v = new CQI_ReportPeriodic_v1130();
                _v.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    num2 = 1;
                    _v.simultaneousAckNackAndCQI_Format3_r11 = (simultaneousAckNackAndCQI_Format3_r11_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    _v.cqi_ReportPeriodicProcExtToReleaseList_r11 = new List<long>();
                    num2 = 2;
                    int num3 = input.readBits(num2) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        long item = input.readBits(2) + 1;
                        _v.cqi_ReportPeriodicProcExtToReleaseList_r11.Add(item);
                    }
                }
                if (stream.Read())
                {
                    _v.cqi_ReportPeriodicProcExtToAddModList_r11 = new List<CQI_ReportPeriodicProcExt_r11>();
                    num2 = 2;
                    int num6 = input.readBits(num2) + 1;
                    for (int j = 0; j < num6; j++)
                    {
                        CQI_ReportPeriodicProcExt_r11 _r = CQI_ReportPeriodicProcExt_r11.PerDecoder.Instance.Decode(input);
                        _v.cqi_ReportPeriodicProcExtToAddModList_r11.Add(_r);
                    }
                }
                return _v;
            }
        }

        public enum simultaneousAckNackAndCQI_Format3_r11_Enum
        {
            setup
        }
    }

    [Serializable]
    public class CQI_ReportPeriodicProcExt_r11
    {
        public void InitDefaults()
        {
        }

        public cqi_FormatIndicatorPeriodic_r11_Type cqi_FormatIndicatorPeriodic_r11 { get; set; }

        public long cqi_pmi_ConfigIndex_r11 { get; set; }

        public long cqi_ReportPeriodicProcExtId_r11 { get; set; }

        public csi_ConfigIndex_r11_Type csi_ConfigIndex_r11 { get; set; }

        public long? ri_ConfigIndex_r11 { get; set; }

        [Serializable]
        public class cqi_FormatIndicatorPeriodic_r11_Type
        {
            public void InitDefaults()
            {
            }

            public subbandCQI_r11_Type subbandCQI_r11 { get; set; }

            public widebandCQI_r11_Type widebandCQI_r11 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public cqi_FormatIndicatorPeriodic_r11_Type Decode(BitArrayInputStream input)
                {
                    cqi_FormatIndicatorPeriodic_r11_Type type = new cqi_FormatIndicatorPeriodic_r11_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.widebandCQI_r11 = widebandCQI_r11_Type.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.subbandCQI_r11 = subbandCQI_r11_Type.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }

            [Serializable]
            public class subbandCQI_r11_Type
            {
                public void InitDefaults()
                {
                }

                public long k { get; set; }

                public periodicityFactor_r11_Enum periodicityFactor_r11 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public subbandCQI_r11_Type Decode(BitArrayInputStream input)
                    {
                        subbandCQI_r11_Type type = new subbandCQI_r11_Type();
                        type.InitDefaults();
                        type.k = input.readBits(2) + 1;
                        int nBits = 1;
                        type.periodicityFactor_r11 = (periodicityFactor_r11_Enum)input.readBits(nBits);
                        return type;
                    }
                }

                public enum periodicityFactor_r11_Enum
                {
                    n2,
                    n4
                }
            }

            [Serializable]
            public class widebandCQI_r11_Type
            {
                public void InitDefaults()
                {
                }

                public csi_ReportMode_r11_Enum? csi_ReportMode_r11 { get; set; }

                public enum csi_ReportMode_r11_Enum
                {
                    submode1,
                    submode2
                }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public widebandCQI_r11_Type Decode(BitArrayInputStream input)
                    {
                        widebandCQI_r11_Type type = new widebandCQI_r11_Type();
                        type.InitDefaults();
                        BitMaskStream stream = new BitMaskStream(input, 1);
                        if (stream.Read())
                        {
                            const int nBits = 1;
                            type.csi_ReportMode_r11 = (csi_ReportMode_r11_Enum)input.readBits(nBits);
                        }
                        return type;
                    }
                }
            }
        }

        [Serializable]
        public class csi_ConfigIndex_r11_Type
        {
            public void InitDefaults()
            {
            }

            public object release { get; set; }

            public setup_Type setup { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public csi_ConfigIndex_r11_Type Decode(BitArrayInputStream input)
                {
                    csi_ConfigIndex_r11_Type type = new csi_ConfigIndex_r11_Type();
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

                public long cqi_pmi_ConfigIndex2_r11 { get; set; }

                public long? ri_ConfigIndex2_r11 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public setup_Type Decode(BitArrayInputStream input)
                    {
                        setup_Type type = new setup_Type();
                        type.InitDefaults();
                        BitMaskStream stream = new BitMaskStream(input, 1);
                        type.cqi_pmi_ConfigIndex2_r11 = input.readBits(10);
                        if (stream.Read())
                        {
                            type.ri_ConfigIndex2_r11 = input.readBits(10);
                        }
                        return type;
                    }
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CQI_ReportPeriodicProcExt_r11 Decode(BitArrayInputStream input)
            {
                CQI_ReportPeriodicProcExt_r11 _r = new CQI_ReportPeriodicProcExt_r11();
                _r.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                _r.cqi_ReportPeriodicProcExtId_r11 = input.readBits(2) + 1;
                _r.cqi_pmi_ConfigIndex_r11 = input.readBits(10);
                _r.cqi_FormatIndicatorPeriodic_r11 = cqi_FormatIndicatorPeriodic_r11_Type.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    _r.ri_ConfigIndex_r11 = input.readBits(10);
                }
                if (stream.Read())
                {
                    _r.csi_ConfigIndex_r11 = csi_ConfigIndex_r11_Type.PerDecoder.Instance.Decode(input);
                }
                return _r;
            }
        }
    }

}
