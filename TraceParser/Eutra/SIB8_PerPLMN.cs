using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SIB8_PerPLMN_r11
    {
        public static void InitDefaults()
        {
        }

        public parametersCDMA2000_r11_Type parametersCDMA2000_r11 { get; set; }

        public long plmn_Identity_r11 { get; set; }

        [Serializable]
        public class parametersCDMA2000_r11_Type
        {
            public static void InitDefaults()
            {
            }

            public object defaultValue { get; set; }

            public ParametersCDMA2000_r11 explicitValue { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public parametersCDMA2000_r11_Type Decode(BitArrayInputStream input)
                {
                    var type = new parametersCDMA2000_r11_Type();
                    InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.explicitValue = ParametersCDMA2000_r11.PerDecoder.Decode(input);
                            return type;

                        case 1:
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public static SIB8_PerPLMN_r11 Decode(BitArrayInputStream input)
            {
                var _r = new SIB8_PerPLMN_r11();
                InitDefaults();
                _r.plmn_Identity_r11 = input.readBits(3) + 1;
                _r.parametersCDMA2000_r11 = parametersCDMA2000_r11_Type.PerDecoder.Instance.Decode(input);
                return _r;
            }
        }
    }
}
