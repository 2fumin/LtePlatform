using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SIB8_PerPLMN_r11
    {
        public void InitDefaults()
        {
        }

        public parametersCDMA2000_r11_Type parametersCDMA2000_r11 { get; set; }

        public long plmn_Identity_r11 { get; set; }

        [Serializable]
        public class parametersCDMA2000_r11_Type
        {
            public void InitDefaults()
            {
            }

            public object defaultValue { get; set; }

            public ParametersCDMA2000_r11 explicitValue { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public parametersCDMA2000_r11_Type Decode(BitArrayInputStream input)
                {
                    parametersCDMA2000_r11_Type type = new parametersCDMA2000_r11_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.explicitValue = ParametersCDMA2000_r11.PerDecoder.Instance.Decode(input);
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

            public SIB8_PerPLMN_r11 Decode(BitArrayInputStream input)
            {
                SIB8_PerPLMN_r11 _r = new SIB8_PerPLMN_r11();
                _r.InitDefaults();
                _r.plmn_Identity_r11 = input.readBits(3) + 1;
                _r.parametersCDMA2000_r11 = parametersCDMA2000_r11_Type.PerDecoder.Instance.Decode(input);
                return _r;
            }
        }
    }
}
