namespace TraceParser.Eutra
{
    public enum ac_BarringFactor_Enum
    {
        p00,
        p05,
        p10,
        p15,
        p20,
        p25,
        p30,
        p40,
        p50,
        p60,
        p70,
        p75,
        p80,
        p85,
        p90,
        p95
    }

    public enum ac_BarringTime_Enum
    {
        s4,
        s8,
        s16,
        s32,
        s64,
        s128,
        s256,
        s512
    }

    public enum AccessStratumRelease
    {
        rel8,
        rel9,
        rel10,
        rel11,
        spare4,
        spare3,
        spare2,
        spare1
    }

    public enum AllowedMeasBandwidth
    {
        mbw6,
        mbw15,
        mbw25,
        mbw50,
        mbw75,
        mbw100
    }

    public enum BandclassCDMA2000
    {
        bc0,
        bc1,
        bc2,
        bc3,
        bc4,
        bc5,
        bc6,
        bc7,
        bc8,
        bc9,
        bc10,
        bc11,
        bc12,
        bc13,
        bc14,
        bc15,
        bc16,
        bc17,
        bc18_v9a0,
        bc19_v9a0,
        bc20_v9a0,
        bc21_v9a0,
        spare10,
        spare9,
        spare8,
        spare7,
        spare6,
        spare5,
        spare4,
        spare3,
        spare2,
        spare1
    }

    public enum BandIndicatorGERAN
    {
        dcs1800,
        pcs1900
    }

    public enum CA_BandwidthClass_r10
    {
        a,
        b,
        c,
        d,
        e,
        f
    }

    public enum CDMA2000_Type
    {
        type1XRTT,
        typeHRPD
    }

    public enum CQI_ReportModeAperiodic
    {
        rm12,
        rm20,
        rm22,
        rm30,
        rm31,
        rm32_v12xx,
        spare2,
        spare1
    }

    public enum EstablishmentCause
    {
        emergency,
        highPriorityAccess,
        mt_Access,
        mo_Signalling,
        mo_Data,
        delayTolerantAccess_v1020,
        spare2,
        spare1
    }

    public enum FilterCoefficient
    {
        fc0,
        fc1,
        fc2,
        fc3,
        fc4,
        fc5,
        fc6,
        fc7,
        fc8,
        fc9,
        fc11,
        fc13,
        fc15,
        fc17,
        fc19,
        spare1
    }

    public enum LoggingDuration_r10
    {
        min10,
        min20,
        min40,
        min60,
        min90,
        min120,
        spare2,
        spare1
    }

    public enum LoggingInterval_r10
    {
        ms1280,
        ms2560,
        ms5120,
        ms10240,
        ms20480,
        ms30720,
        ms40960,
        ms61440
    }

    public enum MeasCycleSCell_r10
    {
        sf160,
        sf256,
        sf320,
        sf512,
        sf640,
        sf1024,
        sf1280,
        spare1
    }

    public enum MIMO_CapabilityDL_r10
    {
        twoLayers,
        fourLayers,
        eightLayers
    }

    public enum MIMO_CapabilityUL_r10
    {
        twoLayers,
        fourLayers
    }

    public enum PollByte
    {
        kB25,
        kB50,
        kB75,
        kB100,
        kB125,
        kB250,
        kB375,
        kB500,
        kB750,
        kB1000,
        kB1250,
        kB1500,
        kB2000,
        kB3000,
        kBinfinity,
        spare1
    }

    public enum PollPDU
    {
        p4,
        p8,
        p16,
        p32,
        p64,
        p128,
        p256,
        pInfinity
    }

    public enum PreambleTransMax
    {
        n3,
        n4,
        n5,
        n6,
        n7,
        n8,
        n10,
        n20,
        n50,
        n100,
        n200
    }

    public enum Q_OffsetRange
    {
        dB_24,
        dB_22,
        dB_20,
        dB_18,
        dB_16,
        dB_14,
        dB_12,
        dB_10,
        dB_8,
        dB_6,
        dB_5,
        dB_4,
        dB_3,
        dB_2,
        dB_1,
        dB0,
        dB1,
        dB2,
        dB3,
        dB4,
        dB5,
        dB6,
        dB8,
        dB10,
        dB12,
        dB14,
        dB16,
        dB18,
        dB20,
        dB22,
        dB24
    }

    public enum RAT_Type
    {
        eutra,
        utra,
        geran_cs,
        geran_ps,
        cdma2000_1XRTT,
        spare3,
        spare2,
        spare1
    }

    public enum ReestablishmentCause
    {
        reconfigurationFailure,
        handoverFailure,
        otherFailure,
        spare1
    }

    public enum ReleaseCause
    {
        loadBalancingTAUrequired,
        other,
        cs_FallbackHighPriority_v1020,
        spare1
    }

    public enum ReportInterval
    {
        ms120,
        ms240,
        ms480,
        ms640,
        ms1024,
        ms2048,
        ms5120,
        ms10240,
        min1,
        min6,
        min12,
        min30,
        min60,
        spare3,
        spare2,
        spare1
    }

    public enum SIB_Type
    {
        sibType3,
        sibType4,
        sibType5,
        sibType6,
        sibType7,
        sibType8,
        sibType9,
        sibType10,
        sibType11,
        sibType12_v920,
        sibType13_v920,
        sibType14_v1130,
        sibType15_v1130,
        sibType16_v1130,
        spare2,
        spare1
    }

