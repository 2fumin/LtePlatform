using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CellsToAddMod
    {
        public void InitDefaults()
        {
        }

        public long cellIndex { get; set; }

        public Q_OffsetRange cellIndividualOffset { get; set; }

        public long physCellId { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellsToAddMod Decode(BitArrayInputStream input)
            {
                CellsToAddMod mod = new CellsToAddMod();
                mod.InitDefaults();
                mod.cellIndex = input.readBits(5) + 1;
                mod.physCellId = input.readBits(9);
                const int nBits = 5;
                mod.cellIndividualOffset = (Q_OffsetRange)input.readBits(nBits);
                return mod;
            }
        }
    }

    [Serializable]
    public class AltTTT_CellsToAddMod_r12
    {
        public void InitDefaults()
        {
        }

        public long cellIndex { get; set; }

        public PhysCellIdRange physCellIdRange { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public AltTTT_CellsToAddMod_r12 Decode(BitArrayInputStream input)
            {
                AltTTT_CellsToAddMod_r12 _r = new AltTTT_CellsToAddMod_r12();
                _r.InitDefaults();
                _r.cellIndex = input.readBits(5) + 1;
                _r.physCellIdRange = PhysCellIdRange.PerDecoder.Instance.Decode(input);
                return _r;
            }
        }
    }

    [Serializable]
    public class CellsToAddModCDMA2000
    {
        public void InitDefaults()
        {
        }

        public long cellIndex { get; set; }

        public long physCellId { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellsToAddModCDMA2000 Decode(BitArrayInputStream input)
            {
                CellsToAddModCDMA2000 dcdma = new CellsToAddModCDMA2000();
                dcdma.InitDefaults();
                dcdma.cellIndex = input.readBits(5) + 1;
                dcdma.physCellId = input.readBits(9);
                return dcdma;
            }
        }
    }

    [Serializable]
    public class CellsToAddModUTRA_FDD
    {
        public void InitDefaults()
        {
        }

        public long cellIndex { get; set; }

        public long physCellId { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellsToAddModUTRA_FDD Decode(BitArrayInputStream input)
            {
                CellsToAddModUTRA_FDD dutra_fdd = new CellsToAddModUTRA_FDD();
                dutra_fdd.InitDefaults();
                dutra_fdd.cellIndex = input.readBits(5) + 1;
                dutra_fdd.physCellId = input.readBits(9);
                return dutra_fdd;
            }
        }
    }

    [Serializable]
    public class CellsToAddModUTRA_TDD
    {
        public void InitDefaults()
        {
        }

        public long cellIndex { get; set; }

        public long physCellId { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellsToAddModUTRA_TDD Decode(BitArrayInputStream input)
            {
                CellsToAddModUTRA_TDD dutra_tdd = new CellsToAddModUTRA_TDD();
                dutra_tdd.InitDefaults();
                dutra_tdd.cellIndex = input.readBits(5) + 1;
                dutra_tdd.physCellId = input.readBits(7);
                return dutra_tdd;
            }
        }
    }

    [Serializable]
    public class BlackCellsToAddMod
    {
        public void InitDefaults()
        {
        }

        public long cellIndex { get; set; }

        public PhysCellIdRange physCellIdRange { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public BlackCellsToAddMod Decode(BitArrayInputStream input)
            {
                BlackCellsToAddMod mod = new BlackCellsToAddMod();
                mod.InitDefaults();
                mod.cellIndex = input.readBits(5) + 1;
                mod.physCellIdRange = PhysCellIdRange.PerDecoder.Instance.Decode(input);
                return mod;
            }
        }
    }

    [Serializable]
    public class SCellToAddMod_r10
    {
        public void InitDefaults()
        {
        }

        public cellIdentification_r10_Type cellIdentification_r10 { get; set; }

        public long? dl_CarrierFreq_v1090 { get; set; }

        public RadioResourceConfigCommonSCell_r10 radioResourceConfigCommonSCell_r10 { get; set; }

        public RadioResourceConfigDedicatedSCell_r10 radioResourceConfigDedicatedSCell_r10 { get; set; }

        public long sCellIndex_r10 { get; set; }

        [Serializable]
        public class cellIdentification_r10_Type
        {
            public void InitDefaults()
            {
            }

            public long dl_CarrierFreq_r10 { get; set; }

            public long physCellId_r10 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public cellIdentification_r10_Type Decode(BitArrayInputStream input)
                {
                    cellIdentification_r10_Type type = new cellIdentification_r10_Type();
                    type.InitDefaults();
                    type.physCellId_r10 = input.readBits(9);
                    type.dl_CarrierFreq_r10 = input.readBits(0x10);
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SCellToAddMod_r10 Decode(BitArrayInputStream input)
            {
                SCellToAddMod_r10 _r = new SCellToAddMod_r10();
                _r.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 3);
                _r.sCellIndex_r10 = input.readBits(3) + 1;
                if (stream.Read())
                {
                    _r.cellIdentification_r10 = cellIdentification_r10_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    _r.radioResourceConfigCommonSCell_r10 = RadioResourceConfigCommonSCell_r10.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    _r.radioResourceConfigDedicatedSCell_r10 = RadioResourceConfigDedicatedSCell_r10.PerDecoder.Instance.Decode(input);
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
