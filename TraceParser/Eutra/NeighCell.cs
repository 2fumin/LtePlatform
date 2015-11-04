using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class NeighCellCDMA2000
    {
        public void InitDefaults()
        {
        }

        public BandclassCDMA2000 bandClass { get; set; }

        public List<NeighCellsPerBandclassCDMA2000> neighCellsPerFreqList { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public NeighCellCDMA2000 Decode(BitArrayInputStream input)
            {
                NeighCellCDMA2000 lcdma = new NeighCellCDMA2000();
                lcdma.InitDefaults();
                int nBits = (input.readBit() == 0) ? 5 : 5;
                lcdma.bandClass = (BandclassCDMA2000)input.readBits(nBits);
                lcdma.neighCellsPerFreqList = new List<NeighCellsPerBandclassCDMA2000>();
                nBits = 4;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    NeighCellsPerBandclassCDMA2000 item = NeighCellsPerBandclassCDMA2000.PerDecoder.Instance.Decode(input);
                    lcdma.neighCellsPerFreqList.Add(item);
                }
                return lcdma;
            }
        }
    }

    [Serializable]
    public class NeighCellCDMA2000_r11
    {
        public void InitDefaults()
        {
        }

        public BandclassCDMA2000 bandClass { get; set; }

        public List<NeighCellsPerBandclassCDMA2000_r11> neighFreqInfoList_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public NeighCellCDMA2000_r11 Decode(BitArrayInputStream input)
            {
                NeighCellCDMA2000_r11 _r = new NeighCellCDMA2000_r11();
                _r.InitDefaults();
                int nBits = (input.readBit() == 0) ? 5 : 5;
                _r.bandClass = (BandclassCDMA2000)input.readBits(nBits);
                _r.neighFreqInfoList_r11 = new List<NeighCellsPerBandclassCDMA2000_r11>();
                nBits = 4;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    NeighCellsPerBandclassCDMA2000_r11 item = NeighCellsPerBandclassCDMA2000_r11.PerDecoder.Instance.Decode(input);
                    _r.neighFreqInfoList_r11.Add(item);
                }
                return _r;
            }
        }
    }

    [Serializable]
    public class NeighCellCDMA2000_v920
    {
        public void InitDefaults()
        {
        }

        public List<NeighCellsPerBandclassCDMA2000_v920> neighCellsPerFreqList_v920 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public NeighCellCDMA2000_v920 Decode(BitArrayInputStream input)
            {
                NeighCellCDMA2000_v920 _v = new NeighCellCDMA2000_v920();
                _v.InitDefaults();
                _v.neighCellsPerFreqList_v920 = new List<NeighCellsPerBandclassCDMA2000_v920>();
                int nBits = 4;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    NeighCellsPerBandclassCDMA2000_v920 item = NeighCellsPerBandclassCDMA2000_v920.PerDecoder.Instance.Decode(input);
                    _v.neighCellsPerFreqList_v920.Add(item);
                }
                return _v;
            }
        }
    }

    [Serializable]
    public class NeighCellsCRS_Info_r11
    {
        public void InitDefaults()
        {
        }

        public object release { get; set; }

        public List<CRS_AssistanceInfo_r11> setup { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public NeighCellsCRS_Info_r11 Decode(BitArrayInputStream input)
            {
                NeighCellsCRS_Info_r11 _r = new NeighCellsCRS_Info_r11();
                _r.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        return _r;

                    case 1:
                        {
                            _r.setup = new List<CRS_AssistanceInfo_r11>();
                            int nBits = 3;
                            int num4 = input.readBits(nBits) + 1;
                            for (int i = 0; i < num4; i++)
                            {
                                CRS_AssistanceInfo_r11 item = CRS_AssistanceInfo_r11.PerDecoder.Instance.Decode(input);
                                _r.setup.Add(item);
                            }
                            return _r;
                        }
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

    [Serializable]
    public class NeighCellSI_AcquisitionParameters_r9
    {
        public void InitDefaults()
        {
        }

        public interFreqSI_AcquisitionForHO_r9_Enum? interFreqSI_AcquisitionForHO_r9 { get; set; }

        public intraFreqSI_AcquisitionForHO_r9_Enum? intraFreqSI_AcquisitionForHO_r9 { get; set; }

        public utran_SI_AcquisitionForHO_r9_Enum? utran_SI_AcquisitionForHO_r9 { get; set; }

        public enum interFreqSI_AcquisitionForHO_r9_Enum
        {
            supported
        }

        public enum intraFreqSI_AcquisitionForHO_r9_Enum
        {
            supported
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public NeighCellSI_AcquisitionParameters_r9 Decode(BitArrayInputStream input)
            {
                int num2;
                NeighCellSI_AcquisitionParameters_r9 _r = new NeighCellSI_AcquisitionParameters_r9();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    num2 = 1;
                    _r.intraFreqSI_AcquisitionForHO_r9 = (intraFreqSI_AcquisitionForHO_r9_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _r.interFreqSI_AcquisitionForHO_r9 = (interFreqSI_AcquisitionForHO_r9_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _r.utran_SI_AcquisitionForHO_r9 = (utran_SI_AcquisitionForHO_r9_Enum)input.readBits(num2);
                }
                return _r;
            }
        }

        public enum utran_SI_AcquisitionForHO_r9_Enum
        {
            supported
        }
    }

    [Serializable]
    public class NeighCellsPerBandclassCDMA2000
    {
        public void InitDefaults()
        {
        }

        public long arfcn { get; set; }

        public List<long> physCellIdList { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public NeighCellsPerBandclassCDMA2000 Decode(BitArrayInputStream input)
            {
                NeighCellsPerBandclassCDMA2000 scdma = new NeighCellsPerBandclassCDMA2000();
                scdma.InitDefaults();
                scdma.arfcn = input.readBits(11);
                scdma.physCellIdList = new List<long>();
                int nBits = 4;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    long item = input.readBits(9);
                    scdma.physCellIdList.Add(item);
                }
                return scdma;
            }
        }
    }

    [Serializable]
    public class NeighCellsPerBandclassCDMA2000_r11
    {
        public void InitDefaults()
        {
        }

        public long arfcn { get; set; }

        public List<long> physCellIdList_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public NeighCellsPerBandclassCDMA2000_r11 Decode(BitArrayInputStream input)
            {
                NeighCellsPerBandclassCDMA2000_r11 _r = new NeighCellsPerBandclassCDMA2000_r11();
                _r.InitDefaults();
                _r.arfcn = input.readBits(11);
                _r.physCellIdList_r11 = new List<long>();
                int nBits = 6;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    long item = input.readBits(9);
                    _r.physCellIdList_r11.Add(item);
                }
                return _r;
            }
        }
    }

    [Serializable]
    public class NeighCellsPerBandclassCDMA2000_v920
    {
        public void InitDefaults()
        {
        }

        public List<long> physCellIdList_v920 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public NeighCellsPerBandclassCDMA2000_v920 Decode(BitArrayInputStream input)
            {
                NeighCellsPerBandclassCDMA2000_v920 _v = new NeighCellsPerBandclassCDMA2000_v920();
                _v.InitDefaults();
                _v.physCellIdList_v920 = new List<long>();
                int nBits = 5;
                int num3 = input.readBits(nBits);
                for (int i = 0; i < num3; i++)
                {
                    long item = input.readBits(9);
                    _v.physCellIdList_v920.Add(item);
                }
                return _v;
            }
        }
    }

}
