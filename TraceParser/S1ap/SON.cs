using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class SONConfigurationTransfer
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public SONInformation sONInformation { get; set; }

        public SourceeNB_ID sourceeNB_ID { get; set; }

        public TargeteNB_ID targeteNB_ID { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SONConfigurationTransfer Decode(BitArrayInputStream input)
            {
                SONConfigurationTransfer transfer = new SONConfigurationTransfer();
                transfer.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                transfer.targeteNB_ID = TargeteNB_ID.PerDecoder.Instance.Decode(input);
                transfer.sourceeNB_ID = SourceeNB_ID.PerDecoder.Instance.Decode(input);
                transfer.sONInformation = SONInformation.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    transfer.iE_Extensions = new List<ProtocolExtensionField>();
                    int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        transfer.iE_Extensions.Add(item);
                    }
                }
                return transfer;
            }
        }
    }

    [Serializable]
    public class SONInformation
    {
        public void InitDefaults()
        {
        }

        public SONInformationReply sONInformationReply { get; set; }

        public SONInformationRequest sONInformationRequest { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SONInformation Decode(BitArrayInputStream input)
            {
                SONInformation information = new SONInformation();
                information.InitDefaults();
                input.readBit();
                switch (input.readBits(1))
                {
                    case 0:
                        {
                            int nBits = 1;
                            information.sONInformationRequest = (SONInformationRequest)input.readBits(nBits);
                            return information;
                        }
                    case 1:
                        information.sONInformationReply = SONInformationReply.PerDecoder.Instance.Decode(input);
                        return information;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

    [Serializable]
    public class SONInformationReply
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public X2TNLConfigurationInfo x2TNLConfigurationInfo { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SONInformationReply Decode(BitArrayInputStream input)
            {
                SONInformationReply reply = new SONInformationReply();
                reply.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    reply.x2TNLConfigurationInfo = X2TNLConfigurationInfo.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    reply.iE_Extensions = new List<ProtocolExtensionField>();
                    int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        reply.iE_Extensions.Add(item);
                    }
                }
                return reply;
            }
        }
    }

}
