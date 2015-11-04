using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SRS_ConfigAp_r10
    {
        public void InitDefaults()
        {
        }

        public cyclicShiftAp_r10_Enum cyclicShiftAp_r10 { get; set; }

        public long freqDomainPositionAp_r10 { get; set; }

        public SRS_AntennaPort srs_AntennaPortAp_r10 { get; set; }

        public srs_BandwidthAp_r10_Enum srs_BandwidthAp_r10 { get; set; }

        public long transmissionCombAp_r10 { get; set; }

        public enum cyclicShiftAp_r10_Enum
        {
            cs0,
            cs1,
            cs2,
            cs3,
            cs4,
            cs5,
            cs6,
            cs7
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SRS_ConfigAp_r10 Decode(BitArrayInputStream input)
            {
                SRS_ConfigAp_r10 _r = new SRS_ConfigAp_r10();
                _r.InitDefaults();
                int nBits = 2;
                _r.srs_AntennaPortAp_r10 = (SRS_AntennaPort)input.readBits(nBits);
                nBits = 2;
                _r.srs_BandwidthAp_r10 = (srs_BandwidthAp_r10_Enum)input.readBits(nBits);
                _r.freqDomainPositionAp_r10 = input.readBits(5);
                _r.transmissionCombAp_r10 = input.readBits(1);
                nBits = 3;
                _r.cyclicShiftAp_r10 = (cyclicShiftAp_r10_Enum)input.readBits(nBits);
                return _r;
            }
        }

        public enum srs_BandwidthAp_r10_Enum
        {
            bw0,
            bw1,
            bw2,
            bw3
        }
    }
}
