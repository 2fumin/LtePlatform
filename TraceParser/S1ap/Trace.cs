using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class TraceActivation
    {
        public void InitDefaults()
        {
        }

        public string e_UTRAN_Trace_ID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string interfacesToTrace { get; set; }

        public string traceCollectionEntityIPAddress { get; set; }

        public TraceDepth traceDepth { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public TraceActivation Decode(BitArrayInputStream input)
            {
                TraceActivation activation = new TraceActivation();
                activation.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.skipUnreadedBits();
                activation.e_UTRAN_Trace_ID = input.readOctetString(8);
                activation.interfacesToTrace = input.readBitString(8);
                int nBits = (input.readBit() == 0) ? 3 : 3;
                activation.traceDepth = (TraceDepth)input.readBits(nBits);
                input.readBit();
                int num = input.readBits(8);
                input.skipUnreadedBits();
                activation.traceCollectionEntityIPAddress = input.readBitString(num + 1);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    activation.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        activation.iE_Extensions.Add(item);
                    }
                }
                return activation;
            }
        }
    }

    [Serializable]
    public class TraceFailureIndication
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public TraceFailureIndication Decode(BitArrayInputStream input)
            {
                TraceFailureIndication indication = new TraceFailureIndication();
                indication.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                indication.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    indication.protocolIEs.Add(item);
                }
                return indication;
            }
        }
    }

    [Serializable]
    public class TraceStart
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public TraceStart Decode(BitArrayInputStream input)
            {
                TraceStart start = new TraceStart();
                start.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                start.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    start.protocolIEs.Add(item);
                }
                return start;
            }
        }
    }

    [Serializable]
    public class E_UTRAN_Trace_ID
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                return input.readOctetString(8);
            }
        }
    }

    [Serializable]
    public class MessageIdentifier
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                return input.readBitString(0x10);
            }
        }
    }

    [Serializable]
    public class RepetitionPeriod
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                return input.readBits(0x10);
            }
        }
    }

    [Serializable]
    public class RequestType
    {
        public void InitDefaults()
        {
        }

        public EventType eventType { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public ReportArea reportArea { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RequestType Decode(BitArrayInputStream input)
            {
                RequestType type = new RequestType();
                type.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                int nBits = (input.readBit() == 0) ? 2 : 2;
                type.eventType = (EventType)input.readBits(nBits);
                nBits = 1;
                type.reportArea = (ReportArea)input.readBits(nBits);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    type.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        type.iE_Extensions.Add(item);
                    }
                }
                return type;
            }
        }
    }

    [Serializable]
    public class SecurityContext
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public long nextHopChainingCount { get; set; }

        public string nextHopParameter { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SecurityContext Decode(BitArrayInputStream input)
            {
                SecurityContext context = new SecurityContext();
                context.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                context.nextHopChainingCount = input.readBits(3);
                input.skipUnreadedBits();
                context.nextHopParameter = input.readBitString(0x100);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    context.iE_Extensions = new List<ProtocolExtensionField>();
                    int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        context.iE_Extensions.Add(item);
                    }
                }
                return context;
            }
        }
    }

    [Serializable]
    public class SecurityKey
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                return input.readBitString(0x100);
            }
        }
    }

    [Serializable]
    public class SerialNumber
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                return input.readBitString(0x10);
            }
        }
    }

    [Serializable]
    public class TransportLayerAddress
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                input.readBit();
                int num = input.readBits(8);
                return input.readBitString(num + 1);
            }
        }
    }

}
