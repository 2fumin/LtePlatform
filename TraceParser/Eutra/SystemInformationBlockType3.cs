using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformationBlockType3
    {
        public void InitDefaults()
        {
        }

        public cellReselectionInfoCommon_Type cellReselectionInfoCommon { get; set; }

        public cellReselectionServingFreqInfo_Type cellReselectionServingFreqInfo { get; set; }

        public intraFreqCellReselectionInfo_Type intraFreqCellReselectionInfo { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public long? q_QualMin_r9 { get; set; }

        public long? q_QualMinWB_r11 { get; set; }

        public s_IntraSearch_v920_Type s_IntraSearch_v920 { get; set; }

        public s_NonIntraSearch_v920_Type s_NonIntraSearch_v920 { get; set; }

        public long? threshServingLowQ_r9 { get; set; }

        [Serializable]
        public class cellReselectionInfoCommon_Type
        {
            public void InitDefaults()
            {
            }

            public q_Hyst_Enum q_Hyst { get; set; }

            public speedStateReselectionPars_Type speedStateReselectionPars { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public cellReselectionInfoCommon_Type Decode(BitArrayInputStream input)
                {
                    var type = new cellReselectionInfoCommon_Type();
                    type.InitDefaults();
                    var stream = new BitMaskStream(input, 1);
                    const int nBits = 4;
                    type.q_Hyst = (q_Hyst_Enum)input.readBits(nBits);
                    if (stream.Read())
                    {
                        type.speedStateReselectionPars = speedStateReselectionPars_Type.PerDecoder.Instance.Decode(input);
                    }
                    return type;
                }
            }

            public enum q_Hyst_Enum
            {
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

            [Serializable]
            public class speedStateReselectionPars_Type
            {
                public void InitDefaults()
                {
                }

                public MobilityStateParameters mobilityStateParameters { get; set; }

                public q_HystSF_Type q_HystSF { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public speedStateReselectionPars_Type Decode(BitArrayInputStream input)
                    {
                        var type = new speedStateReselectionPars_Type();
                        type.InitDefaults();
                        type.mobilityStateParameters = MobilityStateParameters.PerDecoder.Instance.Decode(input);
                        type.q_HystSF = q_HystSF_Type.PerDecoder.Instance.Decode(input);
                        return type;
                    }
                }

                [Serializable]
                public class q_HystSF_Type
                {
                    public void InitDefaults()
                    {
                    }

                    public sf_High_Enum sf_High { get; set; }

                    public sf_Medium_Enum sf_Medium { get; set; }

                    public class PerDecoder
                    {
                        public static readonly PerDecoder Instance = new PerDecoder();

                        public q_HystSF_Type Decode(BitArrayInputStream input)
                        {
                            var type = new q_HystSF_Type();
                            type.InitDefaults();
                            var nBits = 2;
                            type.sf_Medium = (sf_Medium_Enum)input.readBits(nBits);
                            nBits = 2;
                            type.sf_High = (sf_High_Enum)input.readBits(nBits);
                            return type;
                        }
                    }

                    public enum sf_High_Enum
                    {
                        dB_6,
                        dB_4,
                        dB_2,
                        dB0
                    }

                    public enum sf_Medium_Enum
                    {
                        dB_6,
                        dB_4,
                        dB_2,
                        dB0
                    }
                }
            }
        }

        [Serializable]
        public class cellReselectionServingFreqInfo_Type
        {
            public void InitDefaults()
            {
            }

            public long cellReselectionPriority { get; set; }

            public long? s_NonIntraSearch { get; set; }

            public long threshServingLow { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public cellReselectionServingFreqInfo_Type Decode(BitArrayInputStream input)
                {
                    var type = new cellReselectionServingFreqInfo_Type();
                    type.InitDefaults();
                    var stream = new BitMaskStream(input, 1);
                    if (stream.Read())
                    {
                        type.s_NonIntraSearch = input.readBits(5);
                    }
                    type.threshServingLow = input.readBits(5);
                    type.cellReselectionPriority = input.readBits(3);
                    return type;
                }
            }
        }

        [Serializable]
        public class intraFreqCellReselectionInfo_Type
        {
            public void InitDefaults()
            {
            }

            public AllowedMeasBandwidth? allowedMeasBandwidth { get; set; }

            public string neighCellConfig { get; set; }

            public long? p_Max { get; set; }

            public bool presenceAntennaPort1 { get; set; }

            public long q_RxLevMin { get; set; }

            public long? s_IntraSearch { get; set; }

            public long t_ReselectionEUTRA { get; set; }

            public SpeedStateScaleFactors t_ReselectionEUTRA_SF { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public intraFreqCellReselectionInfo_Type Decode(BitArrayInputStream input)
                {
                    var type = new intraFreqCellReselectionInfo_Type();
                    type.InitDefaults();
                    var stream = new BitMaskStream(input, 4);
                    type.q_RxLevMin = input.readBits(6) + -70;
                    if (stream.Read())
                    {
                        type.p_Max = input.readBits(6) + -30;
                    }
                    if (stream.Read())
                    {
                        type.s_IntraSearch = input.readBits(5);
                    }
                    if (stream.Read())
                    {
                        var nBits = 3;
                        type.allowedMeasBandwidth = (AllowedMeasBandwidth)input.readBits(nBits);
                    }
                    type.presenceAntennaPort1 = input.readBit() == 1;
                    type.neighCellConfig = input.readBitString(2);
                    type.t_ReselectionEUTRA = input.readBits(3);
                    if (stream.Read())
                    {
                        type.t_ReselectionEUTRA_SF = SpeedStateScaleFactors.PerDecoder.Instance.Decode(input);
                    }
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType3 Decode(BitArrayInputStream input)
            {
                BitMaskStream stream2;
                var type = new SystemInformationBlockType3();
                type.InitDefaults();
                var flag = false;
                flag = input.readBit() != 0;
                var stream = flag ? new BitMaskStream(input, 1) : new BitMaskStream(input, 0);
                type.cellReselectionInfoCommon = cellReselectionInfoCommon_Type.PerDecoder.Instance.Decode(input);
                type.cellReselectionServingFreqInfo = cellReselectionServingFreqInfo_Type.PerDecoder.Instance.Decode(input);
                type.intraFreqCellReselectionInfo = intraFreqCellReselectionInfo_Type.PerDecoder.Instance.Decode(input);
                if (flag && stream.Read())
                {
                    var nBits = input.readBits(8);
                    type.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 4);
                    if (stream2.Read())
                    {
                        type.s_IntraSearch_v920 = s_IntraSearch_v920_Type.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        type.s_NonIntraSearch_v920 = s_NonIntraSearch_v920_Type.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        type.q_QualMin_r9 = input.readBits(5) + -34;
                    }
                    if (stream2.Read())
                    {
                        type.threshServingLowQ_r9 = input.readBits(5);
                    }
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        type.q_QualMinWB_r11 = input.readBits(5) + -34;
                    }
                }
                return type;
            }
        }

        [Serializable]
        public class s_IntraSearch_v920_Type
        {
            public void InitDefaults()
            {
            }

            public long s_IntraSearchP_r9 { get; set; }

            public long s_IntraSearchQ_r9 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public s_IntraSearch_v920_Type Decode(BitArrayInputStream input)
                {
                    var type = new s_IntraSearch_v920_Type();
                    type.InitDefaults();
                    type.s_IntraSearchP_r9 = input.readBits(5);
                    type.s_IntraSearchQ_r9 = input.readBits(5);
                    return type;
                }
            }
        }

        [Serializable]
        public class s_NonIntraSearch_v920_Type
        {
            public void InitDefaults()
            {
            }

            public long s_NonIntraSearchP_r9 { get; set; }

            public long s_NonIntraSearchQ_r9 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public s_NonIntraSearch_v920_Type Decode(BitArrayInputStream input)
                {
                    var type = new s_NonIntraSearch_v920_Type();
                    type.InitDefaults();
                    type.s_NonIntraSearchP_r9 = input.readBits(5);
                    type.s_NonIntraSearchQ_r9 = input.readBits(5);
                    return type;
                }
            }
        }
    }
}