using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SchedulingInfo
    {
        public void InitDefaults()
        {
        }

        public si_Periodicity_Enum si_Periodicity { get; set; }

        public List<SIB_Type> sib_MappingInfo { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SchedulingInfo Decode(BitArrayInputStream input)
            {
                SchedulingInfo info = new SchedulingInfo();
                info.InitDefaults();
                int nBits = 3;
                info.si_Periodicity = (si_Periodicity_Enum)input.readBits(nBits);
                info.sib_MappingInfo = new List<SIB_Type>();
                nBits = 5;
                int num3 = input.readBits(nBits);
                for (int i = 0; i < num3; i++)
                {
                    nBits = (input.readBit() == 0) ? 4 : 4;
                    SIB_Type item = (SIB_Type)input.readBits(nBits);
                    info.sib_MappingInfo.Add(item);
                }
                return info;
            }
        }

        public enum si_Periodicity_Enum
        {
            rf8,
            rf16,
            rf32,
            rf64,
            rf128,
            rf256,
            rf512
        }
    }

    [Serializable]
    public class SchedulingRequestConfig
    {
        public void InitDefaults()
        {
        }

        public object release { get; set; }

        public setup_Type setup { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SchedulingRequestConfig Decode(BitArrayInputStream input)
            {
                SchedulingRequestConfig config = new SchedulingRequestConfig();
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

            public dsr_TransMax_Enum dsr_TransMax { get; set; }

            public long sr_ConfigIndex { get; set; }

            public long sr_PUCCH_ResourceIndex { get; set; }

            public enum dsr_TransMax_Enum
            {
                n4,
                n8,
                n16,
                n32,
                n64,
                spare3,
                spare2,
                spare1
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public setup_Type Decode(BitArrayInputStream input)
                {
                    setup_Type type = new setup_Type();
                    type.InitDefaults();
                    type.sr_PUCCH_ResourceIndex = input.readBits(11);
                    type.sr_ConfigIndex = input.readBits(8);
                    int nBits = 3;
                    type.dsr_TransMax = (dsr_TransMax_Enum)input.readBits(nBits);
                    return type;
                }
            }
        }
    }

    [Serializable]
    public class SchedulingRequestConfig_v1020
    {
        public void InitDefaults()
        {
        }

        public long? sr_PUCCH_ResourceIndexP1_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SchedulingRequestConfig_v1020 Decode(BitArrayInputStream input)
            {
                SchedulingRequestConfig_v1020 _v = new SchedulingRequestConfig_v1020();
                _v.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    _v.sr_PUCCH_ResourceIndexP1_r10 = input.readBits(11);
                }
                return _v;
            }
        }
    }

}
