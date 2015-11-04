using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class PDCP_Config
    {
        public void InitDefaults()
        {
        }

        public discardTimer_Enum? discardTimer { get; set; }

        public headerCompression_Type headerCompression { get; set; }

        public pdcp_SN_Size_v1130_Enum? pdcp_SN_Size_v1130 { get; set; }

        public rlc_AM_Type rlc_AM { get; set; }

        public rlc_UM_Type rlc_UM { get; set; }

        public rn_IntegrityProtection_r10_Enum? rn_IntegrityProtection_r10 { get; set; }

        public enum discardTimer_Enum
        {
            ms50,
            ms100,
            ms150,
            ms300,
            ms500,
            ms750,
            ms1500,
            infinity
        }

        [Serializable]
        public class headerCompression_Type
        {
            public void InitDefaults()
            {
            }

            public object notUsed { get; set; }

            public rohc_Type rohc { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public headerCompression_Type Decode(BitArrayInputStream input)
                {
                    headerCompression_Type type = new headerCompression_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            return type;

                        case 1:
                            type.rohc = rohc_Type.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }

            [Serializable]
            public class rohc_Type
            {
                public void InitDefaults()
                {
                    maxCID = 15L;
                }

                public long maxCID { get; set; }

                public profiles_Type profiles { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public rohc_Type Decode(BitArrayInputStream input)
                    {
                        rohc_Type type = new rohc_Type();
                        type.InitDefaults();
                        BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                        if (stream.Read())
                        {
                            type.maxCID = input.readBits(14) + 1;
                        }
                        type.profiles = profiles_Type.PerDecoder.Instance.Decode(input);
                        return type;
                    }
                }

                [Serializable]
                public class profiles_Type
                {
                    public void InitDefaults()
                    {
                    }

                    public bool profile0x0001 { get; set; }

                    public bool profile0x0002 { get; set; }

                    public bool profile0x0003 { get; set; }

                    public bool profile0x0004 { get; set; }

                    public bool profile0x0006 { get; set; }

                    public bool profile0x0101 { get; set; }

                    public bool profile0x0102 { get; set; }

                    public bool profile0x0103 { get; set; }

                    public bool profile0x0104 { get; set; }

                    public class PerDecoder
                    {
                        public static readonly PerDecoder Instance = new PerDecoder();

                        public profiles_Type Decode(BitArrayInputStream input)
                        {
                            profiles_Type type = new profiles_Type();
                            type.InitDefaults();
                            type.profile0x0001 = input.readBit() == 1;
                            type.profile0x0002 = input.readBit() == 1;
                            type.profile0x0003 = input.readBit() == 1;
                            type.profile0x0004 = input.readBit() == 1;
                            type.profile0x0006 = input.readBit() == 1;
                            type.profile0x0101 = input.readBit() == 1;
                            type.profile0x0102 = input.readBit() == 1;
                            type.profile0x0103 = input.readBit() == 1;
                            type.profile0x0104 = input.readBit() == 1;
                            return type;
                        }
                    }
                }
            }
        }

        public enum pdcp_SN_Size_v1130_Enum
        {
            len15bits
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PDCP_Config Decode(BitArrayInputStream input)
            {
                int num2;
                BitMaskStream stream2;
                PDCP_Config config = new PDCP_Config();
                config.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    num2 = 3;
                    config.discardTimer = (discardTimer_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    config.rlc_AM = rlc_AM_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    config.rlc_UM = rlc_UM_Type.PerDecoder.Instance.Decode(input);
                }
                config.headerCompression = headerCompression_Type.PerDecoder.Instance.Decode(input);
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        num2 = 1;
                        config.rn_IntegrityProtection_r10 = (rn_IntegrityProtection_r10_Enum)input.readBits(num2);
                    }
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        num2 = 1;
                        config.pdcp_SN_Size_v1130 = (pdcp_SN_Size_v1130_Enum)input.readBits(num2);
                    }
                }
                return config;
            }
        }

        [Serializable]
        public class rlc_AM_Type
        {
            public void InitDefaults()
            {
            }

            public bool statusReportRequired { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public rlc_AM_Type Decode(BitArrayInputStream input)
                {
                    rlc_AM_Type type = new rlc_AM_Type();
                    type.InitDefaults();
                    type.statusReportRequired = input.readBit() == 1;
                    return type;
                }
            }
        }

        [Serializable]
        public class rlc_UM_Type
        {
            public void InitDefaults()
            {
            }

            public pdcp_SN_Size_Enum pdcp_SN_Size { get; set; }

            public enum pdcp_SN_Size_Enum
            {
                len7bits,
                len12bits
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public rlc_UM_Type Decode(BitArrayInputStream input)
                {
                    rlc_UM_Type type = new rlc_UM_Type();
                    type.InitDefaults();
                    int nBits = 1;
                    type.pdcp_SN_Size = (pdcp_SN_Size_Enum)input.readBits(nBits);
                    return type;
                }
            }
        }

        public enum rn_IntegrityProtection_r10_Enum
        {
            enabled
        }
    }

    [Serializable]
    public class PDCP_Parameters
    {
        public void InitDefaults()
        {
            maxNumberROHC_ContextSessions = maxNumberROHC_ContextSessions_Enum.cs16;
        }

        public maxNumberROHC_ContextSessions_Enum maxNumberROHC_ContextSessions { get; set; }

        public supportedROHC_Profiles_Type supportedROHC_Profiles { get; set; }

        public enum maxNumberROHC_ContextSessions_Enum
        {
            cs2,
            cs4,
            cs8,
            cs12,
            cs16,
            cs24,
            cs32,
            cs48,
            cs64,
            cs128,
            cs256,
            cs512,
            cs1024,
            cs16384,
            spare2,
            spare1
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PDCP_Parameters Decode(BitArrayInputStream input)
            {
                PDCP_Parameters parameters = new PDCP_Parameters();
                parameters.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                parameters.supportedROHC_Profiles = supportedROHC_Profiles_Type.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    int nBits = 4;
                    parameters.maxNumberROHC_ContextSessions = (maxNumberROHC_ContextSessions_Enum)input.readBits(nBits);
                }
                return parameters;
            }
        }

        [Serializable]
        public class supportedROHC_Profiles_Type
        {
            public void InitDefaults()
            {
            }

            public bool profile0x0001 { get; set; }

            public bool profile0x0002 { get; set; }

            public bool profile0x0003 { get; set; }

            public bool profile0x0004 { get; set; }

            public bool profile0x0006 { get; set; }

            public bool profile0x0101 { get; set; }

            public bool profile0x0102 { get; set; }

            public bool profile0x0103 { get; set; }

            public bool profile0x0104 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public supportedROHC_Profiles_Type Decode(BitArrayInputStream input)
                {
                    supportedROHC_Profiles_Type type = new supportedROHC_Profiles_Type();
                    type.InitDefaults();
                    type.profile0x0001 = input.readBit() == 1;
                    type.profile0x0002 = input.readBit() == 1;
                    type.profile0x0003 = input.readBit() == 1;
                    type.profile0x0004 = input.readBit() == 1;
                    type.profile0x0006 = input.readBit() == 1;
                    type.profile0x0101 = input.readBit() == 1;
                    type.profile0x0102 = input.readBit() == 1;
                    type.profile0x0103 = input.readBit() == 1;
                    type.profile0x0104 = input.readBit() == 1;
                    return type;
                }
            }
        }
    }

    [Serializable]
    public class PDCP_Parameters_v1130
    {
        public void InitDefaults()
        {
        }

        public pdcp_SN_Extension_r11_Enum? pdcp_SN_Extension_r11 { get; set; }

        public supportRohcContextContinue_r11_Enum? supportRohcContextContinue_r11 { get; set; }

        public enum pdcp_SN_Extension_r11_Enum
        {
            supported
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PDCP_Parameters_v1130 Decode(BitArrayInputStream input)
            {
                int num2;
                PDCP_Parameters_v1130 _v = new PDCP_Parameters_v1130();
                _v.InitDefaults();
                bool flag = false;
                BitMaskStream stream = flag ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    num2 = 1;
                    _v.pdcp_SN_Extension_r11 = (pdcp_SN_Extension_r11_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _v.supportRohcContextContinue_r11 = (supportRohcContextContinue_r11_Enum)input.readBits(num2);
                }
                return _v;
            }
        }

        public enum supportRohcContextContinue_r11_Enum
        {
            supported
        }
    }

}
