using System;
using Lte.Domain.Common;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class BCCH_BCH_Message : ITraceMessage
    {
        public void InitDefaults()
        {
        }

        public MasterInformationBlock message { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public BCCH_BCH_Message Decode(BitArrayInputStream input)
            {
                var message = new BCCH_BCH_Message();
                message.InitDefaults();
                message.message = MasterInformationBlock.PerDecoder.Instance.Decode(input);
                return message;
            }
        }
    }
}
