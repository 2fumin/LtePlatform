using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class MeasObjectCDMA2000
    {
        public void InitDefaults()
        {
            offsetFreq = 0L;
        }

        public CarrierFreqCDMA2000 carrierFreq { get; set; }

        public CDMA2000_Type cdma2000_Type { get; set; }

        public long? cellForWhichToReportCGI { get; set; }

        public List<CellsToAddModCDMA2000> cellsToAddModList { get; set; }

        public List<long> cellsToRemoveList { get; set; }

        public long offsetFreq { get; set; }

        public long? searchWindowSize { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasObjectCDMA2000 Decode(BitArrayInputStream input)
            {
                MeasObjectCDMA2000 tcdma = new MeasObjectCDMA2000();
                tcdma.InitDefaults();
                input.readBit();
                BitMaskStream stream = new BitMaskStream(input, 1);
                BitMaskStream stream2 = new BitMaskStream(input, 4);
                int nBits = 1;
                tcdma.cdma2000_Type = (CDMA2000_Type)input.readBits(nBits);
                tcdma.carrierFreq = CarrierFreqCDMA2000.PerDecoder.Instance.Decode(input);
                if (stream2.Read())
                {
                    tcdma.searchWindowSize = input.readBits(4);
                }
                if (stream.Read())
                {
                    tcdma.offsetFreq = input.readBits(5) + -15;
                }
                if (stream2.Read())
                {
                    tcdma.cellsToRemoveList = new List<long>();
                    nBits = 5;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        long item = input.readBits(5) + 1;
                        tcdma.cellsToRemoveList.Add(item);
                    }
                }
                if (stream2.Read())
                {
                    tcdma.cellsToAddModList = new List<CellsToAddModCDMA2000>();
                    nBits = 5;
                    int num6 = input.readBits(nBits) + 1;
                    for (int j = 0; j < num6; j++)
                    {
                        CellsToAddModCDMA2000 dcdma = CellsToAddModCDMA2000.PerDecoder.Instance.Decode(input);
                        tcdma.cellsToAddModList.Add(dcdma);
                    }
                }
                if (stream2.Read())
                {
                    tcdma.cellForWhichToReportCGI = input.readBits(9);
                }
                return tcdma;
            }
        }
    }

    [Serializable]
    public class MeasObjectEUTRA
    {
        public void InitDefaults()
        {
            offsetFreq = Q_OffsetRange.dB0;
        }

        public AllowedMeasBandwidth allowedMeasBandwidth { get; set; }

        public List<AltTTT_CellsToAddMod_r12> altTTT_CellsToAddModList_r12 { get; set; }

        public List<long> altTTT_CellsToRemoveList_r12 { get; set; }

        public List<BlackCellsToAddMod> blackCellsToAddModList { get; set; }

        public List<long> blackCellsToRemoveList { get; set; }

        public long carrierFreq { get; set; }

        public long? cellForWhichToReportCGI { get; set; }

        public List<CellsToAddMod> cellsToAddModList { get; set; }

        public List<long> cellsToRemoveList { get; set; }

        public MeasCycleSCell_r10? measCycleSCell_r10 { get; set; }

        public MeasSubframePatternConfigNeigh_r10 measSubframePatternConfigNeigh_r10 { get; set; }

        public string neighCellConfig { get; set; }

        public Q_OffsetRange offsetFreq { get; set; }

        public bool presenceAntennaPort1 { get; set; }

        public bool? widebandRSRQ_Meas_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasObjectEUTRA Decode(BitArrayInputStream input)
            {
                int num3;
                int num4;
                long num5;
                BitMaskStream stream3;
                MeasObjectEUTRA teutra = new MeasObjectEUTRA();
                teutra.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 1);
                BitMaskStream stream2 = new BitMaskStream(input, 5);
                teutra.carrierFreq = input.readBits(0x10);
                int nBits = 3;
                teutra.allowedMeasBandwidth = (AllowedMeasBandwidth)input.readBits(nBits);
                teutra.presenceAntennaPort1 = input.readBit() == 1;
                teutra.neighCellConfig = input.readBitString(2);
                if (stream.Read())
                {
                    nBits = 5;
                    teutra.offsetFreq = (Q_OffsetRange)input.readBits(nBits);
                }
                if (stream2.Read())
                {
                    teutra.cellsToRemoveList = new List<long>();
                    nBits = 5;
                    num3 = input.readBits(nBits) + 1;
                    for (num4 = 0; num4 < num3; num4++)
                    {
                        num5 = input.readBits(5) + 1;
                        teutra.cellsToRemoveList.Add(num5);
                    }
                }
                if (stream2.Read())
                {
                    teutra.cellsToAddModList = new List<CellsToAddMod>();
                    nBits = 5;
                    int num6 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num6; i++)
                    {
                        CellsToAddMod item = CellsToAddMod.PerDecoder.Instance.Decode(input);
                        teutra.cellsToAddModList.Add(item);
                    }
                }
                if (stream2.Read())
                {
                    teutra.blackCellsToRemoveList = new List<long>();
                    nBits = 5;
                    num3 = input.readBits(nBits) + 1;
                    for (num4 = 0; num4 < num3; num4++)
                    {
                        num5 = input.readBits(5) + 1;
                        teutra.blackCellsToRemoveList.Add(num5);
                    }
                }
                if (stream2.Read())
                {
                    teutra.blackCellsToAddModList = new List<BlackCellsToAddMod>();
                    nBits = 5;
                    int num8 = input.readBits(nBits) + 1;
                    for (int j = 0; j < num8; j++)
                    {
                        BlackCellsToAddMod mod2 = BlackCellsToAddMod.PerDecoder.Instance.Decode(input);
                        teutra.blackCellsToAddModList.Add(mod2);
                    }
                }
                if (stream2.Read())
                {
                    teutra.cellForWhichToReportCGI = input.readBits(9);
                }
                if (flag)
                {
                    stream3 = new BitMaskStream(input, 2);
                    if (stream3.Read())
                    {
                        nBits = 3;
                        teutra.measCycleSCell_r10 = (MeasCycleSCell_r10)input.readBits(nBits);
                    }
                    if (stream3.Read())
                    {
                        teutra.measSubframePatternConfigNeigh_r10 = MeasSubframePatternConfigNeigh_r10.PerDecoder.Instance.Decode(input);
                    }
                }
                if (flag)
                {
                    stream3 = new BitMaskStream(input, 1);
                    if (stream3.Read())
                    {
                        teutra.widebandRSRQ_Meas_r11 = input.readBit() == 1;
                    }
                }
                if (flag)
                {
                    stream3 = new BitMaskStream(input, 2);
                    if (stream3.Read())
                    {
                        teutra.altTTT_CellsToRemoveList_r12 = new List<long>();
                        nBits = 5;
                        num3 = input.readBits(nBits) + 1;
                        for (num4 = 0; num4 < num3; num4++)
                        {
                            num5 = input.readBits(5) + 1;
                            teutra.altTTT_CellsToRemoveList_r12.Add(num5);
                        }
                    }
                    if (!stream3.Read())
                    {
                        return teutra;
                    }
                    teutra.altTTT_CellsToAddModList_r12 = new List<AltTTT_CellsToAddMod_r12>();
                    nBits = 5;
                    int num10 = input.readBits(nBits) + 1;
                    for (int k = 0; k < num10; k++)
                    {
                        AltTTT_CellsToAddMod_r12 _r = AltTTT_CellsToAddMod_r12.PerDecoder.Instance.Decode(input);
                        teutra.altTTT_CellsToAddModList_r12.Add(_r);
                    }
                }
                return teutra;
            }
        }
    }

    [Serializable]
    public class MeasObjectEUTRA_v9e0
    {
        public void InitDefaults()
        {
        }

        public long carrierFreq_v9e0 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasObjectEUTRA_v9e0 Decode(BitArrayInputStream input)
            {
                MeasObjectEUTRA_v9e0 _ve = new MeasObjectEUTRA_v9e0();
                _ve.InitDefaults();
                _ve.carrierFreq_v9e0 = input.readBits(0x12) + 0x10000;
                return _ve;
            }
        }
    }

    [Serializable]
    public class MeasObjectGERAN
    {
        public void InitDefaults()
        {
            offsetFreq = 0L;
            ncc_Permitted = Util.to_bitstring(0xff);
        }

        public CarrierFreqsGERAN carrierFreqs { get; set; }

        public PhysCellIdGERAN cellForWhichToReportCGI { get; set; }

        public string ncc_Permitted { get; set; }

        public long offsetFreq { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasObjectGERAN Decode(BitArrayInputStream input)
            {
                MeasObjectGERAN tgeran = new MeasObjectGERAN();
                tgeran.InitDefaults();
                input.readBit();
                BitMaskStream stream = new BitMaskStream(input, 2);
                BitMaskStream stream2 = new BitMaskStream(input, 1);
                tgeran.carrierFreqs = CarrierFreqsGERAN.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    tgeran.offsetFreq = input.readBits(5) + -15;
                }
                if (stream.Read())
                {
                    tgeran.ncc_Permitted = input.readBitString(8);
                }
                if (stream2.Read())
                {
                    tgeran.cellForWhichToReportCGI = PhysCellIdGERAN.PerDecoder.Instance.Decode(input);
                }
                return tgeran;
            }
        }
    }

    [Serializable]
    public class MeasObjectToAddMod
    {
        public void InitDefaults()
        {
        }

        public measObject_Type measObject { get; set; }

        public long measObjectId { get; set; }

        [Serializable]
        public class measObject_Type
        {
            public void InitDefaults()
            {
            }

            public MeasObjectCDMA2000 measObjectCDMA2000 { get; set; }

            public MeasObjectEUTRA measObjectEUTRA { get; set; }

            public MeasObjectGERAN measObjectGERAN { get; set; }

            public MeasObjectUTRA measObjectUTRA { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public measObject_Type Decode(BitArrayInputStream input)
                {
                    measObject_Type type = new measObject_Type();
                    type.InitDefaults();
                    input.readBit();
                    switch (input.readBits(2))
                    {
                        case 0:
                            type.measObjectEUTRA = MeasObjectEUTRA.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.measObjectUTRA = MeasObjectUTRA.PerDecoder.Instance.Decode(input);
                            return type;

                        case 2:
                            type.measObjectGERAN = MeasObjectGERAN.PerDecoder.Instance.Decode(input);
                            return type;

                        case 3:
                            type.measObjectCDMA2000 = MeasObjectCDMA2000.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasObjectToAddMod Decode(BitArrayInputStream input)
            {
                MeasObjectToAddMod mod = new MeasObjectToAddMod();
                mod.InitDefaults();
                mod.measObjectId = input.readBits(5) + 1;
                mod.measObject = measObject_Type.PerDecoder.Instance.Decode(input);
                return mod;
            }
        }
    }

    [Serializable]
    public class MeasObjectToAddMod_v9e0
    {
        public void InitDefaults()
        {
        }

        public MeasObjectEUTRA_v9e0 measObjectEUTRA_v9e0 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasObjectToAddMod_v9e0 Decode(BitArrayInputStream input)
            {
                MeasObjectToAddMod_v9e0 _ve = new MeasObjectToAddMod_v9e0();
                _ve.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    _ve.measObjectEUTRA_v9e0 = MeasObjectEUTRA_v9e0.PerDecoder.Instance.Decode(input);
                }
                return _ve;
            }
        }
    }

    [Serializable]
    public class MeasObjectUTRA
    {
        public void InitDefaults()
        {
            offsetFreq = 0L;
        }

        public long carrierFreq { get; set; }

        public cellForWhichToReportCGI_Type cellForWhichToReportCGI { get; set; }

        public cellsToAddModList_Type cellsToAddModList { get; set; }

        public List<long> cellsToRemoveList { get; set; }

        public CSG_AllowedReportingCells_r9 csg_allowedReportingCells_v930 { get; set; }

        public long offsetFreq { get; set; }

        [Serializable]
        public class cellForWhichToReportCGI_Type
        {
            public void InitDefaults()
            {
            }

            public long utra_FDD { get; set; }

            public long utra_TDD { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public cellForWhichToReportCGI_Type Decode(BitArrayInputStream input)
                {
                    cellForWhichToReportCGI_Type type = new cellForWhichToReportCGI_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.utra_FDD = input.readBits(9);
                            return type;

                        case 1:
                            type.utra_TDD = input.readBits(7);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        [Serializable]
        public class cellsToAddModList_Type
        {
            public void InitDefaults()
            {
            }

            public List<CellsToAddModUTRA_FDD> cellsToAddModListUTRA_FDD { get; set; }

            public List<CellsToAddModUTRA_TDD> cellsToAddModListUTRA_TDD { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public cellsToAddModList_Type Decode(BitArrayInputStream input)
                {
                    int num2;
                    cellsToAddModList_Type type = new cellsToAddModList_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            {
                                type.cellsToAddModListUTRA_FDD = new List<CellsToAddModUTRA_FDD>();
                                num2 = 5;
                                int num4 = input.readBits(num2) + 1;
                                for (int i = 0; i < num4; i++)
                                {
                                    CellsToAddModUTRA_FDD item = CellsToAddModUTRA_FDD.PerDecoder.Instance.Decode(input);
                                    type.cellsToAddModListUTRA_FDD.Add(item);
                                }
                                return type;
                            }
                        case 1:
                            {
                                type.cellsToAddModListUTRA_TDD = new List<CellsToAddModUTRA_TDD>();
                                num2 = 5;
                                int num6 = input.readBits(num2) + 1;
                                for (int j = 0; j < num6; j++)
                                {
                                    CellsToAddModUTRA_TDD dutra_tdd = CellsToAddModUTRA_TDD.PerDecoder.Instance.Decode(input);
                                    type.cellsToAddModListUTRA_TDD.Add(dutra_tdd);
                                }
                                return type;
                            }
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasObjectUTRA Decode(BitArrayInputStream input)
            {
                MeasObjectUTRA tutra = new MeasObjectUTRA();
                tutra.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 1);
                BitMaskStream stream2 = new BitMaskStream(input, 3);
                tutra.carrierFreq = input.readBits(14);
                if (stream.Read())
                {
                    tutra.offsetFreq = input.readBits(5) + -15;
                }
                if (stream2.Read())
                {
                    tutra.cellsToRemoveList = new List<long>();
                    int nBits = 5;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        long item = input.readBits(5) + 1;
                        tutra.cellsToRemoveList.Add(item);
                    }
                }
                if (stream2.Read())
                {
                    tutra.cellsToAddModList = cellsToAddModList_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream2.Read())
                {
                    tutra.cellForWhichToReportCGI = cellForWhichToReportCGI_Type.PerDecoder.Instance.Decode(input);
                }
                if (flag)
                {
                    BitMaskStream stream3 = new BitMaskStream(input, 1);
                    if (stream3.Read())
                    {
                        tutra.csg_allowedReportingCells_v930 
                            = CSG_AllowedReportingCells_r9.PerDecoder.Instance.Decode(input);
                    }
                }
                return tutra;
            }
        }
    }

}
