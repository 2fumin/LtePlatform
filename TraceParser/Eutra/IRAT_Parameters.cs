using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class IRAT_ParametersCDMA2000_1XRTT
    {
        public void InitDefaults()
        {
        }

        public rx_Config1XRTT_Enum rx_Config1XRTT { get; set; }

        public List<BandclassCDMA2000> supportedBandList1XRTT { get; set; }

        public tx_Config1XRTT_Enum tx_Config1XRTT { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public IRAT_ParametersCDMA2000_1XRTT Decode(BitArrayInputStream input)
            {
                IRAT_ParametersCDMA2000_1XRTT scdma_xrtt = new IRAT_ParametersCDMA2000_1XRTT();
                scdma_xrtt.InitDefaults();
                scdma_xrtt.supportedBandList1XRTT = new List<BandclassCDMA2000>();
                int nBits = 5;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    nBits = (input.readBit() == 0) ? 5 : 5;
                    BandclassCDMA2000 item = (BandclassCDMA2000)input.readBits(nBits);
                    scdma_xrtt.supportedBandList1XRTT.Add(item);
                }
                nBits = 1;
                scdma_xrtt.tx_Config1XRTT = (tx_Config1XRTT_Enum)input.readBits(nBits);
                nBits = 1;
                scdma_xrtt.rx_Config1XRTT = (rx_Config1XRTT_Enum)input.readBits(nBits);
                return scdma_xrtt;
            }
        }

        public enum rx_Config1XRTT_Enum
        {
            single,
            dual
        }

        public enum tx_Config1XRTT_Enum
        {
            single,
            dual
        }
    }

    [Serializable]
    public class IRAT_ParametersCDMA2000_1XRTT_v1020
    {
        public void InitDefaults()
        {
        }

        public e_CSFB_dual_1XRTT_r10_Enum e_CSFB_dual_1XRTT_r10 { get; set; }

        public enum e_CSFB_dual_1XRTT_r10_Enum
        {
            supported
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public IRAT_ParametersCDMA2000_1XRTT_v1020 Decode(BitArrayInputStream input)
            {
                IRAT_ParametersCDMA2000_1XRTT_v1020 _v = new IRAT_ParametersCDMA2000_1XRTT_v1020();
                _v.InitDefaults();
                int nBits = 1;
                _v.e_CSFB_dual_1XRTT_r10 = (e_CSFB_dual_1XRTT_r10_Enum)input.readBits(nBits);
                return _v;
            }
        }
    }

    [Serializable]
    public class IRAT_ParametersCDMA2000_1XRTT_v920
    {
        public void InitDefaults()
        {
        }

        public e_CSFB_1XRTT_r9_Enum e_CSFB_1XRTT_r9 { get; set; }

        public e_CSFB_ConcPS_Mob1XRTT_r9_Enum? e_CSFB_ConcPS_Mob1XRTT_r9 { get; set; }

        public enum e_CSFB_1XRTT_r9_Enum
        {
            supported
        }

        public enum e_CSFB_ConcPS_Mob1XRTT_r9_Enum
        {
            supported
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public IRAT_ParametersCDMA2000_1XRTT_v920 Decode(BitArrayInputStream input)
            {
                IRAT_ParametersCDMA2000_1XRTT_v920 _v = new IRAT_ParametersCDMA2000_1XRTT_v920();
                _v.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                int nBits = 1;
                _v.e_CSFB_1XRTT_r9 = (e_CSFB_1XRTT_r9_Enum)input.readBits(nBits);
                if (stream.Read())
                {
                    nBits = 1;
                    _v.e_CSFB_ConcPS_Mob1XRTT_r9 = (e_CSFB_ConcPS_Mob1XRTT_r9_Enum)input.readBits(nBits);
                }
                return _v;
            }
        }
    }

    [Serializable]
    public class IRAT_ParametersCDMA2000_HRPD
    {
        public void InitDefaults()
        {
        }

        public rx_ConfigHRPD_Enum rx_ConfigHRPD { get; set; }

        public List<BandclassCDMA2000> supportedBandListHRPD { get; set; }

        public tx_ConfigHRPD_Enum tx_ConfigHRPD { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public IRAT_ParametersCDMA2000_HRPD Decode(BitArrayInputStream input)
            {
                IRAT_ParametersCDMA2000_HRPD scdma_hrpd = new IRAT_ParametersCDMA2000_HRPD();
                scdma_hrpd.InitDefaults();
                scdma_hrpd.supportedBandListHRPD = new List<BandclassCDMA2000>();
                int nBits = 5;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    nBits = (input.readBit() == 0) ? 5 : 5;
                    BandclassCDMA2000 item = (BandclassCDMA2000)input.readBits(nBits);
                    scdma_hrpd.supportedBandListHRPD.Add(item);
                }
                nBits = 1;
                scdma_hrpd.tx_ConfigHRPD = (tx_ConfigHRPD_Enum)input.readBits(nBits);
                nBits = 1;
                scdma_hrpd.rx_ConfigHRPD = (rx_ConfigHRPD_Enum)input.readBits(nBits);
                return scdma_hrpd;
            }
        }

        public enum rx_ConfigHRPD_Enum
        {
            single,
            dual
        }

        public enum tx_ConfigHRPD_Enum
        {
            single,
            dual
        }
    }

    [Serializable]
    public class IRAT_ParametersCDMA2000_v1130
    {
        public void InitDefaults()
        {
        }

        public cdma2000_NW_Sharing_r11_Enum? cdma2000_NW_Sharing_r11 { get; set; }

        public enum cdma2000_NW_Sharing_r11_Enum
        {
            supported
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public IRAT_ParametersCDMA2000_v1130 Decode(BitArrayInputStream input)
            {
                IRAT_ParametersCDMA2000_v1130 _v = new IRAT_ParametersCDMA2000_v1130();
                _v.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    const int nBits = 1;
                    _v.cdma2000_NW_Sharing_r11 = (cdma2000_NW_Sharing_r11_Enum)input.readBits(nBits);
                }
                return _v;
            }
        }
    }

    [Serializable]
    public class IRAT_ParametersGERAN
    {
        public void InitDefaults()
        {
        }

        public bool interRAT_PS_HO_ToGERAN { get; set; }

        public List<SupportedBandGERAN> supportedBandListGERAN { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public IRAT_ParametersGERAN Decode(BitArrayInputStream input)
            {
                IRAT_ParametersGERAN sgeran = new IRAT_ParametersGERAN();
                sgeran.InitDefaults();
                sgeran.supportedBandListGERAN = new List<SupportedBandGERAN>();
                int nBits = 6;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    nBits = (input.readBit() == 0) ? 4 : 4;
                    SupportedBandGERAN item = (SupportedBandGERAN)input.readBits(nBits);
                    sgeran.supportedBandListGERAN.Add(item);
                }
                sgeran.interRAT_PS_HO_ToGERAN = input.readBit() == 1;
                return sgeran;
            }
        }
    }

    [Serializable]
    public class IRAT_ParametersGERAN_v920
    {
        public void InitDefaults()
        {
        }

        public dtm_r9_Enum? dtm_r9 { get; set; }

        public e_RedirectionGERAN_r9_Enum? e_RedirectionGERAN_r9 { get; set; }

        public enum dtm_r9_Enum
        {
            supported
        }

        public enum e_RedirectionGERAN_r9_Enum
        {
            supported
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public IRAT_ParametersGERAN_v920 Decode(BitArrayInputStream input)
            {
                int num2;
                IRAT_ParametersGERAN_v920 _v = new IRAT_ParametersGERAN_v920();
                _v.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    num2 = 1;
                    _v.dtm_r9 = (dtm_r9_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _v.e_RedirectionGERAN_r9 = (e_RedirectionGERAN_r9_Enum)input.readBits(num2);
                }
                return _v;
            }
        }
    }

    [Serializable]
    public class IRAT_ParametersUTRA_FDD
    {
        public void InitDefaults()
        {
        }

        public List<SupportedBandUTRA_FDD> supportedBandListUTRA_FDD { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public IRAT_ParametersUTRA_FDD Decode(BitArrayInputStream input)
            {
                IRAT_ParametersUTRA_FDD sutra_fdd = new IRAT_ParametersUTRA_FDD();
                sutra_fdd.InitDefaults();
                sutra_fdd.supportedBandListUTRA_FDD = new List<SupportedBandUTRA_FDD>();
                int nBits = 6;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    nBits = (input.readBit() == 0) ? 4 : 5;
                    SupportedBandUTRA_FDD item = (SupportedBandUTRA_FDD)input.readBits(nBits);
                    sutra_fdd.supportedBandListUTRA_FDD.Add(item);
                }
                return sutra_fdd;
            }
        }
    }

    [Serializable]
    public class IRAT_ParametersUTRA_TDD_v1020
    {
        public void InitDefaults()
        {
        }

        public e_RedirectionUTRA_TDD_r10_Enum e_RedirectionUTRA_TDD_r10 { get; set; }

        public enum e_RedirectionUTRA_TDD_r10_Enum
        {
            supported
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public IRAT_ParametersUTRA_TDD_v1020 Decode(BitArrayInputStream input)
            {
                IRAT_ParametersUTRA_TDD_v1020 _v = new IRAT_ParametersUTRA_TDD_v1020();
                _v.InitDefaults();
                int nBits = 1;
                _v.e_RedirectionUTRA_TDD_r10 = (e_RedirectionUTRA_TDD_r10_Enum)input.readBits(nBits);
                return _v;
            }
        }
    }

    [Serializable]
    public class IRAT_ParametersUTRA_TDD128
    {
        public void InitDefaults()
        {
        }

        public List<SupportedBandUTRA_TDD128> supportedBandListUTRA_TDD128 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public IRAT_ParametersUTRA_TDD128 Decode(BitArrayInputStream input)
            {
                IRAT_ParametersUTRA_TDD128 sutra_tdd = new IRAT_ParametersUTRA_TDD128();
                sutra_tdd.InitDefaults();
                sutra_tdd.supportedBandListUTRA_TDD128 = new List<SupportedBandUTRA_TDD128>();
                int nBits = 6;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    nBits = (input.readBit() == 0) ? 4 : 4;
                    SupportedBandUTRA_TDD128 item = (SupportedBandUTRA_TDD128)input.readBits(nBits);
                    sutra_tdd.supportedBandListUTRA_TDD128.Add(item);
                }
                return sutra_tdd;
            }
        }
    }

    [Serializable]
    public class IRAT_ParametersUTRA_TDD384
    {
        public void InitDefaults()
        {
        }

        public List<SupportedBandUTRA_TDD384> supportedBandListUTRA_TDD384 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public IRAT_ParametersUTRA_TDD384 Decode(BitArrayInputStream input)
            {
                IRAT_ParametersUTRA_TDD384 sutra_tdd = new IRAT_ParametersUTRA_TDD384();
                sutra_tdd.InitDefaults();
                sutra_tdd.supportedBandListUTRA_TDD384 = new List<SupportedBandUTRA_TDD384>();
                int nBits = 6;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    nBits = (input.readBit() == 0) ? 4 : 4;
                    SupportedBandUTRA_TDD384 item = (SupportedBandUTRA_TDD384)input.readBits(nBits);
                    sutra_tdd.supportedBandListUTRA_TDD384.Add(item);
                }
                return sutra_tdd;
            }
        }
    }

    [Serializable]
    public class IRAT_ParametersUTRA_TDD768
    {
        public void InitDefaults()
        {
        }

        public List<SupportedBandUTRA_TDD768> supportedBandListUTRA_TDD768 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public IRAT_ParametersUTRA_TDD768 Decode(BitArrayInputStream input)
            {
                IRAT_ParametersUTRA_TDD768 sutra_tdd = new IRAT_ParametersUTRA_TDD768();
                sutra_tdd.InitDefaults();
                sutra_tdd.supportedBandListUTRA_TDD768 = new List<SupportedBandUTRA_TDD768>();
                int nBits = 6;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    nBits = (input.readBit() == 0) ? 4 : 4;
                    SupportedBandUTRA_TDD768 item = (SupportedBandUTRA_TDD768)input.readBits(nBits);
                    sutra_tdd.supportedBandListUTRA_TDD768.Add(item);
                }
                return sutra_tdd;
            }
        }
    }

    [Serializable]
    public class IRAT_ParametersUTRA_v920
    {
        public void InitDefaults()
        {
        }

        public e_RedirectionUTRA_r9_Enum e_RedirectionUTRA_r9 { get; set; }

        public enum e_RedirectionUTRA_r9_Enum
        {
            supported
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public IRAT_ParametersUTRA_v920 Decode(BitArrayInputStream input)
            {
                IRAT_ParametersUTRA_v920 _v = new IRAT_ParametersUTRA_v920();
                _v.InitDefaults();
                int nBits = 1;
                _v.e_RedirectionUTRA_r9 = (e_RedirectionUTRA_r9_Enum)input.readBits(nBits);
                return _v;
            }
        }
    }

    [Serializable]
    public class IRAT_ParametersUTRA_v9c0
    {
        public void InitDefaults()
        {
        }

        public srvcc_FromUTRA_FDD_ToGERAN_r9_Enum? srvcc_FromUTRA_FDD_ToGERAN_r9 { get; set; }

        public srvcc_FromUTRA_FDD_ToUTRA_FDD_r9_Enum? srvcc_FromUTRA_FDD_ToUTRA_FDD_r9 { get; set; }

        public srvcc_FromUTRA_TDD128_ToGERAN_r9_Enum? srvcc_FromUTRA_TDD128_ToGERAN_r9 { get; set; }

        public srvcc_FromUTRA_TDD128_ToUTRA_TDD128_r9_Enum? srvcc_FromUTRA_TDD128_ToUTRA_TDD128_r9 { get; set; }

        public voiceOverPS_HS_UTRA_FDD_r9_Enum? voiceOverPS_HS_UTRA_FDD_r9 { get; set; }

        public voiceOverPS_HS_UTRA_TDD128_r9_Enum? voiceOverPS_HS_UTRA_TDD128_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public IRAT_ParametersUTRA_v9c0 Decode(BitArrayInputStream input)
            {
                int num2;
                IRAT_ParametersUTRA_v9c0 _vc = new IRAT_ParametersUTRA_v9c0();
                _vc.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 6);
                if (stream.Read())
                {
                    num2 = 1;
                    _vc.voiceOverPS_HS_UTRA_FDD_r9 = (voiceOverPS_HS_UTRA_FDD_r9_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _vc.voiceOverPS_HS_UTRA_TDD128_r9 = (voiceOverPS_HS_UTRA_TDD128_r9_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _vc.srvcc_FromUTRA_FDD_ToUTRA_FDD_r9 = (srvcc_FromUTRA_FDD_ToUTRA_FDD_r9_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _vc.srvcc_FromUTRA_FDD_ToGERAN_r9 = (srvcc_FromUTRA_FDD_ToGERAN_r9_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _vc.srvcc_FromUTRA_TDD128_ToUTRA_TDD128_r9 = (srvcc_FromUTRA_TDD128_ToUTRA_TDD128_r9_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _vc.srvcc_FromUTRA_TDD128_ToGERAN_r9 = (srvcc_FromUTRA_TDD128_ToGERAN_r9_Enum)input.readBits(num2);
                }
                return _vc;
            }
        }

        public enum srvcc_FromUTRA_FDD_ToGERAN_r9_Enum
        {
            supported
        }

        public enum srvcc_FromUTRA_FDD_ToUTRA_FDD_r9_Enum
        {
            supported
        }

        public enum srvcc_FromUTRA_TDD128_ToGERAN_r9_Enum
        {
            supported
        }

        public enum srvcc_FromUTRA_TDD128_ToUTRA_TDD128_r9_Enum
        {
            supported
        }

        public enum voiceOverPS_HS_UTRA_FDD_r9_Enum
        {
            supported
        }

        public enum voiceOverPS_HS_UTRA_TDD128_r9_Enum
        {
            supported
        }
    }

    [Serializable]
    public class IRAT_ParametersUTRA_v9h0
    {
        public void InitDefaults()
        {
        }

        public mfbi_UTRA_r9_Enum mfbi_UTRA_r9 { get; set; }

        public enum mfbi_UTRA_r9_Enum
        {
            supported
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public IRAT_ParametersUTRA_v9h0 Decode(BitArrayInputStream input)
            {
                IRAT_ParametersUTRA_v9h0 _vh = new IRAT_ParametersUTRA_v9h0();
                _vh.InitDefaults();
                int nBits = 1;
                _vh.mfbi_UTRA_r9 = (mfbi_UTRA_r9_Enum)input.readBits(nBits);
                return _vh;
            }
        }
    }

}
