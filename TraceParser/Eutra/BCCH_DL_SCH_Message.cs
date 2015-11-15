using System;
using Lte.Domain.Common;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class BCCH_DL_SCH_Message : ITraceMessage
    {
        public void InitDefaults()
        {
        }

        public BCCH_DL_SCH_MessageType message { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public BCCH_DL_SCH_Message Decode(BitArrayInputStream input)
            {
                var message = new BCCH_DL_SCH_Message();
                message.InitDefaults();
                message.message = BCCH_DL_SCH_MessageType.PerDecoder.Instance.Decode(input);
                return message;
            }
        }
    }
}