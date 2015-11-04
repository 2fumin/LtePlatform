using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class EAB_Config_r11
    {
        public void InitDefaults()
        {
        }

        public string eab_BarringBitmap_r11 { get; set; }

        public eab_Category_r11_Enum eab_Category_r11 { get; set; }

        public enum eab_Category_r11_Enum
        {
            a,
            b,
            c
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public EAB_Config_r11 Decode(BitArrayInputStream input)
            {
                EAB_Config_r11 _r = new EAB_Config_r11();
                _r.InitDefaults();
                int nBits = 2;
                _r.eab_Category_r11 = (eab_Category_r11_Enum)input.readBits(nBits);
                _r.eab_BarringBitmap_r11 = input.readBitString(10);
                return _r;
            }
        }
    }

    [Serializable]
    public class EAB_ConfigPLMN_r11
    {
        public void InitDefaults()
        {
        }

        public EAB_Config_r11 eab_Config_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public EAB_ConfigPLMN_r11 Decode(BitArrayInputStream input)
            {
                EAB_ConfigPLMN_r11 _r = new EAB_ConfigPLMN_r11();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    _r.eab_Config_r11 = EAB_Config_r11.PerDecoder.Instance.Decode(input);
                }
                return _r;
            }
        }
    }

}
