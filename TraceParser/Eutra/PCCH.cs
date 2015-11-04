using System;
using Lte.Domain.Common;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class PCCH_Config
    {
        public void InitDefaults()
        {
        }

        public defaultPagingCycle_Enum defaultPagingCycle { get; set; }

        public nB_Enum nB { get; set; }

        public enum defaultPagingCycle_Enum
        {
            rf32,
            rf64,
            rf128,
            rf256
        }

        public enum nB_Enum
        {
            fourT,
            twoT,
            oneT,
            halfT,
            quarterT,
            oneEighthT,
            oneSixteenthT,
            oneThirtySecondT
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PCCH_Config Decode(BitArrayInputStream input)
            {
                PCCH_Config config = new PCCH_Config();
                config.InitDefaults();
                int nBits = 2;
                config.defaultPagingCycle = (defaultPagingCycle_Enum)input.readBits(nBits);
                nBits = 3;
                config.nB = (nB_Enum)input.readBits(nBits);
                return config;
            }
        }
    }

    [Serializable]
    public class PCCH_Message : ITraceMessage
    {
        public void InitDefaults()
        {
        }

        public PCCH_MessageType message { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PCCH_Message Decode(BitArrayInputStream input)
            {
                PCCH_Message message = new PCCH_Message();
                message.InitDefaults();
                message.message = PCCH_MessageType.PerDecoder.Instance.Decode(input);
                return message;
            }
        }
    }

    [Serializable]
    public class PCCH_MessageType
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

            public Paging paging { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public c1_Type Decode(BitArrayInputStream input)
                {
                    c1_Type type = new c1_Type();
                    type.InitDefaults();
                    if (input.readBits(1) != 0)
                    {
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                    type.paging = Paging.PerDecoder.Instance.Decode(input);
                    return type;
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

            public PCCH_MessageType Decode(BitArrayInputStream input)
            {
                PCCH_MessageType type = new PCCH_MessageType();
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
