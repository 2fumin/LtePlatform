using System;
using Lte.Domain.Common;
using TraceParser.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class UL_DCCH_Message : ITraceMessage
    {
        public void InitDefaults()
        {
        }

        public UL_DCCH_MessageType message { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UL_DCCH_Message Decode(BitArrayInputStream input)
            {
                UL_DCCH_Message message = new UL_DCCH_Message();
                message.InitDefaults();
                message.message = UL_DCCH_MessageType.PerDecoder.Instance.Decode(input);
                return message;
            }
        }
    }

    [Serializable]
    public class UL_DCCH_MessageType
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

            public CounterCheckResponse counterCheckResponse { get; set; }

            public CSFBParametersRequestCDMA2000 csfbParametersRequestCDMA2000 { get; set; }

            public InterFreqRSTDMeasurementIndication_r10 interFreqRSTDMeasurementIndication_r10 { get; set; }

            public MBMSCountingResponse_r10 mbmsCountingResponse_r10 { get; set; }

            public MeasurementReport measurementReport { get; set; }

            public ProximityIndication_r9 proximityIndication_r9 { get; set; }

            public RNReconfigurationComplete_r10 rnReconfigurationComplete_r10 { get; set; }

            public RRCConnectionReconfigurationComplete rrcConnectionReconfigurationComplete { get; set; }

            public RRCConnectionReestablishmentComplete rrcConnectionReestablishmentComplete { get; set; }

            public RRCConnectionSetupComplete rrcConnectionSetupComplete { get; set; }

            public SecurityModeComplete securityModeComplete { get; set; }

            public SecurityModeFailure securityModeFailure { get; set; }

            public UECapabilityInformation ueCapabilityInformation { get; set; }

            public UEInformationResponse_r9 ueInformationResponse_r9 { get; set; }

            public ULHandoverPreparationTransfer ulHandoverPreparationTransfer { get; set; }

            public ULInformationTransfer ulInformationTransfer { get; set; }

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
                            type.csfbParametersRequestCDMA2000 = CSFBParametersRequestCDMA2000.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.measurementReport = MeasurementReport.PerDecoder.Instance.Decode(input);
                            return type;

                        case 2:
                            type.rrcConnectionReconfigurationComplete 
                                = RRCConnectionReconfigurationComplete.PerDecoder.Instance.Decode(input);
                            return type;

                        case 3:
                            type.rrcConnectionReestablishmentComplete 
                                = RRCConnectionReestablishmentComplete.PerDecoder.Instance.Decode(input);
                            return type;

                        case 4:
                            type.rrcConnectionSetupComplete 
                                = RRCConnectionSetupComplete.PerDecoder.Instance.Decode(input);
                            return type;

                        case 5:
                            type.securityModeComplete = SecurityModeComplete.PerDecoder.Instance.Decode(input);
                            return type;

                        case 6:
                            type.securityModeFailure = SecurityModeFailure.PerDecoder.Instance.Decode(input);
                            return type;

                        case 7:
                            type.ueCapabilityInformation = UECapabilityInformation.PerDecoder.Instance.Decode(input);
                            return type;

                        case 8:
                            type.ulHandoverPreparationTransfer 
                                = ULHandoverPreparationTransfer.PerDecoder.Instance.Decode(input);
                            return type;

                        case 9:
                            type.ulInformationTransfer = ULInformationTransfer.PerDecoder.Instance.Decode(input);
                            return type;

                        case 10:
                            type.counterCheckResponse = CounterCheckResponse.PerDecoder.Instance.Decode(input);
                            return type;

                        case 11:
                            type.ueInformationResponse_r9 = UEInformationResponse_r9.PerDecoder.Instance.Decode(input);
                            return type;

                        case 12:
                            type.proximityIndication_r9 = ProximityIndication_r9.PerDecoder.Instance.Decode(input);
                            return type;

                        case 13:
                            type.rnReconfigurationComplete_r10 
                                = RNReconfigurationComplete_r10.PerDecoder.Instance.Decode(input);
                            return type;

                        case 14:
                            type.mbmsCountingResponse_r10 = MBMSCountingResponse_r10.PerDecoder.Instance.Decode(input);
                            return type;

                        case 15:
                            type.interFreqRSTDMeasurementIndication_r10 
                                = InterFreqRSTDMeasurementIndication_r10.PerDecoder.Instance.Decode(input);
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

            public c2_Type c2 { get; set; }

            public messageClassExtensionFuture_r11_Type messageClassExtensionFuture_r11 { get; set; }

            [Serializable]
            public class c2_Type
            {
                public void InitDefaults()
                {
                }

                public InDeviceCoexIndication_r11 inDeviceCoexIndication_r11 { get; set; }

                public MBMSInterestIndication_r11 mbmsInterestIndication_r11 { get; set; }

                public object spare1 { get; set; }

                public object spare10 { get; set; }

                public object spare11 { get; set; }

                public object spare12 { get; set; }

                public object spare13 { get; set; }

                public object spare2 { get; set; }

                public object spare3 { get; set; }

                public object spare4 { get; set; }

                public object spare5 { get; set; }

                public object spare6 { get; set; }

                public object spare7 { get; set; }

                public object spare8 { get; set; }

                public object spare9 { get; set; }

                public UEAssistanceInformation_r11 ueAssistanceInformation_r11 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public c2_Type Decode(BitArrayInputStream input)
                    {
                        c2_Type type = new c2_Type();
                        type.InitDefaults();
                        switch (input.readBits(4))
                        {
                            case 0:
                                type.ueAssistanceInformation_r11 
                                    = UEAssistanceInformation_r11.PerDecoder.Instance.Decode(input);
                                return type;

                            case 1:
                                type.inDeviceCoexIndication_r11 
                                    = InDeviceCoexIndication_r11.PerDecoder.Instance.Decode(input);
                                return type;

                            case 2:
                                type.mbmsInterestIndication_r11 
                                    = MBMSInterestIndication_r11.PerDecoder.Instance.Decode(input);
                                return type;

                            case 3:
                                return type;

                            case 4:
                                return type;

                            case 5:
                                return type;

                            case 6:
                                return type;

                            case 7:
                                return type;

                            case 8:
                                return type;

                            case 9:
                                return type;

                            case 10:
                                return type;

                            case 11:
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
            public class messageClassExtensionFuture_r11_Type
            {
                public void InitDefaults()
                {
                }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public messageClassExtensionFuture_r11_Type Decode(BitArrayInputStream input)
                    {
                        messageClassExtensionFuture_r11_Type type = new messageClassExtensionFuture_r11_Type();
                        type.InitDefaults();
                        return type;
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public messageClassExtension_Type Decode(BitArrayInputStream input)
                {
                    messageClassExtension_Type type = new messageClassExtension_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.c2 = c2_Type.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.messageClassExtensionFuture_r11 = messageClassExtensionFuture_r11_Type.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public UL_DCCH_MessageType Decode(BitArrayInputStream input)
            {
                UL_DCCH_MessageType type = new UL_DCCH_MessageType();
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
