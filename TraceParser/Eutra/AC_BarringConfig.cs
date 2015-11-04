using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class AC_BarringConfig
    {
        public void InitDefaults()
        {
        }

        public ac_BarringFactor_Enum ac_BarringFactor { get; set; }

        public string ac_BarringForSpecialAC { get; set; }

        public ac_BarringTime_Enum ac_BarringTime { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public AC_BarringConfig Decode(BitArrayInputStream input)
            {
                AC_BarringConfig config = new AC_BarringConfig();
                config.InitDefaults();
                int nBits = 4;
                config.ac_BarringFactor = (ac_BarringFactor_Enum)input.readBits(nBits);
                nBits = 3;
                config.ac_BarringTime = (ac_BarringTime_Enum)input.readBits(nBits);
                config.ac_BarringForSpecialAC = input.readBitString(5);
                return config;
            }
        }
    }

    [Serializable]
    public class AC_BarringConfig1XRTT_r9
    {
        public void InitDefaults()
        {
        }

        public long ac_Barring0to9_r9 { get; set; }

        public long ac_Barring10_r9 { get; set; }

        public long ac_Barring11_r9 { get; set; }

        public long ac_Barring12_r9 { get; set; }

        public long ac_Barring13_r9 { get; set; }

        public long ac_Barring14_r9 { get; set; }

        public long ac_Barring15_r9 { get; set; }

        public long ac_BarringEmg_r9 { get; set; }

        public long ac_BarringMsg_r9 { get; set; }

        public long ac_BarringReg_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public AC_BarringConfig1XRTT_r9 Decode(BitArrayInputStream input)
            {
                AC_BarringConfig1XRTT_r9 _r = new AC_BarringConfig1XRTT_r9();
                _r.InitDefaults();
                _r.ac_Barring0to9_r9 = input.readBits(6);
                _r.ac_Barring10_r9 = input.readBits(3);
                _r.ac_Barring11_r9 = input.readBits(3);
                _r.ac_Barring12_r9 = input.readBits(3);
                _r.ac_Barring13_r9 = input.readBits(3);
                _r.ac_Barring14_r9 = input.readBits(3);
                _r.ac_Barring15_r9 = input.readBits(3);
                _r.ac_BarringMsg_r9 = input.readBits(3);
                _r.ac_BarringReg_r9 = input.readBits(3);
                _r.ac_BarringEmg_r9 = input.readBits(3);
                return _r;
            }
        }
    }
}
