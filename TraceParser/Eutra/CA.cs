using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CA_MIMO_ParametersDL_r10
    {
        public void InitDefaults()
        {
        }

        public CA_BandwidthClass_r10 ca_BandwidthClassDL_r10 { get; set; }

        public MIMO_CapabilityDL_r10? supportedMIMO_CapabilityDL_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CA_MIMO_ParametersDL_r10 Decode(BitArrayInputStream input)
            {
                CA_MIMO_ParametersDL_r10 _r = new CA_MIMO_ParametersDL_r10();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                int nBits = (input.readBit() == 0) ? 3 : 3;
                _r.ca_BandwidthClassDL_r10 = (CA_BandwidthClass_r10)input.readBits(nBits);
                if (stream.Read())
                {
                    nBits = 2;
                    _r.supportedMIMO_CapabilityDL_r10 = (MIMO_CapabilityDL_r10)input.readBits(nBits);
                }
                return _r;
            }
        }
    }

    [Serializable]
    public class CA_MIMO_ParametersUL_r10
    {
        public void InitDefaults()
        {
        }

        public CA_BandwidthClass_r10 ca_BandwidthClassUL_r10 { get; set; }

        public MIMO_CapabilityUL_r10? supportedMIMO_CapabilityUL_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CA_MIMO_ParametersUL_r10 Decode(BitArrayInputStream input)
            {
                CA_MIMO_ParametersUL_r10 _r = new CA_MIMO_ParametersUL_r10();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                int nBits = (input.readBit() == 0) ? 3 : 3;
                _r.ca_BandwidthClassUL_r10 = (CA_BandwidthClass_r10)input.readBits(nBits);
                if (stream.Read())
                {
                    nBits = 1;
                    _r.supportedMIMO_CapabilityUL_r10 = (MIMO_CapabilityUL_r10)input.readBits(nBits);
                }
                return _r;
            }
        }
    }

}
