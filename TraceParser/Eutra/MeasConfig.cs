using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class MeasConfig
    {
        public void InitDefaults()
        {
        }

        public MeasGapConfig measGapConfig { get; set; }

        public List<MeasIdToAddMod> measIdToAddModList { get; set; }

        public List<MeasIdToAddMod_v12xy> measIdToAddModList_v12xy { get; set; }

        public List<long> measIdToRemoveList { get; set; }

        public List<MeasObjectToAddMod> measObjectToAddModList { get; set; }

        public List<MeasObjectToAddMod_v9e0> measObjectToAddModList_v9e0 { get; set; }

        public List<long> measObjectToRemoveList { get; set; }

        public PreRegistrationInfoHRPD preRegistrationInfoHRPD { get; set; }

        public QuantityConfig quantityConfig { get; set; }

        public List<ReportConfigToAddMod> reportConfigToAddModList { get; set; }

        public List<long> reportConfigToRemoveList { get; set; }

        public long? s_Measure { get; set; }

        public speedStatePars_Type speedStatePars { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasConfig Decode(BitArrayInputStream input)
            {
                int num2;
                BitMaskStream stream2;
                MeasConfig config = new MeasConfig();
                config.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 11);
                if (stream.Read())//measObjectToRemoveListPresent
                {
                    config.measObjectToRemoveList = new List<long>();
                    num2 = 5;
                    int num3 = input.readBits(num2) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        long item = input.readBits(5) + 1;
                        config.measObjectToRemoveList.Add(item);
                    }
                }
                if (stream.Read())//measObjectToAddModListPresent
                {
                    config.measObjectToAddModList = new List<MeasObjectToAddMod>();
                    num2 = 5;
                    int num6 = input.readBits(num2) + 1;
                    for (int j = 0; j < num6; j++)
                    {
                        MeasObjectToAddMod mod = MeasObjectToAddMod.PerDecoder.Instance.Decode(input);
                        config.measObjectToAddModList.Add(mod);
                    }
                }
                if (stream.Read())//reportConfigToRemoveListPresent
                {
                    config.reportConfigToRemoveList = new List<long>();
                    num2 = 5;
                    int num8 = input.readBits(num2) + 1;
                    for (int k = 0; k < num8; k++)
                    {
                        long num10 = input.readBits(5) + 1;
                        config.reportConfigToRemoveList.Add(num10);
                    }
                }
                if (stream.Read())//reportConfigToAddModListPresent
                {
                    config.reportConfigToAddModList = new List<ReportConfigToAddMod>();
                    num2 = 5;
                    int num11 = input.readBits(num2) + 1;
                    for (int m = 0; m < num11; m++)
                    {
                        ReportConfigToAddMod mod2 = ReportConfigToAddMod.PerDecoder.Instance.Decode(input);
                        config.reportConfigToAddModList.Add(mod2);
                    }
                }
                if (stream.Read())//measIdToRemoveListPresent
                {
                    config.measIdToRemoveList = new List<long>();
                    num2 = 5;
                    int num13 = input.readBits(num2) + 1;
                    for (int n = 0; n < num13; n++)
                    {
                        long num15 = input.readBits(5) + 1;
                        config.measIdToRemoveList.Add(num15);
                    }
                }
                if (stream.Read())//measIdToAddModListPresent
                {
                    config.measIdToAddModList = new List<MeasIdToAddMod>();
                    num2 = 5;
                    int num16 = input.readBits(num2) + 1;
                    for (int num17 = 0; num17 < num16; num17++)
                    {
                        MeasIdToAddMod mod3 = MeasIdToAddMod.PerDecoder.Instance.Decode(input);
                        config.measIdToAddModList.Add(mod3);
                    }
                }
                if (stream.Read())//quantityConfigPresent
                {
                    config.quantityConfig = QuantityConfig.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())//measGapConfigPresent
                {
                    config.measGapConfig = MeasGapConfig.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())//s_MeasurePresent
                {
                    config.s_Measure = input.readBits(7);
                }
                if (stream.Read())//preRegistrationInfoHRPDPresent
                {
                    config.preRegistrationInfoHRPD = PreRegistrationInfoHRPD.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())//speedStateParsPresent
                {
                    config.speedStatePars = speedStatePars_Type.PerDecoder.Instance.Decode(input);
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        config.measObjectToAddModList_v9e0 = new List<MeasObjectToAddMod_v9e0>();
                        num2 = 5;
                        int num18 = input.readBits(num2) + 1;
                        for (int num19 = 0; num19 < num18; num19++)
                        {
                            MeasObjectToAddMod_v9e0 _ve = MeasObjectToAddMod_v9e0.PerDecoder.Instance.Decode(input);
                            config.measObjectToAddModList_v9e0.Add(_ve);
                        }
                    }
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (!stream2.Read())
                    {
                        return config;
                    }
                    config.measIdToAddModList_v12xy = new List<MeasIdToAddMod_v12xy>();
                    num2 = 5;
                    int num20 = input.readBits(num2) + 1;
                    for (int num21 = 0; num21 < num20; num21++)
                    {
                        MeasIdToAddMod_v12xy _vxy = MeasIdToAddMod_v12xy.PerDecoder.Instance.Decode(input);
                        config.measIdToAddModList_v12xy.Add(_vxy);
                    }
                }
                return config;
            }
        }

        [Serializable]
        public class speedStatePars_Type
        {
            public void InitDefaults()
            {
            }

            public object release { get; set; }

            public setup_Type setup { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public speedStatePars_Type Decode(BitArrayInputStream input)
                {
                    speedStatePars_Type type = new speedStatePars_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            return type;

                        case 1:
                            type.setup = setup_Type.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }

            [Serializable]
            public class setup_Type
            {
                public void InitDefaults()
                {
                }

                public MobilityStateParameters mobilityStateParameters { get; set; }

                public SpeedStateScaleFactors timeToTrigger_SF { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public setup_Type Decode(BitArrayInputStream input)
                    {
                        setup_Type type = new setup_Type();
                        type.InitDefaults();
                        type.mobilityStateParameters = MobilityStateParameters.PerDecoder.Instance.Decode(input);
                        type.timeToTrigger_SF = SpeedStateScaleFactors.PerDecoder.Instance.Decode(input);
                        return type;
                    }
                }
            }
        }
    }

    [Serializable]
    public class MeasGapConfig
    {
        public void InitDefaults()
        {
        }

        public object release { get; set; }

        public setup_Type setup { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasGapConfig Decode(BitArrayInputStream input)
            {
                MeasGapConfig config = new MeasGapConfig();
                config.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        return config;

                    case 1:
                        config.setup = setup_Type.PerDecoder.Instance.Decode(input);
                        return config;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }

        [Serializable]
        public class setup_Type
        {
            public void InitDefaults()
            {
            }

            public gapOffset_Type gapOffset { get; set; }

            [Serializable]
            public class gapOffset_Type
            {
                public void InitDefaults()
                {
                }

                public long gp0 { get; set; }

                public long gp1 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public gapOffset_Type Decode(BitArrayInputStream input)
                    {
                        gapOffset_Type type = new gapOffset_Type();
                        type.InitDefaults();
                        input.readBit();
                        switch (input.readBits(1))
                        {
                            case 0:
                                type.gp0 = input.readBits(6);
                                return type;

                            case 1:
                                type.gp1 = input.readBits(7);
                                return type;
                        }
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public setup_Type Decode(BitArrayInputStream input)
                {
                    setup_Type type = new setup_Type();
                    type.InitDefaults();
                    type.gapOffset = gapOffset_Type.PerDecoder.Instance.Decode(input);
                    return type;
                }
            }
        }
    }

    [Serializable]
    public class MeasIdToAddMod
    {
        public void InitDefaults()
        {
        }

        public long measId { get; set; }

        public long measObjectId { get; set; }

        public long reportConfigId { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasIdToAddMod Decode(BitArrayInputStream input)
            {
                MeasIdToAddMod mod = new MeasIdToAddMod();
                mod.InitDefaults();
                mod.measId = input.readBits(5) + 1;
                mod.measObjectId = input.readBits(5) + 1;
                mod.reportConfigId = input.readBits(5) + 1;
                return mod;
            }
        }
    }

    [Serializable]
    public class MeasIdToAddMod_v12xy
    {
        public void InitDefaults()
        {
        }

        public t312_r12_Enum? t312_r12 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasIdToAddMod_v12xy Decode(BitArrayInputStream input)
            {
                MeasIdToAddMod_v12xy _vxy = new MeasIdToAddMod_v12xy();
                _vxy.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    const int nBits = 3;
                    _vxy.t312_r12 = (t312_r12_Enum)input.readBits(nBits);
                }
                return _vxy;
            }
        }

        public enum t312_r12_Enum
        {
            ms0,
            ms50,
            ms100,
            ms200,
            ms300,
            ms400,
            ms500,
            ms1000
        }
    }

    [Serializable]
    public class LoggedMeasurementConfiguration_r10
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

                public LoggedMeasurementConfiguration_r10_IEs loggedMeasurementConfiguration_r10 { get; set; }

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
                                type.loggedMeasurementConfiguration_r10 = LoggedMeasurementConfiguration_r10_IEs.PerDecoder.Instance.Decode(input);
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

            public LoggedMeasurementConfiguration_r10 Decode(BitArrayInputStream input)
            {
                LoggedMeasurementConfiguration_r10 _r = new LoggedMeasurementConfiguration_r10();
                _r.InitDefaults();
                _r.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return _r;
            }
        }
    }

    [Serializable]
    public class LoggedMeasurementConfiguration_r10_IEs
    {
        public void InitDefaults()
        {
        }

        public string absoluteTimeInfo_r10 { get; set; }

        public AreaConfiguration_r10 areaConfiguration_r10 { get; set; }

        public LoggingDuration_r10 loggingDuration_r10 { get; set; }

        public LoggingInterval_r10 loggingInterval_r10 { get; set; }

        public LoggedMeasurementConfiguration_v1080_IEs nonCriticalExtension { get; set; }

        public string tce_Id_r10 { get; set; }

        public string traceRecordingSessionRef_r10 { get; set; }

        public TraceReference_r10 traceReference_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public LoggedMeasurementConfiguration_r10_IEs Decode(BitArrayInputStream input)
            {
                LoggedMeasurementConfiguration_r10_IEs es = new LoggedMeasurementConfiguration_r10_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                es.traceReference_r10 = TraceReference_r10.PerDecoder.Instance.Decode(input);
                es.traceRecordingSessionRef_r10 = input.readOctetString(2);
                es.tce_Id_r10 = input.readOctetString(1);
                es.absoluteTimeInfo_r10 = input.readBitString(0x30);
                if (stream.Read())
                {
                    es.areaConfiguration_r10 = AreaConfiguration_r10.PerDecoder.Instance.Decode(input);
                }
                int nBits = 3;
                es.loggingDuration_r10 = (LoggingDuration_r10)input.readBits(nBits);
                nBits = 3;
                es.loggingInterval_r10 = (LoggingInterval_r10)input.readBits(nBits);
                if (stream.Read())
                {
                    es.nonCriticalExtension = LoggedMeasurementConfiguration_v1080_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class LoggedMeasurementConfiguration_v1080_IEs
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension_r10 { get; set; }

        public LoggedMeasurementConfiguration_v1130_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public LoggedMeasurementConfiguration_v1080_IEs Decode(BitArrayInputStream input)
            {
                LoggedMeasurementConfiguration_v1080_IEs es = new LoggedMeasurementConfiguration_v1080_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    es.lateNonCriticalExtension_r10 = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = LoggedMeasurementConfiguration_v1130_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class LoggedMeasurementConfiguration_v1130_IEs
    {
        public void InitDefaults()
        {
        }

        public AreaConfiguration_v1130 areaConfiguration_v1130 { get; set; }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        public List<PLMN_Identity> plmn_IdentityList_r11 { get; set; }

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

            public LoggedMeasurementConfiguration_v1130_IEs Decode(BitArrayInputStream input)
            {
                LoggedMeasurementConfiguration_v1130_IEs es = new LoggedMeasurementConfiguration_v1130_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    es.plmn_IdentityList_r11 = new List<PLMN_Identity>();
                    const int nBits = 4;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        PLMN_Identity item = PLMN_Identity.PerDecoder.Instance.Decode(input);
                        es.plmn_IdentityList_r11.Add(item);
                    }
                }
                if (stream.Read())
                {
                    es.areaConfiguration_v1130 = AreaConfiguration_v1130.PerDecoder.Instance.Decode(input);
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
