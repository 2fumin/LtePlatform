using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class PhyLayerParameters
    {
        public void InitDefaults()
        {
        }

        public bool ue_SpecificRefSigsSupported { get; set; }

        public bool ue_TxAntennaSelectionSupported { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PhyLayerParameters Decode(BitArrayInputStream input)
            {
                PhyLayerParameters parameters = new PhyLayerParameters();
                parameters.InitDefaults();
                parameters.ue_TxAntennaSelectionSupported = input.readBit() == 1;
                parameters.ue_SpecificRefSigsSupported = input.readBit() == 1;
                return parameters;
            }
        }
    }

    [Serializable]
    public class PhyLayerParameters_v1020
    {
        public void InitDefaults()
        {
        }

        public crossCarrierScheduling_r10_Enum? crossCarrierScheduling_r10 { get; set; }

        public multiClusterPUSCH_WithinCC_r10_Enum? multiClusterPUSCH_WithinCC_r10 { get; set; }

        public List<NonContiguousUL_RA_WithinCC_r10> nonContiguousUL_RA_WithinCC_List_r10 { get; set; }

        public pmi_Disabling_r10_Enum? pmi_Disabling_r10 { get; set; }

        public simultaneousPUCCH_PUSCH_r10_Enum? simultaneousPUCCH_PUSCH_r10 { get; set; }

        public tm9_With_8Tx_FDD_r10_Enum? tm9_With_8Tx_FDD_r10 { get; set; }

        public twoAntennaPortsForPUCCH_r10_Enum? twoAntennaPortsForPUCCH_r10 { get; set; }

        public enum crossCarrierScheduling_r10_Enum
        {
            supported
        }

        public enum multiClusterPUSCH_WithinCC_r10_Enum
        {
            supported
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PhyLayerParameters_v1020 Decode(BitArrayInputStream input)
            {
                int num2;
                PhyLayerParameters_v1020 _v = new PhyLayerParameters_v1020();
                _v.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 7);
                if (stream.Read())
                {
                    num2 = 1;
                    _v.twoAntennaPortsForPUCCH_r10 = (twoAntennaPortsForPUCCH_r10_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _v.tm9_With_8Tx_FDD_r10 = (tm9_With_8Tx_FDD_r10_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _v.pmi_Disabling_r10 = (pmi_Disabling_r10_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _v.crossCarrierScheduling_r10 = (crossCarrierScheduling_r10_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _v.simultaneousPUCCH_PUSCH_r10 = (simultaneousPUCCH_PUSCH_r10_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _v.multiClusterPUSCH_WithinCC_r10 = (multiClusterPUSCH_WithinCC_r10_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    _v.nonContiguousUL_RA_WithinCC_List_r10 = new List<NonContiguousUL_RA_WithinCC_r10>();
                    num2 = 6;
                    int num3 = input.readBits(num2) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        NonContiguousUL_RA_WithinCC_r10 item = NonContiguousUL_RA_WithinCC_r10.PerDecoder.Instance.Decode(input);
                        _v.nonContiguousUL_RA_WithinCC_List_r10.Add(item);
                    }
                }
                return _v;
            }
        }

        public enum pmi_Disabling_r10_Enum
        {
            supported
        }

        public enum simultaneousPUCCH_PUSCH_r10_Enum
        {
            supported
        }

        public enum tm9_With_8Tx_FDD_r10_Enum
        {
            supported
        }

        public enum twoAntennaPortsForPUCCH_r10_Enum
        {
            supported
        }
    }

    [Serializable]
    public class PhyLayerParameters_v1130
    {
        public void InitDefaults()
        {
        }

        public crs_InterfHandl_r11_Enum? crs_InterfHandl_r11 { get; set; }

        public ePDCCH_r11_Enum? ePDCCH_r11 { get; set; }

        public multiACK_CSI_Reporting_r11_Enum? multiACK_CSI_Reporting_r11 { get; set; }

        public ss_CCH_InterfHandl_r11_Enum? ss_CCH_InterfHandl_r11 { get; set; }

        public tdd_SpecialSubframe_r11_Enum? tdd_SpecialSubframe_r11 { get; set; }

        public txDiv_PUCCH1b_ChSelect_r11_Enum? txDiv_PUCCH1b_ChSelect_r11 { get; set; }

        public ul_CoMP_r11_Enum? ul_CoMP_r11 { get; set; }

        public enum crs_InterfHandl_r11_Enum
        {
            supported
        }

        public enum ePDCCH_r11_Enum
        {
            supported
        }

        public enum multiACK_CSI_Reporting_r11_Enum
        {
            supported
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PhyLayerParameters_v1130 Decode(BitArrayInputStream input)
            {
                int num2;
                PhyLayerParameters_v1130 _v = new PhyLayerParameters_v1130();
                _v.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 7);
                if (stream.Read())
                {
                    num2 = 1;
                    _v.crs_InterfHandl_r11 = (crs_InterfHandl_r11_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _v.ePDCCH_r11 = (ePDCCH_r11_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _v.multiACK_CSI_Reporting_r11 = (multiACK_CSI_Reporting_r11_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _v.ss_CCH_InterfHandl_r11 = (ss_CCH_InterfHandl_r11_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _v.tdd_SpecialSubframe_r11 = (tdd_SpecialSubframe_r11_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _v.txDiv_PUCCH1b_ChSelect_r11 = (txDiv_PUCCH1b_ChSelect_r11_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _v.ul_CoMP_r11 = (ul_CoMP_r11_Enum)input.readBits(num2);
                }
                return _v;
            }
        }

        public enum ss_CCH_InterfHandl_r11_Enum
        {
            supported
        }

        public enum tdd_SpecialSubframe_r11_Enum
        {
            supported
        }

        public enum txDiv_PUCCH1b_ChSelect_r11_Enum
        {
            supported
        }

        public enum ul_CoMP_r11_Enum
        {
            supported
        }
    }

    [Serializable]
    public class PhyLayerParameters_v1170
    {
        public void InitDefaults()
        {
        }

        public string interBandTDD_CA_WithDifferentConfig_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PhyLayerParameters_v1170 Decode(BitArrayInputStream input)
            {
                PhyLayerParameters_v1170 _v = new PhyLayerParameters_v1170();
                _v.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    _v.interBandTDD_CA_WithDifferentConfig_r11 = input.readBitString(2);
                }
                return _v;
            }
        }
    }

    [Serializable]
    public class PhyLayerParameters_v920
    {
        public void InitDefaults()
        {
        }

        public enhancedDualLayerFDD_r9_Enum? enhancedDualLayerFDD_r9 { get; set; }

        public enhancedDualLayerTDD_r9_Enum? enhancedDualLayerTDD_r9 { get; set; }

        public enum enhancedDualLayerFDD_r9_Enum
        {
            supported
        }

        public enum enhancedDualLayerTDD_r9_Enum
        {
            supported
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PhyLayerParameters_v920 Decode(BitArrayInputStream input)
            {
                int num2;
                PhyLayerParameters_v920 _v = new PhyLayerParameters_v920();
                _v.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    num2 = 1;
                    _v.enhancedDualLayerFDD_r9 = (enhancedDualLayerFDD_r9_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _v.enhancedDualLayerTDD_r9 = (enhancedDualLayerTDD_r9_Enum)input.readBits(num2);
                }
                return _v;
            }
        }
    }

    [Serializable]
    public class PhyLayerParameters_v9d0
    {
        public void InitDefaults()
        {
        }

        public tm5_FDD_r9_Enum? tm5_FDD_r9 { get; set; }

        public tm5_TDD_r9_Enum? tm5_TDD_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PhyLayerParameters_v9d0 Decode(BitArrayInputStream input)
            {
                int num2;
                PhyLayerParameters_v9d0 _vd = new PhyLayerParameters_v9d0();
                _vd.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    num2 = 1;
                    _vd.tm5_FDD_r9 = (tm5_FDD_r9_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _vd.tm5_TDD_r9 = (tm5_TDD_r9_Enum)input.readBits(num2);
                }
                return _vd;
            }
        }

        public enum tm5_FDD_r9_Enum
        {
            supported
        }

        public enum tm5_TDD_r9_Enum
        {
            supported
        }
    }

}
