using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.X2ap
{
    [Serializable]
    public class ENBConfigurationUpdate
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ENBConfigurationUpdate Decode(BitArrayInputStream input)
            {
                ENBConfigurationUpdate update = new ENBConfigurationUpdate();
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
    public class ENBConfigurationUpdateAcknowledge
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ENBConfigurationUpdateAcknowledge Decode(BitArrayInputStream input)
            {
                ENBConfigurationUpdateAcknowledge acknowledge = new ENBConfigurationUpdateAcknowledge();
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
    public class ENBConfigurationUpdateFailure
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ENBConfigurationUpdateFailure Decode(BitArrayInputStream input)
            {
                ENBConfigurationUpdateFailure failure = new ENBConfigurationUpdateFailure();
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
