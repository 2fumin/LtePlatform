using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class BandCombinationParameters_v1130
    {
        public void InitDefaults()
        {
        }

        public List<BandParameters_v1130> bandParameterList_r11 { get; set; }

        public multipleTimingAdvance_r11_Enum? multipleTimingAdvance_r11 { get; set; }

        public simultaneousRx_Tx_r11_Enum? simultaneousRx_Tx_r11 { get; set; }

        public enum multipleTimingAdvance_r11_Enum
        {
            supported
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public BandCombinationParameters_v1130 Decode(BitArrayInputStream input)
            {
                int num2;
                BandCombinationParameters_v1130 _v = new BandCombinationParameters_v1130();
                _v.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 3) : new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    num2 = 1;
                    _v.multipleTimingAdvance_r11 = (multipleTimingAdvance_r11_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _v.simultaneousRx_Tx_r11 = (simultaneousRx_Tx_r11_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    _v.bandParameterList_r11 = new List<BandParameters_v1130>();
                    num2 = 6;
                    int num3 = input.readBits(num2) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        BandParameters_v1130 item = BandParameters_v1130.PerDecoder.Instance.Decode(input);
                        _v.bandParameterList_r11.Add(item);
                    }
                }
                return _v;
            }
        }

        public enum simultaneousRx_Tx_r11_Enum
        {
            supported
        }
    }

    [Serializable]
    public class BandCombinationParametersExt_r10
    {
        public void InitDefaults()
        {
        }

        public string supportedBandwidthCombinationSet_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public BandCombinationParametersExt_r10 Decode(BitArrayInputStream input)
            {
                BandCombinationParametersExt_r10 _r = new BandCombinationParametersExt_r10();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    int num = input.readBits(5);
                    _r.supportedBandwidthCombinationSet_r10 = input.readBitString(num + 1);
                }
                return _r;
            }
        }
    }

    [Serializable]
    public class BandInfoEUTRA
    {
        public void InitDefaults()
        {
        }

        public List<InterFreqBandInfo> interFreqBandList { get; set; }

        public List<InterRAT_BandInfo> interRAT_BandList { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public BandInfoEUTRA Decode(BitArrayInputStream input)
            {
                BandInfoEUTRA oeutra = new BandInfoEUTRA();
                oeutra.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                oeutra.interFreqBandList = new List<InterFreqBandInfo>();
                int nBits = 6;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    InterFreqBandInfo item = InterFreqBandInfo.PerDecoder.Instance.Decode(input);
                    oeutra.interFreqBandList.Add(item);
                }
                if (stream.Read())
                {
                    oeutra.interRAT_BandList = new List<InterRAT_BandInfo>();
                    nBits = 6;
                    int num5 = input.readBits(nBits) + 1;
                    for (int j = 0; j < num5; j++)
                    {
                        InterRAT_BandInfo info2 = InterRAT_BandInfo.PerDecoder.Instance.Decode(input);
                        oeutra.interRAT_BandList.Add(info2);
                    }
                }
                return oeutra;
            }
        }
    }

    [Serializable]
    public class BandParameters_r10
    {
        public void InitDefaults()
        {
        }

        public long bandEUTRA_r10 { get; set; }

        public List<CA_MIMO_ParametersDL_r10> bandParametersDL_r10 { get; set; }

        public List<CA_MIMO_ParametersUL_r10> bandParametersUL_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public BandParameters_r10 Decode(BitArrayInputStream input)
            {
                int num2;
                BandParameters_r10 _r = new BandParameters_r10();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                _r.bandEUTRA_r10 = input.readBits(6) + 1;
                if (stream.Read())
                {
                    _r.bandParametersUL_r10 = new List<CA_MIMO_ParametersUL_r10>();
                    num2 = 4;
                    int num3 = input.readBits(num2) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        CA_MIMO_ParametersUL_r10 item = CA_MIMO_ParametersUL_r10.PerDecoder.Instance.Decode(input);
                        _r.bandParametersUL_r10.Add(item);
                    }
                }
                if (stream.Read())
                {
                    _r.bandParametersDL_r10 = new List<CA_MIMO_ParametersDL_r10>();
                    num2 = 4;
                    int num5 = input.readBits(num2) + 1;
                    for (int j = 0; j < num5; j++)
                    {
                        CA_MIMO_ParametersDL_r10 _r3 = CA_MIMO_ParametersDL_r10.PerDecoder.Instance.Decode(input);
                        _r.bandParametersDL_r10.Add(_r3);
                    }
                }
                return _r;
            }
        }
    }

    [Serializable]
    public class BandParameters_v1090
    {
        public void InitDefaults()
        {
        }

        public long? bandEUTRA_v1090 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public BandParameters_v1090 Decode(BitArrayInputStream input)
            {
                BandParameters_v1090 _v = new BandParameters_v1090();
                _v.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    _v.bandEUTRA_v1090 = input.readBits(8) + 0x41;
                }
                return _v;
            }
        }
    }

    [Serializable]
    public class BandParameters_v1130
    {
        public void InitDefaults()
        {
        }

        public supportedCSI_Proc_r11_Enum supportedCSI_Proc_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public BandParameters_v1130 Decode(BitArrayInputStream input)
            {
                BandParameters_v1130 _v = new BandParameters_v1130();
                _v.InitDefaults();
                int nBits = 2;
                _v.supportedCSI_Proc_r11 = (supportedCSI_Proc_r11_Enum)input.readBits(nBits);
                return _v;
            }
        }

        public enum supportedCSI_Proc_r11_Enum
        {
            n1,
            n3,
            n4
        }
    }

    [Serializable]
    public class InterRAT_BandInfo
    {
        public void InitDefaults()
        {
        }

        public bool interRAT_NeedForGaps { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public InterRAT_BandInfo Decode(BitArrayInputStream input)
            {
                InterRAT_BandInfo info = new InterRAT_BandInfo();
                info.InitDefaults();
                info.interRAT_NeedForGaps = input.readBit() == 1;
                return info;
            }
        }
    }

}
