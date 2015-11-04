using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class DRX_Config
    {
        public void InitDefaults()
        {
        }

        public object release { get; set; }

        public setup_Type setup { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public DRX_Config Decode(BitArrayInputStream input)
            {
                DRX_Config config = new DRX_Config();
                config.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        return config;

                    case 1:
                        config.setup = setup_Type.PerDecoder.Instance.Decode(input);
                        return config;
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

            public drx_InactivityTimer_Enum drx_InactivityTimer { get; set; }

            public drx_RetransmissionTimer_Enum drx_RetransmissionTimer { get; set; }

            public longDRX_CycleStartOffset_Type longDRX_CycleStartOffset { get; set; }

            public onDurationTimer_Enum onDurationTimer { get; set; }

            public shortDRX_Type shortDRX { get; set; }

            public enum drx_InactivityTimer_Enum
            {
                psf1,
                psf2,
                psf3,
                psf4,
                psf5,
                psf6,
                psf8,
                psf10,
                psf20,
                psf30,
                psf40,
                psf50,
                psf60,
                psf80,
                psf100,
                psf200,
                psf300,
                psf500,
                psf750,
                psf1280,
                psf1920,
                psf2560,
                psf0_v1020,
                spare9,
                spare8,
                spare7,
                spare6,
                spare5,
                spare4,
                spare3,
                spare2,
                spare1
            }

            public enum drx_RetransmissionTimer_Enum
            {
                psf1,
                psf2,
                psf4,
                psf6,
                psf8,
                psf16,
                psf24,
                psf33
            }

            [Serializable]
            public class longDRX_CycleStartOffset_Type
            {
                public void InitDefaults()
                {
                }

                public long sf10 { get; set; }

                public long sf1024 { get; set; }

                public long sf128 { get; set; }

                public long sf1280 { get; set; }

                public long sf160 { get; set; }

                public long sf20 { get; set; }

                public long sf2048 { get; set; }

                public long sf256 { get; set; }

                public long sf2560 { get; set; }

                public long sf32 { get; set; }

                public long sf320 { get; set; }

                public long sf40 { get; set; }

                public long sf512 { get; set; }

                public long sf64 { get; set; }

                public long sf640 { get; set; }

                public long sf80 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public longDRX_CycleStartOffset_Type Decode(BitArrayInputStream input)
                    {
                        longDRX_CycleStartOffset_Type type = new longDRX_CycleStartOffset_Type();
                        type.InitDefaults();
                        switch (input.readBits(4))
                        {
                            case 0:
                                type.sf10 = input.readBits(4);
                                return type;

                            case 1:
                                type.sf20 = input.readBits(5);
                                return type;

                            case 2:
                                type.sf32 = input.readBits(5);
                                return type;

                            case 3:
                                type.sf40 = input.readBits(6);
                                return type;

                            case 4:
                                type.sf64 = input.readBits(6);
                                return type;

                            case 5:
                                type.sf80 = input.readBits(7);
                                return type;

                            case 6:
                                type.sf128 = input.readBits(7);
                                return type;

                            case 7:
                                type.sf160 = input.readBits(8);
                                return type;

                            case 8:
                                type.sf256 = input.readBits(8);
                                return type;

                            case 9:
                                type.sf320 = input.readBits(9);
                                return type;

                            case 10:
                                type.sf512 = input.readBits(9);
                                return type;

                            case 11:
                                type.sf640 = input.readBits(10);
                                return type;

                            case 12:
                                type.sf1024 = input.readBits(10);
                                return type;

                            case 13:
                                type.sf1280 = input.readBits(11);
                                return type;

                            case 14:
                                type.sf2048 = input.readBits(11);
                                return type;

                            case 15:
                                type.sf2560 = input.readBits(12);
                                return type;
                        }
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                }
            }

            public enum onDurationTimer_Enum
            {
                psf1,
                psf2,
                psf3,
                psf4,
                psf5,
                psf6,
                psf8,
                psf10,
                psf20,
                psf30,
                psf40,
                psf50,
                psf60,
                psf80,
                psf100,
                psf200
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public setup_Type Decode(BitArrayInputStream input)
                {
                    setup_Type type = new setup_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 1);
                    int nBits = 4;
                    type.onDurationTimer = (onDurationTimer_Enum)input.readBits(nBits);
                    nBits = 5;
                    type.drx_InactivityTimer = (drx_InactivityTimer_Enum)input.readBits(nBits);
                    nBits = 3;
                    type.drx_RetransmissionTimer = (drx_RetransmissionTimer_Enum)input.readBits(nBits);
                    type.longDRX_CycleStartOffset = longDRX_CycleStartOffset_Type.PerDecoder.Instance.Decode(input);
                    if (stream.Read())
                    {
                        type.shortDRX = shortDRX_Type.PerDecoder.Instance.Decode(input);
                    }
                    return type;
                }
            }

            [Serializable]
            public class shortDRX_Type
            {
                public void InitDefaults()
                {
                }

                public long drxShortCycleTimer { get; set; }

                public shortDRX_Cycle_Enum shortDRX_Cycle { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public shortDRX_Type Decode(BitArrayInputStream input)
                    {
                        shortDRX_Type type = new shortDRX_Type();
                        type.InitDefaults();
                        int nBits = 4;
                        type.shortDRX_Cycle = (shortDRX_Cycle_Enum)input.readBits(nBits);
                        type.drxShortCycleTimer = input.readBits(4) + 1;
                        return type;
                    }
                }

                public enum shortDRX_Cycle_Enum
                {
                    sf2,
                    sf5,
                    sf8,
                    sf10,
                    sf16,
                    sf20,
                    sf32,
                    sf40,
                    sf64,
                    sf80,
                    sf128,
                    sf160,
                    sf256,
                    sf320,
                    sf512,
                    sf640
                }
            }
        }
    }

    [Serializable]
    public class DRX_Config_v1130
    {
        public void InitDefaults()
        {
        }

        public drx_RetransmissionTimer_v1130_Enum? drx_RetransmissionTimer_v1130 { get; set; }

        public longDRX_CycleStartOffset_v1130_Type longDRX_CycleStartOffset_v1130 { get; set; }

        public shortDRX_Cycle_v1130_Enum? shortDRX_Cycle_v1130 { get; set; }

        public enum drx_RetransmissionTimer_v1130_Enum
        {
            psf0_v1130
        }

        [Serializable]
        public class longDRX_CycleStartOffset_v1130_Type
        {
            public void InitDefaults()
            {
            }

            public long sf60_v1130 { get; set; }

            public long sf70_v1130 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public longDRX_CycleStartOffset_v1130_Type Decode(BitArrayInputStream input)
                {
                    longDRX_CycleStartOffset_v1130_Type type = new longDRX_CycleStartOffset_v1130_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.sf60_v1130 = input.readBits(6);
                            return type;

                        case 1:
                            type.sf70_v1130 = input.readBits(7);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public DRX_Config_v1130 Decode(BitArrayInputStream input)
            {
                int num2;
                DRX_Config_v1130 _v = new DRX_Config_v1130();
                _v.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    num2 = 1;
                    _v.drx_RetransmissionTimer_v1130 = (drx_RetransmissionTimer_v1130_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    _v.longDRX_CycleStartOffset_v1130 = longDRX_CycleStartOffset_v1130_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _v.shortDRX_Cycle_v1130 = (shortDRX_Cycle_v1130_Enum)input.readBits(num2);
                }
                return _v;
            }
        }

        public enum shortDRX_Cycle_v1130_Enum
        {
            sf4_v1130
        }
    }

}
