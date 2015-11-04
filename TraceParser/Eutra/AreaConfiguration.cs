using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class AreaConfiguration_r10
    {
        public void InitDefaults()
        {
        }

        public List<CellGlobalIdEUTRA> cellGlobalIdList_r10 { get; set; }

        public List<string> trackingAreaCodeList_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public AreaConfiguration_r10 Decode(BitArrayInputStream input)
            {
                int num2;
                AreaConfiguration_r10 _r = new AreaConfiguration_r10();
                _r.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        {
                            _r.cellGlobalIdList_r10 = new List<CellGlobalIdEUTRA>();
                            num2 = 5;
                            int num4 = input.readBits(num2) + 1;
                            for (int i = 0; i < num4; i++)
                            {
                                CellGlobalIdEUTRA item = CellGlobalIdEUTRA.PerDecoder.Instance.Decode(input);
                                _r.cellGlobalIdList_r10.Add(item);
                            }
                            return _r;
                        }
                    case 1:
                        {
                            _r.trackingAreaCodeList_r10 = new List<string>();
                            num2 = 3;
                            int num6 = input.readBits(num2) + 1;
                            for (int j = 0; j < num6; j++)
                            {
                                string str = input.readBitString(0x10);
                                _r.trackingAreaCodeList_r10.Add(str);
                            }
                            return _r;
                        }
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

    [Serializable]
    public class AreaConfiguration_v1130
    {
        public void InitDefaults()
        {
        }

        public TrackingAreaCodeList_v1130 trackingAreaCodeList_v1130 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public AreaConfiguration_v1130 Decode(BitArrayInputStream input)
            {
                AreaConfiguration_v1130 _v = new AreaConfiguration_v1130();
                _v.InitDefaults();
                _v.trackingAreaCodeList_v1130 = TrackingAreaCodeList_v1130.PerDecoder.Instance.Decode(input);
                return _v;
            }
        }
    }

}
