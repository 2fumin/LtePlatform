using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class FreqPriorityEUTRA
    {
        public void InitDefaults()
        {
        }

        public long carrierFreq { get; set; }

        public long cellReselectionPriority { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public FreqPriorityEUTRA Decode(BitArrayInputStream input)
            {
                FreqPriorityEUTRA yeutra = new FreqPriorityEUTRA();
                yeutra.InitDefaults();
                yeutra.carrierFreq = input.readBits(0x10);
                yeutra.cellReselectionPriority = input.readBits(3);
                return yeutra;
            }
        }
    }

    [Serializable]
    public class FreqPriorityEUTRA_v9e0
    {
        public void InitDefaults()
        {
        }

        public long? carrierFreq_v9e0 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public FreqPriorityEUTRA_v9e0 Decode(BitArrayInputStream input)
            {
                FreqPriorityEUTRA_v9e0 _ve = new FreqPriorityEUTRA_v9e0();
                _ve.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    _ve.carrierFreq_v9e0 = input.readBits(0x12) + 0x10000;
                }
                return _ve;
            }
        }
    }

    [Serializable]
    public class FreqPriorityUTRA_FDD
    {
        public void InitDefaults()
        {
        }

        public long carrierFreq { get; set; }

        public long cellReselectionPriority { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public FreqPriorityUTRA_FDD Decode(BitArrayInputStream input)
            {
                FreqPriorityUTRA_FDD yutra_fdd = new FreqPriorityUTRA_FDD();
                yutra_fdd.InitDefaults();
                yutra_fdd.carrierFreq = input.readBits(14);
                yutra_fdd.cellReselectionPriority = input.readBits(3);
                return yutra_fdd;
            }
        }
    }

    [Serializable]
    public class FreqPriorityUTRA_TDD
    {
        public void InitDefaults()
        {
        }

        public long carrierFreq { get; set; }

        public long cellReselectionPriority { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public FreqPriorityUTRA_TDD Decode(BitArrayInputStream input)
            {
                FreqPriorityUTRA_TDD yutra_tdd = new FreqPriorityUTRA_TDD();
                yutra_tdd.InitDefaults();
                yutra_tdd.carrierFreq = input.readBits(14);
                yutra_tdd.cellReselectionPriority = input.readBits(3);
                return yutra_tdd;
            }
        }
    }

    [Serializable]
    public class FreqsPriorityGERAN
    {
        public void InitDefaults()
        {
        }

        public CarrierFreqsGERAN carrierFreqs { get; set; }

        public long cellReselectionPriority { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public FreqsPriorityGERAN Decode(BitArrayInputStream input)
            {
                FreqsPriorityGERAN ygeran = new FreqsPriorityGERAN();
                ygeran.InitDefaults();
                ygeran.carrierFreqs = CarrierFreqsGERAN.PerDecoder.Instance.Decode(input);
                ygeran.cellReselectionPriority = input.readBits(3);
                return ygeran;
            }
        }
    }

}
