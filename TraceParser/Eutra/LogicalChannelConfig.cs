using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class LogicalChannelConfig
    {
        public void InitDefaults()
        {
        }

        public logicalChannelSR_Mask_r9_Enum? logicalChannelSR_Mask_r9 { get; set; }

        public ul_SpecificParameters_Type ul_SpecificParameters { get; set; }

        public enum logicalChannelSR_Mask_r9_Enum
        {
            setup
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public LogicalChannelConfig Decode(BitArrayInputStream input)
            {
                LogicalChannelConfig config = new LogicalChannelConfig();
                config.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    config.ul_SpecificParameters = ul_SpecificParameters_Type.PerDecoder.Instance.Decode(input);
                }
                if (flag)
                {
                    BitMaskStream stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        const int nBits = 1;
                        config.logicalChannelSR_Mask_r9 = (logicalChannelSR_Mask_r9_Enum)input.readBits(nBits);
                    }
                }
                return config;
            }
        }

        [Serializable]
        public class ul_SpecificParameters_Type
        {
            public void InitDefaults()
            {
            }

            public bucketSizeDuration_Enum bucketSizeDuration { get; set; }

            public long? logicalChannelGroup { get; set; }

            public prioritisedBitRate_Enum prioritisedBitRate { get; set; }

            public long priority { get; set; }

            public enum bucketSizeDuration_Enum
            {
                ms50,
                ms100,
                ms150,
                ms300,
                ms500,
                ms1000,
                spare2,
                spare1
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public ul_SpecificParameters_Type Decode(BitArrayInputStream input)
                {
                    ul_SpecificParameters_Type type = new ul_SpecificParameters_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 1);
                    type.priority = input.readBits(4) + 1;
                    int nBits = 4;
                    type.prioritisedBitRate = (prioritisedBitRate_Enum)input.readBits(nBits);
                    nBits = 3;
                    type.bucketSizeDuration = (bucketSizeDuration_Enum)input.readBits(nBits);
                    if (stream.Read())
                    {
                        type.logicalChannelGroup = input.readBits(2);
                    }
                    return type;
                }
            }

            public enum prioritisedBitRate_Enum
            {
                kBps0,
                kBps8,
                kBps16,
                kBps32,
                kBps64,
                kBps128,
                kBps256,
                infinity,
                kBps512_v1020,
                kBps1024_v1020,
                kBps2048_v1020,
                spare5,
                spare4,
                spare3,
                spare2,
                spare1
            }
        }
    }
}
