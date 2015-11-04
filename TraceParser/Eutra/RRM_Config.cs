using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class RRM_Config
    {
        public void InitDefaults()
        {
        }

        public List<CandidateCellInfo_r10> candidateCellInfoList_r10 { get; set; }

        public ue_InactiveTime_Enum? ue_InactiveTime { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RRM_Config Decode(BitArrayInputStream input)
            {
                int num2;
                RRM_Config config = new RRM_Config();
                config.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    num2 = 6;
                    config.ue_InactiveTime = (ue_InactiveTime_Enum)input.readBits(num2);
                }
                if (flag)
                {
                    BitMaskStream stream2 = new BitMaskStream(input, 1);
                    if (!stream2.Read())
                    {
                        return config;
                    }
                    config.candidateCellInfoList_r10 = new List<CandidateCellInfo_r10>();
                    num2 = 3;
                    int num3 = input.readBits(num2) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        CandidateCellInfo_r10 item = CandidateCellInfo_r10.PerDecoder.Instance.Decode(input);
                        config.candidateCellInfoList_r10.Add(item);
                    }
                }
                return config;
            }
        }

        public enum ue_InactiveTime_Enum
        {
            s1,
            s2,
            s3,
            s5,
            s7,
            s10,
            s15,
            s20,
            s25,
            s30,
            s40,
            s50,
            min1,
            min1s20c,
            min1s40,
            min2,
            min2s30,
            min3,
            min3s30,
            min4,
            min5,
            min6,
            min7,
            min8,
            min9,
            min10,
            min12,
            min14,
            min17,
            min20,
            min24,
            min28,
            min33,
            min38,
            min44,
            min50,
            hr1,
            hr1min30,
            hr2,
            hr2min30,
            hr3,
            hr3min30,
            hr4,
            hr5,
            hr6,
            hr8,
            hr10,
            hr13,
            hr16,
            hr20,
            day1,
            day1hr12,
            day2,
            day2hr12,
            day3,
            day4,
            day5,
            day7,
            day10,
            day14,
            day19,
            day24,
            day30,
            dayMoreThan30
        }
    }
}
