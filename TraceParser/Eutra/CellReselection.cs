using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CellReselectionParametersCDMA2000
    {
        public void InitDefaults()
        {
        }

        public List<BandClassInfoCDMA2000> bandClassList { get; set; }

        public List<NeighCellCDMA2000> neighCellList { get; set; }

        public long t_ReselectionCDMA2000 { get; set; }

        public SpeedStateScaleFactors t_ReselectionCDMA2000_SF { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellReselectionParametersCDMA2000 Decode(BitArrayInputStream input)
            {
                CellReselectionParametersCDMA2000 scdma = new CellReselectionParametersCDMA2000();
                scdma.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                scdma.bandClassList = new List<BandClassInfoCDMA2000>();
                int nBits = 5;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    BandClassInfoCDMA2000 item = BandClassInfoCDMA2000.PerDecoder.Instance.Decode(input);
                    scdma.bandClassList.Add(item);
                }
                scdma.neighCellList = new List<NeighCellCDMA2000>();
                nBits = 4;
                int num5 = input.readBits(nBits) + 1;
                for (int j = 0; j < num5; j++)
                {
                    NeighCellCDMA2000 lcdma = NeighCellCDMA2000.PerDecoder.Instance.Decode(input);
                    scdma.neighCellList.Add(lcdma);
                }
                scdma.t_ReselectionCDMA2000 = input.readBits(3);
                if (stream.Read())
                {
                    scdma.t_ReselectionCDMA2000_SF = SpeedStateScaleFactors.PerDecoder.Instance.Decode(input);
                }
                return scdma;
            }
        }
    }

    [Serializable]
    public class CellReselectionParametersCDMA2000_r11
    {
        public void InitDefaults()
        {
        }

        public List<BandClassInfoCDMA2000> bandClassList { get; set; }

        public List<NeighCellCDMA2000_r11> neighCellList_r11 { get; set; }

        public long t_ReselectionCDMA2000 { get; set; }

        public SpeedStateScaleFactors t_ReselectionCDMA2000_SF { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellReselectionParametersCDMA2000_r11 Decode(BitArrayInputStream input)
            {
                CellReselectionParametersCDMA2000_r11 _r = new CellReselectionParametersCDMA2000_r11();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                _r.bandClassList = new List<BandClassInfoCDMA2000>();
                int nBits = 5;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    BandClassInfoCDMA2000 item = BandClassInfoCDMA2000.PerDecoder.Instance.Decode(input);
                    _r.bandClassList.Add(item);
                }
                _r.neighCellList_r11 = new List<NeighCellCDMA2000_r11>();
                nBits = 4;
                int num5 = input.readBits(nBits) + 1;
                for (int j = 0; j < num5; j++)
                {
                    NeighCellCDMA2000_r11 _r2 = NeighCellCDMA2000_r11.PerDecoder.Instance.Decode(input);
                    _r.neighCellList_r11.Add(_r2);
                }
                _r.t_ReselectionCDMA2000 = input.readBits(3);
                if (stream.Read())
                {
                    _r.t_ReselectionCDMA2000_SF = SpeedStateScaleFactors.PerDecoder.Instance.Decode(input);
                }
                return _r;
            }
        }
    }

    [Serializable]
    public class CellReselectionParametersCDMA2000_v920
    {
        public void InitDefaults()
        {
        }

        public List<NeighCellCDMA2000_v920> neighCellList_v920 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellReselectionParametersCDMA2000_v920 Decode(BitArrayInputStream input)
            {
                CellReselectionParametersCDMA2000_v920 _v = new CellReselectionParametersCDMA2000_v920();
                _v.InitDefaults();
                _v.neighCellList_v920 = new List<NeighCellCDMA2000_v920>();
                int nBits = 4;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    NeighCellCDMA2000_v920 item = NeighCellCDMA2000_v920.PerDecoder.Instance.Decode(input);
                    _v.neighCellList_v920.Add(item);
                }
                return _v;
            }
        }
    }

    [Serializable]
    public class CellChangeOrder
    {
        public void InitDefaults()
        {
        }

        public t304_Enum t304 { get; set; }

        public targetRAT_Type_Type targetRAT_Type { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellChangeOrder Decode(BitArrayInputStream input)
            {
                CellChangeOrder order = new CellChangeOrder();
                order.InitDefaults();
                int nBits = 3;
                order.t304 = (t304_Enum)input.readBits(nBits);
                order.targetRAT_Type = targetRAT_Type_Type.PerDecoder.Instance.Decode(input);
                return order;
            }
        }

        public enum t304_Enum
        {
            ms100,
            ms200,
            ms500,
            ms1000,
            ms2000,
            ms4000,
            ms8000,
            spare1
        }

        [Serializable]
        public class targetRAT_Type_Type
        {
            public void InitDefaults()
            {
            }

            public geran_Type geran { get; set; }

            [Serializable]
            public class geran_Type
            {
                public void InitDefaults()
                {
                }

                public CarrierFreqGERAN carrierFreq { get; set; }

                public string networkControlOrder { get; set; }

                public PhysCellIdGERAN physCellId { get; set; }

                public SI_OrPSI_GERAN systemInformation { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public geran_Type Decode(BitArrayInputStream input)
                    {
                        geran_Type type = new geran_Type();
                        type.InitDefaults();
                        BitMaskStream stream = new BitMaskStream(input, 2);
                        type.physCellId = PhysCellIdGERAN.PerDecoder.Instance.Decode(input);
                        type.carrierFreq = CarrierFreqGERAN.PerDecoder.Instance.Decode(input);
                        if (stream.Read())
                        {
                            type.networkControlOrder = input.readBitString(2);
                        }
                        if (stream.Read())
                        {
                            type.systemInformation = SI_OrPSI_GERAN.PerDecoder.Instance.Decode(input);
                        }
                        return type;
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public targetRAT_Type_Type Decode(BitArrayInputStream input)
                {
                    targetRAT_Type_Type type = new targetRAT_Type_Type();
                    type.InitDefaults();
                    input.readBit();
                    if (input.readBits(1) != 0)
                    {
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                    type.geran = geran_Type.PerDecoder.Instance.Decode(input);
                    return type;
                }
            }
        }
    }

}
