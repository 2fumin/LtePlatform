using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformationBlockType5_v9e0_IEs
    {
        public void InitDefaults()
        {
        }

        public List<InterFreqCarrierFreqInfo_v9e0> interFreqCarrierFreqList_v9e0 { get; set; }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        [Serializable]
        public class nonCriticalExtension_Type
        {
            public void InitDefaults()
            {
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public nonCriticalExtension_Type Decode(BitArrayInputStream input)
                {
                    var type = new nonCriticalExtension_Type();
                    type.InitDefaults();
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType5_v9e0_IEs Decode(BitArrayInputStream input)
            {
                var es = new SystemInformationBlockType5_v9e0_IEs();
                es.InitDefaults();
                var stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.interFreqCarrierFreqList_v9e0 = new List<InterFreqCarrierFreqInfo_v9e0>();
                    var nBits = 3;
                    var num3 = input.readBits(nBits) + 1;
                    for (var i = 0; i < num3; i++)
                    {
                        var item = InterFreqCarrierFreqInfo_v9e0.PerDecoder.Instance.Decode(input);
                        es.interFreqCarrierFreqList_v9e0.Add(item);
                    }
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = nonCriticalExtension_Type.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }
}