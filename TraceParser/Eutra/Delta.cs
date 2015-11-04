using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class DeltaFList_PUCCH
    {
        public void InitDefaults()
        {
        }

        public deltaF_PUCCH_Format1_Enum deltaF_PUCCH_Format1 { get; set; }

        public deltaF_PUCCH_Format1b_Enum deltaF_PUCCH_Format1b { get; set; }

        public deltaF_PUCCH_Format2_Enum deltaF_PUCCH_Format2 { get; set; }

        public deltaF_PUCCH_Format2a_Enum deltaF_PUCCH_Format2a { get; set; }

        public deltaF_PUCCH_Format2b_Enum deltaF_PUCCH_Format2b { get; set; }

        public enum deltaF_PUCCH_Format1_Enum
        {
            deltaF_2,
            deltaF0,
            deltaF2
        }

        public enum deltaF_PUCCH_Format1b_Enum
        {
            deltaF1,
            deltaF3,
            deltaF5
        }

        public enum deltaF_PUCCH_Format2_Enum
        {
            deltaF_2,
            deltaF0,
            deltaF1,
            deltaF2
        }

        public enum deltaF_PUCCH_Format2a_Enum
        {
            deltaF_2,
            deltaF0,
            deltaF2
        }

        public enum deltaF_PUCCH_Format2b_Enum
        {
            deltaF_2,
            deltaF0,
            deltaF2
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public DeltaFList_PUCCH Decode(BitArrayInputStream input)
            {
                DeltaFList_PUCCH t_pucch = new DeltaFList_PUCCH();
                t_pucch.InitDefaults();
                int nBits = 2;
                t_pucch.deltaF_PUCCH_Format1 = (deltaF_PUCCH_Format1_Enum)input.readBits(nBits);
                nBits = 2;
                t_pucch.deltaF_PUCCH_Format1b = (deltaF_PUCCH_Format1b_Enum)input.readBits(nBits);
                nBits = 2;
                t_pucch.deltaF_PUCCH_Format2 = (deltaF_PUCCH_Format2_Enum)input.readBits(nBits);
                nBits = 2;
                t_pucch.deltaF_PUCCH_Format2a = (deltaF_PUCCH_Format2a_Enum)input.readBits(nBits);
                nBits = 2;
                t_pucch.deltaF_PUCCH_Format2b = (deltaF_PUCCH_Format2b_Enum)input.readBits(nBits);
                return t_pucch;
            }
        }
    }

    [Serializable]
    public class DeltaTxD_OffsetListPUCCH_r10
    {
        public void InitDefaults()
        {
        }

        public deltaTxD_OffsetPUCCH_Format1_r10_Enum deltaTxD_OffsetPUCCH_Format1_r10 { get; set; }

        public deltaTxD_OffsetPUCCH_Format1a1b_r10_Enum deltaTxD_OffsetPUCCH_Format1a1b_r10 { get; set; }

        public deltaTxD_OffsetPUCCH_Format22a2b_r10_Enum deltaTxD_OffsetPUCCH_Format22a2b_r10 { get; set; }

        public deltaTxD_OffsetPUCCH_Format3_r10_Enum deltaTxD_OffsetPUCCH_Format3_r10 { get; set; }

        public enum deltaTxD_OffsetPUCCH_Format1_r10_Enum
        {
            dB0,
            dB_2
        }

        public enum deltaTxD_OffsetPUCCH_Format1a1b_r10_Enum
        {
            dB0,
            dB_2
        }

        public enum deltaTxD_OffsetPUCCH_Format22a2b_r10_Enum
        {
            dB0,
            dB_2
        }

        public enum deltaTxD_OffsetPUCCH_Format3_r10_Enum
        {
            dB0,
            dB_2
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public DeltaTxD_OffsetListPUCCH_r10 Decode(BitArrayInputStream input)
            {
                DeltaTxD_OffsetListPUCCH_r10 _r = new DeltaTxD_OffsetListPUCCH_r10();
                _r.InitDefaults();
                input.readBit();
                int nBits = 1;
                _r.deltaTxD_OffsetPUCCH_Format1_r10 = (deltaTxD_OffsetPUCCH_Format1_r10_Enum)input.readBits(nBits);
                nBits = 1;
                _r.deltaTxD_OffsetPUCCH_Format1a1b_r10 = (deltaTxD_OffsetPUCCH_Format1a1b_r10_Enum)input.readBits(nBits);
                nBits = 1;
                _r.deltaTxD_OffsetPUCCH_Format22a2b_r10 = (deltaTxD_OffsetPUCCH_Format22a2b_r10_Enum)input.readBits(nBits);
                nBits = 1;
                _r.deltaTxD_OffsetPUCCH_Format3_r10 = (deltaTxD_OffsetPUCCH_Format3_r10_Enum)input.readBits(nBits);
                return _r;
            }
        }
    }

    [Serializable]
    public class DeltaTxD_OffsetListPUCCH_v1130
    {
        public void InitDefaults()
        {
        }

        public deltaTxD_OffsetPUCCH_Format1bCS_r11_Enum deltaTxD_OffsetPUCCH_Format1bCS_r11 { get; set; }

        public enum deltaTxD_OffsetPUCCH_Format1bCS_r11_Enum
        {
            dB0,
            dB_1
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public DeltaTxD_OffsetListPUCCH_v1130 Decode(BitArrayInputStream input)
            {
                DeltaTxD_OffsetListPUCCH_v1130 _v = new DeltaTxD_OffsetListPUCCH_v1130();
                _v.InitDefaults();
                int nBits = 1;
                _v.deltaTxD_OffsetPUCCH_Format1bCS_r11 = (deltaTxD_OffsetPUCCH_Format1bCS_r11_Enum)input.readBits(nBits);
                return _v;
            }
        }
    }

}
