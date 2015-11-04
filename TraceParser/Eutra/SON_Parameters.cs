using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SON_Parameters_r9
    {
        public void InitDefaults()
        {
        }

        public rach_Report_r9_Enum? rach_Report_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SON_Parameters_r9 Decode(BitArrayInputStream input)
            {
                SON_Parameters_r9 _r = new SON_Parameters_r9();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    const int nBits = 1;
                    _r.rach_Report_r9 = (rach_Report_r9_Enum)input.readBits(nBits);
                }
                return _r;
            }
        }

        public enum rach_Report_r9_Enum
        {
            supported
        }
    }
}
