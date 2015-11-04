using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class MMEConfigurationTransfer
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MMEConfigurationTransfer Decode(BitArrayInputStream input)
            {
                MMEConfigurationTransfer transfer = new MMEConfigurationTransfer();
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
    public class MMEConfigurationUpdate
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MMEConfigurationUpdate Decode(BitArrayInputStream input)
            {
                MMEConfigurationUpdate update = new MMEConfigurationUpdate();
                update.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                update.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    update.protocolIEs.Add(item);
                }
                return update;
            }
        }
    }

    [Serializable]
    public class MMEConfigurationUpdateAcknowledge
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MMEConfigurationUpdateAcknowledge Decode(BitArrayInputStream input)
            {
                MMEConfigurationUpdateAcknowledge acknowledge = new MMEConfigurationUpdateAcknowledge();
                acknowledge.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                acknowledge.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    acknowledge.protocolIEs.Add(item);
                }
                return acknowledge;
            }
        }
    }

    [Serializable]
    public class MMEConfigurationUpdateFailure
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MMEConfigurationUpdateFailure Decode(BitArrayInputStream input)
            {
                MMEConfigurationUpdateFailure failure = new MMEConfigurationUpdateFailure();
                failure.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                failure.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    failure.protocolIEs.Add(item);
                }
                return failure;
            }
        }
    }

}
