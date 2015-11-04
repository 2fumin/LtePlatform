using System;
using Lte.Domain.Common;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class DL_DCCH_Message : ITraceMessage
    {
        public void InitDefaults()
        {
        }

        public DL_DCCH_MessageType message { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public DL_DCCH_Message Decode(BitArrayInputStream input)
            {
                DL_DCCH_Message message = new DL_DCCH_Message();
                message.InitDefaults();
                message.message = DL_DCCH_MessageType.PerDecoder.Instance.Decode(input);
                return message;
            }
        }
    }

    [Serializable]
    public class DL_DCCH_MessageType
    {
        public void InitDefaults()
        {
        }

        public c1_Type c1 { get; set; }

        public messageClassExtension_Type messageClassExtension { get; set; }

        [Serializable]
        public class c1_Type
        {
            public void InitDefaults()
            {
            }

            public CounterCheck counterCheck { get; set; }

            public CSFBParametersResponseCDMA2000 csfbParametersResponseCDMA2000 { get; set; }

            public DLInformationTransfer dlInformationTransfer { get; set; }

            public HandoverFromEUTRAPreparationRequest handoverFromEUTRAPreparationRequest { get; set; }

            public LoggedMeasurementConfiguration_r10 loggedMeasurementConfiguration_r10 { get; set; }

            public MobilityFromEUTRACommand mobilityFromEUTRACommand { get; set; }

            public RNReconfiguration_r10 rnReconfiguration_r10 { get; set; }

            public RRCConnectionReconfiguration rrcConnectionReconfiguration { get; set; }

            public RRCConnectionRelease rrcConnectionRelease { get; set; }

            public SecurityModeCommand securityModeCommand { get; set; }

            public object spare1 { get; set; }

            public object spare2 { get; set; }

            public object spare3 { get; set; }

            public object spare4 { get; set; }

            public UECapabilityEnquiry ueCapabilityEnquiry { get; set; }

            public UEInformationRequest_r9 ueInformationRequest_r9 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public c1_Type Decode(BitArrayInputStream input)
                {
                    c1_Type type = new c1_Type();
                    type.InitDefaults();
                    switch (input.readBits(4))
                    {
                        case 0:
                            type.csfbParametersResponseCDMA2000 
                                = CSFBParametersResponseCDMA2000.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.dlInformationTransfer = DLInformationTransfer.PerDecoder.Instance.Decode(input);
                            return type;

                        case 2:
                            type.handoverFromEUTRAPreparationRequest 
                                = HandoverFromEUTRAPreparationRequest.PerDecoder.Instance.Decode(input);
                            return type;

                        case 3:
                            type.mobilityFromEUTRACommand = MobilityFromEUTRACommand.PerDecoder.Instance.Decode(input);
                            return type;

                        case 4:
                            type.rrcConnectionReconfiguration 
                                = RRCConnectionReconfiguration.PerDecoder.Instance.Decode(input);
                            return type;

                        case 5:
                            type.rrcConnectionRelease = RRCConnectionRelease.PerDecoder.Instance.Decode(input);
                            return type;

                        case 6:
                            type.securityModeCommand = SecurityModeCommand.PerDecoder.Instance.Decode(input);
                            return type;

                        case 7:
                            type.ueCapabilityEnquiry = UECapabilityEnquiry.PerDecoder.Instance.Decode(input);
                            return type;

                        case 8:
                            type.counterCheck = CounterCheck.PerDecoder.Instance.Decode(input);
                            return type;

                        case 9:
                            type.ueInformationRequest_r9 = UEInformationRequest_r9.PerDecoder.Instance.Decode(input);
                            return type;

                        case 10:
                            type.loggedMeasurementConfiguration_r10 = LoggedMeasurementConfiguration_r10.PerDecoder.Instance.Decode(input);
                            return type;

                        case 11:
                            type.rnReconfiguration_r10 = RNReconfiguration_r10.PerDecoder.Instance.Decode(input);
                            return type;

                        case 12:
                            return type;

                        case 13:
                            return type;

                        case 14:
                            return type;

                        case 15:
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        [Serializable]
        public class messageClassExtension_Type
        {
            public void InitDefaults()
            {
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public messageClassExtension_Type Decode(BitArrayInputStream input)
                {
                    messageClassExtension_Type type = new messageClassExtension_Type();
                    type.InitDefaults();
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public DL_DCCH_MessageType Decode(BitArrayInputStream input)
            {
                DL_DCCH_MessageType type = new DL_DCCH_MessageType();
                type.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        type.c1 = c1_Type.PerDecoder.Instance.Decode(input);
                        return type;

                    case 1:
                        type.messageClassExtension = messageClassExtension_Type.PerDecoder.Instance.Decode(input);
                        return type;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

}

