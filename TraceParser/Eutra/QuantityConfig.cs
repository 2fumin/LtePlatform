using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class QuantityConfig
    {
        public void InitDefaults()
        {
        }

        public QuantityConfigCDMA2000 quantityConfigCDMA2000 { get; set; }

        public QuantityConfigEUTRA quantityConfigEUTRA { get; set; }

        public QuantityConfigGERAN quantityConfigGERAN { get; set; }

        public QuantityConfigUTRA quantityConfigUTRA { get; set; }

        public QuantityConfigUTRA_v1020 quantityConfigUTRA_v1020 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public QuantityConfig Decode(BitArrayInputStream input)
            {
                QuantityConfig config = new QuantityConfig();
                config.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 4);
                if (stream.Read())
                {
                    config.quantityConfigEUTRA = QuantityConfigEUTRA.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    config.quantityConfigUTRA = QuantityConfigUTRA.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    config.quantityConfigGERAN = QuantityConfigGERAN.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    config.quantityConfigCDMA2000 = QuantityConfigCDMA2000.PerDecoder.Instance.Decode(input);
                }
                if (flag)
                {
                    BitMaskStream stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        config.quantityConfigUTRA_v1020 = QuantityConfigUTRA_v1020.PerDecoder.Instance.Decode(input);
                    }
                }
                return config;
            }
        }
    }

    [Serializable]
    public class QuantityConfigCDMA2000
    {
        public void InitDefaults()
        {
        }

        public measQuantityCDMA2000_Enum measQuantityCDMA2000 { get; set; }

        public enum measQuantityCDMA2000_Enum
        {
            pilotStrength,
            pilotPnPhaseAndPilotStrength
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public QuantityConfigCDMA2000 Decode(BitArrayInputStream input)
            {
                QuantityConfigCDMA2000 gcdma = new QuantityConfigCDMA2000();
                gcdma.InitDefaults();
                int nBits = 1;
                gcdma.measQuantityCDMA2000 = (measQuantityCDMA2000_Enum)input.readBits(nBits);
                return gcdma;
            }
        }
    }

    [Serializable]
    public class QuantityConfigEUTRA
    {
        public void InitDefaults()
        {
            filterCoefficientRSRP = FilterCoefficient.fc4;
            filterCoefficientRSRQ = FilterCoefficient.fc4;
        }

        public FilterCoefficient filterCoefficientRSRP { get; set; }

        public FilterCoefficient filterCoefficientRSRQ { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public QuantityConfigEUTRA Decode(BitArrayInputStream input)
            {
                int num2;
                QuantityConfigEUTRA geutra = new QuantityConfigEUTRA();
                geutra.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    num2 = (input.readBit() == 0) ? 4 : 4;
                    geutra.filterCoefficientRSRP = (FilterCoefficient)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = (input.readBit() == 0) ? 4 : 4;
                    geutra.filterCoefficientRSRQ = (FilterCoefficient)input.readBits(num2);
                }
                return geutra;
            }
        }
    }

    [Serializable]
    public class QuantityConfigGERAN
    {
        public void InitDefaults()
        {
            filterCoefficient = FilterCoefficient.fc2;
        }

        public FilterCoefficient filterCoefficient { get; set; }

        public measQuantityGERAN_Enum measQuantityGERAN { get; set; }

        public enum measQuantityGERAN_Enum
        {
            rssi
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public QuantityConfigGERAN Decode(BitArrayInputStream input)
            {
                QuantityConfigGERAN ggeran = new QuantityConfigGERAN();
                ggeran.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                int nBits = 1;
                ggeran.measQuantityGERAN = (measQuantityGERAN_Enum)input.readBits(nBits);
                if (stream.Read())
                {
                    nBits = (input.readBit() == 0) ? 4 : 4;
                    ggeran.filterCoefficient = (FilterCoefficient)input.readBits(nBits);
                }
                return ggeran;
            }
        }
    }

    [Serializable]
    public class QuantityConfigUTRA
    {
        public void InitDefaults()
        {
            filterCoefficient = FilterCoefficient.fc4;
        }

        public FilterCoefficient filterCoefficient { get; set; }

        public measQuantityUTRA_FDD_Enum measQuantityUTRA_FDD { get; set; }

        public measQuantityUTRA_TDD_Enum measQuantityUTRA_TDD { get; set; }

        public enum measQuantityUTRA_FDD_Enum
        {
            cpich_RSCP,
            cpich_EcN0
        }

        public enum measQuantityUTRA_TDD_Enum
        {
            pccpch_RSCP
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public QuantityConfigUTRA Decode(BitArrayInputStream input)
            {
                QuantityConfigUTRA gutra = new QuantityConfigUTRA();
                gutra.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                int nBits = 1;
                gutra.measQuantityUTRA_FDD = (measQuantityUTRA_FDD_Enum)input.readBits(nBits);
                nBits = 1;
                gutra.measQuantityUTRA_TDD = (measQuantityUTRA_TDD_Enum)input.readBits(nBits);
                if (stream.Read())
                {
                    nBits = (input.readBit() == 0) ? 4 : 4;
                    gutra.filterCoefficient = (FilterCoefficient)input.readBits(nBits);
                }
                return gutra;
            }
        }
    }

    [Serializable]
    public class QuantityConfigUTRA_v1020
    {
        public void InitDefaults()
        {
            filterCoefficient2_FDD_r10 = FilterCoefficient.fc4;
        }

        public FilterCoefficient filterCoefficient2_FDD_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public QuantityConfigUTRA_v1020 Decode(BitArrayInputStream input)
            {
                QuantityConfigUTRA_v1020 _v = new QuantityConfigUTRA_v1020();
                _v.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    int nBits = (input.readBit() == 0) ? 4 : 4;
                    _v.filterCoefficient2_FDD_r10 = (FilterCoefficient)input.readBits(nBits);
                }
                return _v;
            }
        }
    }

}
