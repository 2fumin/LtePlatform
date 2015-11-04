using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class UplinkNonUEAssociatedLPPaTransport
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UplinkNonUEAssociatedLPPaTransport Decode(BitArrayInputStream input)
            {
                UplinkNonUEAssociatedLPPaTransport transport = new UplinkNonUEAssociatedLPPaTransport();
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
    public class UplinkS1cdma2000tunneling
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UplinkS1cdma2000tunneling Decode(BitArrayInputStream input)
            {
                UplinkS1cdma2000tunneling kscdmatunneling = new UplinkS1cdma2000tunneling();
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
    public class UplinkUEAssociatedLPPaTransport
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UplinkUEAssociatedLPPaTransport Decode(BitArrayInputStream input)
            {
                UplinkUEAssociatedLPPaTransport transport = new UplinkUEAssociatedLPPaTransport();
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
