using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class MeasParameters
    {
        public void InitDefaults()
        {
        }

        public List<BandInfoEUTRA> bandListEUTRA { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasParameters Decode(BitArrayInputStream input)
            {
                MeasParameters parameters = new MeasParameters();
                parameters.InitDefaults();
                parameters.bandListEUTRA = new List<BandInfoEUTRA>();
                int nBits = 6;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    BandInfoEUTRA item = BandInfoEUTRA.PerDecoder.Instance.Decode(input);
                    parameters.bandListEUTRA.Add(item);
                }
                return parameters;
            }
        }
    }

    [Serializable]
    public class MeasParameters_v1020
    {
        public void InitDefaults()
        {
        }

        public List<BandInfoEUTRA> bandCombinationListEUTRA_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasParameters_v1020 Decode(BitArrayInputStream input)
            {
                MeasParameters_v1020 _v = new MeasParameters_v1020();
                _v.InitDefaults();
                _v.bandCombinationListEUTRA_r10 = new List<BandInfoEUTRA>();
                int nBits = 7;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    BandInfoEUTRA item = BandInfoEUTRA.PerDecoder.Instance.Decode(input);
                    _v.bandCombinationListEUTRA_r10.Add(item);
                }
                return _v;
            }
        }
    }

    [Serializable]
    public class MeasParameters_v1130
    {
        public void InitDefaults()
        {
        }

        public rsrqMeasWideband_r11_Enum? rsrqMeasWideband_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasParameters_v1130 Decode(BitArrayInputStream input)
            {
                MeasParameters_v1130 _v = new MeasParameters_v1130();
                _v.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    const int nBits = 1;
                    _v.rsrqMeasWideband_r11 = (rsrqMeasWideband_r11_Enum)input.readBits(nBits);
                }
                return _v;
            }
        }

        public enum rsrqMeasWideband_r11_Enum
        {
            supported
        }
    }

    [Serializable]
    public class UE_BasedNetwPerfMeasParameters_r10
    {
        public void InitDefaults()
        {
        }

        public loggedMeasurementsIdle_r10_Enum? loggedMeasurementsIdle_r10 { get; set; }

        public standaloneGNSS_Location_r10_Enum? standaloneGNSS_Location_r10 { get; set; }

        public enum loggedMeasurementsIdle_r10_Enum
        {
            supported
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UE_BasedNetwPerfMeasParameters_r10 Decode(BitArrayInputStream input)
            {
                int num2;
                UE_BasedNetwPerfMeasParameters_r10 _r = new UE_BasedNetwPerfMeasParameters_r10();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    num2 = 1;
                    _r.loggedMeasurementsIdle_r10 = (loggedMeasurementsIdle_r10_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _r.standaloneGNSS_Location_r10 = (standaloneGNSS_Location_r10_Enum)input.readBits(num2);
                }
                return _r;
            }
        }

        public enum standaloneGNSS_Location_r10_Enum
        {
            supported
        }
    }

}
