using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class DownlinkNASTransport
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public DownlinkNASTransport Decode(BitArrayInputStream input)
            {
                DownlinkNASTransport transport = new DownlinkNASTransport();
                transport.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                transport.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    transport.protocolIEs.Add(item);
                }
                return transport;
            }
        }
    }

    [Serializable]
    public class DownlinkNonUEAssociatedLPPaTransport
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public DownlinkNonUEAssociatedLPPaTransport Decode(BitArrayInputStream input)
            {
                DownlinkNonUEAssociatedLPPaTransport transport = new DownlinkNonUEAssociatedLPPaTransport();
                transport.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                transport.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    transport.protocolIEs.Add(item);
                }
                return transport;
            }
        }
    }
    
    [Serializable]
    public class DownlinkS1cdma2000tunneling
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public DownlinkS1cdma2000tunneling Decode(BitArrayInputStream input)
            {
                DownlinkS1cdma2000tunneling kscdmatunneling = new DownlinkS1cdma2000tunneling();
                kscdmatunneling.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                kscdmatunneling.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    kscdmatunneling.protocolIEs.Add(item);
                }
                return kscdmatunneling;
            }
        }
    }

    [Serializable]
    public class DownlinkUEAssociatedLPPaTransport
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public DownlinkUEAssociatedLPPaTransport Decode(BitArrayInputStream input)
            {
                DownlinkUEAssociatedLPPaTransport transport = new DownlinkUEAssociatedLPPaTransport();
                transport.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                transport.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    transport.protocolIEs.Add(item);
                }
                return transport;
            }
        }
    }

}
