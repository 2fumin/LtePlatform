using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common;
using Lte.Domain.Regular;
using TraceParser.Eutra;
using TraceParser.S1ap;
using TraceParser.X2ap;

namespace TraceParser.Common
{
    public static class CommonTraceParser
    {
        public static ITraceMessage DecodeMsg(string hexStr, string messageName)
        {
            using (BitArrayInputStream stream = hexStr.GetInputStream())
            {
                return DecodeMsg(stream, messageName);
            }
        }

        public static ITraceMessage DecodeMsg(BitArrayInputStream stream, string messageName)
        {
            switch (messageName)
            {
                case "DL_CCCH_Message":
                    return DL_CCCH_Message.PerDecoder.Instance.Decode(stream);

                case "DL_DCCH_Message":
                    return DL_DCCH_Message.PerDecoder.Instance.Decode(stream);

                case "UL_CCCH_Message":
                    return UL_CCCH_Message.PerDecoder.Instance.Decode(stream);

                case "UL_DCCH_Message":
                    return UL_DCCH_Message.PerDecoder.Instance.Decode(stream);

                case "PCCH_Message":
                    return PCCH_Message.PerDecoder.Instance.Decode(stream);

                case "MCCH_Message":
                    return MCCH_Message.PerDecoder.Instance.Decode(stream);

                case "BCCH_DL_SCH_Message":
                    return BCCH_DL_SCH_Message.PerDecoder.Instance.Decode(stream);

                case "BCCH_BCH_Message":
                    return BCCH_BCH_Message.PerDecoder.Instance.Decode(stream);

                case "X2AP_PDU":
                    return X2AP_PDU.PerDecoder.Instance.Decode(stream);

                case "S1AP_PDU":
                    return S1AP_PDU.PerDecoder.Instance.Decode(stream);

                default:
                    return null;
            }
        }
    }
}
