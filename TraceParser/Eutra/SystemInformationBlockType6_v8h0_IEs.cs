using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformationBlockType6_v8h0_IEs
    {
        public void InitDefaults()
        {
        }

        public List<CarrierFreqInfoUTRA_FDD_v8h0> carrierFreqListUTRA_FDD_v8h0 { get; set; }

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

            public SystemInformationBlockType6_v8h0_IEs Decode(BitArrayInputStream input)
            {
                var es = new SystemInformationBlockType6_v8h0_IEs();
                es.InitDefaults();
                var stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.carrierFreqListUTRA_FDD_v8h0 = new List<CarrierFreqInfoUTRA_FDD_v8h0>();
                    var nBits = 4;
                    var num3 = input.readBits(nBits) + 1;
                    for (var i = 0; i < num3; i++)
                    {
                        var item = CarrierFreqInfoUTRA_FDD_v8h0.PerDecoder.Instance.Decode(input);
                        es.carrierFreqListUTRA_FDD_v8h0.Add(item);
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