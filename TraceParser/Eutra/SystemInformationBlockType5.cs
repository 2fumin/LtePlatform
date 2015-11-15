using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformationBlockType5
    {
        public void InitDefaults()
        {
        }

        public List<InterFreqCarrierFreqInfo> interFreqCarrierFreqList { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType5 Decode(BitArrayInputStream input)
            {
                var type = new SystemInformationBlockType5();
                type.InitDefaults();
                var flag = false;
                flag = input.readBit() != 0;
                var stream = flag ? new BitMaskStream(input, 1) : new BitMaskStream(input, 0);
                type.interFreqCarrierFreqList = new List<InterFreqCarrierFreqInfo>();
                var num2 = 3;
                var num3 = input.readBits(num2) + 1;
                for (var i = 0; i < num3; i++)
                {
                    var item = InterFreqCarrierFreqInfo.PerDecoder.Instance.Decode(input);
                    type.interFreqCarrierFreqList.Add(item);
                }
                if (flag && stream.Read())
                {
                    var nBits = input.readBits(8);
                    type.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                return type;
            }
        }
    }
}