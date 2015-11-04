using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CounterCheck
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

                public CounterCheck_r8_IEs counterCheck_r8 { get; set; }

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
                                type.counterCheck_r8 = CounterCheck_r8_IEs.PerDecoder.Instance.Decode(input);
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

            public CounterCheck Decode(BitArrayInputStream input)
            {
                CounterCheck check = new CounterCheck();
                check.InitDefaults();
                check.rrc_TransactionIdentifier = input.readBits(2);
                check.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return check;
            }
        }
    }

    [Serializable]
    public class CounterCheck_r8_IEs
    {
        public void InitDefaults()
        {
        }

        public List<DRB_CountMSB_Info> drb_CountMSB_InfoList { get; set; }

        public CounterCheck_v8a0_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CounterCheck_r8_IEs Decode(BitArrayInputStream input)
            {
                CounterCheck_r8_IEs es = new CounterCheck_r8_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                es.drb_CountMSB_InfoList = new List<DRB_CountMSB_Info>();
                const int nBits = 4;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    DRB_CountMSB_Info item = DRB_CountMSB_Info.PerDecoder.Instance.Decode(input);
                    es.drb_CountMSB_InfoList.Add(item);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = CounterCheck_v8a0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class CounterCheck_v8a0_IEs
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

            public CounterCheck_v8a0_IEs Decode(BitArrayInputStream input)
            {
                CounterCheck_v8a0_IEs es = new CounterCheck_v8a0_IEs();
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

    [Serializable]
    public class CounterCheckResponse
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

            public CounterCheckResponse_r8_IEs counterCheckResponse_r8 { get; set; }

            public criticalExtensionsFuture_Type criticalExtensionsFuture { get; set; }

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
                            type.counterCheckResponse_r8 = CounterCheckResponse_r8_IEs.PerDecoder.Instance.Decode(input);
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

            public CounterCheckResponse Decode(BitArrayInputStream input)
            {
                CounterCheckResponse response = new CounterCheckResponse();
                response.InitDefaults();
                response.rrc_TransactionIdentifier = input.readBits(2);
                response.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return response;
            }
        }
    }

    [Serializable]
    public class CounterCheckResponse_r8_IEs
    {
        public void InitDefaults()
        {
        }

        public List<DRB_CountInfo> drb_CountInfoList { get; set; }

        public CounterCheckResponse_v8a0_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CounterCheckResponse_r8_IEs Decode(BitArrayInputStream input)
            {
                CounterCheckResponse_r8_IEs es = new CounterCheckResponse_r8_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                es.drb_CountInfoList = new List<DRB_CountInfo>();
                const int nBits = 4;
                int num3 = input.readBits(nBits);
                for (int i = 0; i < num3; i++)
                {
                    DRB_CountInfo item = DRB_CountInfo.PerDecoder.Instance.Decode(input);
                    es.drb_CountInfoList.Add(item);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = CounterCheckResponse_v8a0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class CounterCheckResponse_v8a0_IEs
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

            public CounterCheckResponse_v8a0_IEs Decode(BitArrayInputStream input)
            {
                CounterCheckResponse_v8a0_IEs es = new CounterCheckResponse_v8a0_IEs();
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
