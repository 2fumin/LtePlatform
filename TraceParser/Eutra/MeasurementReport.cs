using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class MeasurementReport
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

                public MeasurementReport_r8_IEs measurementReport_r8 { get; set; }

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
                                type.measurementReport_r8 = MeasurementReport_r8_IEs.PerDecoder.Instance.Decode(input);
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

            public MeasurementReport Decode(BitArrayInputStream input)
            {
                MeasurementReport report = new MeasurementReport();
                report.InitDefaults();
                report.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return report;
            }
        }
    }

    [Serializable]
    public class MeasurementReport_r8_IEs
    {
        public void InitDefaults()
        {
        }

        public MeasResults measResults { get; set; }

        public MeasurementReport_v8a0_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasurementReport_r8_IEs Decode(BitArrayInputStream input)
            {
                MeasurementReport_r8_IEs es = new MeasurementReport_r8_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                es.measResults = MeasResults.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    es.nonCriticalExtension = MeasurementReport_v8a0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }


    [Serializable]
    public class MeasurementReport_v8a0_IEs
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

            public MeasurementReport_v8a0_IEs Decode(BitArrayInputStream input)
            {
                MeasurementReport_v8a0_IEs es = new MeasurementReport_v8a0_IEs();
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
    public class LogMeasReport_r10
    {
        public void InitDefaults()
        {
        }

        public string absoluteTimeStamp_r10 { get; set; }

        public logMeasAvailable_r10_Enum? logMeasAvailable_r10 { get; set; }

        public List<LogMeasInfo_r10> logMeasInfoList_r10 { get; set; }

        public string tce_Id_r10 { get; set; }

        public string traceRecordingSessionRef_r10 { get; set; }

        public TraceReference_r10 traceReference_r10 { get; set; }

        public enum logMeasAvailable_r10_Enum
        {
            _true
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public LogMeasReport_r10 Decode(BitArrayInputStream input)
            {
                LogMeasReport_r10 _r = new LogMeasReport_r10();
                _r.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                _r.absoluteTimeStamp_r10 = input.readBitString(0x30);
                _r.traceReference_r10 = TraceReference_r10.PerDecoder.Instance.Decode(input);
                _r.traceRecordingSessionRef_r10 = input.readOctetString(2);
                _r.tce_Id_r10 = input.readOctetString(1);
                _r.logMeasInfoList_r10 = new List<LogMeasInfo_r10>();
                int nBits = 10;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    LogMeasInfo_r10 item = LogMeasInfo_r10.PerDecoder.Instance.Decode(input);
                    _r.logMeasInfoList_r10.Add(item);
                }
                if (stream.Read())
                {
                    nBits = 1;
                    _r.logMeasAvailable_r10 = (logMeasAvailable_r10_Enum)input.readBits(nBits);
                }
                return _r;
            }
        }
    }

    [Serializable]
    public class LogMeasInfo_r10
    {
        public void InitDefaults()
        {
        }

        public LocationInfo_r10 locationInfo_r10 { get; set; }

        public List<MeasResult2EUTRA_v9e0> measResultListEUTRA_v1090 { get; set; }

        public measResultNeighCells_r10_Type measResultNeighCells_r10 { get; set; }

        public measResultServCell_r10_Type measResultServCell_r10 { get; set; }

        public long relativeTimeStamp_r10 { get; set; }

        public CellGlobalIdEUTRA servCellIdentity_r10 { get; set; }

        [Serializable]
        public class measResultNeighCells_r10_Type
        {
            public void InitDefaults()
            {
            }

            public List<MeasResult2CDMA2000_r9> measResultListCDMA2000_r10 { get; set; }

            public List<MeasResult2EUTRA_r9> measResultListEUTRA_r10 { get; set; }

            public List<List<MeasResultGERAN>> measResultListGERAN_r10 { get; set; }

            public List<MeasResult2UTRA_r9> measResultListUTRA_r10 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public measResultNeighCells_r10_Type Decode(BitArrayInputStream input)
                {
                    int num2;
                    measResultNeighCells_r10_Type type = new measResultNeighCells_r10_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 4);
                    if (stream.Read())
                    {
                        type.measResultListEUTRA_r10 = new List<MeasResult2EUTRA_r9>();
                        num2 = 3;
                        int num3 = input.readBits(num2) + 1;
                        for (int i = 0; i < num3; i++)
                        {
                            MeasResult2EUTRA_r9 item = MeasResult2EUTRA_r9.PerDecoder.Instance.Decode(input);
                            type.measResultListEUTRA_r10.Add(item);
                        }
                    }
                    if (stream.Read())
                    {
                        type.measResultListUTRA_r10 = new List<MeasResult2UTRA_r9>();
                        num2 = 3;
                        int num5 = input.readBits(num2) + 1;
                        for (int j = 0; j < num5; j++)
                        {
                            MeasResult2UTRA_r9 _r2 = MeasResult2UTRA_r9.PerDecoder.Instance.Decode(input);
                            type.measResultListUTRA_r10.Add(_r2);
                        }
                    }
                    if (stream.Read())
                    {
                        type.measResultListGERAN_r10 = new List<List<MeasResultGERAN>>();
                        num2 = 2;
                        int num7 = input.readBits(num2) + 1;
                        for (int k = 0; k < num7; k++)
                        {
                            List<MeasResultGERAN> list = new List<MeasResultGERAN>();
                            num2 = 3;
                            int num9 = input.readBits(num2) + 1;
                            for (int m = 0; m < num9; m++)
                            {
                                MeasResultGERAN tgeran = MeasResultGERAN.PerDecoder.Instance.Decode(input);
                                list.Add(tgeran);
                            }
                            type.measResultListGERAN_r10.Add(list);
                        }
                    }
                    if (stream.Read())
                    {
                        type.measResultListCDMA2000_r10 = new List<MeasResult2CDMA2000_r9>();
                        num2 = 3;
                        int num11 = input.readBits(num2) + 1;
                        for (int n = 0; n < num11; n++)
                        {
                            MeasResult2CDMA2000_r9 _r3 = MeasResult2CDMA2000_r9.PerDecoder.Instance.Decode(input);
                            type.measResultListCDMA2000_r10.Add(_r3);
                        }
                    }
                    return type;
                }
            }
        }

        [Serializable]
        public class measResultServCell_r10_Type
        {
            public void InitDefaults()
            {
            }

            public long rsrpResult_r10 { get; set; }

            public long rsrqResult_r10 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public measResultServCell_r10_Type Decode(BitArrayInputStream input)
                {
                    measResultServCell_r10_Type type = new measResultServCell_r10_Type();
                    type.InitDefaults();
                    type.rsrpResult_r10 = input.readBits(7);
                    type.rsrqResult_r10 = input.readBits(6);
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public LogMeasInfo_r10 Decode(BitArrayInputStream input)
            {
                LogMeasInfo_r10 _r = new LogMeasInfo_r10();
                _r.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    _r.locationInfo_r10 = LocationInfo_r10.PerDecoder.Instance.Decode(input);
                }
                _r.relativeTimeStamp_r10 = input.readBits(13);
                _r.servCellIdentity_r10 = CellGlobalIdEUTRA.PerDecoder.Instance.Decode(input);
                _r.measResultServCell_r10 = measResultServCell_r10_Type.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    _r.measResultNeighCells_r10 = measResultNeighCells_r10_Type.PerDecoder.Instance.Decode(input);
                }
                if (flag)
                {
                    BitMaskStream stream2 = new BitMaskStream(input, 1);
                    if (!stream2.Read())
                    {
                        return _r;
                    }
                    _r.measResultListEUTRA_v1090 = new List<MeasResult2EUTRA_v9e0>();
                    int nBits = 3;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        MeasResult2EUTRA_v9e0 item = MeasResult2EUTRA_v9e0.PerDecoder.Instance.Decode(input);
                        _r.measResultListEUTRA_v1090.Add(item);
                    }
                }
                return _r;
            }
        }
    }

}
