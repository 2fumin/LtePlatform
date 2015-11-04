using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class ReportConfigEUTRA
    {
        public void InitDefaults()
        {
        }

        public TimeToTrigger? alternativeTimeToTrigger_r12 { get; set; }

        public includeLocationInfo_r10_Enum? includeLocationInfo_r10 { get; set; }

        public long maxReportCells { get; set; }

        public reportAddNeighMeas_r10_Enum? reportAddNeighMeas_r10 { get; set; }

        public reportAmount_Enum reportAmount { get; set; }

        public ReportInterval reportInterval { get; set; }

        public reportQuantity_Enum reportQuantity { get; set; }

        public si_RequestForHO_r9_Enum? si_RequestForHO_r9 { get; set; }

        public triggerQuantity_Enum triggerQuantity { get; set; }

        public triggerType_Type triggerType { get; set; }

        public ue_RxTxTimeDiffPeriodical_r9_Enum? ue_RxTxTimeDiffPeriodical_r9 { get; set; }

        public enum includeLocationInfo_r10_Enum
        {
            _true
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ReportConfigEUTRA Decode(BitArrayInputStream input)
            {
                BitMaskStream stream;
                ReportConfigEUTRA geutra = new ReportConfigEUTRA();
                geutra.InitDefaults();
                bool flag = input.readBit() != 0;
                geutra.triggerType = triggerType_Type.PerDecoder.Instance.Decode(input);
                int nBits = 1;
                geutra.triggerQuantity = (triggerQuantity_Enum)input.readBits(nBits);
                nBits = 1;
                geutra.reportQuantity = (reportQuantity_Enum)input.readBits(nBits);
                geutra.maxReportCells = input.readBits(3) + 1;
                nBits = 4;
                geutra.reportInterval = (ReportInterval)input.readBits(nBits);
                nBits = 3;
                geutra.reportAmount = (reportAmount_Enum)input.readBits(nBits);
                if (flag)
                {
                    stream = new BitMaskStream(input, 8);
                    bool flag2 = input.readBit() != 0;//verExt3Present
                    stream=new BitMaskStream(input, 6);
                    stream=new BitMaskStream(input, 2);
                    if (stream.Read())
                    {
                        nBits = 1;
                        geutra.si_RequestForHO_r9 = (si_RequestForHO_r9_Enum) input.readBits(nBits);
                    }
                    if (stream.Read()) 
                    {
                        nBits = 1;
                        geutra.ue_RxTxTimeDiffPeriodical_r9 = (ue_RxTxTimeDiffPeriodical_r9_Enum) input.readBits(nBits);
                    }
                    stream=new BitMaskStream(input, 7);
                    if (flag2)
                    {
                        stream = new BitMaskStream(input, 8);
                        stream = new BitMaskStream(input, 2);
                        if (stream.Read())
                        {
                            nBits = 1;
                            geutra.includeLocationInfo_r10 = (includeLocationInfo_r10_Enum) input.readBits(nBits);
                        }
                        if (stream.Read())
                        {
                            nBits = 1;
                            geutra.reportAddNeighMeas_r10 = (reportAddNeighMeas_r10_Enum) input.readBits(nBits);
                        }
                        stream = new BitMaskStream(input, 4);
                        bool flag3 = input.readBit() != 0; //verExt4Present
                        if (flag3)
                        {
                            stream = new BitMaskStream(input, 1);
                            if (stream.Read())
                            {
                                nBits = 4;
                                geutra.alternativeTimeToTrigger_r12 = (TimeToTrigger) input.readBits(nBits);
                            }
                        }
                    }
                }
                return geutra;
            }
        }

        public enum reportAddNeighMeas_r10_Enum
        {
            setup
        }

        public enum reportAmount_Enum
        {
            r1,
            r2,
            r4,
            r8,
            r16,
            r32,
            r64,
            infinity
        }

        public enum reportQuantity_Enum
        {
            sameAsTriggerQuantity,
            both
        }

        public enum si_RequestForHO_r9_Enum
        {
            setup
        }

        public enum triggerQuantity_Enum
        {
            rsrp,
            rsrq
        }

        [Serializable]
        public class triggerType_Type
        {
            public void InitDefaults()
            {
            }

            public event__Type event_ { get; set; }

            public periodical_Type periodical { get; set; }

            [Serializable]
            public class event__Type
            {
                public void InitDefaults()
                {
                }

                public eventId_Type eventId { get; set; }

                public long hysteresis { get; set; }

                public TimeToTrigger timeToTrigger { get; set; }

                [Serializable]
                public class eventId_Type
                {
                    public void InitDefaults()
                    {
                    }

                    public eventA1_Type eventA1 { get; set; }

                    public eventA2_Type eventA2 { get; set; }

                    public eventA3_Type eventA3 { get; set; }

                    public eventA4_Type eventA4 { get; set; }

                    public eventA5_Type eventA5 { get; set; }

                    public eventA6_r10_Type eventA6_r10 { get; set; }

                    [Serializable]
                    public class eventA1_Type
                    {
                        public void InitDefaults()
                        {
                        }

                        public ThresholdEUTRA a1_Threshold { get; set; }

                        public class PerDecoder
                        {
                            public static readonly PerDecoder Instance = new PerDecoder();

                            public eventA1_Type Decode(BitArrayInputStream input)
                            {
                                eventA1_Type type = new eventA1_Type();
                                type.InitDefaults();
                                type.a1_Threshold = ThresholdEUTRA.PerDecoder.Instance.Decode(input);
                                return type;
                            }
                        }
                    }

                    [Serializable]
                    public class eventA2_Type
                    {
                        public void InitDefaults()
                        {
                        }

                        public ThresholdEUTRA a2_Threshold { get; set; }

                        public class PerDecoder
                        {
                            public static readonly PerDecoder Instance = new PerDecoder();

                            public eventA2_Type Decode(BitArrayInputStream input)
                            {
                                eventA2_Type type = new eventA2_Type();
                                type.InitDefaults();
                                type.a2_Threshold = ThresholdEUTRA.PerDecoder.Instance.Decode(input);
                                return type;
                            }
                        }
                    }

                    [Serializable]
                    public class eventA3_Type
                    {
                        public void InitDefaults()
                        {
                        }

                        public long a3_Offset { get; set; }

                        public bool reportOnLeave { get; set; }

                        public class PerDecoder
                        {
                            public static readonly PerDecoder Instance = new PerDecoder();

                            public eventA3_Type Decode(BitArrayInputStream input)
                            {
                                eventA3_Type type = new eventA3_Type();
                                type.InitDefaults();
                                type.a3_Offset = input.readBits(6) + -30;
                                type.reportOnLeave = input.readBit() == 1;
                                return type;
                            }
                        }
                    }

                    [Serializable]
                    public class eventA4_Type
                    {
                        public void InitDefaults()
                        {
                        }

                        public ThresholdEUTRA a4_Threshold { get; set; }

                        public class PerDecoder
                        {
                            public static readonly PerDecoder Instance = new PerDecoder();

                            public eventA4_Type Decode(BitArrayInputStream input)
                            {
                                eventA4_Type type = new eventA4_Type();
                                type.InitDefaults();
                                type.a4_Threshold = ThresholdEUTRA.PerDecoder.Instance.Decode(input);
                                return type;
                            }
                        }
                    }

                    [Serializable]
                    public class eventA5_Type
                    {
                        public void InitDefaults()
                        {
                        }

                        public ThresholdEUTRA a5_Threshold1 { get; set; }

                        public ThresholdEUTRA a5_Threshold2 { get; set; }

                        public class PerDecoder
                        {
                            public static readonly PerDecoder Instance = new PerDecoder();

                            public eventA5_Type Decode(BitArrayInputStream input)
                            {
                                eventA5_Type type = new eventA5_Type();
                                type.InitDefaults();
                                type.a5_Threshold1 = ThresholdEUTRA.PerDecoder.Instance.Decode(input);
                                type.a5_Threshold2 = ThresholdEUTRA.PerDecoder.Instance.Decode(input);
                                return type;
                            }
                        }
                    }

                    [Serializable]
                    public class eventA6_r10_Type
                    {
                        public void InitDefaults()
                        {
                        }

                        public long a6_Offset_r10 { get; set; }

                        public bool a6_ReportOnLeave_r10 { get; set; }

                        public class PerDecoder
                        {
                            public static readonly PerDecoder Instance = new PerDecoder();

                            public eventA6_r10_Type Decode(BitArrayInputStream input)
                            {
                                eventA6_r10_Type type = new eventA6_r10_Type();
                                type.InitDefaults();
                                type.a6_Offset_r10 = input.readBits(6) + -30;
                                type.a6_ReportOnLeave_r10 = input.readBit() == 1;
                                return type;
                            }
                        }
                    }

                    public class PerDecoder
                    {
                        public static readonly PerDecoder Instance = new PerDecoder();

                        public eventId_Type Decode(BitArrayInputStream input)
                        {
                            eventId_Type type = new eventId_Type();
                            type.InitDefaults();
                            bool flag = input.readBit() != 0;
                            switch (input.readBits(3))
                            {
                                case 0:
                                    type.eventA1 = eventA1_Type.PerDecoder.Instance.Decode(input);
                                    return type;

                                case 1:
                                    type.eventA2 = eventA2_Type.PerDecoder.Instance.Decode(input);
                                    return type;

                                case 2:
                                    type.eventA3 = eventA3_Type.PerDecoder.Instance.Decode(input);
                                    return type;

                                case 3:
                                    type.eventA4 = eventA4_Type.PerDecoder.Instance.Decode(input);
                                    return type;

                                case 4:
                                    type.eventA5 = eventA5_Type.PerDecoder.Instance.Decode(input);
                                    return type;

                                case 5:
                                    if (flag)
                                    {
                                        type.eventA6_r10 = eventA6_r10_Type.PerDecoder.Instance.Decode(input);
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

                    public event__Type Decode(BitArrayInputStream input)
                    {
                        event__Type type = new event__Type();
                        type.InitDefaults();
                        type.eventId = eventId_Type.PerDecoder.Instance.Decode(input);
                        type.hysteresis = input.readBits(5);
                        int nBits = 4;
                        type.timeToTrigger = (TimeToTrigger)input.readBits(nBits);
                        return type;
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public triggerType_Type Decode(BitArrayInputStream input)
                {
                    triggerType_Type type = new triggerType_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.event_ = event__Type.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.periodical = periodical_Type.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }

            [Serializable]
            public class periodical_Type
            {
                public void InitDefaults()
                {
                }

                public purpose_Enum purpose { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public periodical_Type Decode(BitArrayInputStream input)
                    {
                        periodical_Type type = new periodical_Type();
                        type.InitDefaults();
                        int nBits = 1;
                        type.purpose = (purpose_Enum)input.readBits(nBits);
                        return type;
                    }
                }

                public enum purpose_Enum
                {
                    reportStrongestCells,
                    reportCGI
                }
            }
        }

        public enum ue_RxTxTimeDiffPeriodical_r9_Enum
        {
            setup
        }
    }

    [Serializable]
    public class ReportConfigInterRAT
    {
        public void InitDefaults()
        {
        }

        public bool? includeLocationInfo_r11 { get; set; }

        public long maxReportCells { get; set; }

        public reportAmount_Enum reportAmount { get; set; }

        public ReportInterval reportInterval { get; set; }

        public reportQuantityUTRA_FDD_r10_Enum? reportQuantityUTRA_FDD_r10 { get; set; }

        public si_RequestForHO_r9_Enum? si_RequestForHO_r9 { get; set; }

        public triggerType_Type triggerType { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ReportConfigInterRAT Decode(BitArrayInputStream input)
            {
                BitMaskStream stream;
                ReportConfigInterRAT rrat = new ReportConfigInterRAT();
                rrat.InitDefaults();
                bool flag = input.readBit() != 0;
                rrat.triggerType = triggerType_Type.PerDecoder.Instance.Decode(input);
                rrat.maxReportCells = input.readBits(3) + 1;
                int nBits = 4;
                rrat.reportInterval = (ReportInterval)input.readBits(nBits);
                nBits = 3;
                rrat.reportAmount = (reportAmount_Enum)input.readBits(nBits);
                if (flag)
                {
                    stream = new BitMaskStream(input, 1);
                    if (stream.Read())
                    {
                        nBits = 1;
                        rrat.si_RequestForHO_r9 = (si_RequestForHO_r9_Enum)input.readBits(nBits);
                    }
                }
                if (flag)
                {
                    stream = new BitMaskStream(input, 1);
                    if (stream.Read())
                    {
                        nBits = 1;
                        rrat.reportQuantityUTRA_FDD_r10 = (reportQuantityUTRA_FDD_r10_Enum)input.readBits(nBits);
                    }
                }
                if (flag)
                {
                    stream = new BitMaskStream(input, 1);
                    if (stream.Read())
                    {
                        rrat.includeLocationInfo_r11 = input.readBit() == 1;
                    }
                }
                return rrat;
            }
        }

        public enum reportAmount_Enum
        {
            r1,
            r2,
            r4,
            r8,
            r16,
            r32,
            r64,
            infinity
        }

        public enum reportQuantityUTRA_FDD_r10_Enum
        {
            both
        }

        public enum si_RequestForHO_r9_Enum
        {
            setup
        }

        [Serializable]
        public class triggerType_Type
        {
            public void InitDefaults()
            {
            }

            public event__Type event_ { get; set; }

            public periodical_Type periodical { get; set; }

            [Serializable]
            public class event__Type
            {
                public void InitDefaults()
                {
                }

                public eventId_Type eventId { get; set; }

                public long hysteresis { get; set; }

                public TimeToTrigger timeToTrigger { get; set; }

                [Serializable]
                public class eventId_Type
                {
                    public void InitDefaults()
                    {
                    }

                    public eventB1_Type eventB1 { get; set; }

                    public eventB2_Type eventB2 { get; set; }

                    [Serializable]
                    public class eventB1_Type
                    {
                        public void InitDefaults()
                        {
                        }

                        public b1_Threshold_Type b1_Threshold { get; set; }

                        [Serializable]
                        public class b1_Threshold_Type
                        {
                            public void InitDefaults()
                            {
                            }

                            public long b1_ThresholdCDMA2000 { get; set; }

                            public long b1_ThresholdGERAN { get; set; }

                            public ThresholdUTRA b1_ThresholdUTRA { get; set; }

                            public class PerDecoder
                            {
                                public static readonly PerDecoder Instance = new PerDecoder();

                                public b1_Threshold_Type Decode(BitArrayInputStream input)
                                {
                                    b1_Threshold_Type type = new b1_Threshold_Type();
                                    type.InitDefaults();
                                    switch (input.readBits(2))
                                    {
                                        case 0:
                                            type.b1_ThresholdUTRA = ThresholdUTRA.PerDecoder.Instance.Decode(input);
                                            return type;

                                        case 1:
                                            type.b1_ThresholdGERAN = input.readBits(6);
                                            return type;

                                        case 2:
                                            type.b1_ThresholdCDMA2000 = input.readBits(6);
                                            return type;
                                    }
                                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                                }
                            }
                        }

                        public class PerDecoder
                        {
                            public static readonly PerDecoder Instance = new PerDecoder();

                            public eventB1_Type Decode(BitArrayInputStream input)
                            {
                                eventB1_Type type = new eventB1_Type();
                                type.InitDefaults();
                                type.b1_Threshold = b1_Threshold_Type.PerDecoder.Instance.Decode(input);
                                return type;
                            }
                        }
                    }

                    [Serializable]
                    public class eventB2_Type
                    {
                        public void InitDefaults()
                        {
                        }

                        public ThresholdEUTRA b2_Threshold1 { get; set; }

                        public b2_Threshold2_Type b2_Threshold2 { get; set; }

                        [Serializable]
                        public class b2_Threshold2_Type
                        {
                            public void InitDefaults()
                            {
                            }

                            public long b2_Threshold2CDMA2000 { get; set; }

                            public long b2_Threshold2GERAN { get; set; }

                            public ThresholdUTRA b2_Threshold2UTRA { get; set; }

                            public class PerDecoder
                            {
                                public static readonly PerDecoder Instance = new PerDecoder();

                                public b2_Threshold2_Type Decode(BitArrayInputStream input)
                                {
                                    b2_Threshold2_Type type = new b2_Threshold2_Type();
                                    type.InitDefaults();
                                    switch (input.readBits(2))
                                    {
                                        case 0:
                                            type.b2_Threshold2UTRA = ThresholdUTRA.PerDecoder.Instance.Decode(input);
                                            return type;

                                        case 1:
                                            type.b2_Threshold2GERAN = input.readBits(6);
                                            return type;

                                        case 2:
                                            type.b2_Threshold2CDMA2000 = input.readBits(6);
                                            return type;
                                    }
                                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                                }
                            }
                        }

                        public class PerDecoder
                        {
                            public static readonly PerDecoder Instance = new PerDecoder();

                            public eventB2_Type Decode(BitArrayInputStream input)
                            {
                                eventB2_Type type = new eventB2_Type();
                                type.InitDefaults();
                                type.b2_Threshold1 = ThresholdEUTRA.PerDecoder.Instance.Decode(input);
                                type.b2_Threshold2 = b2_Threshold2_Type.PerDecoder.Instance.Decode(input);
                                return type;
                            }
                        }
                    }

                    public class PerDecoder
                    {
                        public static readonly PerDecoder Instance = new PerDecoder();

                        public eventId_Type Decode(BitArrayInputStream input)
                        {
                            eventId_Type type = new eventId_Type();
                            type.InitDefaults();
                            input.readBit();
                            switch (input.readBits(1))
                            {
                                case 0:
                                    type.eventB1 = eventB1_Type.PerDecoder.Instance.Decode(input);
                                    return type;

                                case 1:
                                    type.eventB2 = eventB2_Type.PerDecoder.Instance.Decode(input);
                                    return type;
                            }
                            throw new Exception(GetType().Name + ":NoChoice had been choose");
                        }
                    }
                }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public event__Type Decode(BitArrayInputStream input)
                    {
                        event__Type type = new event__Type();
                        type.InitDefaults();
                        type.eventId = eventId_Type.PerDecoder.Instance.Decode(input);
                        type.hysteresis = input.readBits(5);
                        int nBits = 4;
                        type.timeToTrigger = (TimeToTrigger)input.readBits(nBits);
                        return type;
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public triggerType_Type Decode(BitArrayInputStream input)
                {
                    triggerType_Type type = new triggerType_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.event_ = event__Type.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.periodical = periodical_Type.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }

            [Serializable]
            public class periodical_Type
            {
                public void InitDefaults()
                {
                }

                public purpose_Enum purpose { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public periodical_Type Decode(BitArrayInputStream input)
                    {
                        periodical_Type type = new periodical_Type();
                        type.InitDefaults();
                        int nBits = 2;
                        type.purpose = (purpose_Enum)input.readBits(nBits);
                        return type;
                    }
                }

                public enum purpose_Enum
                {
                    reportStrongestCells,
                    reportStrongestCellsForSON,
                    reportCGI
                }
            }
        }
    }

    [Serializable]
    public class ReportConfigToAddMod
    {
        public void InitDefaults()
        {
        }

        public reportConfig_Type reportConfig { get; set; }

        public long reportConfigId { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ReportConfigToAddMod Decode(BitArrayInputStream input)
            {
                ReportConfigToAddMod mod = new ReportConfigToAddMod();
                mod.InitDefaults();
                mod.reportConfigId = input.readBits(5) + 1;
                mod.reportConfig = reportConfig_Type.PerDecoder.Instance.Decode(input);
                return mod;
            }
        }

        [Serializable]
        public class reportConfig_Type
        {
            public void InitDefaults()
            {
            }

            public ReportConfigEUTRA reportConfigEUTRA { get; set; }

            public ReportConfigInterRAT reportConfigInterRAT { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public reportConfig_Type Decode(BitArrayInputStream input)
                {
                    reportConfig_Type type = new reportConfig_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.reportConfigEUTRA = ReportConfigEUTRA.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.reportConfigInterRAT = ReportConfigInterRAT.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }
    }

    [Serializable]
    public class ReportProximityConfig_r9
    {
        public void InitDefaults()
        {
        }

        public proximityIndicationEUTRA_r9_Enum? proximityIndicationEUTRA_r9 { get; set; }

        public proximityIndicationUTRA_r9_Enum? proximityIndicationUTRA_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ReportProximityConfig_r9 Decode(BitArrayInputStream input)
            {
                int num2;
                ReportProximityConfig_r9 _r = new ReportProximityConfig_r9();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    num2 = 1;
                    _r.proximityIndicationEUTRA_r9 = (proximityIndicationEUTRA_r9_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _r.proximityIndicationUTRA_r9 = (proximityIndicationUTRA_r9_Enum)input.readBits(num2);
                }
                return _r;
            }
        }

        public enum proximityIndicationEUTRA_r9_Enum
        {
            enabled
        }

        public enum proximityIndicationUTRA_r9_Enum
        {
            enabled
        }
    }

}
