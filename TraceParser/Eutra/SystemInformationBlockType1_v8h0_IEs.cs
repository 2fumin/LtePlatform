using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformationBlockType1_v8h0_IEs
    {
        public void InitDefaults()
        {
        }

        public List<long> multiBandInfoList { get; set; }

        public SystemInformationBlockType1_v9e0_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType1_v8h0_IEs Decode(BitArrayInputStream input)
            {
                var es = new SystemInformationBlockType1_v8h0_IEs();
                es.InitDefaults();
                var stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.multiBandInfoList = new List<long>();
                    const int nBits = 3;
                    var num3 = input.readBits(nBits) + 1;
                    for (var i = 0; i < num3; i++)
                    {
                        long item = input.readBits(6) + 1;
                        es.multiBandInfoList.Add(item);
                    }
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = SystemInformationBlockType1_v9e0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }
}