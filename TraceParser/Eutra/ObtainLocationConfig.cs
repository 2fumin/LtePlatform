using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class ObtainLocationConfig_r11
    {
        public void InitDefaults()
        {
        }

        public obtainLocation_r11_Enum? obtainLocation_r11 { get; set; }

        public enum obtainLocation_r11_Enum
        {
            setup
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ObtainLocationConfig_r11 Decode(BitArrayInputStream input)
            {
                ObtainLocationConfig_r11 _r = new ObtainLocationConfig_r11();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    const int nBits = 1;
                    _r.obtainLocation_r11 = (obtainLocation_r11_Enum)input.readBits(nBits);
                }
                return _r;
            }
        }
    }
}
