using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class RF_Parameters
    {
        public void InitDefaults()
        {
        }

        public List<SupportedBandEUTRA> supportedBandListEUTRA { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RF_Parameters Decode(BitArrayInputStream input)
            {
                RF_Parameters parameters = new RF_Parameters();
                parameters.InitDefaults();
                parameters.supportedBandListEUTRA = new List<SupportedBandEUTRA>();
                const int nBits = 6;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    SupportedBandEUTRA item = SupportedBandEUTRA.PerDecoder.Instance.Decode(input);
                    parameters.supportedBandListEUTRA.Add(item);
                }
                return parameters;
            }
        }
    }

    [Serializable]
    public class RF_Parameters_v1020
    {
        public void InitDefaults()
        {
        }

        public List<List<BandParameters_r10>> supportedBandCombination_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RF_Parameters_v1020 Decode(BitArrayInputStream input)
            {
                RF_Parameters_v1020 _v = new RF_Parameters_v1020();
                _v.InitDefaults();
                _v.supportedBandCombination_r10 = new List<List<BandParameters_r10>>();
                int nBits = 7;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    List<BandParameters_r10> item = new List<BandParameters_r10>();
                    nBits = 6;
                    int num5 = input.readBits(nBits) + 1;
                    for (int j = 0; j < num5; j++)
                    {
                        BandParameters_r10 _r = BandParameters_r10.PerDecoder.Instance.Decode(input);
                        item.Add(_r);
                    }
                    _v.supportedBandCombination_r10.Add(item);
                }
                return _v;
            }
        }
    }

    [Serializable]
    public class RF_Parameters_v1060
    {
        public void InitDefaults()
        {
        }

        public List<BandCombinationParametersExt_r10> supportedBandCombinationExt_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RF_Parameters_v1060 Decode(BitArrayInputStream input)
            {
                RF_Parameters_v1060 _v = new RF_Parameters_v1060();
                _v.InitDefaults();
                _v.supportedBandCombinationExt_r10 = new List<BandCombinationParametersExt_r10>();
                const int nBits = 7;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    BandCombinationParametersExt_r10 item = BandCombinationParametersExt_r10.PerDecoder.Instance.Decode(input);
                    _v.supportedBandCombinationExt_r10.Add(item);
                }
                return _v;
            }
        }
    }

    [Serializable]
    public class RF_Parameters_v1090
    {
        public void InitDefaults()
        {
        }

        public List<List<BandParameters_v1090>> supportedBandCombination_v1090 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RF_Parameters_v1090 Decode(BitArrayInputStream input)
            {
                RF_Parameters_v1090 _v = new RF_Parameters_v1090();
                _v.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    _v.supportedBandCombination_v1090 = new List<List<BandParameters_v1090>>();
                    int nBits = 7;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        List<BandParameters_v1090> item = new List<BandParameters_v1090>();
                        nBits = 6;
                        int num5 = input.readBits(nBits) + 1;
                        for (int j = 0; j < num5; j++)
                        {
                            BandParameters_v1090 _v2 = BandParameters_v1090.PerDecoder.Instance.Decode(input);
                            item.Add(_v2);
                        }
                        _v.supportedBandCombination_v1090.Add(item);
                    }
                }
                return _v;
            }
        }
    }

    [Serializable]
    public class RF_Parameters_v1130
    {
        public void InitDefaults()
        {
        }

        public List<BandCombinationParameters_v1130> supportedBandCombination_v1130 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RF_Parameters_v1130 Decode(BitArrayInputStream input)
            {
                RF_Parameters_v1130 _v = new RF_Parameters_v1130();
                _v.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    _v.supportedBandCombination_v1130 = new List<BandCombinationParameters_v1130>();
                    const int nBits = 7;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        BandCombinationParameters_v1130 item = BandCombinationParameters_v1130.PerDecoder.Instance.Decode(input);
                        _v.supportedBandCombination_v1130.Add(item);
                    }
                }
                return _v;
            }
        }
    }

    [Serializable]
    public class RF_Parameters_v9e0
    {
        public void InitDefaults()
        {
        }

        public List<SupportedBandEUTRA_v9e0> supportedBandListEUTRA_v9e0 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RF_Parameters_v9e0 Decode(BitArrayInputStream input)
            {
                RF_Parameters_v9e0 _ve = new RF_Parameters_v9e0();
                _ve.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    _ve.supportedBandListEUTRA_v9e0 = new List<SupportedBandEUTRA_v9e0>();
                    const int nBits = 6;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        SupportedBandEUTRA_v9e0 item = SupportedBandEUTRA_v9e0.PerDecoder.Instance.Decode(input);
                        _ve.supportedBandListEUTRA_v9e0.Add(item);
                    }
                }
                return _ve;
            }
        }
    }

}
