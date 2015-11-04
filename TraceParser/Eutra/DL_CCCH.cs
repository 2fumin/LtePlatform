using System;
using Lte.Domain.Common;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class DL_CCCH_Message : ITraceMessage
    {
        public void InitDefaults()
        {
        }

        public DL_CCCH_MessageType message { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public DL_CCCH_Message Decode(BitArrayInputStream input)
            {
                DL_CCCH_Message message = new DL_CCCH_Message();
                message.InitDefaults();
                message.message = DL_CCCH_MessageType.PerDecoder.Instance.Decode(input);
                return message;
            }
        }
    }

    [Serializable]
    public class DL_CCCH_MessageType
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

            public RRCConnectionReestablishment rrcConnectionReestablishment { get; set; }

            public RRCConnectionReestablishmentReject rrcConnectionReestablishmentReject { get; set; }

            public RRCConnectionReject rrcConnectionReject { get; set; }

            public RRCConnectionSetup rrcConnectionSetup { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public c1_Type Decode(BitArrayInputStream input)
                {
                    c1_Type type = new c1_Type();
                    type.InitDefaults();
                    switch (input.readBits(2))
                    {
                        case 0:
                            type.rrcConnectionReestablishment 
                                = RRCConnectionReestablishment.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.rrcConnectionReestablishmentReject 
                                = RRCConnectionReestablishmentReject.PerDecoder.Instance.Decode(input);
                            return type;

                        case 2:
                            type.rrcConnectionReject = RRCConnectionReject.PerDecoder.Instance.Decode(input);
                            return type;

                        case 3:
                            type.rrcConnectionSetup = RRCConnectionSetup.PerDecoder.Instance.Decode(input);
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

            public DL_CCCH_MessageType Decode(BitArrayInputStream input)
            {
                DL_CCCH_MessageType type = new DL_CCCH_MessageType();
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
