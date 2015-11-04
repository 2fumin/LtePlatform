using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class PUSCH_ConfigCommon
    {
        public void InitDefaults()
        {
        }

        public pusch_ConfigBasic_Type pusch_ConfigBasic { get; set; }

        public UL_ReferenceSignalsPUSCH ul_ReferenceSignalsPUSCH { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PUSCH_ConfigCommon Decode(BitArrayInputStream input)
            {
                PUSCH_ConfigCommon common = new PUSCH_ConfigCommon();
                common.InitDefaults();
                common.pusch_ConfigBasic = pusch_ConfigBasic_Type.PerDecoder.Instance.Decode(input);
                common.ul_ReferenceSignalsPUSCH = UL_ReferenceSignalsPUSCH.PerDecoder.Instance.Decode(input);
                return common;
            }
        }

        [Serializable]
        public class pusch_ConfigBasic_Type
        {
            public void InitDefaults()
            {
            }

            public bool enable64QAM { get; set; }

            public hoppingMode_Enum hoppingMode { get; set; }

            public long n_SB { get; set; }

            public long pusch_HoppingOffset { get; set; }

            public enum hoppingMode_Enum
            {
                interSubFrame,
                intraAndInterSubFrame
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public pusch_ConfigBasic_Type Decode(BitArrayInputStream input)
                {
                    pusch_ConfigBasic_Type type = new pusch_ConfigBasic_Type();
                    type.InitDefaults();
                    type.n_SB = input.readBits(2) + 1;
                    int nBits = 1;
                    type.hoppingMode = (hoppingMode_Enum)input.readBits(nBits);
                    type.pusch_HoppingOffset = input.readBits(7);
                    type.enable64QAM = input.readBit() == 1;
                    return type;
                }
            }
        }
    }

    [Serializable]
    public class PUSCH_ConfigDedicated
    {
        public void InitDefaults()
        {
        }

        public long betaOffset_ACK_Index { get; set; }

        public long betaOffset_CQI_Index { get; set; }

        public long betaOffset_RI_Index { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PUSCH_ConfigDedicated Decode(BitArrayInputStream input)
            {
                PUSCH_ConfigDedicated dedicated = new PUSCH_ConfigDedicated();
                dedicated.InitDefaults();
                dedicated.betaOffset_ACK_Index = input.readBits(4);
                dedicated.betaOffset_RI_Index = input.readBits(4);
                dedicated.betaOffset_CQI_Index = input.readBits(4);
                return dedicated;
            }
        }
    }

    [Serializable]
    public class PUSCH_ConfigDedicated_v1020
    {
        public void InitDefaults()
        {
        }

        public betaOffsetMC_r10_Type betaOffsetMC_r10 { get; set; }

        public dmrs_WithOCC_Activated_r10_Enum? dmrs_WithOCC_Activated_r10 { get; set; }

        public groupHoppingDisabled_r10_Enum? groupHoppingDisabled_r10 { get; set; }

        [Serializable]
        public class betaOffsetMC_r10_Type
        {
            public void InitDefaults()
            {
            }

            public long betaOffset_ACK_Index_MC_r10 { get; set; }

            public long betaOffset_CQI_Index_MC_r10 { get; set; }

            public long betaOffset_RI_Index_MC_r10 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public betaOffsetMC_r10_Type Decode(BitArrayInputStream input)
                {
                    betaOffsetMC_r10_Type type = new betaOffsetMC_r10_Type();
                    type.InitDefaults();
                    type.betaOffset_ACK_Index_MC_r10 = input.readBits(4);
                    type.betaOffset_RI_Index_MC_r10 = input.readBits(4);
                    type.betaOffset_CQI_Index_MC_r10 = input.readBits(4);
                    return type;
                }
            }
        }

        public enum dmrs_WithOCC_Activated_r10_Enum
        {
            _true
        }

        public enum groupHoppingDisabled_r10_Enum
        {
            _true
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PUSCH_ConfigDedicated_v1020 Decode(BitArrayInputStream input)
            {
                int num2;
                PUSCH_ConfigDedicated_v1020 _v = new PUSCH_ConfigDedicated_v1020();
                _v.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    _v.betaOffsetMC_r10 = betaOffsetMC_r10_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _v.groupHoppingDisabled_r10 = (groupHoppingDisabled_r10_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _v.dmrs_WithOCC_Activated_r10 = (dmrs_WithOCC_Activated_r10_Enum)input.readBits(num2);
                }
                return _v;
            }
        }
    }

    [Serializable]
    public class PUSCH_ConfigDedicated_v1130
    {
        public void InitDefaults()
        {
        }

        public pusch_DMRS_r11_Type pusch_DMRS_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PUSCH_ConfigDedicated_v1130 Decode(BitArrayInputStream input)
            {
                PUSCH_ConfigDedicated_v1130 _v = new PUSCH_ConfigDedicated_v1130();
                _v.InitDefaults();
                _v.pusch_DMRS_r11 = pusch_DMRS_r11_Type.PerDecoder.Instance.Decode(input);
                return _v;
            }
        }

        [Serializable]
        public class pusch_DMRS_r11_Type
        {
            public void InitDefaults()
            {
            }

            public object release { get; set; }

            public setup_Type setup { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public pusch_DMRS_r11_Type Decode(BitArrayInputStream input)
                {
                    pusch_DMRS_r11_Type type = new pusch_DMRS_r11_Type();
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

                public long nDMRS_CSH_Identity_r11 { get; set; }

                public long nPUSCH_Identity_r11 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public setup_Type Decode(BitArrayInputStream input)
                    {
                        setup_Type type = new setup_Type();
                        type.InitDefaults();
                        type.nPUSCH_Identity_r11 = input.readBits(9);
                        type.nDMRS_CSH_Identity_r11 = input.readBits(9);
                        return type;
                    }
                }
            }
        }
    }

    [Serializable]
    public class PUSCH_ConfigDedicatedSCell_r10
    {
        public void InitDefaults()
        {
        }

        public dmrs_WithOCC_Activated_r10_Enum? dmrs_WithOCC_Activated_r10 { get; set; }

        public groupHoppingDisabled_r10_Enum? groupHoppingDisabled_r10 { get; set; }

        public enum dmrs_WithOCC_Activated_r10_Enum
        {
            _true
        }

        public enum groupHoppingDisabled_r10_Enum
        {
            _true
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PUSCH_ConfigDedicatedSCell_r10 Decode(BitArrayInputStream input)
            {
                int num2;
                PUSCH_ConfigDedicatedSCell_r10 _r = new PUSCH_ConfigDedicatedSCell_r10();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    num2 = 1;
                    _r.groupHoppingDisabled_r10 = (groupHoppingDisabled_r10_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _r.dmrs_WithOCC_Activated_r10 = (dmrs_WithOCC_Activated_r10_Enum)input.readBits(num2);
                }
                return _r;
            }
        }
    }

}
