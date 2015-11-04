using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class ConnEstFailReport_r11
    {
        public void InitDefaults()
        {
        }

        public bool contentionDetected_r11 { get; set; }

        public CellGlobalIdEUTRA failedCellId_r11 { get; set; }

        public LocationInfo_r10 locationInfo_r11 { get; set; }

        public bool maxTxPowerReached_r11 { get; set; }

        public measResultFailedCell_r11_Type measResultFailedCell_r11 { get; set; }

        public List<MeasResult2EUTRA_v9e0> measResultListEUTRA_v1130 { get; set; }

        public measResultNeighCells_r11_Type measResultNeighCells_r11 { get; set; }

        public long numberOfPreamblesSent_r11 { get; set; }

        public long timeSinceFailure_r11 { get; set; }

        [Serializable]
        public class measResultFailedCell_r11_Type
        {
            public void InitDefaults()
            {
            }

            public long rsrpResult_r11 { get; set; }

            public long? rsrqResult_r11 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public measResultFailedCell_r11_Type Decode(BitArrayInputStream input)
                {
                    measResultFailedCell_r11_Type type = new measResultFailedCell_r11_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 1);
                    type.rsrpResult_r11 = input.readBits(7);
                    if (stream.Read())
                    {
                        type.rsrqResult_r11 = input.readBits(6);
                    }
                    return type;
                }
            }
        }

        [Serializable]
        public class measResultNeighCells_r11_Type
        {
            public void InitDefaults()
            {
            }

            public List<MeasResult2EUTRA_r9> measResultListEUTRA_r11 { get; set; }

            public List<MeasResultGERAN> measResultListGERAN_r11 { get; set; }

            public List<MeasResult2UTRA_r9> measResultListUTRA_r11 { get; set; }

            public List<MeasResult2CDMA2000_r9> measResultsCDMA2000_r11 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public measResultNeighCells_r11_Type Decode(BitArrayInputStream input)
                {
                    int num2;
                    measResultNeighCells_r11_Type type = new measResultNeighCells_r11_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 4);
                    if (stream.Read())
                    {
                        type.measResultListEUTRA_r11 = new List<MeasResult2EUTRA_r9>();
                        num2 = 3;
                        int num3 = input.readBits(num2) + 1;
                        for (int i = 0; i < num3; i++)
                        {
                            MeasResult2EUTRA_r9 item = MeasResult2EUTRA_r9.PerDecoder.Instance.Decode(input);
                            type.measResultListEUTRA_r11.Add(item);
                        }
                    }
                    if (stream.Read())
                    {
                        type.measResultListUTRA_r11 = new List<MeasResult2UTRA_r9>();
                        num2 = 3;
                        int num5 = input.readBits(num2) + 1;
                        for (int j = 0; j < num5; j++)
                        {
                            MeasResult2UTRA_r9 _r2 = MeasResult2UTRA_r9.PerDecoder.Instance.Decode(input);
                            type.measResultListUTRA_r11.Add(_r2);
                        }
                    }
                    if (stream.Read())
                    {
                        type.measResultListGERAN_r11 = new List<MeasResultGERAN>();
                        num2 = 3;
                        int num7 = input.readBits(num2) + 1;
                        for (int k = 0; k < num7; k++)
                        {
                            MeasResultGERAN tgeran = MeasResultGERAN.PerDecoder.Instance.Decode(input);
                            type.measResultListGERAN_r11.Add(tgeran);
                        }
                    }
                    if (stream.Read())
                    {
                        type.measResultsCDMA2000_r11 = new List<MeasResult2CDMA2000_r9>();
                        num2 = 3;
                        int num9 = input.readBits(num2) + 1;
                        for (int m = 0; m < num9; m++)
                        {
                            MeasResult2CDMA2000_r9 _r3 = MeasResult2CDMA2000_r9.PerDecoder.Instance.Decode(input);
                            type.measResultsCDMA2000_r11.Add(_r3);
                        }
                    }
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ConnEstFailReport_r11 Decode(BitArrayInputStream input)
            {
                ConnEstFailReport_r11 _r = new ConnEstFailReport_r11();
                _r.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 3) : new BitMaskStream(input, 3);
                _r.failedCellId_r11 = CellGlobalIdEUTRA.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    _r.locationInfo_r11 = LocationInfo_r10.PerDecoder.Instance.Decode(input);
                }
                _r.measResultFailedCell_r11 = measResultFailedCell_r11_Type.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    _r.measResultNeighCells_r11 = measResultNeighCells_r11_Type.PerDecoder.Instance.Decode(input);
                }
                _r.numberOfPreamblesSent_r11 = input.readBits(8) + 1;
                _r.contentionDetected_r11 = input.readBit() == 1;
                _r.maxTxPowerReached_r11 = input.readBit() == 1;
                _r.timeSinceFailure_r11 = input.readBits(0x12);
                if (stream.Read())
                {
                    _r.measResultListEUTRA_v1130 = new List<MeasResult2EUTRA_v9e0>();
                    int nBits = 3;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        MeasResult2EUTRA_v9e0 item = MeasResult2EUTRA_v9e0.PerDecoder.Instance.Decode(input);
                        _r.measResultListEUTRA_v1130.Add(item);
                    }
                }
                return _r;
            }
        }
    }
}
