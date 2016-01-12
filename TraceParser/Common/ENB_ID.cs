using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common;

namespace TraceParser.Common
{
    [Serializable]
    public class ENB_ID
    {
        public void InitDefaults()
        {
        }

        public string home_eNB_ID { get; set; }

        public string macro_eNB_ID { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ENB_ID Decode(BitArrayInputStream input)
            {
                var enb_id = new ENB_ID();
                enb_id.InitDefaults();
                input.readBit();
                switch (input.readBits(1))
                {
                    case 0:
                        enb_id.macro_eNB_ID = input.readBitString(20);
                        return enb_id;

                    case 1:
                        enb_id.home_eNB_ID = input.readBitString(0x1c);
                        return enb_id;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

}
