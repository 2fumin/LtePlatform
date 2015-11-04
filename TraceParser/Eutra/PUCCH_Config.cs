using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class PUCCH_ConfigCommon
    {
        public void InitDefaults()
        {
        }

        public deltaPUCCH_Shift_Enum deltaPUCCH_Shift { get; set; }

        public long n1PUCCH_AN { get; set; }

        public long nCS_AN { get; set; }

        public long nRB_CQI { get; set; }

        public enum deltaPUCCH_Shift_Enum
        {
            ds1,
            ds2,
            ds3
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PUCCH_ConfigCommon Decode(BitArrayInputStream input)
            {
                PUCCH_ConfigCommon common = new PUCCH_ConfigCommon();
                common.InitDefaults();
                int nBits = 2;
                common.deltaPUCCH_Shift = (deltaPUCCH_Shift_Enum)input.readBits(nBits);
                common.nRB_CQI = input.readBits(7);
                common.nCS_AN = input.readBits(3);
                common.n1PUCCH_AN = input.readBits(11);
                return common;
            }
        }
    }

    [Serializable]
    public class PUCCH_ConfigDedicated
    {
        public void InitDefaults()
        {
        }

        public ackNackRepetition_Type ackNackRepetition { get; set; }

        public tdd_AckNackFeedbackMode_Enum? tdd_AckNackFeedbackMode { get; set; }

        [Serializable]
        public class ackNackRepetition_Type
        {
            public void InitDefaults()
            {
            }

            public object release { get; set; }

            public setup_Type setup { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public ackNackRepetition_Type Decode(BitArrayInputStream input)
                {
                    ackNackRepetition_Type type = new ackNackRepetition_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.release=new object();
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

                public long n1PUCCH_AN_Rep { get; set; }

                public repetitionFactor_Enum repetitionFactor { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public setup_Type Decode(BitArrayInputStream input)
                    {
                        setup_Type type = new setup_Type();
                        type.InitDefaults();
                        int nBits = 2;
                        type.repetitionFactor = (repetitionFactor_Enum)input.readBits(nBits);
                        type.n1PUCCH_AN_Rep = input.readBits(11);
                        return type;
                    }
                }

                public enum repetitionFactor_Enum
                {
                    n2,
                    n4,
                    n6,
                    spare1
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PUCCH_ConfigDedicated Decode(BitArrayInputStream input)
            {
                PUCCH_ConfigDedicated dedicated = new PUCCH_ConfigDedicated();
                dedicated.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                dedicated.ackNackRepetition = ackNackRepetition_Type.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    int nBits = 1;
                    dedicated.tdd_AckNackFeedbackMode = (tdd_AckNackFeedbackMode_Enum)input.readBits(nBits);
                }
                return dedicated;
            }
        }

        public enum tdd_AckNackFeedbackMode_Enum
        {
            bundling,
            multiplexing
        }
    }

    [Serializable]
    public class PUCCH_ConfigDedicated_v1020
    {
        public void InitDefaults()
        {
        }

        public long? n1PUCCH_AN_RepP1_r10 { get; set; }

        public pucch_Format_r10_Type pucch_Format_r10 { get; set; }

        public simultaneousPUCCH_PUSCH_r10_Enum? simultaneousPUCCH_PUSCH_r10 { get; set; }

        public twoAntennaPortActivatedPUCCH_Format1a1b_r10_Enum? twoAntennaPortActivatedPUCCH_Format1a1b_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PUCCH_ConfigDedicated_v1020 Decode(BitArrayInputStream input)
            {
                int num2;
                PUCCH_ConfigDedicated_v1020 _v = new PUCCH_ConfigDedicated_v1020();
                _v.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 4);
                if (stream.Read())
                {
                    _v.pucch_Format_r10 = pucch_Format_r10_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _v.twoAntennaPortActivatedPUCCH_Format1a1b_r10 = (twoAntennaPortActivatedPUCCH_Format1a1b_r10_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _v.simultaneousPUCCH_PUSCH_r10 = (simultaneousPUCCH_PUSCH_r10_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    _v.n1PUCCH_AN_RepP1_r10 = input.readBits(11);
                }
                return _v;
            }
        }

        [Serializable]
        public class pucch_Format_r10_Type
        {
            public void InitDefaults()
            {
            }

            public channelSelection_r10_Type channelSelection_r10 { get; set; }

            public format3_r10_Type format3_r10 { get; set; }

            [Serializable]
            public class channelSelection_r10_Type
            {
                public void InitDefaults()
                {
                }

                public n1PUCCH_AN_CS_r10_Type n1PUCCH_AN_CS_r10 { get; set; }

                [Serializable]
                public class n1PUCCH_AN_CS_r10_Type
                {
                    public void InitDefaults()
                    {
                    }

                    public object release { get; set; }

                    public setup_Type setup { get; set; }

                    public class PerDecoder
                    {
                        public static readonly PerDecoder Instance = new PerDecoder();

                        public n1PUCCH_AN_CS_r10_Type Decode(BitArrayInputStream input)
                        {
                            n1PUCCH_AN_CS_r10_Type type = new n1PUCCH_AN_CS_r10_Type();
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

                        public List<List<long>> n1PUCCH_AN_CS_List_r10 { get; set; }

                        public class PerDecoder
                        {
                            public static readonly PerDecoder Instance = new PerDecoder();

                            public setup_Type Decode(BitArrayInputStream input)
                            {
                                setup_Type type = new setup_Type();
                                type.InitDefaults();
                                type.n1PUCCH_AN_CS_List_r10 = new List<List<long>>();
                                int nBits = 1;
                                int num3 = input.readBits(nBits) + 1;
                                for (int i = 0; i < num3; i++)
                                {
                                    List<long> item = new List<long>();
                                    nBits = 2;
                                    int num5 = input.readBits(nBits) + 1;
                                    for (int j = 0; j < num5; j++)
                                    {
                                        long num7 = input.readBits(11);
                                        item.Add(num7);
                                    }
                                    type.n1PUCCH_AN_CS_List_r10.Add(item);
                                }
                                return type;
                            }
                        }
                    }
                }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public channelSelection_r10_Type Decode(BitArrayInputStream input)
                    {
                        channelSelection_r10_Type type = new channelSelection_r10_Type();
                        type.InitDefaults();
                        BitMaskStream stream = new BitMaskStream(input, 1);
                        if (stream.Read())
                        {
                            type.n1PUCCH_AN_CS_r10 = n1PUCCH_AN_CS_r10_Type.PerDecoder.Instance.Decode(input);
                        }
                        return type;
                    }
                }
            }

            [Serializable]
            public class format3_r10_Type
            {
                public void InitDefaults()
                {
                }

                public List<long> n3PUCCH_AN_List_r10 { get; set; }

                public twoAntennaPortActivatedPUCCH_Format3_r10_Type twoAntennaPortActivatedPUCCH_Format3_r10 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public format3_r10_Type Decode(BitArrayInputStream input)
                    {
                        format3_r10_Type type = new format3_r10_Type();
                        type.InitDefaults();
                        BitMaskStream stream = new BitMaskStream(input, 2);
                        if (stream.Read())
                        {
                            type.n3PUCCH_AN_List_r10 = new List<long>();
                            int nBits = 2;
                            int num3 = input.readBits(nBits) + 1;
                            for (int i = 0; i < num3; i++)
                            {
                                long item = input.readBits(10);
                                type.n3PUCCH_AN_List_r10.Add(item);
                            }
                        }
                        if (stream.Read())
                        {
                            type.twoAntennaPortActivatedPUCCH_Format3_r10 = twoAntennaPortActivatedPUCCH_Format3_r10_Type.PerDecoder.Instance.Decode(input);
                        }
                        return type;
                    }
                }

                [Serializable]
                public class twoAntennaPortActivatedPUCCH_Format3_r10_Type
                {
                    public void InitDefaults()
                    {
                    }

                    public object release { get; set; }

                    public setup_Type setup { get; set; }

                    public class PerDecoder
                    {
                        public static readonly PerDecoder Instance = new PerDecoder();

                        public twoAntennaPortActivatedPUCCH_Format3_r10_Type Decode(BitArrayInputStream input)
                        {
                            twoAntennaPortActivatedPUCCH_Format3_r10_Type type = new twoAntennaPortActivatedPUCCH_Format3_r10_Type();
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

                        public List<long> n3PUCCH_AN_ListP1_r10 { get; set; }

                        public class PerDecoder
                        {
                            public static readonly PerDecoder Instance = new PerDecoder();

                            public setup_Type Decode(BitArrayInputStream input)
                            {
                                setup_Type type = new setup_Type();
                                type.InitDefaults();
                                type.n3PUCCH_AN_ListP1_r10 = new List<long>();
                                int nBits = 2;
                                int num3 = input.readBits(nBits) + 1;
                                for (int i = 0; i < num3; i++)
                                {
                                    long item = input.readBits(10);
                                    type.n3PUCCH_AN_ListP1_r10.Add(item);
                                }
                                return type;
                            }
                        }
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public pucch_Format_r10_Type Decode(BitArrayInputStream input)
                {
                    pucch_Format_r10_Type type = new pucch_Format_r10_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.format3_r10 = format3_r10_Type.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.channelSelection_r10 = channelSelection_r10_Type.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public enum simultaneousPUCCH_PUSCH_r10_Enum
        {
            _true
        }

        public enum twoAntennaPortActivatedPUCCH_Format1a1b_r10_Enum
        {
            _true
        }
    }

    [Serializable]
    public class PUCCH_ConfigDedicated_v1130
    {
        public void InitDefaults()
        {
        }

        public n1PUCCH_AN_CS_v1130_Type n1PUCCH_AN_CS_v1130 { get; set; }

        public nPUCCH_Param_r11_Type nPUCCH_Param_r11 { get; set; }

        [Serializable]
        public class n1PUCCH_AN_CS_v1130_Type
        {
            public void InitDefaults()
            {
            }

            public object release { get; set; }

            public setup_Type setup { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public n1PUCCH_AN_CS_v1130_Type Decode(BitArrayInputStream input)
                {
                    n1PUCCH_AN_CS_v1130_Type type = new n1PUCCH_AN_CS_v1130_Type();
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

                public List<long> n1PUCCH_AN_CS_ListP1_r11 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public setup_Type Decode(BitArrayInputStream input)
                    {
                        setup_Type type = new setup_Type();
                        type.InitDefaults();
                        type.n1PUCCH_AN_CS_ListP1_r11 = new List<long>();
                        int nBits = 2;
                        int num3 = input.readBits(nBits) + 2;
                        for (int i = 0; i < num3; i++)
                        {
                            long item = input.readBits(11);
                            type.n1PUCCH_AN_CS_ListP1_r11.Add(item);
                        }
                        return type;
                    }
                }
            }
        }

        [Serializable]
        public class nPUCCH_Param_r11_Type
        {
            public void InitDefaults()
            {
            }

            public object release { get; set; }

            public setup_Type setup { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public nPUCCH_Param_r11_Type Decode(BitArrayInputStream input)
                {
                    nPUCCH_Param_r11_Type type = new nPUCCH_Param_r11_Type();
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

                public long n1PUCCH_AN_r11 { get; set; }

                public long nPUCCH_Identity_r11 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public setup_Type Decode(BitArrayInputStream input)
                    {
                        setup_Type type = new setup_Type();
                        type.InitDefaults();
                        type.nPUCCH_Identity_r11 = input.readBits(9);
                        type.n1PUCCH_AN_r11 = input.readBits(11);
                        return type;
                    }
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PUCCH_ConfigDedicated_v1130 Decode(BitArrayInputStream input)
            {
                PUCCH_ConfigDedicated_v1130 _v = new PUCCH_ConfigDedicated_v1130();
                _v.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    _v.n1PUCCH_AN_CS_v1130 = n1PUCCH_AN_CS_v1130_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    _v.nPUCCH_Param_r11 = nPUCCH_Param_r11_Type.PerDecoder.Instance.Decode(input);
                }
                return _v;
            }
        }
    }

}
