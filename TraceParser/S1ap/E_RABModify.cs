using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class E_RABModifyItemBearerModRes
    {
        public void InitDefaults()
        {
        }

        public long e_RAB_ID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABModifyItemBearerModRes Decode(BitArrayInputStream input)
            {
                E_RABModifyItemBearerModRes res = new E_RABModifyItemBearerModRes();
                res.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.readBit();
                res.e_RAB_ID = input.readBits(4);
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
    public class E_RABModifyListBearerModRes
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
    public class E_RABModifyRequest
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABModifyRequest Decode(BitArrayInputStream input)
            {
                E_RABModifyRequest request = new E_RABModifyRequest();
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
    public class E_RABModifyResponse
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABModifyResponse Decode(BitArrayInputStream input)
            {
                E_RABModifyResponse response = new E_RABModifyResponse();
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
    public class E_RABToBeModifiedItemBearerModReq
    {
        public void InitDefaults()
        {
        }

        public long e_RAB_ID { get; set; }

        public E_RABLevelQoSParameters e_RABLevelQoSParameters { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string nAS_PDU { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public E_RABToBeModifiedItemBearerModReq Decode(BitArrayInputStream input)
            {
                E_RABToBeModifiedItemBearerModReq req = new E_RABToBeModifiedItemBearerModReq();
                req.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.readBit();
                req.e_RAB_ID = input.readBits(4);
                req.e_RABLevelQoSParameters = E_RABLevelQoSParameters.PerDecoder.Instance.Decode(input);
                input.skipUnreadedBits();
                int nBits = 0;
                while (true)
                {
                    switch (input.readBit())
                    {
                        case 0:
                            nBits += input.readBits(7);
                            goto Label_00F3;

                        case 1:
                            switch (input.readBit())
                            {
                                case 0:
                                    nBits += input.readBits(14);
                                    goto Label_00F3;

                                case 1:
                                    input.readBits(2);
                                    nBits += input.readBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_00F3:
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
    public class E_RABToBeModifiedListBearerModReq
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
