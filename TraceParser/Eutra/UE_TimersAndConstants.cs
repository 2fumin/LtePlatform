using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class UE_TimersAndConstants
    {
        public void InitDefaults()
        {
        }

        public n310_Enum n310 { get; set; }

        public n311_Enum n311 { get; set; }

        public t300_Enum t300 { get; set; }

        public t301_Enum t301 { get; set; }

        public t310_Enum t310 { get; set; }

        public t311_Enum t311 { get; set; }

        public enum n310_Enum
        {
            n1,
            n2,
            n3,
            n4,
            n6,
            n8,
            n10,
            n20
        }

        public enum n311_Enum
        {
            n1,
            n2,
            n3,
            n4,
            n5,
            n6,
            n8,
            n10
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UE_TimersAndConstants Decode(BitArrayInputStream input)
            {
                UE_TimersAndConstants constants = new UE_TimersAndConstants();
                constants.InitDefaults();
                input.readBit();
                int nBits = 3;
                constants.t300 = (t300_Enum)input.readBits(nBits);
                nBits = 3;
                constants.t301 = (t301_Enum)input.readBits(nBits);
                nBits = 3;
                constants.t310 = (t310_Enum)input.readBits(nBits);
                nBits = 3;
                constants.n310 = (n310_Enum)input.readBits(nBits);
                nBits = 3;
                constants.t311 = (t311_Enum)input.readBits(nBits);
                nBits = 3;
                constants.n311 = (n311_Enum)input.readBits(nBits);
                return constants;
            }
        }

        public enum t300_Enum
        {
            ms100,
            ms200,
            ms300,
            ms400,
            ms600,
            ms1000,
            ms1500,
            ms2000
        }

        public enum t301_Enum
        {
            ms100,
            ms200,
            ms300,
            ms400,
            ms600,
            ms1000,
            ms1500,
            ms2000
        }

        public enum t310_Enum
        {
            ms0,
            ms50,
            ms100,
            ms200,
            ms500,
            ms1000,
            ms2000
        }

        public enum t311_Enum
        {
            ms1000,
            ms3000,
            ms5000,
            ms10000,
            ms15000,
            ms20000,
            ms30000
        }
    }
}
