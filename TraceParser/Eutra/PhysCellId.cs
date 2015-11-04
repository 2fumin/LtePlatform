using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class PhysCellIdGERAN
    {
        public void InitDefaults()
        {
        }

        public string baseStationColourCode { get; set; }

        public string networkColourCode { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PhysCellIdGERAN Decode(BitArrayInputStream input)
            {
                PhysCellIdGERAN dgeran = new PhysCellIdGERAN();
                dgeran.InitDefaults();
                dgeran.networkColourCode = input.readBitString(3);
                dgeran.baseStationColourCode = input.readBitString(3);
                return dgeran;
            }
        }
    }

    [Serializable]
    public class PhysCellIdRange
    {
        public void InitDefaults()
        {
        }

        public range_Enum? range { get; set; }

        public long start { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PhysCellIdRange Decode(BitArrayInputStream input)
            {
                PhysCellIdRange range = new PhysCellIdRange();
                range.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                range.start = input.readBits(9);
                if (stream.Read())
                {
                    const int nBits = 4;
                    range.range = (range_Enum)input.readBits(nBits);
                }
                return range;
            }
        }

        public enum range_Enum
        {
            n4,
            n8,
            n12,
            n16,
            n24,
            n32,
            n48,
            n64,
            n84,
            n96,
            n128,
            n168,
            n252,
            n504,
            spare2,
            spare1
        }
    }

    [Serializable]
    public class PhysCellIdRangeUTRA_FDD_r9
    {
        public void InitDefaults()
        {
        }

        public long? range_r9 { get; set; }

        public long start_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PhysCellIdRangeUTRA_FDD_r9 Decode(BitArrayInputStream input)
            {
                PhysCellIdRangeUTRA_FDD_r9 _r = new PhysCellIdRangeUTRA_FDD_r9();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                _r.start_r9 = input.readBits(9);
                if (stream.Read())
                {
                    _r.range_r9 = input.readBits(9) + 2;
                }
                return _r;
            }
        }
    }

}
