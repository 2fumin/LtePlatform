using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformationBlockType1_v9e0_IEs
    {
        public void InitDefaults()
        {
        }

        public long? freqBandIndicator_v9e0 { get; set; }

        public List<MultiBandInfo_v9e0> multiBandInfoList_v9e0 { get; set; }

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

            public SystemInformationBlockType1_v9e0_IEs Decode(BitArrayInputStream input)
            {
                var es = new SystemInformationBlockType1_v9e0_IEs();
                es.InitDefaults();
                var stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    es.freqBandIndicator_v9e0 = input.readBits(8) + 0x41;
                }
                if (stream.Read())
                {
                    es.multiBandInfoList_v9e0 = new List<MultiBandInfo_v9e0>();
                    const int nBits = 3;
                    var num3 = input.readBits(nBits) + 1;
                    for (var i = 0; i < num3; i++)
                    {
                        var item = MultiBandInfo_v9e0.PerDecoder.Instance.Decode(input);
                        es.multiBandInfoList_v9e0.Add(item);
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