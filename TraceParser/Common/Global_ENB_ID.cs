using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common;
using TraceParser.S1ap;

namespace TraceParser.Common
{
    [Serializable]
    public class Global_ENB_ID
    {
        public void InitDefaults()
        {
        }

        public ENB_ID eNB_ID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string pLMNidentity { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public Global_ENB_ID Decode(BitArrayInputStream input)
            {
                var l_enb_id = new Global_ENB_ID();
                l_enb_id.InitDefaults();
                var stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.skipUnreadedBits();
                l_enb_id.pLMNidentity = input.readOctetString(3);
                l_enb_id.eNB_ID = ENB_ID.PerDecoder.Instance.Decode(input);
                if (!stream.Read()) return l_enb_id;
                input.skipUnreadedBits();
                l_enb_id.iE_Extensions = new List<ProtocolExtensionField>();
                const int nBits = 0x10;
                var num5 = input.readBits(nBits) + 1;
                for (var i = 0; i < num5; i++)
                {
                    var item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                    l_enb_id.iE_Extensions.Add(item);
                }
                return l_enb_id;
            }
        }
    }

}
