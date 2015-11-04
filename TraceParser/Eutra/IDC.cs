using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class IDC_Config_r11
    {
        public void InitDefaults()
        {
        }

        public autonomousDenialParameters_r11_Type autonomousDenialParameters_r11 { get; set; }

        public idc_Indication_r11_Enum? idc_Indication_r11 { get; set; }

        [Serializable]
        public class autonomousDenialParameters_r11_Type
        {
            public void InitDefaults()
            {
            }

            public autonomousDenialSubframes_r11_Enum autonomousDenialSubframes_r11 { get; set; }

            public autonomousDenialValidity_r11_Enum autonomousDenialValidity_r11 { get; set; }

            public enum autonomousDenialSubframes_r11_Enum
            {
                n2,
                n5,
                n10,
                n15,
                n20,
                n30,
                spare2,
                spare1
            }

            public enum autonomousDenialValidity_r11_Enum
            {
                sf200,
                sf500,
                sf1000,
                sf2000,
                spare4,
                spare3,
                spare2,
                spare1
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public autonomousDenialParameters_r11_Type Decode(BitArrayInputStream input)
                {
                    autonomousDenialParameters_r11_Type type = new autonomousDenialParameters_r11_Type();
                    type.InitDefaults();
                    int nBits = 3;
                    type.autonomousDenialSubframes_r11 = (autonomousDenialSubframes_r11_Enum)input.readBits(nBits);
                    nBits = 3;
                    type.autonomousDenialValidity_r11 = (autonomousDenialValidity_r11_Enum)input.readBits(nBits);
                    return type;
                }
            }
        }

        public enum idc_Indication_r11_Enum
        {
            setup
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public IDC_Config_r11 Decode(BitArrayInputStream input)
            {
                IDC_Config_r11 _r = new IDC_Config_r11();
                _r.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    const int nBits = 1;
                    _r.idc_Indication_r11 = (idc_Indication_r11_Enum)input.readBits(nBits);
                }
                if (stream.Read())
                {
                    _r.autonomousDenialParameters_r11 = autonomousDenialParameters_r11_Type.PerDecoder.Instance.Decode(input);
                }
                return _r;
            }
        }
    }

    [Serializable]
    public class IDC_SubframePattern_r11
    {
        public void InitDefaults()
        {
        }

        public string subframePatternFDD_r11 { get; set; }

        public subframePatternTDD_r11_Type subframePatternTDD_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public IDC_SubframePattern_r11 Decode(BitArrayInputStream input)
            {
                IDC_SubframePattern_r11 _r = new IDC_SubframePattern_r11();
                _r.InitDefaults();
                input.readBit();
                switch (input.readBits(1))
                {
                    case 0:
                        _r.subframePatternFDD_r11 = input.readBitString(4);
                        return _r;

                    case 1:
                        _r.subframePatternTDD_r11 = subframePatternTDD_r11_Type.PerDecoder.Instance.Decode(input);
                        return _r;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }

        [Serializable]
        public class subframePatternTDD_r11_Type
        {
            public void InitDefaults()
            {
            }

            public string subframeConfig0_r11 { get; set; }

            public string subframeConfig1_5_r11 { get; set; }

            public string subframeConfig6_r11 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public subframePatternTDD_r11_Type Decode(BitArrayInputStream input)
                {
                    subframePatternTDD_r11_Type type = new subframePatternTDD_r11_Type();
                    type.InitDefaults();
                    switch (input.readBits(2))
                    {
                        case 0:
                            type.subframeConfig0_r11 = input.readBitString(70);
                            return type;

                        case 1:
                            type.subframeConfig1_5_r11 = input.readBitString(10);
                            return type;

                        case 2:
                            type.subframeConfig6_r11 = input.readBitString(60);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }
    }

}