    public enum SN_FieldLength
    {
        size5,
        size10
    }

    public enum SRS_AntennaPort
    {
        an1,
        an2,
        an4,
        spare1
    }

    public enum SupportedBandGERAN
    {
        gsm450,
        gsm480,
        gsm710,
        gsm750,
        gsm810,
        gsm850,
        gsm900P,
        gsm900E,
        gsm900R,
        gsm1800,
        gsm1900,
        spare5,
        spare4,
        spare3,
        spare2,
        spare1
    }

    public enum SupportedBandUTRA_FDD
    {
        bandI,
        bandII,
        bandIII,
        bandIV,
        bandV,
        bandVI,
        bandVII,
        bandVIII,
        bandIX,
        bandX,
        bandXI,
        bandXII,
        bandXIII,
        bandXIV,
        bandXV,
        bandXVI,
        bandXVII_8a0,
        bandXVIII_8a0,
        bandXIX_8a0,
        bandXX_8a0,
        bandXXI_8a0,
        bandXXII_8a0,
        bandXXIII_8a0,
        bandXXIV_8a0,
        bandXXV_8a0,
        bandXXVI_8a0,
        bandXXVII_8a0,
        bandXXVIII_8a0,
        bandXXIX_8a0,
        bandXXX_8a0,
        bandXXXI_8a0,
        bandXXXII_8a0
    }

    public enum SupportedBandUTRA_TDD128
    {
        a,
        b,
        c,
        d,
        e,
        f,
        g,
        h,
        i,
        j,
        k,
        l,
        m,
        n,
        o,
        p
    }

    public enum SupportedBandUTRA_TDD384
    {
        a,
        b,
        c,
        d,
        e,
        f,
        g,
        h,
        i,
        j,
        k,
        l,
        m,
        n,
        o,
        p
    }

    public enum SupportedBandUTRA_TDD768
    {
        a,
        b,
        c,
        d,
        e,
        f,
        g,
        h,
        i,
        j,
        k,
        l,
        m,
        n,
        o,
        p
    }

    public enum T_PollRetransmit
    {
        ms5,
        ms10,
        ms15,
        ms20,
        ms25,
        ms30,
        ms35,
        ms40,
        ms45,
        ms50,
        ms55,
        ms60,
        ms65,
        ms70,
        ms75,
        ms80,
        ms85,
        ms90,
        ms95,
        ms100,
        ms105,
        ms110,
        ms115,
        ms120,
        ms125,
        ms130,
        ms135,
        ms140,
        ms145,
        ms150,
        ms155,
        ms160,
        ms165,
        ms170,
        ms175,
        ms180,
        ms185,
        ms190,
        ms195,
        ms200,
        ms205,
        ms210,
        ms215,
        ms220,
        ms225,
        ms230,
        ms235,
        ms240,
        ms245,
        ms250,
        ms300,
        ms350,
        ms400,
        ms450,
        ms500,
        spare9,
        spare8,
        spare7,
        spare6,
        spare5,
        spare4,
        spare3,
        spare2,
        spare1
    }

    public enum T_Reordering
    {
        ms0,
        ms5,
        ms10,
        ms15,
        ms20,
        ms25,
        ms30,
        ms35,
        ms40,
        ms45,
        ms50,
        ms55,
        ms60,
        ms65,
        ms70,
        ms75,
        ms80,
        ms85,
        ms90,
        ms95,
        ms100,
        ms110,
        ms120,
        ms130,
        ms140,
        ms150,
        ms160,
        ms170,
        ms180,
        ms190,
        ms200,
        spare1
    }

    public enum T_StatusProhibit
    {
        ms0,
        ms5,
        ms10,
        ms15,
        ms20,
        ms25,
        ms30,
        ms35,
        ms40,
        ms45,
        ms50,
        ms55,
        ms60,
        ms65,
        ms70,
        ms75,
        ms80,
        ms85,
        ms90,
        ms95,
        ms100,
        ms105,
        ms110,
        ms115,
        ms120,
        ms125,
        ms130,
        ms135,
        ms140,
        ms145,
        ms150,
        ms155,
        ms160,
        ms165,
        ms170,
        ms175,
        ms180,
        ms185,
        ms190,
        ms195,
        ms200,
        ms205,
        ms210,
        ms215,
        ms220,
        ms225,
        ms230,
        ms235,
        ms240,
        ms245,
        ms250,
        ms300,
        ms350,
        ms400,
        ms450,
        ms500,
        spare8,
        spare7,
        spare6,
        spare5,
        spare4,
        spare3,
        spare2,
        spare1
    }

    public enum TimeAlignmentTimer
    {
        sf500,
        sf750,
        sf1280,
        sf1920,
        sf2560,
        sf5120,
        sf10240,
        infinity
    }

    public enum TimeToTrigger
    {
        ms0,
        ms40,
        ms64,
        ms80,
        ms100,
        ms128,
        ms160,
        ms256,
        ms320,
        ms480,
        ms512,
        ms640,
        ms1024,
        ms1280,
        ms2560,
        ms5120
    }

    public enum UL_CyclicPrefixLength
    {
        len1,
        len2
    }

}
