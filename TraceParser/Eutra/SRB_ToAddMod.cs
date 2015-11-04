using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SRB_ToAddMod
    {
        public void InitDefaults()
        {
        }

        public logicalChannelConfig_Type logicalChannelConfig { get; set; }

        public rlc_Config_Type rlc_Config { get; set; }

        public long srb_Identity { get; set; }

        [Serializable]
        public class logicalChannelConfig_Type
        {
            public void InitDefaults()
            {
            }

            public object defaultValue { get; set; }

            public LogicalChannelConfig explicitValue { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public logicalChannelConfig_Type Decode(BitArrayInputStream input)
                {
                    logicalChannelConfig_Type type = new logicalChannelConfig_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.explicitValue = LogicalChannelConfig.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SRB_ToAddMod Decode(BitArrayInputStream input)
            {
                SRB_ToAddMod mod = new SRB_ToAddMod();
                mod.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                mod.srb_Identity = input.readBits(1) + 1;
                if (stream.Read())
                {
                    mod.rlc_Config = rlc_Config_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    mod.logicalChannelConfig = logicalChannelConfig_Type.PerDecoder.Instance.Decode(input);
                }
                return mod;
            }
        }

        [Serializable]
        public class rlc_Config_Type
        {
            public void InitDefaults()
            {
            }

            public object defaultValue { get; set; }

            public RLC_Config explicitValue { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public rlc_Config_Type Decode(BitArrayInputStream input)
                {
                    rlc_Config_Type type = new rlc_Config_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.explicitValue = RLC_Config.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }
    }
}
