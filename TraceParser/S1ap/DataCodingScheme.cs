using System;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class DataCodingScheme
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                return input.readBitString(8);
            }
        }
    }
}
