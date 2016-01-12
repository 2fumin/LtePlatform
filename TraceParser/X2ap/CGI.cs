using System;
using System.Collections.Generic;
using Lte.Domain.Common;
using TraceParser.Common;

namespace TraceParser.X2ap
{
    [Serializable]
    public class CSG_Id
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                return input.readBitString(0x1b);
            }
        }
    }

    [Serializable]
    public class CRNTI
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
    public class E_RAB_ID
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                input.readBit();
                return input.readBits(4);
            }
        }
    }

    [Serializable]
    public class EARFCN
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                int num2 = input.readBits(1) + 1;
                input.skipUnreadedBits();
                return input.readBits(num2 * 8);
            }
        }
    }

    [Serializable]
    public class EARFCNExtension
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                input.readBit();
                int num2 = input.readBits(2) + 1;
                input.skipUnreadedBits();
                return input.readBits(num2 * 8) + 0x10000;
            }
        }
    }

    [Serializable]
    public class ECGI
    {
        public void InitDefaults()
        {
        }

        public string eUTRANcellIdentifier { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string pLMN_Identity { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ECGI Decode(BitArrayInputStream input)
            {
                ECGI ecgi = new ECGI();
                ecgi.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.skipUnreadedBits();
                ecgi.pLMN_Identity = input.readOctetString(3);
                ecgi.eUTRANcellIdentifier = input.readBitString(0x1c);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    ecgi.iE_Extensions = new List<ProtocolExtensionField>();
                    const int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        ecgi.iE_Extensions.Add(item);
                    }
                }
                return ecgi;
            }
        }
    }

    [Serializable]
    public class EPLMNs
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<string> Decode(BitArrayInputStream input)
            {
                return new List<string>();
            }
        }
    }

    [Serializable]
    public class GTP_TEI
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
    public class GUMMEI
    {
        public void InitDefaults()
        {
        }

        public GU_Group_ID gU_Group_ID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string mME_Code { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public GUMMEI Decode(BitArrayInputStream input)
            {
                GUMMEI gummei = new GUMMEI();
                gummei.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                gummei.gU_Group_ID = GU_Group_ID.PerDecoder.Instance.Decode(input);
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
    public class GU_Group_ID
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string mME_Group_ID { get; set; }

        public string pLMN_Identity { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public GU_Group_ID Decode(BitArrayInputStream input)
            {
                GU_Group_ID p_id = new GU_Group_ID();
                p_id.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.skipUnreadedBits();
                p_id.pLMN_Identity = input.readOctetString(3);
                input.skipUnreadedBits();
                p_id.mME_Group_ID = input.readOctetString(2);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    p_id.iE_Extensions = new List<ProtocolExtensionField>();
                    int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        p_id.iE_Extensions.Add(item);
                    }
                }
                return p_id;
            }
        }
    }

    [Serializable]
    public class GUGroupIDList
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<GU_Group_ID> Decode(BitArrayInputStream input)
            {
                return new List<GU_Group_ID>();
            }
        }
    }

    [Serializable]
    public class HFN
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
    public class HFNModified
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
    public class PCI
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                input.readBit();
                input.skipUnreadedBits();
                return input.readBits(0x10);
            }
        }
    }

    [Serializable]
    public class PDCP_SN
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
    public class PDCP_SNExtended
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                return input.readBits(15);
            }
        }
    }

    [Serializable]
    public class PLMN_Identity
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                return input.readOctetString(3);
            }
        }
    }

    [Serializable]
    public class QCI
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
    public class TAC
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

}
