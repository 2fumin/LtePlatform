using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class CellIdentity
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                return input.readBitString(0x1c);
            }
        }
    }

    [Serializable]
    public class CellTrafficTrace
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellTrafficTrace Decode(BitArrayInputStream input)
            {
                CellTrafficTrace trace = new CellTrafficTrace();
                trace.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                trace.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    trace.protocolIEs.Add(item);
                }
                return trace;
            }
        }
    }

    [Serializable]
    public class CellType
    {
        public void InitDefaults()
        {
        }

        public Cell_Size cell_Size { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellType Decode(BitArrayInputStream input)
            {
                CellType type = new CellType();
                type.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                int nBits = (input.readBit() == 0) ? 2 : 2;
                type.cell_Size = (Cell_Size)input.readBits(nBits);
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
    public class CGI
    {
        public void InitDefaults()
        {
        }

        public string cI { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string lAC { get; set; }

        public string pLMNidentity { get; set; }

        public string rAC { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CGI Decode(BitArrayInputStream input)
            {
                CGI cgi = new CGI();
                cgi.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                input.skipUnreadedBits();
                cgi.pLMNidentity = input.readOctetString(3);
                input.skipUnreadedBits();
                cgi.lAC = input.readOctetString(2);
                input.skipUnreadedBits();
                cgi.cI = input.readOctetString(2);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    cgi.rAC = input.readOctetString(1);
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    cgi.iE_Extensions = new List<ProtocolExtensionField>();
                    int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        cgi.iE_Extensions.Add(item);
                    }
                }
                return cgi;
            }
        }
    }

    [Serializable]
    public class CI
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                return input.readOctetString(2);
            }
        }
    }

    [Serializable]
    public class EUTRAN_CGI
    {
        public void InitDefaults()
        {
        }

        public string cell_ID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string pLMNidentity { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public EUTRAN_CGI Decode(BitArrayInputStream input)
            {
                EUTRAN_CGI eutran_cgi = new EUTRAN_CGI();
                eutran_cgi.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.skipUnreadedBits();
                eutran_cgi.pLMNidentity = input.readOctetString(3);
                eutran_cgi.cell_ID = input.readBitString(0x1c);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    eutran_cgi.iE_Extensions = new List<ProtocolExtensionField>();
                    int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        eutran_cgi.iE_Extensions.Add(item);
                    }
                }
                return eutran_cgi;
            }
        }
    }

    [Serializable]
    public class EUTRANRoundTripDelayEstimationInfo
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

}
