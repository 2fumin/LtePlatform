using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class MAC_MainConfig
    {
        public void InitDefaults()
        {
        }

        public DRX_Config drx_Config { get; set; }

        public DRX_Config_v1130 drx_Config_v1130 { get; set; }

        public mac_MainConfig_v1020_Type mac_MainConfig_v1020 { get; set; }

        public phr_Config_Type phr_Config { get; set; }

        public long? sr_ProhibitTimer_r9 { get; set; }

        public List<STAG_ToAddMod_r11> stag_ToAddModList_r11 { get; set; }

        public List<long> stag_ToReleaseList_r11 { get; set; }

        public TimeAlignmentTimer timeAlignmentTimerDedicated { get; set; }

        public ul_SCH_Config_Type ul_SCH_Config { get; set; }

        [Serializable]
        public class mac_MainConfig_v1020_Type
        {
            public void InitDefaults()
            {
            }

            public extendedBSR_Sizes_r10_Enum? extendedBSR_Sizes_r10 { get; set; }

            public extendedPHR_r10_Enum? extendedPHR_r10 { get; set; }

            public sCellDeactivationTimer_r10_Enum? sCellDeactivationTimer_r10 { get; set; }

            public enum extendedBSR_Sizes_r10_Enum
            {
                setup
            }

            public enum extendedPHR_r10_Enum
            {
                setup
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public mac_MainConfig_v1020_Type Decode(BitArrayInputStream input)
                {
                    int num2;
                    mac_MainConfig_v1020_Type type = new mac_MainConfig_v1020_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 3);
                    if (stream.Read())
                    {
                        num2 = 3;
                        type.sCellDeactivationTimer_r10 = (sCellDeactivationTimer_r10_Enum)input.readBits(num2);
                    }
                    if (stream.Read())
                    {
                        num2 = 1;
                        type.extendedBSR_Sizes_r10 = (extendedBSR_Sizes_r10_Enum)input.readBits(num2);
                    }
                    if (stream.Read())
                    {
                        num2 = 1;
                        type.extendedPHR_r10 = (extendedPHR_r10_Enum)input.readBits(num2);
                    }
                    return type;
                }
            }

            public enum sCellDeactivationTimer_r10_Enum
            {
                rf2,
                rf4,
                rf8,
                rf16,
                rf32,
                rf64,
                rf128,
                spare
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MAC_MainConfig Decode(BitArrayInputStream input)
            {
                BitMaskStream stream2;
                MAC_MainConfig config = new MAC_MainConfig();
                config.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    config.ul_SCH_Config = ul_SCH_Config_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    config.drx_Config = DRX_Config.PerDecoder.Instance.Decode(input);
                }
                int nBits = 3;
                config.timeAlignmentTimerDedicated = (TimeAlignmentTimer)input.readBits(nBits);
                if (stream.Read())
                {
                    config.phr_Config = phr_Config_Type.PerDecoder.Instance.Decode(input);
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        config.sr_ProhibitTimer_r9 = input.readBits(3);
                    }
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        config.mac_MainConfig_v1020 = mac_MainConfig_v1020_Type.PerDecoder.Instance.Decode(input);
                    }
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 3);
                    if (stream2.Read())
                    {
                        config.stag_ToReleaseList_r11 = new List<long>();
                        nBits = 2;
                        int num3 = input.readBits(nBits) + 1;
                        for (int i = 0; i < num3; i++)
                        {
                            long item = input.readBits(2) + 1;
                            config.stag_ToReleaseList_r11.Add(item);
                        }
                    }
                    if (stream2.Read())
                    {
                        config.stag_ToAddModList_r11 = new List<STAG_ToAddMod_r11>();
                        nBits = 2;
                        int num6 = input.readBits(nBits) + 1;
                        for (int j = 0; j < num6; j++)
                        {
                            STAG_ToAddMod_r11 _r = STAG_ToAddMod_r11.PerDecoder.Instance.Decode(input);
                            config.stag_ToAddModList_r11.Add(_r);
                        }
                    }
                    if (stream2.Read())
                    {
                        config.drx_Config_v1130 = DRX_Config_v1130.PerDecoder.Instance.Decode(input);
                    }
                }
                return config;
            }
        }

        [Serializable]
        public class phr_Config_Type
        {
            public void InitDefaults()
            {
            }

            public object release { get; set; }

            public setup_Type setup { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public phr_Config_Type Decode(BitArrayInputStream input)
                {
                    phr_Config_Type type = new phr_Config_Type();
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

                public dl_PathlossChange_Enum dl_PathlossChange { get; set; }

                public periodicPHR_Timer_Enum periodicPHR_Timer { get; set; }

                public prohibitPHR_Timer_Enum prohibitPHR_Timer { get; set; }

                public enum dl_PathlossChange_Enum
                {
                    dB1,
                    dB3,
                    dB6,
                    infinity
                }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public setup_Type Decode(BitArrayInputStream input)
                    {
                        setup_Type type = new setup_Type();
                        type.InitDefaults();
                        int nBits = 3;
                        type.periodicPHR_Timer = (periodicPHR_Timer_Enum)input.readBits(nBits);
                        nBits = 3;
                        type.prohibitPHR_Timer = (prohibitPHR_Timer_Enum)input.readBits(nBits);
                        nBits = 2;
                        type.dl_PathlossChange = (dl_PathlossChange_Enum)input.readBits(nBits);
                        return type;
                    }
                }

                public enum periodicPHR_Timer_Enum
                {
                    sf10,
                    sf20,
                    sf50,
                    sf100,
                    sf200,
                    sf500,
                    sf1000,
                    infinity
                }

                public enum prohibitPHR_Timer_Enum
                {
                    sf0,
                    sf10,
                    sf20,
                    sf50,
                    sf100,
                    sf200,
                    sf500,
                    sf1000
                }
            }
        }

        [Serializable]
        public class ul_SCH_Config_Type
        {
            public void InitDefaults()
            {
            }

            public maxHARQ_Tx_Enum? maxHARQ_Tx { get; set; }

            public periodicBSR_Timer_Enum? periodicBSR_Timer { get; set; }

            public retxBSR_Timer_Enum retxBSR_Timer { get; set; }

            public bool ttiBundling { get; set; }

            public enum maxHARQ_Tx_Enum
            {
                n1,
                n2,
                n3,
                n4,
                n5,
                n6,
                n7,
                n8,
                n10,
                n12,
                n16,
                n20,
                n24,
                n28,
                spare2,
                spare1
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public ul_SCH_Config_Type Decode(BitArrayInputStream input)
                {
                    int num2;
                    ul_SCH_Config_Type type = new ul_SCH_Config_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 2);
                    if (stream.Read())
                    {
                        num2 = 4;
                        type.maxHARQ_Tx = (maxHARQ_Tx_Enum)input.readBits(num2);
                    }
                    if (stream.Read())
                    {
                        num2 = 4;
                        type.periodicBSR_Timer = (periodicBSR_Timer_Enum)input.readBits(num2);
                    }
                    num2 = 3;
                    type.retxBSR_Timer = (retxBSR_Timer_Enum)input.readBits(num2);
                    type.ttiBundling = input.readBit() == 1;
                    return type;
                }
            }

            public enum periodicBSR_Timer_Enum
            {
                sf5,
                sf10,
                sf16,
                sf20,
                sf32,
                sf40,
                sf64,
                sf80,
                sf128,
                sf160,
                sf320,
                sf640,
                sf1280,
                sf2560,
                infinity,
                spare1
            }

            public enum retxBSR_Timer_Enum
            {
                sf320,
                sf640,
                sf1280,
                sf2560,
                sf5120,
                sf10240,
                spare2,
                spare1
            }
        }
    }

    [Serializable]
    public class MAC_MainConfigSCell_r11
    {
        public void InitDefaults()
        {
        }

        public long? stag_Id_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MAC_MainConfigSCell_r11 Decode(BitArrayInputStream input)
            {
                MAC_MainConfigSCell_r11 _r = new MAC_MainConfigSCell_r11();
                _r.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    _r.stag_Id_r11 = input.readBits(2) + 1;
                }
                return _r;
            }
        }
    }

}
