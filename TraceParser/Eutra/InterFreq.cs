using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class InterFreqBandInfo
    {
        public void InitDefaults()
        {
        }

        public bool interFreqNeedForGaps { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public InterFreqBandInfo Decode(BitArrayInputStream input)
            {
                InterFreqBandInfo info = new InterFreqBandInfo();
                info.InitDefaults();
                info.interFreqNeedForGaps = input.readBit() == 1;
                return info;
            }
        }
    }

    [Serializable]
    public class InterFreqCarrierFreqInfo
    {
        public void InitDefaults()
        {
            q_OffsetFreq = Q_OffsetRange.dB0;
        }

        public AllowedMeasBandwidth allowedMeasBandwidth { get; set; }

        public long? cellReselectionPriority { get; set; }

        public long dl_CarrierFreq { get; set; }

        public List<PhysCellIdRange> interFreqBlackCellList { get; set; }

        public List<InterFreqNeighCellInfo> interFreqNeighCellList { get; set; }

        public string neighCellConfig { get; set; }

        public long? p_Max { get; set; }

        public bool presenceAntennaPort1 { get; set; }

        public Q_OffsetRange q_OffsetFreq { get; set; }

        public long? q_QualMin_r9 { get; set; }

        public long? q_QualMinWB_r11 { get; set; }

        public long q_RxLevMin { get; set; }

        public long t_ReselectionEUTRA { get; set; }

        public SpeedStateScaleFactors t_ReselectionEUTRA_SF { get; set; }

        public long threshX_High { get; set; }

        public long threshX_Low { get; set; }

        public threshX_Q_r9_Type threshX_Q_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public InterFreqCarrierFreqInfo Decode(BitArrayInputStream input)
            {
                BitMaskStream stream3;
                InterFreqCarrierFreqInfo info = new InterFreqCarrierFreqInfo();
                info.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 1);
                BitMaskStream stream2 = new BitMaskStream(input, 5);
                info.dl_CarrierFreq = input.readBits(0x10);
                info.q_RxLevMin = input.readBits(6) + -70;
                if (stream2.Read())
                {
                    info.p_Max = input.readBits(6) + -30;
                }
                info.t_ReselectionEUTRA = input.readBits(3);
                if (stream2.Read())
                {
                    info.t_ReselectionEUTRA_SF = SpeedStateScaleFactors.PerDecoder.Instance.Decode(input);
                }
                info.threshX_High = input.readBits(5);
                info.threshX_Low = input.readBits(5);
                int nBits = 3;
                info.allowedMeasBandwidth = (AllowedMeasBandwidth)input.readBits(nBits);
                info.presenceAntennaPort1 = input.readBit() == 1;
                if (stream2.Read())
                {
                    info.cellReselectionPriority = input.readBits(3);
                }
                info.neighCellConfig = input.readBitString(2);
                if (stream.Read())
                {
                    nBits = 5;
                    info.q_OffsetFreq = (Q_OffsetRange)input.readBits(nBits);
                }
                if (stream2.Read())
                {
                    info.interFreqNeighCellList = new List<InterFreqNeighCellInfo>();
                    nBits = 4;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        InterFreqNeighCellInfo item = InterFreqNeighCellInfo.PerDecoder.Instance.Decode(input);
                        info.interFreqNeighCellList.Add(item);
                    }
                }
                if (stream2.Read())
                {
                    info.interFreqBlackCellList = new List<PhysCellIdRange>();
                    nBits = 4;
                    int num5 = input.readBits(nBits) + 1;
                    for (int j = 0; j < num5; j++)
                    {
                        PhysCellIdRange range = PhysCellIdRange.PerDecoder.Instance.Decode(input);
                        info.interFreqBlackCellList.Add(range);
                    }
                }
                if (flag)
                {
                    stream3 = new BitMaskStream(input, 2);
                    if (stream3.Read())
                    {
                        info.q_QualMin_r9 = input.readBits(5) + -34;
                    }
                    if (stream3.Read())
                    {
                        info.threshX_Q_r9 = threshX_Q_r9_Type.PerDecoder.Instance.Decode(input);
                    }
                }
                if (flag)
                {
                    stream3 = new BitMaskStream(input, 1);
                    if (stream3.Read())
                    {
                        info.q_QualMinWB_r11 = input.readBits(5) + -34;
                    }
                }
                return info;
            }
        }

        [Serializable]
        public class threshX_Q_r9_Type
        {
            public void InitDefaults()
            {
            }

            public long threshX_HighQ_r9 { get; set; }

            public long threshX_LowQ_r9 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public threshX_Q_r9_Type Decode(BitArrayInputStream input)
                {
                    threshX_Q_r9_Type type = new threshX_Q_r9_Type();
                    type.InitDefaults();
                    type.threshX_HighQ_r9 = input.readBits(5);
                    type.threshX_LowQ_r9 = input.readBits(5);
                    return type;
                }
            }
        }
    }

    [Serializable]
    public class InterFreqCarrierFreqInfo_v8h0
    {
        public void InitDefaults()
        {
        }

        public List<long> multiBandInfoList { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public InterFreqCarrierFreqInfo_v8h0 Decode(BitArrayInputStream input)
            {
                InterFreqCarrierFreqInfo_v8h0 _vh = new InterFreqCarrierFreqInfo_v8h0();
                _vh.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    _vh.multiBandInfoList = new List<long>();
                    int nBits = 3;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        long item = input.readBits(6) + 1;
                        _vh.multiBandInfoList.Add(item);
                    }
                }
                return _vh;
            }
        }
    }

    [Serializable]
    public class InterFreqCarrierFreqInfo_v9e0
    {
        public void InitDefaults()
        {
        }

        public long? dl_CarrierFreq_v9e0 { get; set; }

        public List<MultiBandInfo_v9e0> multiBandInfoList_v9e0 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public InterFreqCarrierFreqInfo_v9e0 Decode(BitArrayInputStream input)
            {
                InterFreqCarrierFreqInfo_v9e0 _ve = new InterFreqCarrierFreqInfo_v9e0();
                _ve.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    _ve.dl_CarrierFreq_v9e0 = input.readBits(0x12) + 0x10000;
                }
                if (stream.Read())
                {
                    _ve.multiBandInfoList_v9e0 = new List<MultiBandInfo_v9e0>();
                    int nBits = 3;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        MultiBandInfo_v9e0 item = MultiBandInfo_v9e0.PerDecoder.Instance.Decode(input);
                        _ve.multiBandInfoList_v9e0.Add(item);
                    }
                }
                return _ve;
            }
        }
    }

    [Serializable]
    public class InterFreqNeighCellInfo
    {
        public void InitDefaults()
        {
        }

        public long physCellId { get; set; }

        public Q_OffsetRange q_OffsetCell { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public InterFreqNeighCellInfo Decode(BitArrayInputStream input)
            {
                InterFreqNeighCellInfo info = new InterFreqNeighCellInfo();
                info.InitDefaults();
                info.physCellId = input.readBits(9);
                int nBits = 5;
                info.q_OffsetCell = (Q_OffsetRange)input.readBits(nBits);
                return info;
            }
        }
    }

    [Serializable]
    public class InterFreqRSTDMeasurementIndication_r10
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

                public InterFreqRSTDMeasurementIndication_r10_IEs interFreqRSTDMeasurementIndication_r10 { get; set; }

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
                                type.interFreqRSTDMeasurementIndication_r10 = InterFreqRSTDMeasurementIndication_r10_IEs.PerDecoder.Instance.Decode(input);
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

            public InterFreqRSTDMeasurementIndication_r10 Decode(BitArrayInputStream input)
            {
                InterFreqRSTDMeasurementIndication_r10 _r = new InterFreqRSTDMeasurementIndication_r10();
                _r.InitDefaults();
                _r.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return _r;
            }
        }
    }

    [Serializable]
    public class InterFreqRSTDMeasurementIndication_r10_IEs
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        public rstd_InterFreqIndication_r10_Type rstd_InterFreqIndication_r10 { get; set; }

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

            public InterFreqRSTDMeasurementIndication_r10_IEs Decode(BitArrayInputStream input)
            {
                InterFreqRSTDMeasurementIndication_r10_IEs es = new InterFreqRSTDMeasurementIndication_r10_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                es.rstd_InterFreqIndication_r10 = rstd_InterFreqIndication_r10_Type.PerDecoder.Instance.Decode(input);
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

        [Serializable]
        public class rstd_InterFreqIndication_r10_Type
        {
            public void InitDefaults()
            {
            }

            public start_Type start { get; set; }

            public object stop { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public rstd_InterFreqIndication_r10_Type Decode(BitArrayInputStream input)
                {
                    rstd_InterFreqIndication_r10_Type type = new rstd_InterFreqIndication_r10_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.start = start_Type.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }

            [Serializable]
            public class start_Type
            {
                public void InitDefaults()
                {
                }

                public List<RSTD_InterFreqInfo_r10> rstd_InterFreqInfoList_r10 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public start_Type Decode(BitArrayInputStream input)
                    {
                        start_Type type = new start_Type();
                        type.InitDefaults();
                        type.rstd_InterFreqInfoList_r10 = new List<RSTD_InterFreqInfo_r10>();
                        int nBits = 2;
                        int num3 = input.readBits(nBits) + 1;
                        for (int i = 0; i < num3; i++)
                        {
                            RSTD_InterFreqInfo_r10 item = RSTD_InterFreqInfo_r10.PerDecoder.Instance.Decode(input);
                            type.rstd_InterFreqInfoList_r10.Add(item);
                        }
                        return type;
                    }
                }
            }
        }
    }

}
