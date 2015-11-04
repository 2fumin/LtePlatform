using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.X2ap
{
    [Serializable]
    public class SuccessfulOutcome
    {
        public void InitDefaults()
        {
        }

        public Criticality criticality { get; set; }

        public long procedureCode { get; set; }

        public object value { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SuccessfulOutcome Decode(BitArrayInputStream input)
            {
                SuccessfulOutcome outcome = new SuccessfulOutcome();
                outcome.InitDefaults();
                input.skipUnreadedBits();
                outcome.procedureCode = input.readBits(8);
                const int num4 = 2;
                outcome.criticality = (Criticality)input.readBits(num4);
                input.skipUnreadedBits();
                int nBits = 0;
                while (true)
                {
                    switch (input.readBit())
                    {
                        case 0:
                            nBits += input.readBits(7);
                            goto Label_00CF;

                        case 1:
                            switch (input.readBit())
                            {
                                case 0:
                                    nBits += input.readBits(14);
                                    goto Label_00CF;

                                case 1:
                                    input.readBits(2);
                                    nBits += input.readBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_00CF:
                long num3 = input.Position;
                try
                {
                    outcome.value = X2AP_ELEMENTARY_PROCEDURE.Switcher(outcome.procedureCode, "SuccessfulOutcome", input);
                    input.skipUnreadedBits();
                }
                catch (Exception)
                {
                    input.skipUnreadedBits();
                    input.Position = num3;
                    outcome.value = input.readOctetString(nBits);
                }
                if (input.Position != (num3 + nBits))
                {
                    input.Position = num3 + nBits;
                }
                return outcome;
            }
        }
    }

    [Serializable]
    public class UnsuccessfulOutcome
    {
        public void InitDefaults()
        {
        }

        public Criticality criticality { get; set; }

        public long procedureCode { get; set; }

        public object value { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UnsuccessfulOutcome Decode(BitArrayInputStream input)
            {
                UnsuccessfulOutcome outcome = new UnsuccessfulOutcome();
                outcome.InitDefaults();
                input.skipUnreadedBits();
                outcome.procedureCode = input.readBits(8);
                const int num4 = 2;
                outcome.criticality = (Criticality)input.readBits(num4);
                input.skipUnreadedBits();
                int nBits = 0;
                while (true)
                {
                    switch (input.readBit())
                    {
                        case 0:
                            nBits += input.readBits(7);
                            goto Label_00CF;

                        case 1:
                            switch (input.readBit())
                            {
                                case 0:
                                    nBits += input.readBits(14);
                                    goto Label_00CF;

                                case 1:
                                    input.readBits(2);
                                    nBits += input.readBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_00CF:
                long num3 = input.Position;
                try
                {
                    outcome.value = X2AP_ELEMENTARY_PROCEDURE.Switcher(outcome.procedureCode, "UnsuccessfulOutcome", input);
                    input.skipUnreadedBits();
                }
                catch (Exception)
                {
                    input.skipUnreadedBits();
                    input.Position = num3;
                    outcome.value = input.readOctetString(nBits);
                }
                if (input.Position != (num3 + nBits))
                {
                    input.Position = num3 + nBits;
                }
                return outcome;
            }
        }
    }

    [Serializable]
    public class ShortMAC_I
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
    public class TargetCellInUTRAN
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                int nBits = 0;
                while (true)
                {
                    switch (input.readBit())
                    {
                        case 0:
                            nBits += input.readBits(7);
                            goto Label_0096;

                        case 1:
                            switch (input.readBit())
                            {
                                case 0:
                                    nBits += input.readBits(14);
                                    goto Label_0096;

                                case 1:
                                    input.readBits(2);
                                    nBits += input.readBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_0096:
                return input.readOctetString(nBits);
            }
        }
    }

    [Serializable]
    public class TargeteNBtoSource_eNBTransparentContainer
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                int nBits = 0;
                while (true)
                {
                    switch (input.readBit())
                    {
                        case 0:
                            nBits += input.readBits(7);
                            goto Label_0096;

                        case 1:
                            switch (input.readBit())
                            {
                                case 0:
                                    nBits += input.readBits(14);
                                    goto Label_0096;

                                case 1:
                                    input.readBits(2);
                                    nBits += input.readBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_0096:
                return input.readOctetString(nBits);
            }
        }
    }

    [Serializable]
    public class TraceActivation
    {
        public void InitDefaults()
        {
        }

        public string eUTRANTraceID { get; set; }

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
                activation.eUTRANTraceID = input.readOctetString(8);
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
    public class TraceCollectionEntityIPAddress
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
