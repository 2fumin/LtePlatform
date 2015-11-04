using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CellSelectionInfo_v1130
    {
        public void InitDefaults()
        {
        }

        public long q_QualMinWB_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellSelectionInfo_v1130 Decode(BitArrayInputStream input)
            {
                CellSelectionInfo_v1130 _v = new CellSelectionInfo_v1130();
                _v.InitDefaults();
                _v.q_QualMinWB_r11 = input.readBits(5) + -34;
                return _v;
            }
        }
    }

    [Serializable]
    public class CellSelectionInfo_v920
    {
        public void InitDefaults()
        {
        }

        public long q_QualMin_r9 { get; set; }

        public long? q_QualMinOffset_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellSelectionInfo_v920 Decode(BitArrayInputStream input)
            {
                CellSelectionInfo_v920 _v = new CellSelectionInfo_v920();
                _v.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                _v.q_QualMin_r9 = input.readBits(5) + -34;
                if (stream.Read())
                {
                    _v.q_QualMinOffset_r9 = input.readBits(3) + 1;
                }
                return _v;
            }
        }
    }

    [Serializable]
    public class CandidateCellInfo_r10
    {
        public void InitDefaults()
        {
        }

        public long dl_CarrierFreq_r10 { get; set; }

        public long? dl_CarrierFreq_v1090 { get; set; }

        public long physCellId_r10 { get; set; }

        public long? rsrpResult_r10 { get; set; }

        public long? rsrqResult_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CandidateCellInfo_r10 Decode(BitArrayInputStream input)
            {
                CandidateCellInfo_r10 _r = new CandidateCellInfo_r10();
                _r.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 2);
                _r.physCellId_r10 = input.readBits(9);
                _r.dl_CarrierFreq_r10 = input.readBits(0x10);
                if (stream.Read())
                {
                    _r.rsrpResult_r10 = input.readBits(7);
                }
                if (stream.Read())
                {
                    _r.rsrqResult_r10 = input.readBits(6);
                }
                if (flag)
                {
                    BitMaskStream stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        _r.dl_CarrierFreq_v1090 = input.readBits(0x12) + 0x10000;
                    }
                }
                return _r;
            }
        }
    }

}
