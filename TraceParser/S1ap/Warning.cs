using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class WarningAreaList
    {
        public void InitDefaults()
        {
        }

        public List<EUTRAN_CGI> cellIDList { get; set; }

        public List<string> emergencyAreaIDList { get; set; }

        public List<TAI> trackingAreaListforWarning { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public WarningAreaList Decode(BitArrayInputStream input)
            {
                int num4;
                WarningAreaList list = new WarningAreaList();
                list.InitDefaults();
                input.readBit();
                switch (input.readBits(2))
                {
                    case 0:
                        {
                            input.skipUnreadedBits();
                            list.cellIDList = new List<EUTRAN_CGI>();
                            num4 = 0x10;
                            int num6 = input.readBits(num4) + 1;
                            for (int i = 0; i < num6; i++)
                            {
                                EUTRAN_CGI item = EUTRAN_CGI.PerDecoder.Instance.Decode(input);
                                list.cellIDList.Add(item);
                            }
                            return list;
                        }
                    case 1:
                        {
                            input.skipUnreadedBits();
                            list.trackingAreaListforWarning = new List<TAI>();
                            num4 = 0x10;
                            int num8 = input.readBits(num4) + 1;
                            for (int j = 0; j < num8; j++)
                            {
                                TAI tai = TAI.PerDecoder.Instance.Decode(input);
                                list.trackingAreaListforWarning.Add(tai);
                            }
                            return list;
                        }
                    case 2:
                        {
                            input.skipUnreadedBits();
                            list.emergencyAreaIDList = new List<string>();
                            num4 = 0x10;
                            int num10 = input.readBits(num4) + 1;
                            for (int k = 0; k < num10; k++)
                            {
                                input.skipUnreadedBits();
                                string str = input.readOctetString(3);
                                list.emergencyAreaIDList.Add(str);
                            }
                            return list;
                        }
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

    [Serializable]
    public class WarningMessageContents
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                int num2 = input.readBits(14);
                input.skipUnreadedBits();
                return input.readOctetString(num2 + 1);
            }
        }
    }

    [Serializable]
    public class WarningSecurityInfo
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                return input.readOctetString(50);
            }
        }
    }

    [Serializable]
    public class WarningType
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
    public class WriteReplaceWarningRequest
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public WriteReplaceWarningRequest Decode(BitArrayInputStream input)
            {
                WriteReplaceWarningRequest request = new WriteReplaceWarningRequest();
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
    public class WriteReplaceWarningResponse
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public WriteReplaceWarningResponse Decode(BitArrayInputStream input)
            {
                WriteReplaceWarningResponse response = new WriteReplaceWarningResponse();
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

}
