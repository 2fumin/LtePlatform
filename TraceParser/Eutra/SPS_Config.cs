using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SPS_Config
    {
        public void InitDefaults()
        {
        }

        public string semiPersistSchedC_RNTI { get; set; }

        public SPS_ConfigDL sps_ConfigDL { get; set; }

        public SPS_ConfigUL sps_ConfigUL { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SPS_Config Decode(BitArrayInputStream input)
            {
                SPS_Config config = new SPS_Config();
                config.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    config.semiPersistSchedC_RNTI = input.readBitString(0x10);
                }
                if (stream.Read())
                {
                    config.sps_ConfigDL = SPS_ConfigDL.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    config.sps_ConfigUL = SPS_ConfigUL.PerDecoder.Instance.Decode(input);
                }
                return config;
            }
        }
    }

    [Serializable]
    public class SPS_ConfigDL
    {
        public void InitDefaults()
        {
        }

        public object release { get; set; }

        public setup_Type setup { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SPS_ConfigDL Decode(BitArrayInputStream input)
            {
                SPS_ConfigDL gdl = new SPS_ConfigDL();
                gdl.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        gdl.release=new object();
                        return gdl;

                    case 1:
                        gdl.setup = setup_Type.PerDecoder.Instance.Decode(input);
                        return gdl;
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

            public List<long> n1PUCCH_AN_PersistentList { get; set; }

            public long numberOfConfSPS_Processes { get; set; }

            public semiPersistSchedIntervalDL_Enum semiPersistSchedIntervalDL { get; set; }

            public twoAntennaPortActivated_r10_Type twoAntennaPortActivated_r10 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public setup_Type Decode(BitArrayInputStream input)
                {
                    setup_Type type = new setup_Type();
                    type.InitDefaults();
                    bool flag = input.readBit() != 0;
                    int nBits = 4;
                    type.semiPersistSchedIntervalDL = (semiPersistSchedIntervalDL_Enum)input.readBits(nBits);
                    type.numberOfConfSPS_Processes = input.readBits(3) + 1;
                    type.n1PUCCH_AN_PersistentList = new List<long>();
                    nBits = 2;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        long item = input.readBits(11);
                        type.n1PUCCH_AN_PersistentList.Add(item);
                    }
                    if (flag)
                    {
                        BitMaskStream stream = new BitMaskStream(input, 1);
                        if (stream.Read())
                        {
                            type.twoAntennaPortActivated_r10 = twoAntennaPortActivated_r10_Type.PerDecoder.Instance.Decode(input);
                        }
                    }
                    return type;
                }
            }

            public enum semiPersistSchedIntervalDL_Enum
            {
                sf10,
                sf20,
                sf32,
                sf40,
                sf64,
                sf80,
                sf128,
                sf160,
                sf320,
                sf640,
                spare6,
                spare5,
                spare4,
                spare3,
                spare2,
                spare1
            }

            [Serializable]
            public class twoAntennaPortActivated_r10_Type
            {
                public void InitDefaults()
                {
                }

                public object release { get; set; }

                public setup_Type setup { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public twoAntennaPortActivated_r10_Type Decode(BitArrayInputStream input)
                    {
                        twoAntennaPortActivated_r10_Type type = new twoAntennaPortActivated_r10_Type();
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

                    public List<long> n1PUCCH_AN_PersistentListP1_r10 { get; set; }

                    public class PerDecoder
                    {
                        public static readonly PerDecoder Instance = new PerDecoder();

                        public setup_Type Decode(BitArrayInputStream input)
                        {
                            setup_Type type = new setup_Type();
                            type.InitDefaults();
                            type.n1PUCCH_AN_PersistentListP1_r10 = new List<long>();
                            const int nBits = 2;
                            int num3 = input.readBits(nBits) + 1;
                            for (int i = 0; i < num3; i++)
                            {
                                long item = input.readBits(11);
                                type.n1PUCCH_AN_PersistentListP1_r10.Add(item);
                            }
                            return type;
                        }
                    }
                }
            }
        }
    }

    [Serializable]
    public class SPS_ConfigUL
    {
        public void InitDefaults()
        {
        }

        public object release { get; set; }

        public setup_Type setup { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SPS_ConfigUL Decode(BitArrayInputStream input)
            {
                SPS_ConfigUL gul = new SPS_ConfigUL();
                gul.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        gul.release=new object();
                        return gul;

                    case 1:
                        gul.setup = setup_Type.PerDecoder.Instance.Decode(input);
                        return gul;
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

            public implicitReleaseAfter_Enum implicitReleaseAfter { get; set; }

            public p0_Persistent_Type p0_Persistent { get; set; }

            public semiPersistSchedIntervalUL_Enum semiPersistSchedIntervalUL { get; set; }

            public twoIntervalsConfig_Enum? twoIntervalsConfig { get; set; }

            public enum implicitReleaseAfter_Enum
            {
                e2,
                e3,
                e4,
                e8
            }

            [Serializable]
            public class p0_Persistent_Type
            {
                public void InitDefaults()
                {
                }

                public long p0_NominalPUSCH_Persistent { get; set; }

                public long p0_UE_PUSCH_Persistent { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public p0_Persistent_Type Decode(BitArrayInputStream input)
                    {
                        p0_Persistent_Type type = new p0_Persistent_Type();
                        type.InitDefaults();
                        type.p0_NominalPUSCH_Persistent = input.readBits(8) + -126;
                        type.p0_UE_PUSCH_Persistent = input.readBits(4) + -8;
                        return type;
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public setup_Type Decode(BitArrayInputStream input)
                {
                    setup_Type type = new setup_Type();
                    type.InitDefaults();
                    BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                    int nBits = 4;
                    type.semiPersistSchedIntervalUL = (semiPersistSchedIntervalUL_Enum)input.readBits(nBits);
                    nBits = 2;
                    type.implicitReleaseAfter = (implicitReleaseAfter_Enum)input.readBits(nBits);
                    if (stream.Read())
                    {
                        type.p0_Persistent = p0_Persistent_Type.PerDecoder.Instance.Decode(input);
                    }
                    if (stream.Read())
                    {
                        nBits = 1;
                        type.twoIntervalsConfig = (twoIntervalsConfig_Enum)input.readBits(nBits);
                    }
                    return type;
                }
            }

            public enum semiPersistSchedIntervalUL_Enum
            {
                sf10,
                sf20,
                sf32,
                sf40,
                sf64,
                sf80,
                sf128,
                sf160,
                sf320,
                sf640,
                spare6,
                spare5,
                spare4,
                spare3,
                spare2,
                spare1
            }

            public enum twoIntervalsConfig_Enum
            {
                _true
            }
        }
    }

}
