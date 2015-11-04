using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class RRCConnectionReestablishment
    {
        public void InitDefaults()
        {
        }

        public criticalExtensions_Type criticalExtensions { get; set; }

        public long rrc_TransactionIdentifier { get; set; }

        [Serializable]
        public class criticalExtensions_Type
        {
            public void InitDefaults()
            {
            }

            public c1_Type c1 { get; set; }

            public criticalExtensionsFuture_Type criticalExtensionsFuture { get; set; }

            [Serializable]
            public class c1_Type
            {
                public void InitDefaults()
                {
                }

                public RRCConnectionReestablishment_r8_IEs rrcConnectionReestablishment_r8 { get; set; }

                public object spare1 { get; set; }

                public object spare2 { get; set; }

                public object spare3 { get; set; }

                public object spare4 { get; set; }

                public object spare5 { get; set; }

                public object spare6 { get; set; }

                public object spare7 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public c1_Type Decode(BitArrayInputStream input)
                    {
                        c1_Type type = new c1_Type();
                        type.InitDefaults();
                        switch (input.readBits(3))
                        {
                            case 0:
                                type.rrcConnectionReestablishment_r8 = RRCConnectionReestablishment_r8_IEs.PerDecoder.Instance.Decode(input);
                                return type;

                            case 1:
                                return type;

                            case 2:
                                return type;

                            case 3:
                                return type;

                            case 4:
                                return type;

                            case 5:
                                return type;

                            case 6:
                                return type;

                            case 7:
                                return type;
                        }
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                }
            }

            [Serializable]
            public class criticalExtensionsFuture_Type
            {
                public void InitDefaults()
                {
                }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public criticalExtensionsFuture_Type Decode(BitArrayInputStream input)
                    {
                        criticalExtensionsFuture_Type type = new criticalExtensionsFuture_Type();
                        type.InitDefaults();
                        return type;
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public criticalExtensions_Type Decode(BitArrayInputStream input)
                {
                    criticalExtensions_Type type = new criticalExtensions_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.c1 = c1_Type.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.criticalExtensionsFuture = criticalExtensionsFuture_Type.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RRCConnectionReestablishment Decode(BitArrayInputStream input)
            {
                RRCConnectionReestablishment reestablishment = new RRCConnectionReestablishment();
                reestablishment.InitDefaults();
                reestablishment.rrc_TransactionIdentifier = input.readBits(2);
                reestablishment.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return reestablishment;
            }
        }
    }

    [Serializable]
    public class RRCConnectionReestablishment_r8_IEs
    {
        public void InitDefaults()
        {
        }

        public long nextHopChainingCount { get; set; }

        public RRCConnectionReestablishment_v8a0_IEs nonCriticalExtension { get; set; }

        public RadioResourceConfigDedicated radioResourceConfigDedicated { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RRCConnectionReestablishment_r8_IEs Decode(BitArrayInputStream input)
            {
                RRCConnectionReestablishment_r8_IEs es = new RRCConnectionReestablishment_r8_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                es.radioResourceConfigDedicated = RadioResourceConfigDedicated.PerDecoder.Instance.Decode(input);
                es.nextHopChainingCount = input.readBits(3);
                if (stream.Read())
                {
                    es.nonCriticalExtension = RRCConnectionReestablishment_v8a0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class RRCConnectionReestablishment_v8a0_IEs
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        [Serializable]
        public class nonCriticalExtension_Type
        {
            public void InitDefaults()
            {
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public nonCriticalExtension_Type Decode(BitArrayInputStream input)
                {
                    nonCriticalExtension_Type type = new nonCriticalExtension_Type();
                    type.InitDefaults();
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RRCConnectionReestablishment_v8a0_IEs Decode(BitArrayInputStream input)
            {
                RRCConnectionReestablishment_v8a0_IEs es = new RRCConnectionReestablishment_v8a0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    es.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = nonCriticalExtension_Type.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

}
