using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class EPDCCH_Config_r11
    {
        public void InitDefaults()
        {
        }

        public config_r11_Type config_r11 { get; set; }

        [Serializable]
        public class config_r11_Type
        {
            public void InitDefaults()
            {
            }

            public object release { get; set; }

            public setup_Type setup { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public config_r11_Type Decode(BitArrayInputStream input)
                {
                    config_r11_Type type = new config_r11_Type();
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

                public List<EPDCCH_SetConfig_r11> setConfigToAddModList_r11 { get; set; }

                public List<long> setConfigToReleaseList_r11 { get; set; }

                public long? startSymbol_r11 { get; set; }

                public subframePatternConfig_r11_Type subframePatternConfig_r11 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public setup_Type Decode(BitArrayInputStream input)
                    {
                        int num2;
                        setup_Type type = new setup_Type();
                        type.InitDefaults();
                        BitMaskStream stream = new BitMaskStream(input, 4);
                        if (stream.Read())
                        {
                            type.subframePatternConfig_r11 = subframePatternConfig_r11_Type.PerDecoder.Instance.Decode(input);
                        }
                        if (stream.Read())
                        {
                            type.startSymbol_r11 = input.readBits(2) + 1;
                        }
                        if (stream.Read())
                        {
                            type.setConfigToReleaseList_r11 = new List<long>();
                            num2 = 1;
                            int num3 = input.readBits(num2) + 1;
                            for (int i = 0; i < num3; i++)
                            {
                                long item = input.readBits(1);
                                type.setConfigToReleaseList_r11.Add(item);
                            }
                        }
                        if (stream.Read())
                        {
                            type.setConfigToAddModList_r11 = new List<EPDCCH_SetConfig_r11>();
                            num2 = 1;
                            int num6 = input.readBits(num2) + 1;
                            for (int j = 0; j < num6; j++)
                            {
                                EPDCCH_SetConfig_r11 _r = EPDCCH_SetConfig_r11.PerDecoder.Instance.Decode(input);
                                type.setConfigToAddModList_r11.Add(_r);
                            }
                        }
                        return type;
                    }
                }

                [Serializable]
                public class subframePatternConfig_r11_Type
                {
                    public void InitDefaults()
                    {
                    }

                    public object release { get; set; }

                    public setup_Type setup { get; set; }

                    public class PerDecoder
                    {
                        public static readonly PerDecoder Instance = new PerDecoder();

                        public subframePatternConfig_r11_Type Decode(BitArrayInputStream input)
                        {
                            subframePatternConfig_r11_Type type = new subframePatternConfig_r11_Type();
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

                        public MeasSubframePattern_r10 subframePattern_r11 { get; set; }

                        public class PerDecoder
                        {
                            public static readonly PerDecoder Instance = new PerDecoder();

                            public setup_Type Decode(BitArrayInputStream input)
                            {
                                setup_Type type = new setup_Type();
                                type.InitDefaults();
                                type.subframePattern_r11 = MeasSubframePattern_r10.PerDecoder.Instance.Decode(input);
                                return type;
                            }
                        }
                    }
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public EPDCCH_Config_r11 Decode(BitArrayInputStream input)
            {
                EPDCCH_Config_r11 _r = new EPDCCH_Config_r11();
                _r.InitDefaults();
                _r.config_r11 = config_r11_Type.PerDecoder.Instance.Decode(input);
                return _r;
            }
        }
    }

    [Serializable]
    public class EPDCCH_SetConfig_r11
    {
        public void InitDefaults()
        {
        }

        public long dmrs_ScramblingSequenceInt_r11 { get; set; }

        public long pucch_ResourceStartOffset_r11 { get; set; }

        public long? re_MappingQCL_ConfigId_r11 { get; set; }

        public resourceBlockAssignment_r11_Type resourceBlockAssignment_r11 { get; set; }

        public long setConfigId_r11 { get; set; }

        public transmissionType_r11_Enum transmissionType_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public EPDCCH_SetConfig_r11 Decode(BitArrayInputStream input)
            {
                EPDCCH_SetConfig_r11 _r = new EPDCCH_SetConfig_r11();
                _r.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                _r.setConfigId_r11 = input.readBits(1);
                int nBits = 1;
                _r.transmissionType_r11 = (transmissionType_r11_Enum)input.readBits(nBits);
                _r.resourceBlockAssignment_r11 = resourceBlockAssignment_r11_Type.PerDecoder.Instance.Decode(input);
                _r.dmrs_ScramblingSequenceInt_r11 = input.readBits(9);
                _r.pucch_ResourceStartOffset_r11 = input.readBits(11);
                if (stream.Read())
                {
                    _r.re_MappingQCL_ConfigId_r11 = input.readBits(2) + 1;
                }
                return _r;
            }
        }

        [Serializable]
        public class resourceBlockAssignment_r11_Type
        {
            public void InitDefaults()
            {
            }

            public numberPRB_Pairs_r11_Enum numberPRB_Pairs_r11 { get; set; }

            public string resourceBlockAssignment_r11 { get; set; }

            public enum numberPRB_Pairs_r11_Enum
            {
                n2,
                n4,
                n8
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public resourceBlockAssignment_r11_Type Decode(BitArrayInputStream input)
                {
                    resourceBlockAssignment_r11_Type type = new resourceBlockAssignment_r11_Type();
                    type.InitDefaults();
                    const int nBits = 2;
                    type.numberPRB_Pairs_r11 = (numberPRB_Pairs_r11_Enum)input.readBits(nBits);
                    int num = input.readBits(6);
                    type.resourceBlockAssignment_r11 = input.readBitString(num + 4);
                    return type;
                }
            }
        }

        public enum transmissionType_r11_Enum
        {
            localised,
            distributed
        }
    }

}
