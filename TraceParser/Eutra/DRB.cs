using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class DRB_CountInfo
    {
        public void InitDefaults()
        {
        }

        public long count_Downlink { get; set; }

        public long count_Uplink { get; set; }

        public long drb_Identity { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public DRB_CountInfo Decode(BitArrayInputStream input)
            {
                DRB_CountInfo info = new DRB_CountInfo();
                info.InitDefaults();
                info.drb_Identity = input.readBits(5) + 1;
                info.count_Uplink = input.readBits(0x20);
                info.count_Downlink = input.readBits(0x20);
                return info;
            }
        }
    }

    [Serializable]
    public class DRB_CountMSB_Info
    {
        public void InitDefaults()
        {
        }

        public long countMSB_Downlink { get; set; }

        public long countMSB_Uplink { get; set; }

        public long drb_Identity { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public DRB_CountMSB_Info Decode(BitArrayInputStream input)
            {
                DRB_CountMSB_Info info = new DRB_CountMSB_Info();
                info.InitDefaults();
                info.drb_Identity = input.readBits(5) + 1;
                info.countMSB_Uplink = input.readBits(0x19);
                info.countMSB_Downlink = input.readBits(0x19);
                return info;
            }
        }
    }

    [Serializable]
    public class DRB_ToAddMod
    {
        public void InitDefaults()
        {
        }

        public long drb_Identity { get; set; }

        public long? eps_BearerIdentity { get; set; }

        public LogicalChannelConfig logicalChannelConfig { get; set; }

        public long? logicalChannelIdentity { get; set; }

        public PDCP_Config pdcp_Config { get; set; }

        public RLC_Config rlc_Config { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public DRB_ToAddMod Decode(BitArrayInputStream input)
            {
                DRB_ToAddMod mod = new DRB_ToAddMod();
                mod.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 5) : new BitMaskStream(input, 5);
                if (stream.Read())
                {
                    mod.eps_BearerIdentity = input.readBits(4);
                }
                mod.drb_Identity = input.readBits(5) + 1;
                if (stream.Read())
                {
                    mod.pdcp_Config = PDCP_Config.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    mod.rlc_Config = RLC_Config.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    mod.logicalChannelIdentity = input.readBits(3) + 3;
                }
                if (stream.Read())
                {
                    mod.logicalChannelConfig = LogicalChannelConfig.PerDecoder.Instance.Decode(input);
                }
                return mod;
            }
        }
    }

}
