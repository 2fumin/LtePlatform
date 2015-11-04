using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.X2ap
{
    [Serializable]
    public class MobilityChangeAcknowledge
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MobilityChangeAcknowledge Decode(BitArrayInputStream input)
            {
                MobilityChangeAcknowledge acknowledge = new MobilityChangeAcknowledge();
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
    public class MobilityChangeFailure
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MobilityChangeFailure Decode(BitArrayInputStream input)
            {
                MobilityChangeFailure failure = new MobilityChangeFailure();
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

    [Serializable]
    public class MobilityChangeRequest
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MobilityChangeRequest Decode(BitArrayInputStream input)
            {
                MobilityChangeRequest request = new MobilityChangeRequest();
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
    public class MobilityInformation
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                return input.readBitString(0x20);
            }
        }
    }

    [Serializable]
    public class MobilityParametersInformation
    {
        public void InitDefaults()
        {
        }

        public long handoverTriggerChange { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MobilityParametersInformation Decode(BitArrayInputStream input)
            {
                MobilityParametersInformation information = new MobilityParametersInformation();
                information.InitDefaults();
                input.readBit();
                information.handoverTriggerChange = input.readBits(6) + -20;
                return information;
            }
        }
    }

    [Serializable]
    public class MobilityParametersModificationRange
    {
        public void InitDefaults()
        {
        }

        public long handoverTriggerChangeLowerLimit { get; set; }

        public long handoverTriggerChangeUpperLimit { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MobilityParametersModificationRange Decode(BitArrayInputStream input)
            {
                MobilityParametersModificationRange range = new MobilityParametersModificationRange();
                range.InitDefaults();
                input.readBit();
                range.handoverTriggerChangeLowerLimit = input.readBits(6) + -20;
                range.handoverTriggerChangeUpperLimit = input.readBits(6) + -20;
                return range;
            }
        }
    }

}
