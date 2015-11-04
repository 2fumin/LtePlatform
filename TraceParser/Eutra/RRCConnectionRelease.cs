using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class RRCConnectionRelease
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

                public RRCConnectionRelease_r8_IEs rrcConnectionRelease_r8 { get; set; }

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
                                type.rrcConnectionRelease_r8 = RRCConnectionRelease_r8_IEs.PerDecoder.Instance.Decode(input);
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

            public RRCConnectionRelease Decode(BitArrayInputStream input)
            {
                RRCConnectionRelease release = new RRCConnectionRelease();
                release.InitDefaults();
                release.rrc_TransactionIdentifier = input.readBits(2);
                release.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return release;
            }
        }
    }

    [Serializable]
    public class RRCConnectionRelease_r8_IEs
    {
        public void InitDefaults()
        {
        }

        public IdleModeMobilityControlInfo idleModeMobilityControlInfo { get; set; }

        public RRCConnectionRelease_v890_IEs nonCriticalExtension { get; set; }

        public RedirectedCarrierInfo redirectedCarrierInfo { get; set; }

        public ReleaseCause releaseCause { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RRCConnectionRelease_r8_IEs Decode(BitArrayInputStream input)
            {
                RRCConnectionRelease_r8_IEs es = new RRCConnectionRelease_r8_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 3);
                const int nBits = 2;
                es.releaseCause = (ReleaseCause)input.readBits(nBits);
                if (stream.Read())
                {
                    es.redirectedCarrierInfo = RedirectedCarrierInfo.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.idleModeMobilityControlInfo = IdleModeMobilityControlInfo.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = RRCConnectionRelease_v890_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class RRCConnectionRelease_v1020_IEs
    {
        public void InitDefaults()
        {
        }

        public long? extendedWaitTime_r10 { get; set; }

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

            public RRCConnectionRelease_v1020_IEs Decode(BitArrayInputStream input)
            {
                RRCConnectionRelease_v1020_IEs es = new RRCConnectionRelease_v1020_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.extendedWaitTime_r10 = input.readBits(11) + 1;
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
    public class RRCConnectionRelease_v890_IEs
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public RRCConnectionRelease_v920_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RRCConnectionRelease_v890_IEs Decode(BitArrayInputStream input)
            {
                RRCConnectionRelease_v890_IEs es = new RRCConnectionRelease_v890_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    es.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = RRCConnectionRelease_v920_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class RRCConnectionRelease_v920_IEs
    {
        public void InitDefaults()
        {
        }

        public cellInfoList_r9_Type cellInfoList_r9 { get; set; }

        public RRCConnectionRelease_v1020_IEs nonCriticalExtension { get; set; }

        [Serializable]
        public class cellInfoList_r9_Type
        {
            public void InitDefaults()
            {
            }

            public List<CellInfoGERAN_r9> geran_r9 { get; set; }

            public List<CellInfoUTRA_FDD_r9> utra_FDD_r9 { get; set; }

            public List<CellInfoUTRA_TDD_r10> utra_TDD_r10 { get; set; }

            public List<CellInfoUTRA_TDD_r9> utra_TDD_r9 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public cellInfoList_r9_Type Decode(BitArrayInputStream input)
                {
                    int num2;
                    cellInfoList_r9_Type type = new cellInfoList_r9_Type();
                    type.InitDefaults();
                    bool flag = input.readBit() != 0;
                    switch (input.readBits(2))
                    {
                        case 0:
                            {
                                type.geran_r9 = new List<CellInfoGERAN_r9>();
                                num2 = 5;
                                int num4 = input.readBits(num2) + 1;
                                for (int i = 0; i < num4; i++)
                                {
                                    CellInfoGERAN_r9 item = CellInfoGERAN_r9.PerDecoder.Instance.Decode(input);
                                    type.geran_r9.Add(item);
                                }
                                return type;
                            }
                        case 1:
                            {
                                type.utra_FDD_r9 = new List<CellInfoUTRA_FDD_r9>();
                                num2 = 4;
                                int num6 = input.readBits(num2) + 1;
                                for (int j = 0; j < num6; j++)
                                {
                                    CellInfoUTRA_FDD_r9 _r2 = CellInfoUTRA_FDD_r9.PerDecoder.Instance.Decode(input);
                                    type.utra_FDD_r9.Add(_r2);
                                }
                                return type;
                            }
                        case 2:
                            {
                                type.utra_TDD_r9 = new List<CellInfoUTRA_TDD_r9>();
                                num2 = 4;
                                int num8 = input.readBits(num2) + 1;
                                for (int k = 0; k < num8; k++)
                                {
                                    CellInfoUTRA_TDD_r9 _r3 = CellInfoUTRA_TDD_r9.PerDecoder.Instance.Decode(input);
                                    type.utra_TDD_r9.Add(_r3);
                                }
                                return type;
                            }
                        case 3:
                            if (flag)
                            {
                                type.utra_TDD_r10 = new List<CellInfoUTRA_TDD_r10>();
                                num2 = 4;
                                int num10 = input.readBits(num2) + 1;
                                for (int m = 0; m < num10; m++)
                                {
                                    CellInfoUTRA_TDD_r10 _r4 = CellInfoUTRA_TDD_r10.PerDecoder.Instance.Decode(input);
                                    type.utra_TDD_r10.Add(_r4);
                                }
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

            public RRCConnectionRelease_v920_IEs Decode(BitArrayInputStream input)
            {
                RRCConnectionRelease_v920_IEs es = new RRCConnectionRelease_v920_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.cellInfoList_r9 = cellInfoList_r9_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = RRCConnectionRelease_v1020_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class RRCConnectionRelease_v9e0_IEs
    {
        public void InitDefaults()
        {
        }

        public IdleModeMobilityControlInfo_v9e0 idleModeMobilityControlInfo_v9e0 { get; set; }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        public RedirectedCarrierInfo_v9e0 redirectedCarrierInfo_v9e0 { get; set; }

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

            public RRCConnectionRelease_v9e0_IEs Decode(BitArrayInputStream input)
            {
                RRCConnectionRelease_v9e0_IEs es = new RRCConnectionRelease_v9e0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    es.redirectedCarrierInfo_v9e0 = RedirectedCarrierInfo_v9e0.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.idleModeMobilityControlInfo_v9e0 = IdleModeMobilityControlInfo_v9e0.PerDecoder.Instance.Decode(input);
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
