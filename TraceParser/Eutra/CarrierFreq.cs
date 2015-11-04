using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CarrierFreqCDMA2000
    {
        public void InitDefaults()
        {
        }

        public long arfcn { get; set; }

        public BandclassCDMA2000 bandClass { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CarrierFreqCDMA2000 Decode(BitArrayInputStream input)
            {
                CarrierFreqCDMA2000 qcdma = new CarrierFreqCDMA2000();
                qcdma.InitDefaults();
                int nBits = (input.readBit() == 0) ? 5 : 5;
                qcdma.bandClass = (BandclassCDMA2000)input.readBits(nBits);
                qcdma.arfcn = input.readBits(11);
                return qcdma;
            }
        }
    }

    [Serializable]
    public class CarrierFreqEUTRA
    {
        public void InitDefaults()
        {
        }

        public long dl_CarrierFreq { get; set; }

        public long? ul_CarrierFreq { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CarrierFreqEUTRA Decode(BitArrayInputStream input)
            {
                CarrierFreqEUTRA qeutra = new CarrierFreqEUTRA();
                qeutra.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                qeutra.dl_CarrierFreq = input.readBits(0x10);
                if (stream.Read())
                {
                    qeutra.ul_CarrierFreq = input.readBits(0x10);
                }
                return qeutra;
            }
        }
    }

    [Serializable]
    public class CarrierFreqEUTRA_v9e0
    {
        public void InitDefaults()
        {
        }

        public long dl_CarrierFreq_v9e0 { get; set; }

        public long? ul_CarrierFreq_v9e0 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CarrierFreqEUTRA_v9e0 Decode(BitArrayInputStream input)
            {
                CarrierFreqEUTRA_v9e0 _ve = new CarrierFreqEUTRA_v9e0();
                _ve.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                _ve.dl_CarrierFreq_v9e0 = input.readBits(0x12);
                if (stream.Read())
                {
                    _ve.ul_CarrierFreq_v9e0 = input.readBits(0x12);
                }
                return _ve;
            }
        }
    }

    [Serializable]
    public class CarrierFreqGERAN
    {
        public void InitDefaults()
        {
        }

        public long arfcn { get; set; }

        public BandIndicatorGERAN bandIndicator { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CarrierFreqGERAN Decode(BitArrayInputStream input)
            {
                CarrierFreqGERAN qgeran = new CarrierFreqGERAN();
                qgeran.InitDefaults();
                qgeran.arfcn = input.readBits(10);
                const int nBits = 1;
                qgeran.bandIndicator = (BandIndicatorGERAN)input.readBits(nBits);
                return qgeran;
            }
        }
    }

    [Serializable]
    public class CarrierFreqInfoUTRA_FDD_v8h0
    {
        public void InitDefaults()
        {
        }

        public List<long> multiBandInfoList { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CarrierFreqInfoUTRA_FDD_v8h0 Decode(BitArrayInputStream input)
            {
                CarrierFreqInfoUTRA_FDD_v8h0 _vh = new CarrierFreqInfoUTRA_FDD_v8h0();
                _vh.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    _vh.multiBandInfoList = new List<long>();
                    int nBits = 3;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        long item = input.readBits(7) + 1;
                        _vh.multiBandInfoList.Add(item);
                    }
                }
                return _vh;
            }
        }
    }

    [Serializable]
    public class CarrierFreqsGERAN
    {
        public void InitDefaults()
        {
        }

        public BandIndicatorGERAN bandIndicator { get; set; }

        public followingARFCNs_Type followingARFCNs { get; set; }

        public long startingARFCN { get; set; }

        [Serializable]
        public class followingARFCNs_Type
        {
            public void InitDefaults()
            {
            }

            public equallySpacedARFCNs_Type equallySpacedARFCNs { get; set; }

            public List<long> explicitListOfARFCNs { get; set; }

            public string variableBitMapOfARFCNs { get; set; }

            [Serializable]
            public class equallySpacedARFCNs_Type
            {
                public void InitDefaults()
                {
                }

                public long arfcn_Spacing { get; set; }

                public long numberOfFollowingARFCNs { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public equallySpacedARFCNs_Type Decode(BitArrayInputStream input)
                    {
                        equallySpacedARFCNs_Type type = new equallySpacedARFCNs_Type();
                        type.InitDefaults();
                        type.arfcn_Spacing = input.readBits(3) + 1;
                        type.numberOfFollowingARFCNs = input.readBits(5);
                        return type;
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public followingARFCNs_Type Decode(BitArrayInputStream input)
                {
                    followingARFCNs_Type type = new followingARFCNs_Type();
                    type.InitDefaults();
                    switch (input.readBits(2))
                    {
                        case 0:
                            {
                                type.explicitListOfARFCNs = new List<long>();
                                const int nBits = 5;
                                int num4 = input.readBits(nBits);
                                for (int i = 0; i < num4; i++)
                                {
                                    long item = input.readBits(10);
                                    type.explicitListOfARFCNs.Add(item);
                                }
                                return type;
                            }
                        case 1:
                            type.equallySpacedARFCNs = equallySpacedARFCNs_Type.PerDecoder.Instance.Decode(input);
                            return type;

                        case 2:
                            int num = input.readBits(4);
                            type.variableBitMapOfARFCNs = input.readOctetString(num + 1);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CarrierFreqsGERAN Decode(BitArrayInputStream input)
            {
                CarrierFreqsGERAN sgeran = new CarrierFreqsGERAN();
                sgeran.InitDefaults();
                sgeran.startingARFCN = input.readBits(10);
                int nBits = 1;
                sgeran.bandIndicator = (BandIndicatorGERAN)input.readBits(nBits);
                sgeran.followingARFCNs = followingARFCNs_Type.PerDecoder.Instance.Decode(input);
                return sgeran;
            }
        }
    }

    [Serializable]
    public class CarrierFreqsInfoGERAN
    {
        public void InitDefaults()
        {
        }

        public CarrierFreqsGERAN carrierFreqs { get; set; }

        public commonInfo_Type commonInfo { get; set; }

        [Serializable]
        public class commonInfo_Type
        {
            public void InitDefaults()
            {
            }

            public long? cellReselectionPriority { get; set; }

            public string ncc_Permitted { get; set; }

            public long? p_MaxGERAN { get; set; }

            public long q_RxLevMin { get; set; }

            public long threshX_High { get; set; }

            public long threshX_Low { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public commonInfo_Type Decode(BitArrayInputStream input)
                {
                    commonInfo_Type type = new commonInfo_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 2);
                    if (stream.Read())
                    {
                        type.cellReselectionPriority = input.readBits(3);
                    }
                    type.ncc_Permitted = input.readBitString(8);
                    type.q_RxLevMin = input.readBits(6);
                    if (stream.Read())
                    {
                        type.p_MaxGERAN = input.readBits(6);
                    }
                    type.threshX_High = input.readBits(5);
                    type.threshX_Low = input.readBits(5);
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CarrierFreqsInfoGERAN Decode(BitArrayInputStream input)
            {
                CarrierFreqsInfoGERAN ogeran = new CarrierFreqsInfoGERAN();
                ogeran.InitDefaults();
                input.readBit();
                ogeran.carrierFreqs = CarrierFreqsGERAN.PerDecoder.Instance.Decode(input);
                ogeran.commonInfo = commonInfo_Type.PerDecoder.Instance.Decode(input);
                return ogeran;
            }
        }
    }

    [Serializable]
    public class CarrierFreqUTRA_FDD
    {
        public void InitDefaults()
        {
        }

        public long carrierFreq { get; set; }

        public long? cellReselectionPriority { get; set; }

        public long p_MaxUTRA { get; set; }

        public long q_QualMin { get; set; }

        public long q_RxLevMin { get; set; }

        public long threshX_High { get; set; }

        public long threshX_Low { get; set; }

        public threshX_Q_r9_Type threshX_Q_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CarrierFreqUTRA_FDD Decode(BitArrayInputStream input)
            {
                CarrierFreqUTRA_FDD qutra_fdd = new CarrierFreqUTRA_FDD();
                qutra_fdd.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 1);
                qutra_fdd.carrierFreq = input.readBits(14);
                if (stream.Read())
                {
                    qutra_fdd.cellReselectionPriority = input.readBits(3);
                }
                qutra_fdd.threshX_High = input.readBits(5);
                qutra_fdd.threshX_Low = input.readBits(5);
                qutra_fdd.q_RxLevMin = input.readBits(6) + -60;
                qutra_fdd.p_MaxUTRA = input.readBits(7) + -50;
                qutra_fdd.q_QualMin = input.readBits(5) + -24;
                if (flag)
                {
                    BitMaskStream stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        qutra_fdd.threshX_Q_r9 = threshX_Q_r9_Type.PerDecoder.Instance.Decode(input);
                    }
                }
                return qutra_fdd;
            }
        }

        [Serializable]
        public class threshX_Q_r9_Type
        {
            public void InitDefaults()
            {
            }

            public long threshX_HighQ_r9 { get; set; }

            public long threshX_LowQ_r9 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public threshX_Q_r9_Type Decode(BitArrayInputStream input)
                {
                    threshX_Q_r9_Type type = new threshX_Q_r9_Type();
                    type.InitDefaults();
                    type.threshX_HighQ_r9 = input.readBits(5);
                    type.threshX_LowQ_r9 = input.readBits(5);
                    return type;
                }
            }
        }
    }

    [Serializable]
    public class CarrierFreqUTRA_TDD
    {
        public void InitDefaults()
        {
        }

        public long carrierFreq { get; set; }

        public long? cellReselectionPriority { get; set; }

        public long p_MaxUTRA { get; set; }

        public long q_RxLevMin { get; set; }

        public long threshX_High { get; set; }

        public long threshX_Low { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CarrierFreqUTRA_TDD Decode(BitArrayInputStream input)
            {
                CarrierFreqUTRA_TDD qutra_tdd = new CarrierFreqUTRA_TDD();
                qutra_tdd.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                qutra_tdd.carrierFreq = input.readBits(14);
                if (stream.Read())
                {
                    qutra_tdd.cellReselectionPriority = input.readBits(3);
                }
                qutra_tdd.threshX_High = input.readBits(5);
                qutra_tdd.threshX_Low = input.readBits(5);
                qutra_tdd.q_RxLevMin = input.readBits(6) + -60;
                qutra_tdd.p_MaxUTRA = input.readBits(7) + -50;
                return qutra_tdd;
            }
        }
    }

    [Serializable]
    public class AffectedCarrierFreq_r11
    {
        public void InitDefaults()
        {
        }

        public long carrierFreq_r11 { get; set; }

        public interferenceDirection_r11_Enum interferenceDirection_r11 { get; set; }

        public enum interferenceDirection_r11_Enum
        {
            eutra,
            other,
            both,
            spare
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public AffectedCarrierFreq_r11 Decode(BitArrayInputStream input)
            {
                AffectedCarrierFreq_r11 _r = new AffectedCarrierFreq_r11();
                _r.InitDefaults();
                _r.carrierFreq_r11 = input.readBits(5) + 1;
                const int nBits = 2;
                _r.interferenceDirection_r11 = (interferenceDirection_r11_Enum)input.readBits(nBits);
                return _r;
            }
        }
    }

    [Serializable]
    public class CarrierBandwidthEUTRA
    {
        public void InitDefaults()
        {
        }

        public dl_Bandwidth_Enum dl_Bandwidth { get; set; }

        public ul_Bandwidth_Enum? ul_Bandwidth { get; set; }

        public enum dl_Bandwidth_Enum
        {
            n6,
            n15,
            n25,
            n50,
            n75,
            n100,
            spare10,
            spare9,
            spare8,
            spare7,
            spare6,
            spare5,
            spare4,
            spare3,
            spare2,
            spare1
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CarrierBandwidthEUTRA Decode(BitArrayInputStream input)
            {
                CarrierBandwidthEUTRA heutra = new CarrierBandwidthEUTRA();
                heutra.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                int nBits = 4;
                heutra.dl_Bandwidth = (dl_Bandwidth_Enum)input.readBits(nBits);
                if (stream.Read())
                {
                    nBits = 4;
                    heutra.ul_Bandwidth = (ul_Bandwidth_Enum)input.readBits(nBits);
                }
                return heutra;
            }
        }

        public enum ul_Bandwidth_Enum
        {
            n6,
            n15,
            n25,
            n50,
            n75,
            n100,
            spare10,
            spare9,
            spare8,
            spare7,
            spare6,
            spare5,
            spare4,
            spare3,
            spare2,
            spare1
        }
    }

}
