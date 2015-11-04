using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class RLF_Report_r9
    {
        public void InitDefaults()
        {
        }

        public basicFields_r11_Type basicFields_r11 { get; set; }

        public connectionFailureType_r10_Enum? connectionFailureType_r10 { get; set; }

        public failedPCellId_r10_Type failedPCellId_r10 { get; set; }

        public failedPCellId_v1090_Type failedPCellId_v1090 { get; set; }

        public LocationInfo_r10 locationInfo_r10 { get; set; }

        public measResultLastServCell_r9_Type measResultLastServCell_r9 { get; set; }

        public measResultNeighCells_r9_Type measResultNeighCells_r9 { get; set; }

        public CellGlobalIdEUTRA previousPCellId_r10 { get; set; }

        public previousUTRA_CellId_r11_Type previousUTRA_CellId_r11 { get; set; }

        public CellGlobalIdEUTRA reestablishmentCellId_r10 { get; set; }

        public selectedUTRA_CellId_r11_Type selectedUTRA_CellId_r11 { get; set; }

        public long? timeConnFailure_r10 { get; set; }

        [Serializable]
        public class basicFields_r11_Type
        {
            public void InitDefaults()
            {
            }

            public string c_RNTI_r11 { get; set; }

            public rlf_Cause_r11_Enum rlf_Cause_r11 { get; set; }

            public long timeSinceFailure_r11 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public basicFields_r11_Type Decode(BitArrayInputStream input)
                {
                    basicFields_r11_Type type = new basicFields_r11_Type();
                    type.InitDefaults();
                    type.c_RNTI_r11 = input.readBitString(0x10);
                    int nBits = 2;
                    type.rlf_Cause_r11 = (rlf_Cause_r11_Enum)input.readBits(nBits);
                    type.timeSinceFailure_r11 = input.readBits(0x12);
                    return type;
                }
            }

            public enum rlf_Cause_r11_Enum
            {
                t310_Expiry,
                randomAccessProblem,
                rlc_MaxNumRetx,
                spare1
            }
        }

        public enum connectionFailureType_r10_Enum
        {
            rlf,
            hof
        }

        [Serializable]
        public class failedPCellId_r10_Type
        {
            public void InitDefaults()
            {
            }

            public CellGlobalIdEUTRA cellGlobalId_r10 { get; set; }

            public pci_arfcn_r10_Type pci_arfcn_r10 { get; set; }

            [Serializable]
            public class pci_arfcn_r10_Type
            {
                public void InitDefaults()
                {
                }

                public long carrierFreq_r10 { get; set; }

                public long physCellId_r10 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public pci_arfcn_r10_Type Decode(BitArrayInputStream input)
                    {
                        pci_arfcn_r10_Type type = new pci_arfcn_r10_Type();
                        type.InitDefaults();
                        type.physCellId_r10 = input.readBits(9);
                        type.carrierFreq_r10 = input.readBits(0x10);
                        return type;
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public failedPCellId_r10_Type Decode(BitArrayInputStream input)
                {
                    failedPCellId_r10_Type type = new failedPCellId_r10_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.cellGlobalId_r10 = CellGlobalIdEUTRA.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.pci_arfcn_r10 = pci_arfcn_r10_Type.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(base.GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        [Serializable]
        public class failedPCellId_v1090_Type
        {
            public void InitDefaults()
            {
            }

            public long carrierFreq_v1090 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public failedPCellId_v1090_Type Decode(BitArrayInputStream input)
                {
                    failedPCellId_v1090_Type type = new failedPCellId_v1090_Type();
                    type.InitDefaults();
                    type.carrierFreq_v1090 = input.readBits(0x12) + 0x10000;
                    return type;
                }
            }
        }

        [Serializable]
        public class measResultLastServCell_r9_Type
        {
            public void InitDefaults()
            {
            }

            public long rsrpResult_r9 { get; set; }

            public long? rsrqResult_r9 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public measResultLastServCell_r9_Type Decode(BitArrayInputStream input)
                {
                    measResultLastServCell_r9_Type type = new measResultLastServCell_r9_Type();
                    type.InitDefaults();
                    bool flag = false;
                    BitMaskStream stream = flag ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                    type.rsrpResult_r9 = input.readBits(7);
                    if (stream.Read())
                    {
                        type.rsrqResult_r9 = input.readBits(6);
                    }
                    return type;
                }
            }
        }

        [Serializable]
        public class measResultNeighCells_r9_Type
        {
            public void InitDefaults()
            {
            }

            public List<MeasResult2EUTRA_r9> measResultListEUTRA_r9 { get; set; }

            public List<MeasResultGERAN> measResultListGERAN_r9 { get; set; }

            public List<MeasResult2UTRA_r9> measResultListUTRA_r9 { get; set; }

            public List<MeasResult2CDMA2000_r9> measResultsCDMA2000_r9 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public measResultNeighCells_r9_Type Decode(BitArrayInputStream input)
                {
                    int num2;
                    measResultNeighCells_r9_Type type = new measResultNeighCells_r9_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 4);
                    if (stream.Read())
                    {
                        type.measResultListEUTRA_r9 = new List<MeasResult2EUTRA_r9>();
                        num2 = 3;
                        int num3 = input.readBits(num2) + 1;
                        for (int i = 0; i < num3; i++)
                        {
                            MeasResult2EUTRA_r9 item = MeasResult2EUTRA_r9.PerDecoder.Instance.Decode(input);
                            type.measResultListEUTRA_r9.Add(item);
                        }
                    }
                    if (stream.Read())
                    {
                        type.measResultListUTRA_r9 = new List<MeasResult2UTRA_r9>();
                        num2 = 3;
                        int num5 = input.readBits(num2) + 1;
                        for (int j = 0; j < num5; j++)
                        {
                            MeasResult2UTRA_r9 _r2 = MeasResult2UTRA_r9.PerDecoder.Instance.Decode(input);
                            type.measResultListUTRA_r9.Add(_r2);
                        }
                    }
                    if (stream.Read())
                    {
                        type.measResultListGERAN_r9 = new List<MeasResultGERAN>();
                        num2 = 3;
                        int num7 = input.readBits(num2) + 1;
                        for (int k = 0; k < num7; k++)
                        {
                            MeasResultGERAN tgeran = MeasResultGERAN.PerDecoder.Instance.Decode(input);
                            type.measResultListGERAN_r9.Add(tgeran);
                        }
                    }
                    if (stream.Read())
                    {
                        type.measResultsCDMA2000_r9 = new List<MeasResult2CDMA2000_r9>();
                        num2 = 3;
                        int num9 = input.readBits(num2) + 1;
                        for (int m = 0; m < num9; m++)
                        {
                            MeasResult2CDMA2000_r9 _r3 = MeasResult2CDMA2000_r9.PerDecoder.Instance.Decode(input);
                            type.measResultsCDMA2000_r9.Add(_r3);
                        }
                    }
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RLF_Report_r9 Decode(BitArrayInputStream input)
            {
                BitMaskStream stream2;
                RLF_Report_r9 _r = new RLF_Report_r9();
                _r.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 1);
                _r.measResultLastServCell_r9 = measResultLastServCell_r9_Type.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    _r.measResultNeighCells_r9 = measResultNeighCells_r9_Type.PerDecoder.Instance.Decode(input);
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 6);
                    if (stream2.Read())
                    {
                        _r.locationInfo_r10 = LocationInfo_r10.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        _r.failedPCellId_r10 = failedPCellId_r10_Type.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        _r.reestablishmentCellId_r10 = CellGlobalIdEUTRA.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        _r.timeConnFailure_r10 = input.readBits(10);
                    }
                    if (stream2.Read())
                    {
                        int nBits = 1;
                        _r.connectionFailureType_r10 = (connectionFailureType_r10_Enum)input.readBits(nBits);
                    }
                    if (stream2.Read())
                    {
                        _r.previousPCellId_r10 = CellGlobalIdEUTRA.PerDecoder.Instance.Decode(input);
                    }
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        _r.failedPCellId_v1090 = failedPCellId_v1090_Type.PerDecoder.Instance.Decode(input);
                    }
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 3);
                    if (stream2.Read())
                    {
                        _r.basicFields_r11 = basicFields_r11_Type.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        _r.previousUTRA_CellId_r11 = previousUTRA_CellId_r11_Type.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        _r.selectedUTRA_CellId_r11 = selectedUTRA_CellId_r11_Type.PerDecoder.Instance.Decode(input);
                    }
                }
                return _r;
            }
        }

        [Serializable]
        public class previousUTRA_CellId_r11_Type
        {
            public void InitDefaults()
            {
            }

            public long carrierFreq_r11 { get; set; }

            public CellGlobalIdUTRA cellGlobalId_r11 { get; set; }

            public physCellId_r11_Type physCellId_r11 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public previousUTRA_CellId_r11_Type Decode(BitArrayInputStream input)
                {
                    previousUTRA_CellId_r11_Type type = new previousUTRA_CellId_r11_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 1);
                    type.carrierFreq_r11 = input.readBits(14);
                    type.physCellId_r11 = physCellId_r11_Type.PerDecoder.Instance.Decode(input);
                    if (stream.Read())
                    {
                        type.cellGlobalId_r11 = CellGlobalIdUTRA.PerDecoder.Instance.Decode(input);
                    }
                    return type;
                }
            }

            [Serializable]
            public class physCellId_r11_Type
            {
                public void InitDefaults()
                {
                }

                public long fdd_r11 { get; set; }

                public long tdd_r11 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public physCellId_r11_Type Decode(BitArrayInputStream input)
                    {
                        physCellId_r11_Type type = new physCellId_r11_Type();
                        type.InitDefaults();
                        switch (input.readBits(1))
                        {
                            case 0:
                                type.fdd_r11 = input.readBits(9);
                                return type;

                            case 1:
                                type.tdd_r11 = input.readBits(7);
                                return type;
                        }
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                }
            }
        }

        [Serializable]
        public class selectedUTRA_CellId_r11_Type
        {
            public void InitDefaults()
            {
            }

            public long carrierFreq_r11 { get; set; }

            public physCellId_r11_Type physCellId_r11 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public selectedUTRA_CellId_r11_Type Decode(BitArrayInputStream input)
                {
                    selectedUTRA_CellId_r11_Type type = new selectedUTRA_CellId_r11_Type();
                    type.InitDefaults();
                    type.carrierFreq_r11 = input.readBits(14);
                    type.physCellId_r11 = physCellId_r11_Type.PerDecoder.Instance.Decode(input);
                    return type;
                }
            }

            [Serializable]
            public class physCellId_r11_Type
            {
                public void InitDefaults()
                {
                }

                public long fdd_r11 { get; set; }

                public long tdd_r11 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public physCellId_r11_Type Decode(BitArrayInputStream input)
                    {
                        physCellId_r11_Type type = new physCellId_r11_Type();
                        type.InitDefaults();
                        switch (input.readBits(1))
                        {
                            case 0:
                                type.fdd_r11 = input.readBits(9);
                                return type;

                            case 1:
                                type.tdd_r11 = input.readBits(7);
                                return type;
                        }
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                }
            }
        }
    }

    [Serializable]
    public class RLF_Report_v9e0
    {
        public void InitDefaults()
        {
        }

        public List<MeasResult2EUTRA_v9e0> measResultListEUTRA_v9e0 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RLF_Report_v9e0 Decode(BitArrayInputStream input)
            {
                RLF_Report_v9e0 _ve = new RLF_Report_v9e0();
                _ve.InitDefaults();
                _ve.measResultListEUTRA_v9e0 = new List<MeasResult2EUTRA_v9e0>();
                int nBits = 3;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    MeasResult2EUTRA_v9e0 item = MeasResult2EUTRA_v9e0.PerDecoder.Instance.Decode(input);
                    _ve.measResultListEUTRA_v9e0.Add(item);
                }
                return _ve;
            }
        }
    }

    [Serializable]
    public class RLF_TimersAndConstants_r9
    {
        public void InitDefaults()
        {
        }

        public object release { get; set; }

        public setup_Type setup { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RLF_TimersAndConstants_r9 Decode(BitArrayInputStream input)
            {
                RLF_TimersAndConstants_r9 _r = new RLF_TimersAndConstants_r9();
                _r.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        return _r;

                    case 1:
                        _r.setup = setup_Type.PerDecoder.Instance.Decode(input);
                        return _r;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }

        [Serializable]
        public class setup_Type
        {
            public void InitDefaults()
            {
            }

            public n310_r9_Enum n310_r9 { get; set; }

            public n311_r9_Enum n311_r9 { get; set; }

            public t301_r9_Enum t301_r9 { get; set; }

            public t310_r9_Enum t310_r9 { get; set; }

            public t311_r9_Enum t311_r9 { get; set; }

            public enum n310_r9_Enum
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

            public enum n311_r9_Enum
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

                public setup_Type Decode(BitArrayInputStream input)
                {
                    setup_Type type = new setup_Type();
                    type.InitDefaults();
                    input.readBit();
                    int nBits = 3;
                    type.t301_r9 = (t301_r9_Enum)input.readBits(nBits);
                    nBits = 3;
                    type.t310_r9 = (t310_r9_Enum)input.readBits(nBits);
                    nBits = 3;
                    type.n310_r9 = (n310_r9_Enum)input.readBits(nBits);
                    nBits = 3;
                    type.t311_r9 = (t311_r9_Enum)input.readBits(nBits);
                    nBits = 3;
                    type.n311_r9 = (n311_r9_Enum)input.readBits(nBits);
                    return type;
                }
            }

            public enum t301_r9_Enum
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

            public enum t310_r9_Enum
            {
                ms0,
                ms50,
                ms100,
                ms200,
                ms500,
                ms1000,
                ms2000
            }

            public enum t311_r9_Enum
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

}
