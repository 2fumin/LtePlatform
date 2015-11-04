using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class PowerPrefIndicationConfig_r11
    {
        public void InitDefaults()
        {
        }

        public object release { get; set; }

        public setup_Type setup { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PowerPrefIndicationConfig_r11 Decode(BitArrayInputStream input)
            {
                PowerPrefIndicationConfig_r11 _r = new PowerPrefIndicationConfig_r11();
                _r.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        return _r;

                    case 1:
                        _r.setup = setup_Type.PerDecoder.Instance.Decode(input);
                        return _r;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }

        [Serializable]
        public class setup_Type
        {
            public void InitDefaults()
            {
            }

            public powerPrefIndicationTimer_r11_Enum powerPrefIndicationTimer_r11 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public setup_Type Decode(BitArrayInputStream input)
                {
                    setup_Type type = new setup_Type();
                    type.InitDefaults();
                    const int nBits = 4;
                    type.powerPrefIndicationTimer_r11 = (powerPrefIndicationTimer_r11_Enum)input.readBits(nBits);
                    return type;
                }
            }

            public enum powerPrefIndicationTimer_r11_Enum
            {
                s0,
                s0dot5,
                s1,
                s2,
                s5,
                s10,
                s20,
                s30,
                s60,
                s90,
                s120,
                s300,
                s600,
                spare3,
                spare2,
                spare1
            }
        }
    }
}
