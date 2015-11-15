using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformationBlockType14_r11
    {
        public void InitDefaults()
        {
        }

        public eab_Param_r11_Type eab_Param_r11 { get; set; }

        public string lateNonCriticalExtension { get; set; }

        [Serializable]
        public class eab_Param_r11_Type
        {
            public void InitDefaults()
            {
            }

            public EAB_Config_r11 eab_Common_r11 { get; set; }

            public List<EAB_ConfigPLMN_r11> eab_PerPLMN_List_r11 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public eab_Param_r11_Type Decode(BitArrayInputStream input)
                {
                    var type = new eab_Param_r11_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.eab_Common_r11 = EAB_Config_r11.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                        {
                            type.eab_PerPLMN_List_r11 = new List<EAB_ConfigPLMN_r11>();
                            var nBits = 3;
                            var num4 = input.readBits(nBits) + 1;
                            for (var i = 0; i < num4; i++)
                            {
                                var item = EAB_ConfigPLMN_r11.PerDecoder.Instance.Decode(input);
                                type.eab_PerPLMN_List_r11.Add(item);
                            }
                            return type;
                        }
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType14_r11 Decode(BitArrayInputStream input)
            {
                var _r = new SystemInformationBlockType14_r11();
                _r.InitDefaults();
                var stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    _r.eab_Param_r11 = eab_Param_r11_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    var nBits = input.readBits(8);
                    _r.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                return _r;
            }
        }
    }
}