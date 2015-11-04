using System;
using Lte.Domain.Common;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class BCCH_BCH_Message : ITraceMessage
    {
        public void InitDefaults()
        {
        }

        public MasterInformationBlock message { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public BCCH_BCH_Message Decode(BitArrayInputStream input)
            {
                BCCH_BCH_Message message = new BCCH_BCH_Message();
                message.InitDefaults();
                message.message = MasterInformationBlock.PerDecoder.Instance.Decode(input);
                return message;
            }
        }
    }

    [Serializable]
    public class BCCH_Config
    {
        public void InitDefaults()
        {
        }

        public modificationPeriodCoeff_Enum modificationPeriodCoeff { get; set; }

        public enum modificationPeriodCoeff_Enum
        {
            n2,
            n4,
            n8,
            n16
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public BCCH_Config Decode(BitArrayInputStream input)
            {
                BCCH_Config config = new BCCH_Config();
                config.InitDefaults();
                int nBits = 2;
                config.modificationPeriodCoeff = (modificationPeriodCoeff_Enum)input.readBits(nBits);
                return config;
            }
        }
    }

    [Serializable]
    public class BCCH_DL_SCH_Message : ITraceMessage
    {
        public void InitDefaults()
        {
        }

        public BCCH_DL_SCH_MessageType message { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public BCCH_DL_SCH_Message Decode(BitArrayInputStream input)
            {
                BCCH_DL_SCH_Message message = new BCCH_DL_SCH_Message();
                message.InitDefaults();
                message.message = BCCH_DL_SCH_MessageType.PerDecoder.Instance.Decode(input);
                return message;
            }
        }
    }

    [Serializable]
    public class BCCH_DL_SCH_MessageType
    {
        public void InitDefaults()
        {
        }

        public c1_Type c1 { get; set; }

        public messageClassExtension_Type messageClassExtension { get; set; }

        [Serializable]
        public class c1_Type
        {
            public void InitDefaults()
            {
            }

            public SystemInformation systemInformation { get; set; }

            public SystemInformationBlockType1 systemInformationBlockType1 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public c1_Type Decode(BitArrayInputStream input)
                {
                    c1_Type type = new c1_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.systemInformation = SystemInformation.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.systemInformationBlockType1 = SystemInformationBlockType1.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        [Serializable]
        public class messageClassExtension_Type
        {
            public void InitDefaults()
            {
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public messageClassExtension_Type Decode(BitArrayInputStream input)
                {
                    messageClassExtension_Type type = new messageClassExtension_Type();
                    type.InitDefaults();
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public BCCH_DL_SCH_MessageType Decode(BitArrayInputStream input)
            {
                BCCH_DL_SCH_MessageType type = new BCCH_DL_SCH_MessageType();
                type.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        type.c1 = c1_Type.PerDecoder.Instance.Decode(input);
                        return type;

                    case 1:
                        type.messageClassExtension = messageClassExtension_Type.PerDecoder.Instance.Decode(input);
                        return type;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

}
