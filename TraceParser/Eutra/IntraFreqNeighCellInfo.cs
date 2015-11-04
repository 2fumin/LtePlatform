using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class IntraFreqNeighCellInfo
    {
        public void InitDefaults()
        {
        }

        public long physCellId { get; set; }

        public Q_OffsetRange q_OffsetCell { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public IntraFreqNeighCellInfo Decode(BitArrayInputStream input)
            {
                IntraFreqNeighCellInfo info = new IntraFreqNeighCellInfo();
                info.InitDefaults();
                input.readBit();
                info.physCellId = input.readBits(9);
                const int nBits = 5;
                info.q_OffsetCell = (Q_OffsetRange)input.readBits(nBits);
                return info;
            }
        }
    }
}
