using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformationBlockType5_v8h0_IEs
    {
        public void InitDefaults()
        {
        }

        public List<InterFreqCarrierFreqInfo_v8h0> interFreqCarrierFreqList_v8h0 { get; set; }

        public SystemInformationBlockType5_v9e0_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType5_v8h0_IEs Decode(BitArrayInputStream input)
            {
                var es = new SystemInformationBlockType5_v8h0_IEs();
                es.InitDefaults();
                var stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.interFreqCarrierFreqList_v8h0 = new List<InterFreqCarrierFreqInfo_v8h0>();
                    var nBits = 3;
                    var num3 = input.readBits(nBits) + 1;
                    for (var i = 0; i < num3; i++)
                    {
                        var item = InterFreqCarrierFreqInfo_v8h0.PerDecoder.Instance.Decode(input);
                        es.interFreqCarrierFreqList_v8h0.Add(item);
                    }
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = SystemInformationBlockType5_v9e0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }
}