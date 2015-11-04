using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class RedirectedCarrierInfo
    {
        public void InitDefaults()
        {
        }

        public CarrierFreqCDMA2000 cdma2000_1xRTT { get; set; }

        public CarrierFreqCDMA2000 cdma2000_HRPD { get; set; }

        public long eutra { get; set; }

        public CarrierFreqsGERAN geran { get; set; }

        public long utra_FDD { get; set; }

        public long utra_TDD { get; set; }

        public List<long> utra_TDD_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RedirectedCarrierInfo Decode(BitArrayInputStream input)
            {
                RedirectedCarrierInfo info = new RedirectedCarrierInfo();
                info.InitDefaults();
                bool flag = input.readBit() != 0;
                switch (input.readBits(3))
                {
                    case 0:
                        info.eutra = input.readBits(0x10);
                        return info;

                    case 1:
                        info.geran = CarrierFreqsGERAN.PerDecoder.Instance.Decode(input);
                        return info;

                    case 2:
                        info.utra_FDD = input.readBits(14);
                        return info;

                    case 3:
                        info.utra_TDD = input.readBits(14);
                        return info;

                    case 4:
                        info.cdma2000_HRPD = CarrierFreqCDMA2000.PerDecoder.Instance.Decode(input);
                        return info;

                    case 5:
                        info.cdma2000_1xRTT = CarrierFreqCDMA2000.PerDecoder.Instance.Decode(input);
                        return info;

                    case 6:
                        if (flag)
                        {
                            info.utra_TDD_r10 = new List<long>();
                            int nBits = 3;
                            int num4 = input.readBits(nBits) + 1;
                            for (int i = 0; i < num4; i++)
                            {
                                long item = input.readBits(14);
                                info.utra_TDD_r10.Add(item);
                            }
                        }
                        return info;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

    [Serializable]
    public class RedirectedCarrierInfo_v9e0
    {
        public void InitDefaults()
        {
        }

        public long eutra_v9e0 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RedirectedCarrierInfo_v9e0 Decode(BitArrayInputStream input)
            {
                RedirectedCarrierInfo_v9e0 _ve = new RedirectedCarrierInfo_v9e0();
                _ve.InitDefaults();
                _ve.eutra_v9e0 = input.readBits(0x12) + 0x10000;
                return _ve;
            }
        }
    }

}
