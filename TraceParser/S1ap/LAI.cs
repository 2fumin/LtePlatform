using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class LAI
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string lAC { get; set; }

        public string pLMNidentity { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public static LAI Decode(BitArrayInputStream input)
            {
                var lai = new LAI();
                lai.InitDefaults();
                var stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.skipUnreadedBits();
                lai.pLMNidentity = input.readOctetString(3);
                input.skipUnreadedBits();
                lai.lAC = input.readOctetString(2);
                if (!stream.Read()) return lai;
                input.skipUnreadedBits();
                lai.iE_Extensions = new List<ProtocolExtensionField>();
                const int nBits = 0x10;
                var num5 = input.readBits(nBits) + 1;
                for (var i = 0; i < num5; i++)
                {
                    var item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                    lai.iE_Extensions.Add(item);
                }
                return lai;
            }
        }
    }
}
