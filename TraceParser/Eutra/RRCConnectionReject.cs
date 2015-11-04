using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class RRCConnectionReject
    {
        public void InitDefaults()
        {
        }

        public criticalExtensions_Type criticalExtensions { get; set; }

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

                public RRCConnectionReject_r8_IEs rrcConnectionReject_r8 { get; set; }

                public object spare1 { get; set; }

                public object spare2 { get; set; }

                public object spare3 { get; set; }

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
                                type.rrcConnectionReject_r8 = RRCConnectionReject_r8_IEs.PerDecoder.Instance.Decode(input);
                                return type;

                            case 1:
                                return type;

                            case 2:
                                return type;

                            case 3:
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

            public RRCConnectionReject Decode(BitArrayInputStream input)
            {
                RRCConnectionReject reject = new RRCConnectionReject();
                reject.InitDefaults();
                reject.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return reject;
            }
        }
    }

    [Serializable]
    public class RRCConnectionReject_r8_IEs
    {
        public void InitDefaults()
        {
        }

        public RRCConnectionReject_v8a0_IEs nonCriticalExtension { get; set; }

        public long waitTime { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RRCConnectionReject_r8_IEs Decode(BitArrayInputStream input)
            {
                RRCConnectionReject_r8_IEs es = new RRCConnectionReject_r8_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                es.waitTime = input.readBits(4) + 1;
                if (stream.Read())
                {
                    es.nonCriticalExtension = RRCConnectionReject_v8a0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class RRCConnectionReject_v1020_IEs
    {
        public void InitDefaults()
        {
        }

        public long? extendedWaitTime_r10 { get; set; }

        public RRCConnectionReject_v1130_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RRCConnectionReject_v1020_IEs Decode(BitArrayInputStream input)
            {
                RRCConnectionReject_v1020_IEs es = new RRCConnectionReject_v1020_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.extendedWaitTime_r10 = input.readBits(11) + 1;
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = RRCConnectionReject_v1130_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class RRCConnectionReject_v1130_IEs
    {
        public void InitDefaults()
        {
        }

        public deprioritisationReq_r11_Type deprioritisationReq_r11 { get; set; }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        [Serializable]
        public class deprioritisationReq_r11_Type
        {
            public void InitDefaults()
            {
            }

            public deprioritisationTimer_r11_Enum deprioritisationTimer_r11 { get; set; }

            public deprioritisationType_r11_Enum deprioritisationType_r11 { get; set; }

            public enum deprioritisationTimer_r11_Enum
            {
                min5,
                min10,
                min15,
                min30
            }

            public enum deprioritisationType_r11_Enum
            {
                frequency,
                e_utra
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public deprioritisationReq_r11_Type Decode(BitArrayInputStream input)
                {
                    deprioritisationReq_r11_Type type = new deprioritisationReq_r11_Type();
                    type.InitDefaults();
                    int nBits = 1;
                    type.deprioritisationType_r11 = (deprioritisationType_r11_Enum)input.readBits(nBits);
                    nBits = 2;
                    type.deprioritisationTimer_r11 = (deprioritisationTimer_r11_Enum)input.readBits(nBits);
                    return type;
                }
            }
        }

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

            public RRCConnectionReject_v1130_IEs Decode(BitArrayInputStream input)
            {
                RRCConnectionReject_v1130_IEs es = new RRCConnectionReject_v1130_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.deprioritisationReq_r11 = deprioritisationReq_r11_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = nonCriticalExtension_Type.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class RRCConnectionReject_v8a0_IEs
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public RRCConnectionReject_v1020_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RRCConnectionReject_v8a0_IEs Decode(BitArrayInputStream input)
            {
                RRCConnectionReject_v8a0_IEs es = new RRCConnectionReject_v8a0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    es.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = RRCConnectionReject_v1020_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

}
