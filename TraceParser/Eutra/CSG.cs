using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CSG_AllowedReportingCells_r9
    {
        public void InitDefaults()
        {
        }

        public List<PhysCellIdRangeUTRA_FDD_r9> physCellIdRangeUTRA_FDDList_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CSG_AllowedReportingCells_r9 Decode(BitArrayInputStream input)
            {
                CSG_AllowedReportingCells_r9 _r = new CSG_AllowedReportingCells_r9();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    _r.physCellIdRangeUTRA_FDDList_r9 = new List<PhysCellIdRangeUTRA_FDD_r9>();
                    int nBits = 2;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        PhysCellIdRangeUTRA_FDD_r9 item = PhysCellIdRangeUTRA_FDD_r9.PerDecoder.Instance.Decode(input);
                        _r.physCellIdRangeUTRA_FDDList_r9.Add(item);
                    }
                }
                return _r;
            }
        }
    }

    [Serializable]
    public class CSG_ProximityIndicationParameters_r9
    {
        public void InitDefaults()
        {
        }

        public interFreqProximityIndication_r9_Enum? interFreqProximityIndication_r9 { get; set; }

        public intraFreqProximityIndication_r9_Enum? intraFreqProximityIndication_r9 { get; set; }

        public utran_ProximityIndication_r9_Enum? utran_ProximityIndication_r9 { get; set; }

        public enum interFreqProximityIndication_r9_Enum
        {
            supported
        }

        public enum intraFreqProximityIndication_r9_Enum
        {
            supported
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CSG_ProximityIndicationParameters_r9 Decode(BitArrayInputStream input)
            {
                int num2;
                CSG_ProximityIndicationParameters_r9 _r = new CSG_ProximityIndicationParameters_r9();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    num2 = 1;
                    _r.intraFreqProximityIndication_r9 = (intraFreqProximityIndication_r9_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _r.interFreqProximityIndication_r9 = (interFreqProximityIndication_r9_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _r.utran_ProximityIndication_r9 = (utran_ProximityIndication_r9_Enum)input.readBits(num2);
                }
                return _r;
            }
        }

        public enum utran_ProximityIndication_r9_Enum
        {
            supported
        }
    }

}
