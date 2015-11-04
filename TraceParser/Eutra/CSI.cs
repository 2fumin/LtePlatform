using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CSI_IM_Config_r11
    {
        public void InitDefaults()
        {
        }

        public long csi_IM_ConfigId_r11 { get; set; }

        public long resourceConfig_r11 { get; set; }

        public long subframeConfig_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CSI_IM_Config_r11 Decode(BitArrayInputStream input)
            {
                CSI_IM_Config_r11 _r = new CSI_IM_Config_r11();
                _r.InitDefaults();
                input.readBit();
                _r.csi_IM_ConfigId_r11 = input.readBits(2) + 1;
                _r.resourceConfig_r11 = input.readBits(5);
                _r.subframeConfig_r11 = input.readBits(8);
                return _r;
            }
        }
    }

    [Serializable]
    public class CSI_Process_r11
    {
        public void InitDefaults()
        {
        }

        public alternativeCodebookEnabledFor4TXProc_r12_Enum? alternativeCodebookEnabledFor4TXProc_r12 { get; set; }

        public CQI_ReportAperiodicProc_r11 cqi_ReportAperiodicProc_r11 { get; set; }

        public CQI_ReportBothProc_r11 cqi_ReportBothProc_r11 { get; set; }

        public long? cqi_ReportPeriodicProcId_r11 { get; set; }

        public long csi_IM_ConfigId_r11 { get; set; }

        public long csi_ProcessId_r11 { get; set; }

        public long csi_RS_ConfigNZPId_r11 { get; set; }

        public List<P_C_AndCBSR_r11> p_C_AndCBSRList_r11 { get; set; }

        public enum alternativeCodebookEnabledFor4TXProc_r12_Enum
        {
            _true
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CSI_Process_r11 Decode(BitArrayInputStream input)
            {
                CSI_Process_r11 _r = new CSI_Process_r11();
                _r.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 3);
                _r.csi_ProcessId_r11 = input.readBits(2) + 1;
                _r.csi_RS_ConfigNZPId_r11 = input.readBits(2) + 1;
                _r.csi_IM_ConfigId_r11 = input.readBits(2) + 1;
                _r.p_C_AndCBSRList_r11 = new List<P_C_AndCBSR_r11>();
                int nBits = 1;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    P_C_AndCBSR_r11 item = P_C_AndCBSR_r11.PerDecoder.Instance.Decode(input);
                    _r.p_C_AndCBSRList_r11.Add(item);
                }
                if (stream.Read())
                {
                    _r.cqi_ReportBothProc_r11 = CQI_ReportBothProc_r11.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    _r.cqi_ReportPeriodicProcId_r11 = input.readBits(2);
                }
                if (stream.Read())
                {
                    _r.cqi_ReportAperiodicProc_r11 = CQI_ReportAperiodicProc_r11.PerDecoder.Instance.Decode(input);
                }
                if (flag)
                {
                    BitMaskStream stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        nBits = 1;
                        _r.alternativeCodebookEnabledFor4TXProc_r12 = (alternativeCodebookEnabledFor4TXProc_r12_Enum)input.readBits(nBits);
                    }
                }
                return _r;
            }
        }
    }

    [Serializable]
    public class CSI_RS_Config_r10
    {
        public void InitDefaults()
        {
        }

        public csi_RS_r10_Type csi_RS_r10 { get; set; }

        public zeroTxPowerCSI_RS_r10_Type zeroTxPowerCSI_RS_r10 { get; set; }

        [Serializable]
        public class csi_RS_r10_Type
        {
            public void InitDefaults()
            {
            }

            public object release { get; set; }

            public setup_Type setup { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public csi_RS_r10_Type Decode(BitArrayInputStream input)
                {
                    csi_RS_r10_Type type = new csi_RS_r10_Type();
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

                public antennaPortsCount_r10_Enum antennaPortsCount_r10 { get; set; }

                public long p_C_r10 { get; set; }

                public long resourceConfig_r10 { get; set; }

                public long subframeConfig_r10 { get; set; }

                public enum antennaPortsCount_r10_Enum
                {
                    an1,
                    an2,
                    an4,
                    an8
                }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public setup_Type Decode(BitArrayInputStream input)
                    {
                        setup_Type type = new setup_Type();
                        type.InitDefaults();
                        int nBits = 2;
                        type.antennaPortsCount_r10 = (antennaPortsCount_r10_Enum)input.readBits(nBits);
                        type.resourceConfig_r10 = input.readBits(5);
                        type.subframeConfig_r10 = input.readBits(8);
                        type.p_C_r10 = input.readBits(5) + -8;
                        return type;
                    }
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CSI_RS_Config_r10 Decode(BitArrayInputStream input)
            {
                CSI_RS_Config_r10 _r = new CSI_RS_Config_r10();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    _r.csi_RS_r10 = csi_RS_r10_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    _r.zeroTxPowerCSI_RS_r10 = zeroTxPowerCSI_RS_r10_Type.PerDecoder.Instance.Decode(input);
                }
                return _r;
            }
        }

        [Serializable]
        public class zeroTxPowerCSI_RS_r10_Type
        {
            public void InitDefaults()
            {
            }

            public object release { get; set; }

            public setup_Type setup { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public zeroTxPowerCSI_RS_r10_Type Decode(BitArrayInputStream input)
                {
                    zeroTxPowerCSI_RS_r10_Type type = new zeroTxPowerCSI_RS_r10_Type();
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

                public string zeroTxPowerResourceConfigList_r10 { get; set; }

                public long zeroTxPowerSubframeConfig_r10 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public setup_Type Decode(BitArrayInputStream input)
                    {
                        setup_Type type = new setup_Type();
                        type.InitDefaults();
                        type.zeroTxPowerResourceConfigList_r10 = input.readBitString(0x10);
                        type.zeroTxPowerSubframeConfig_r10 = input.readBits(8);
                        return type;
                    }
                }
            }
        }
    }

    [Serializable]
    public class CSI_RS_ConfigNZP_r11
    {
        public void InitDefaults()
        {
        }

        public antennaPortsCount_r11_Enum antennaPortsCount_r11 { get; set; }

        public long csi_RS_ConfigNZPId_r11 { get; set; }

        public qcl_CRS_Info_r11_Type qcl_CRS_Info_r11 { get; set; }

        public long resourceConfig_r11 { get; set; }

        public long scramblingIdentity_r11 { get; set; }

        public long subframeConfig_r11 { get; set; }

        public enum antennaPortsCount_r11_Enum
        {
            an1,
            an2,
            an4,
            an8
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CSI_RS_ConfigNZP_r11 Decode(BitArrayInputStream input)
            {
                CSI_RS_ConfigNZP_r11 _r = new CSI_RS_ConfigNZP_r11();
                _r.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                _r.csi_RS_ConfigNZPId_r11 = input.readBits(2) + 1;
                int nBits = 2;
                _r.antennaPortsCount_r11 = (antennaPortsCount_r11_Enum)input.readBits(nBits);
                _r.resourceConfig_r11 = input.readBits(5);
                _r.subframeConfig_r11 = input.readBits(8);
                _r.scramblingIdentity_r11 = input.readBits(9);
                if (stream.Read())
                {
                    _r.qcl_CRS_Info_r11 = qcl_CRS_Info_r11_Type.PerDecoder.Instance.Decode(input);
                }
                return _r;
            }
        }

        [Serializable]
        public class qcl_CRS_Info_r11_Type
        {
            public void InitDefaults()
            {
            }

            public crs_PortsCount_r11_Enum crs_PortsCount_r11 { get; set; }

            public mbsfn_SubframeConfigList_r11_Type mbsfn_SubframeConfigList_r11 { get; set; }

            public long qcl_ScramblingIdentity_r11 { get; set; }

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

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public qcl_CRS_Info_r11_Type Decode(BitArrayInputStream input)
                {
                    qcl_CRS_Info_r11_Type type = new qcl_CRS_Info_r11_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 1);
                    type.qcl_ScramblingIdentity_r11 = input.readBits(9);
                    const int nBits = 2;
                    type.crs_PortsCount_r11 = (crs_PortsCount_r11_Enum)input.readBits(nBits);
                    if (stream.Read())
                    {
                        type.mbsfn_SubframeConfigList_r11 = mbsfn_SubframeConfigList_r11_Type.PerDecoder.Instance.Decode(input);
                    }
                    return type;
                }
            }
        }
    }

    [Serializable]
    public class CSI_RS_ConfigZP_r11
    {
        public void InitDefaults()
        {
        }

        public long csi_RS_ConfigZPId_r11 { get; set; }

        public string resourceConfigList_r11 { get; set; }

        public long subframeConfig_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CSI_RS_ConfigZP_r11 Decode(BitArrayInputStream input)
            {
                CSI_RS_ConfigZP_r11 _r = new CSI_RS_ConfigZP_r11();
                _r.InitDefaults();
                input.readBit();
                _r.csi_RS_ConfigZPId_r11 = input.readBits(2) + 1;
                _r.resourceConfigList_r11 = input.readBitString(0x10);
                _r.subframeConfig_r11 = input.readBits(8);
                return _r;
            }
        }
    }

}
