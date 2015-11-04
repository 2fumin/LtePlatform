using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class ENB_ID
    {
        public void InitDefaults()
        {
        }

        public string homeENB_ID { get; set; }

        public string macroENB_ID { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ENB_ID Decode(BitArrayInputStream input)
            {
                ENB_ID enb_id = new ENB_ID();
                enb_id.InitDefaults();
                input.readBit();
                switch (input.readBits(1))
                {
                    case 0:
                        enb_id.macroENB_ID = input.readBitString(20);
                        return enb_id;

                    case 1:
                        enb_id.homeENB_ID = input.readBitString(0x1c);
                        return enb_id;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

    [Serializable]
    public class ENB_StatusTransfer_TransparentContainer
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> bearers_SubjectToStatusTransferList { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ENB_StatusTransfer_TransparentContainer Decode(BitArrayInputStream input)
            {
                ENB_StatusTransfer_TransparentContainer container = new ENB_StatusTransfer_TransparentContainer();
                container.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.skipUnreadedBits();
                container.bearers_SubjectToStatusTransferList = new List<ProtocolIE_Field>();
                int nBits = 8;
                int num5 = input.readBits(nBits) + 1;
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    container.bearers_SubjectToStatusTransferList.Add(item);
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    container.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num7 = input.readBits(nBits) + 1;
                    for (int j = 0; j < num7; j++)
                    {
                        ProtocolExtensionField field2 = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        container.iE_Extensions.Add(field2);
                    }
                }
                return container;
            }
        }
    }

    [Serializable]
    public class ENB_UE_S1AP_ID
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
    public class ENBDirectInformationTransfer
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ENBDirectInformationTransfer Decode(BitArrayInputStream input)
            {
                ENBDirectInformationTransfer transfer = new ENBDirectInformationTransfer();
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
    public class ENBname
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
    public class ENBStatusTransfer
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ENBStatusTransfer Decode(BitArrayInputStream input)
            {
                ENBStatusTransfer transfer = new ENBStatusTransfer();
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
    public class ENBX2TLAs
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
    public class Global_ENB_ID
    {
        public void InitDefaults()
        {
        }

        public ENB_ID eNB_ID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string pLMNidentity { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public Global_ENB_ID Decode(BitArrayInputStream input)
            {
                Global_ENB_ID l_enb_id = new Global_ENB_ID();
                l_enb_id.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.skipUnreadedBits();
                l_enb_id.pLMNidentity = input.readOctetString(3);
                l_enb_id.eNB_ID = ENB_ID.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    l_enb_id.iE_Extensions = new List<ProtocolExtensionField>();
                    const int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        l_enb_id.iE_Extensions.Add(item);
                    }
                }
                return l_enb_id;
            }
        }
    }

    [Serializable]
    public class X2TNLConfigurationInfo
    {
        public void InitDefaults()
        {
        }

        public List<string> eNBX2TransportLayerAddresses { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public X2TNLConfigurationInfo Decode(BitArrayInputStream input)
            {
                X2TNLConfigurationInfo info = new X2TNLConfigurationInfo();
                info.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                info.eNBX2TransportLayerAddresses = new List<string>();
                int nBits = 1;
                int num5 = input.readBits(nBits) + 1;
                for (int i = 0; i < num5; i++)
                {
                    input.readBit();
                    int num = input.readBits(8);
                    input.skipUnreadedBits();
                    string item = input.readBitString(num + 1);
                    info.eNBX2TransportLayerAddresses.Add(item);
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    info.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num7 = input.readBits(nBits) + 1;
                    for (int j = 0; j < num7; j++)
                    {
                        ProtocolExtensionField field = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        info.iE_Extensions.Add(field);
                    }
                }
                return info;
            }
        }
    }

}
