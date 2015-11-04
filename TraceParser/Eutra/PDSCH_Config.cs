using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class PDSCH_ConfigCommon
    {
        public void InitDefaults()
        {
        }

        public long p_b { get; set; }

        public long referenceSignalPower { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PDSCH_ConfigCommon Decode(BitArrayInputStream input)
            {
                PDSCH_ConfigCommon common = new PDSCH_ConfigCommon();
                common.InitDefaults();
                common.referenceSignalPower = input.readBits(7) + -60;
                common.p_b = input.readBits(2);
                return common;
            }
        }
    }

    [Serializable]
    public class PDSCH_ConfigDedicated
    {
        public void InitDefaults()
        {
        }

        public p_a_Enum p_a { get; set; }

        public enum p_a_Enum
        {
            dB_6,
            dB_4dot77,
            dB_3,
            dB_1dot77,
            dB0,
            dB1,
            dB2,
            dB3
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PDSCH_ConfigDedicated Decode(BitArrayInputStream input)
            {
                PDSCH_ConfigDedicated dedicated = new PDSCH_ConfigDedicated();
                dedicated.InitDefaults();
                int nBits = 3;
                dedicated.p_a = (p_a_Enum)input.readBits(nBits);
                return dedicated;
            }
        }
    }

    [Serializable]
    public class PDSCH_ConfigDedicated_v1130
    {
        public void InitDefaults()
        {
        }

        public DMRS_Config_r11 dmrs_ConfigPDSCH_r11 { get; set; }

        public qcl_Operation_Enum? qcl_Operation { get; set; }

        public List<PDSCH_RE_MappingQCL_Config_r11> re_MappingQCLConfigToAddModList_r11 { get; set; }

        public List<long> re_MappingQCLConfigToReleaseList_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PDSCH_ConfigDedicated_v1130 Decode(BitArrayInputStream input)
            {
                int num2;
                PDSCH_ConfigDedicated_v1130 _v = new PDSCH_ConfigDedicated_v1130();
                _v.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 4);
                if (stream.Read())
                {
                    _v.dmrs_ConfigPDSCH_r11 = DMRS_Config_r11.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _v.qcl_Operation = (qcl_Operation_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    _v.re_MappingQCLConfigToReleaseList_r11 = new List<long>();
                    num2 = 2;
                    int num3 = input.readBits(num2) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        long item = input.readBits(2) + 1;
                        _v.re_MappingQCLConfigToReleaseList_r11.Add(item);
                    }
                }
                if (stream.Read())
                {
                    _v.re_MappingQCLConfigToAddModList_r11 = new List<PDSCH_RE_MappingQCL_Config_r11>();
                    num2 = 2;
                    int num6 = input.readBits(num2) + 1;
                    for (int j = 0; j < num6; j++)
                    {
                        PDSCH_RE_MappingQCL_Config_r11 _r = PDSCH_RE_MappingQCL_Config_r11.PerDecoder.Instance.Decode(input);
                        _v.re_MappingQCLConfigToAddModList_r11.Add(_r);
                    }
                }
                return _v;
            }
        }

        public enum qcl_Operation_Enum
        {
            typeA,
            typeB
        }
    }

    [Serializable]
    public class PDSCH_RE_MappingQCL_Config_r11
    {
        public void InitDefaults()
        {
        }

        public long csi_RS_ConfigZPId_r11 { get; set; }

        public optionalSetOfFields_r11_Type optionalSetOfFields_r11 { get; set; }

        public long pdsch_RE_MappingQCL_ConfigId_r11 { get; set; }

        public long? qcl_CSI_RS_ConfigNZPId_r11 { get; set; }

        [Serializable]
        public class optionalSetOfFields_r11_Type
        {
            public void InitDefaults()
            {
            }

            public long crs_FreqShift_r11 { get; set; }

            public crs_PortsCount_r11_Enum crs_PortsCount_r11 { get; set; }

            public mbsfn_SubframeConfigList_r11_Type mbsfn_SubframeConfigList_r11 { get; set; }

            public pdsch_Start_r11_Enum pdsch_Start_r11 { get; set; }

            public enum crs_PortsCount_r11_Enum
            {
                n1,
                n2,
                n4,
                spare1
            }

            [Serializable]
            public class mbsfn_SubframeConfigList_r11_Type
            {
                public void InitDefaults()
                {
                }

                public object release { get; set; }

                public setup_Type setup { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public mbsfn_SubframeConfigList_r11_Type Decode(BitArrayInputStream input)
                    {
                        mbsfn_SubframeConfigList_r11_Type type = new mbsfn_SubframeConfigList_r11_Type();
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

                    public List<MBSFN_SubframeConfig> subframeConfigList { get; set; }

                    public class PerDecoder
                    {
                        public static readonly PerDecoder Instance = new PerDecoder();

                        public setup_Type Decode(BitArrayInputStream input)
                        {
                            setup_Type type = new setup_Type();
                            type.InitDefaults();
                            type.subframeConfigList = new List<MBSFN_SubframeConfig>();
                            int nBits = 3;
                            int num3 = input.readBits(nBits) + 1;
                            for (int i = 0; i < num3; i++)
                            {
                                MBSFN_SubframeConfig item = MBSFN_SubframeConfig.PerDecoder.Instance.Decode(input);
                                type.subframeConfigList.Add(item);
                            }
                            return type;
                        }
                    }
                }
            }

            public enum pdsch_Start_r11_Enum
            {
                reserved,
                n1,
                n2,
                n3,
                n4,
                assigned
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public optionalSetOfFields_r11_Type Decode(BitArrayInputStream input)
                {
                    optionalSetOfFields_r11_Type type = new optionalSetOfFields_r11_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 1);
                    int nBits = 2;
                    type.crs_PortsCount_r11 = (crs_PortsCount_r11_Enum)input.readBits(nBits);
                    type.crs_FreqShift_r11 = input.readBits(3);
                    if (stream.Read())
                    {
                        type.mbsfn_SubframeConfigList_r11 
                            = mbsfn_SubframeConfigList_r11_Type.PerDecoder.Instance.Decode(input);
                    }
                    nBits = 3;
                    type.pdsch_Start_r11 = (pdsch_Start_r11_Enum)input.readBits(nBits);
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PDSCH_RE_MappingQCL_Config_r11 Decode(BitArrayInputStream input)
            {
                PDSCH_RE_MappingQCL_Config_r11 _r = new PDSCH_RE_MappingQCL_Config_r11();
                _r.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                _r.pdsch_RE_MappingQCL_ConfigId_r11 = input.readBits(2) + 1;
                if (stream.Read())
                {
                    _r.optionalSetOfFields_r11 = optionalSetOfFields_r11_Type.PerDecoder.Instance.Decode(input);
                }
                _r.csi_RS_ConfigZPId_r11 = input.readBits(2) + 1;
                if (stream.Read())
                {
                    _r.qcl_CSI_RS_ConfigNZPId_r11 = input.readBits(2) + 1;
                }
                return _r;
            }
        }
    }

}
