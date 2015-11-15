using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformationBlockType1_v890_IEs
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public SystemInformationBlockType1_v920_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType1_v890_IEs Decode(BitArrayInputStream input)
            {
                var es = new SystemInformationBlockType1_v890_IEs();
                es.InitDefaults();
                var stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    var nBits = input.readBits(8);
                    es.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = SystemInformationBlockType1_v920_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }
}