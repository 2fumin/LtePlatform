using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class UEInformationRequest_r9
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

                public object spare1 { get; set; }

                public object spare2 { get; set; }

                public object spare3 { get; set; }

                public UEInformationRequest_r9_IEs ueInformationRequest_r9 { get; set; }

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
                                type.ueInformationRequest_r9 = UEInformationRequest_r9_IEs.PerDecoder.Instance.Decode(input);
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

            public UEInformationRequest_r9 Decode(BitArrayInputStream input)
            {
                UEInformationRequest_r9 _r = new UEInformationRequest_r9();
                _r.InitDefaults();
                _r.rrc_TransactionIdentifier = input.readBits(2);
                _r.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return _r;
            }
        }
    }

    [Serializable]
    public class UEInformationRequest_r9_IEs
    {
        public void InitDefaults()
        {
        }

        public UEInformationRequest_v930_IEs nonCriticalExtension { get; set; }

        public bool rach_ReportReq_r9 { get; set; }

        public bool rlf_ReportReq_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UEInformationRequest_r9_IEs Decode(BitArrayInputStream input)
            {
                UEInformationRequest_r9_IEs es = new UEInformationRequest_r9_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                es.rach_ReportReq_r9 = input.readBit() == 1;
                es.rlf_ReportReq_r9 = input.readBit() == 1;
                if (stream.Read())
                {
                    es.nonCriticalExtension = UEInformationRequest_v930_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class UEInformationRequest_v1020_IEs
    {
        public void InitDefaults()
        {
        }

        public logMeasReportReq_r10_Enum? logMeasReportReq_r10 { get; set; }

        public UEInformationRequest_v1130_IEs nonCriticalExtension { get; set; }

        public enum logMeasReportReq_r10_Enum
        {
            _true
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UEInformationRequest_v1020_IEs Decode(BitArrayInputStream input)
            {
                UEInformationRequest_v1020_IEs es = new UEInformationRequest_v1020_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    const int nBits = 1;
                    es.logMeasReportReq_r10 = (logMeasReportReq_r10_Enum)input.readBits(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = UEInformationRequest_v1130_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class UEInformationRequest_v1130_IEs
    {
        public void InitDefaults()
        {
        }

        public connEstFailReportReq_r11_Enum? connEstFailReportReq_r11 { get; set; }

        public UEInformationRequest_v12xy_IEs nonCriticalExtension { get; set; }

        public enum connEstFailReportReq_r11_Enum
        {
            _true
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UEInformationRequest_v1130_IEs Decode(BitArrayInputStream input)
            {
                UEInformationRequest_v1130_IEs es = new UEInformationRequest_v1130_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    const int nBits = 1;
                    es.connEstFailReportReq_r11 = (connEstFailReportReq_r11_Enum)input.readBits(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = UEInformationRequest_v12xy_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class UEInformationRequest_v12xy_IEs
    {
        public void InitDefaults()
        {
        }

        public mobilityHistoryReportReq_r12_Enum? mobilityHistoryReportReq_r12 { get; set; }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        public enum mobilityHistoryReportReq_r12_Enum
        {
            _true
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

            public UEInformationRequest_v12xy_IEs Decode(BitArrayInputStream input)
            {
                UEInformationRequest_v12xy_IEs es = new UEInformationRequest_v12xy_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    const int nBits = 1;
                    es.mobilityHistoryReportReq_r12 = (mobilityHistoryReportReq_r12_Enum)input.readBits(nBits);
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
    public class UEInformationRequest_v930_IEs
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public UEInformationRequest_v1020_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UEInformationRequest_v930_IEs Decode(BitArrayInputStream input)
            {
                UEInformationRequest_v930_IEs es = new UEInformationRequest_v930_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    es.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = UEInformationRequest_v1020_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class UEInformationResponse_r9
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

                public object spare1 { get; set; }

                public object spare2 { get; set; }

                public object spare3 { get; set; }

                public UEInformationResponse_r9_IEs ueInformationResponse_r9 { get; set; }

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
                                type.ueInformationResponse_r9 = UEInformationResponse_r9_IEs.PerDecoder.Instance.Decode(input);
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

            public UEInformationResponse_r9 Decode(BitArrayInputStream input)
            {
                UEInformationResponse_r9 _r = new UEInformationResponse_r9();
                _r.InitDefaults();
                _r.rrc_TransactionIdentifier = input.readBits(2);
                _r.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return _r;
            }
        }
    }

    [Serializable]
    public class UEInformationResponse_r9_IEs
    {
        public void InitDefaults()
        {
        }

        public UEInformationResponse_v930_IEs nonCriticalExtension { get; set; }

        public rach_Report_r9_Type rach_Report_r9 { get; set; }

        public RLF_Report_r9 rlf_Report_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UEInformationResponse_r9_IEs Decode(BitArrayInputStream input)
            {
                UEInformationResponse_r9_IEs es = new UEInformationResponse_r9_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    es.rach_Report_r9 = rach_Report_r9_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.rlf_Report_r9 = RLF_Report_r9.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = UEInformationResponse_v930_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }

        [Serializable]
        public class rach_Report_r9_Type
        {
            public void InitDefaults()
            {
            }

            public bool contentionDetected_r9 { get; set; }

            public long numberOfPreamblesSent_r9 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public rach_Report_r9_Type Decode(BitArrayInputStream input)
                {
                    rach_Report_r9_Type type = new rach_Report_r9_Type();
                    type.InitDefaults();
                    type.numberOfPreamblesSent_r9 = input.readBits(8) + 1;
                    type.contentionDetected_r9 = input.readBit() == 1;
                    return type;
                }
            }
        }
    }

    [Serializable]
    public class UEInformationResponse_v1020_IEs
    {
        public void InitDefaults()
        {
        }

        public LogMeasReport_r10 logMeasReport_r10 { get; set; }

        public UEInformationResponse_v1130_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UEInformationResponse_v1020_IEs Decode(BitArrayInputStream input)
            {
                UEInformationResponse_v1020_IEs es = new UEInformationResponse_v1020_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.logMeasReport_r10 = LogMeasReport_r10.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = UEInformationResponse_v1130_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class UEInformationResponse_v1130_IEs
    {
        public void InitDefaults()
        {
        }

        public ConnEstFailReport_r11 connEstFailReport_r11 { get; set; }

        public UEInformationResponse_v12xy_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UEInformationResponse_v1130_IEs Decode(BitArrayInputStream input)
            {
                UEInformationResponse_v1130_IEs es = new UEInformationResponse_v1130_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.connEstFailReport_r11 = ConnEstFailReport_r11.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = UEInformationResponse_v12xy_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class UEInformationResponse_v12xy_IEs
    {
        public void InitDefaults()
        {
        }

        public List<VisitedCellInfo_r12> mobilityHistoryReport_r12 { get; set; }

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

            public UEInformationResponse_v12xy_IEs Decode(BitArrayInputStream input)
            {
                UEInformationResponse_v12xy_IEs es = new UEInformationResponse_v12xy_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.mobilityHistoryReport_r12 = new List<VisitedCellInfo_r12>();
                    const int nBits = 4;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        VisitedCellInfo_r12 item = VisitedCellInfo_r12.PerDecoder.Instance.Decode(input);
                        es.mobilityHistoryReport_r12.Add(item);
                    }
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
    public class UEInformationResponse_v930_IEs
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public UEInformationResponse_v1020_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UEInformationResponse_v930_IEs Decode(BitArrayInputStream input)
            {
                UEInformationResponse_v930_IEs es = new UEInformationResponse_v930_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    es.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = UEInformationResponse_v1020_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class UEInformationResponse_v9e0_IEs
    {
        public void InitDefaults()
        {
        }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        public RLF_Report_v9e0 rlf_Report_v9e0 { get; set; }

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

            public UEInformationResponse_v9e0_IEs Decode(BitArrayInputStream input)
            {
                UEInformationResponse_v9e0_IEs es = new UEInformationResponse_v9e0_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.rlf_Report_v9e0 = RLF_Report_v9e0.PerDecoder.Instance.Decode(input);
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
