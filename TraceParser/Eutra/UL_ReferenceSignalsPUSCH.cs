using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class UL_ReferenceSignalsPUSCH
    {
        public void InitDefaults()
        {
        }

        public long cyclicShift { get; set; }

        public long groupAssignmentPUSCH { get; set; }

        public bool groupHoppingEnabled { get; set; }

        public bool sequenceHoppingEnabled { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UL_ReferenceSignalsPUSCH Decode(BitArrayInputStream input)
            {
                UL_ReferenceSignalsPUSCH spusch = new UL_ReferenceSignalsPUSCH();
                spusch.InitDefaults();
                spusch.groupHoppingEnabled = input.readBit() == 1;
                spusch.groupAssignmentPUSCH = input.readBits(5);
                spusch.sequenceHoppingEnabled = input.readBit() == 1;
                spusch.cyclicShift = input.readBits(3);
                return spusch;
            }
        }
    }
}
