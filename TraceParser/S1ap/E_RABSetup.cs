using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class E_RABSetupItemBearerSURes
    {
        public void InitDefaults()
        {
        }

        public long e_RAB_ID { get; set; }

        public string gTP_TEID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string transportLayerAddress { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABSetupItemBearerSURes Decode(BitArrayInputStream input)
            {
                E_RABSetupItemBearerSURes res = new E_RABSetupItemBearerSURes();
                res.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.readBit();
                res.e_RAB_ID = input.readBits(4);
                input.readBit();
                int num = input.readBits(8);
                input.skipUnreadedBits();
                res.transportLayerAddress = input.readBitString(num + 1);
                input.skipUnreadedBits();
                res.gTP_TEID = input.readOctetString(4);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    res.iE_Extensions = new List<ProtocolExtensionField>();
                    const int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        res.iE_Extensions.Add(item);
                    }
                }
                return res;
            }
        }
    }

    [Serializable]
    public class E_RABSetupItemCtxtSURes
    {
        public void InitDefaults()
        {
        }

        public long e_RAB_ID { get; set; }

        public string gTP_TEID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string transportLayerAddress { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABSetupItemCtxtSURes Decode(BitArrayInputStream input)
            {
                E_RABSetupItemCtxtSURes res = new E_RABSetupItemCtxtSURes();
                res.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.readBit();
                res.e_RAB_ID = input.readBits(4);
                input.readBit();
                int num = input.readBits(8);
                input.skipUnreadedBits();
                res.transportLayerAddress = input.readBitString(num + 1);
                input.skipUnreadedBits();
                res.gTP_TEID = input.readOctetString(4);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    res.iE_Extensions = new List<ProtocolExtensionField>();
                    int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        res.iE_Extensions.Add(item);
                    }
                }
                return res;
            }
        }
    }

    [Serializable]
    public class E_RABSetupListBearerSURes
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<ProtocolIE_Field> Decode(BitArrayInputStream input)
            {
                return new List<ProtocolIE_Field>();
            }
        }
    }

    [Serializable]
    public class E_RABSetupListCtxtSURes
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<ProtocolIE_Field> Decode(BitArrayInputStream input)
            {
                return new List<ProtocolIE_Field>();
            }
        }
    }

    [Serializable]
    public class E_RABSetupRequest
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABSetupRequest Decode(BitArrayInputStream input)
            {
                E_RABSetupRequest request = new E_RABSetupRequest();
                request.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                request.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    request.protocolIEs.Add(item);
                }
                return request;
            }
        }
    }

    [Serializable]
    public class E_RABSetupResponse
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABSetupResponse Decode(BitArrayInputStream input)
            {
                E_RABSetupResponse response = new E_RABSetupResponse();
                response.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                response.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    response.protocolIEs.Add(item);
                }
                return response;
            }
        }
    }

    [Serializable]
    public class E_RABToBeSetupItemBearerSUReq
    {
        public void InitDefaults()
        {
        }

        public long e_RAB_ID { get; set; }

        public E_RABLevelQoSParameters e_RABlevelQoSParameters { get; set; }

        public string gTP_TEID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string nAS_PDU { get; set; }

        public string transportLayerAddress { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABToBeSetupItemBearerSUReq Decode(BitArrayInputStream input)
            {
                E_RABToBeSetupItemBearerSUReq req = new E_RABToBeSetupItemBearerSUReq();
                req.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.readBit();
                req.e_RAB_ID = input.readBits(4);
                req.e_RABlevelQoSParameters = E_RABLevelQoSParameters.PerDecoder.Instance.Decode(input);
                input.readBit();
                int nBits = input.readBits(8);
                input.skipUnreadedBits();
                req.transportLayerAddress = input.readBitString(nBits + 1);
                input.skipUnreadedBits();
                req.gTP_TEID = input.readOctetString(4);
                input.skipUnreadedBits();
                nBits = 0;
                while (true)
                {
                    switch (input.readBit())
                    {
                        case 0:
                            nBits += input.readBits(7);
                            goto Label_0130;

                        case 1:
                            switch (input.readBit())
                            {
                                case 0:
                                    nBits += input.readBits(14);
                                    goto Label_0130;

                                case 1:
                                    input.readBits(2);
                                    nBits += input.readBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_0130:
                req.nAS_PDU = input.readOctetString(nBits);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    req.iE_Extensions = new List<ProtocolExtensionField>();
                    int num4 = 0x10;
                    int num5 = input.readBits(num4) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        req.iE_Extensions.Add(item);
                    }
                }
                return req;
            }
        }
    }

    [Serializable]
    public class E_RABToBeSetupItemCtxtSUReq
    {
        public void InitDefaults()
        {
        }

        public long e_RAB_ID { get; set; }

        public E_RABLevelQoSParameters e_RABlevelQoSParameters { get; set; }

        public string gTP_TEID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string nAS_PDU { get; set; }

        public string transportLayerAddress { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABToBeSetupItemCtxtSUReq Decode(BitArrayInputStream input)
            {
                E_RABToBeSetupItemCtxtSUReq req = new E_RABToBeSetupItemCtxtSUReq();
                req.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                input.readBit();
                req.e_RAB_ID = input.readBits(4);
                req.e_RABlevelQoSParameters = E_RABLevelQoSParameters.PerDecoder.Instance.Decode(input);
                input.readBit();
                int nBits = input.readBits(8);
                input.skipUnreadedBits();
                req.transportLayerAddress = input.readBitString(nBits + 1);
                input.skipUnreadedBits();
                req.gTP_TEID = input.readOctetString(4);
                if (!stream.Read())
                {
                    goto Label_0154;
                }
                input.skipUnreadedBits();
                nBits = 0;
                while (true)
                {
                    switch (input.readBit())
                    {
                        case 0:
                            nBits += input.readBits(7);
                            goto Label_0144;

                        case 1:
                            switch (input.readBit())
                            {
                                case 0:
                                    nBits += input.readBits(14);
                                    goto Label_0144;

                                case 1:
                                    input.readBits(2);
                                    nBits += input.readBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_0144:
                req.nAS_PDU = input.readOctetString(nBits);
            Label_0154:
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    req.iE_Extensions = new List<ProtocolExtensionField>();
                    int num4 = 0x10;
                    int num5 = input.readBits(num4) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        req.iE_Extensions.Add(item);
                    }
                }
                return req;
            }
        }
    }

    [Serializable]
    public class E_RABToBeSetupItemHOReq
    {
        public void InitDefaults()
        {
        }

        public long e_RAB_ID { get; set; }

        public E_RABLevelQoSParameters e_RABlevelQosParameters { get; set; }

        public string gTP_TEID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string transportLayerAddress { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABToBeSetupItemHOReq Decode(BitArrayInputStream input)
            {
                E_RABToBeSetupItemHOReq req = new E_RABToBeSetupItemHOReq();
                req.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.readBit();
                req.e_RAB_ID = input.readBits(4);
                input.readBit();
                int num = input.readBits(8);
                input.skipUnreadedBits();
                req.transportLayerAddress = input.readBitString(num + 1);
                input.skipUnreadedBits();
                req.gTP_TEID = input.readOctetString(4);
                req.e_RABlevelQosParameters = E_RABLevelQoSParameters.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    req.iE_Extensions = new List<ProtocolExtensionField>();
                    int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        req.iE_Extensions.Add(item);
                    }
                }
                return req;
            }
        }
    }

    [Serializable]
    public class E_RABToBeSetupListBearerSUReq
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<ProtocolIE_Field> Decode(BitArrayInputStream input)
            {
                return new List<ProtocolIE_Field>();
            }
        }
    }

    [Serializable]
    public class E_RABToBeSetupListCtxtSUReq
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<ProtocolIE_Field> Decode(BitArrayInputStream input)
            {
                return new List<ProtocolIE_Field>();
            }
        }
    }

}
