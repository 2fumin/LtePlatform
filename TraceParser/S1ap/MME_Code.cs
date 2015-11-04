using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class MME_Code
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                return input.readOctetString(1);
            }
        }
    }

    [Serializable]
    public class MME_Group_ID
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
    public class MME_UE_S1AP_ID
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                int num2 = input.readBits(2) + 1;
                input.skipUnreadedBits();
                return input.readBits(num2 * 8);
            }
        }
    }

    [Serializable]
    public class MMEDirectInformationTransfer
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MMEDirectInformationTransfer Decode(BitArrayInputStream input)
            {
                MMEDirectInformationTransfer transfer = new MMEDirectInformationTransfer();
                transfer.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                transfer.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    transfer.protocolIEs.Add(item);
                }
                return transfer;
            }
        }
    }

    [Serializable]
    public class MMEname
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                int num2 = input.readBits(8);
                input.skipUnreadedBits();
                return input.readOctetString(num2 + 1);
            }
        }
    }

    [Serializable]
    public class MMEStatusTransfer
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MMEStatusTransfer Decode(BitArrayInputStream input)
            {
                MMEStatusTransfer transfer = new MMEStatusTransfer();
                transfer.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                transfer.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    transfer.protocolIEs.Add(item);
                }
                return transfer;
            }
        }
    }

    [Serializable]
    public class GUMMEI
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string mME_Code { get; set; }

        public string mME_Group_ID { get; set; }

        public string pLMN_Identity { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public GUMMEI Decode(BitArrayInputStream input)
            {
                GUMMEI gummei = new GUMMEI();
                gummei.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.skipUnreadedBits();
                gummei.pLMN_Identity = input.readOctetString(3);
                input.skipUnreadedBits();
                gummei.mME_Group_ID = input.readOctetString(2);
                input.skipUnreadedBits();
                gummei.mME_Code = input.readOctetString(1);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    gummei.iE_Extensions = new List<ProtocolExtensionField>();
                    int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        gummei.iE_Extensions.Add(item);
                    }
                }
                return gummei;
            }
        }
    }

    [Serializable]
    public class M_TMSI
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                return input.readOctetString(4);
            }
        }
    }

    [Serializable]
    public class RelativeMMECapacity
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                return input.readBits(8);
            }
        }
    }

    [Serializable]
    public class Routing_ID
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                return input.readBits(8);
            }
        }
    }

    [Serializable]
    public class S_TMSI
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string m_TMSI { get; set; }

        public string mMEC { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public S_TMSI Decode(BitArrayInputStream input)
            {
                S_TMSI s_tmsi = new S_TMSI();
                s_tmsi.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.skipUnreadedBits();
                s_tmsi.mMEC = input.readOctetString(1);
                input.skipUnreadedBits();
                s_tmsi.m_TMSI = input.readOctetString(4);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    s_tmsi.iE_Extensions = new List<ProtocolExtensionField>();
                    int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        s_tmsi.iE_Extensions.Add(item);
                    }
                }
                return s_tmsi;
            }
        }
    }

}
