using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class ProximityIndication_r9
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

                public ProximityIndication_r9_IEs proximityIndication_r9 { get; set; }

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
                                type.proximityIndication_r9 = ProximityIndication_r9_IEs.PerDecoder.Instance.Decode(input);
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

            public ProximityIndication_r9 Decode(BitArrayInputStream input)
            {
                ProximityIndication_r9 _r = new ProximityIndication_r9();
                _r.InitDefaults();
                _r.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return _r;
            }
        }
    }

    [Serializable]
    public class ProximityIndication_r9_IEs
    {
        public void InitDefaults()
        {
        }

        public carrierFreq_r9_Type carrierFreq_r9 { get; set; }

        public ProximityIndication_v930_IEs nonCriticalExtension { get; set; }

        public type_r9_Enum type_r9 { get; set; }

        [Serializable]
        public class carrierFreq_r9_Type
        {
            public void InitDefaults()
            {
            }

            public long eutra_r9 { get; set; }

            public long? eutra2_v9e0 { get; set; }

            public long utra_r9 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public carrierFreq_r9_Type Decode(BitArrayInputStream input)
                {
                    carrierFreq_r9_Type type = new carrierFreq_r9_Type();
                    type.InitDefaults();
                    bool flag = input.readBit() != 0;
                    switch (input.readBits(2))
                    {
                        case 0:
                            type.eutra_r9 = input.readBits(0x10);
                            return type;

                        case 1:
                            type.utra_r9 = input.readBits(14);
                            return type;

                        case 2:
                            if (flag)
                            {
                                type.eutra2_v9e0 = input.readBits(0x12) + 0x10000;
                            }
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ProximityIndication_r9_IEs Decode(BitArrayInputStream input)
            {
                ProximityIndication_r9_IEs es = new ProximityIndication_r9_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                const int nBits = 1;
                es.type_r9 = (type_r9_Enum)input.readBits(nBits);
                es.carrierFreq_r9 = carrierFreq_r9_Type.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    es.nonCriticalExtension = ProximityIndication_v930_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }

        public enum type_r9_Enum
        {
            entering,
            leaving
        }
    }

    [Serializable]
    public class ProximityIndication_v930_IEs
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

            public ProximityIndication_v930_IEs Decode(BitArrayInputStream input)
            {
                ProximityIndication_v930_IEs es = new ProximityIndication_v930_IEs();
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
