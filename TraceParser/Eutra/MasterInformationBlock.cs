using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class MasterInformationBlock
    {
        public void InitDefaults()
        {
        }

        public dl_Bandwidth_Enum dl_Bandwidth { get; set; }

        public PHICH_Config phich_Config { get; set; }

        public string spare { get; set; }

        public string systemFrameNumber { get; set; }

        public enum dl_Bandwidth_Enum
        {
            n6,
            n15,
            n25,
            n50,
            n75,
            n100
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MasterInformationBlock Decode(BitArrayInputStream input)
            {
                MasterInformationBlock block = new MasterInformationBlock();
                block.InitDefaults();
                const int nBits = 3;
                block.dl_Bandwidth = (dl_Bandwidth_Enum)input.readBits(nBits);
                block.phich_Config = PHICH_Config.PerDecoder.Instance.Decode(input);
                block.systemFrameNumber = input.readBitString(8);
                block.spare = input.readBitString(10);
                return block;
            }
        }
    }
}
