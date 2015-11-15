using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformationBlockType2
    {
        public void InitDefaults()
        {
        }

        public AC_BarringConfig ac_BarringForCSFB_r10 { get; set; }

        public ac_BarringInfo_Type ac_BarringInfo { get; set; }

        public freqInfo_Type freqInfo { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public List<MBSFN_SubframeConfig> mbsfn_SubframeConfigList { get; set; }

        public RadioResourceConfigCommonSIB radioResourceConfigCommon { get; set; }

        public AC_BarringConfig ssac_BarringForMMTEL_Video_r9 { get; set; }

        public AC_BarringConfig ssac_BarringForMMTEL_Voice_r9 { get; set; }

        public TimeAlignmentTimer timeAlignmentTimerCommon { get; set; }

        public UE_TimersAndConstants ue_TimersAndConstants { get; set; }

        [Serializable]
        public class ac_BarringInfo_Type
        {
            public void InitDefaults()
            {
            }

            public bool ac_BarringForEmergency { get; set; }

            public AC_BarringConfig ac_BarringForMO_Data { get; set; }

            public AC_BarringConfig ac_BarringForMO_Signalling { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public ac_BarringInfo_Type Decode(BitArrayInputStream input)
                {
                    var type = new ac_BarringInfo_Type();
                    type.InitDefaults();
                    var stream = new BitMaskStream(input, 2);
                    type.ac_BarringForEmergency = input.readBit() == 1;
                    if (stream.Read())
                    {
                        type.ac_BarringForMO_Signalling = AC_BarringConfig.PerDecoder.Decode(input);
                    }
                    if (stream.Read())
                    {
                        type.ac_BarringForMO_Data = AC_BarringConfig.PerDecoder.Decode(input);
                    }
                    return type;
                }
            }
        }

        [Serializable]
        public class freqInfo_Type
        {
            public void InitDefaults()
            {
            }

            public long additionalSpectrumEmission { get; set; }

            public ul_Bandwidth_Enum? ul_Bandwidth { get; set; }

            public long? ul_CarrierFreq { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public freqInfo_Type Decode(BitArrayInputStream input)
                {
                    var type = new freqInfo_Type();
                    type.InitDefaults();
                    var stream = new BitMaskStream(input, 2);
                    if (stream.Read())
                    {
                        type.ul_CarrierFreq = input.readBits(0x10);
                    }
                    if (stream.Read())
                    {
                        var nBits = 3;
                        type.ul_Bandwidth = (ul_Bandwidth_Enum)input.readBits(nBits);
                    }
                    type.additionalSpectrumEmission = input.readBits(5) + 1;
                    return type;
                }
            }

            public enum ul_Bandwidth_Enum
            {
                n6,
                n15,
                n25,
                n50,
                n75,
                n100
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType2 Decode(BitArrayInputStream input)
            {
                int num2;
                BitMaskStream stream2;
                var type = new SystemInformationBlockType2();
                type.InitDefaults();
                var flag = false;
                flag = input.readBit() != 0;
                var stream = flag ? new BitMaskStream(input, 3) : new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    type.ac_BarringInfo = ac_BarringInfo_Type.PerDecoder.Instance.Decode(input);
                }
                type.radioResourceConfigCommon = RadioResourceConfigCommonSIB.PerDecoder.Instance.Decode(input);
                type.ue_TimersAndConstants = UE_TimersAndConstants.PerDecoder.Instance.Decode(input);
                type.freqInfo = freqInfo_Type.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    type.mbsfn_SubframeConfigList = new List<MBSFN_SubframeConfig>();
                    num2 = 3;
                    var num3 = input.readBits(num2) + 1;
                    for (var i = 0; i < num3; i++)
                    {
                        var item = MBSFN_SubframeConfig.PerDecoder.Instance.Decode(input);
                        type.mbsfn_SubframeConfigList.Add(item);
                    }
                }
                num2 = 3;
                type.timeAlignmentTimerCommon = (TimeAlignmentTimer)input.readBits(num2);
                if (flag && stream.Read())
                {
                    var nBits = input.readBits(8);
                    type.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 2);
                    if (stream2.Read())
                    {
                        type.ssac_BarringForMMTEL_Voice_r9 = AC_BarringConfig.PerDecoder.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        type.ssac_BarringForMMTEL_Video_r9 = AC_BarringConfig.PerDecoder.Decode(input);
                    }
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        type.ac_BarringForCSFB_r10 = AC_BarringConfig.PerDecoder.Decode(input);
                    }
                }
                return type;
            }
        }
    }
}