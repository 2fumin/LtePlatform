using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class Handover
    {
        public void InitDefaults()
        {
        }

        public string nas_SecurityParamFromEUTRA { get; set; }

        public SI_OrPSI_GERAN systemInformation { get; set; }

        public string targetRAT_MessageContainer { get; set; }

        public targetRAT_Type_Enum targetRAT_Type { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public Handover Decode(BitArrayInputStream input)
            {
                Handover handover = new Handover();
                handover.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                int num2 = (input.readBit() == 0) ? 3 : 3;
                handover.targetRAT_Type = (targetRAT_Type_Enum)input.readBits(num2);
                int nBits = input.readBits(8);
                handover.targetRAT_MessageContainer = input.readOctetString(nBits);
                if (stream.Read())
                {
                    handover.nas_SecurityParamFromEUTRA = input.readOctetString(1);
                }
                if (stream.Read())
                {
                    handover.systemInformation = SI_OrPSI_GERAN.PerDecoder.Instance.Decode(input);
                }
                return handover;
            }
        }

        public enum targetRAT_Type_Enum
        {
            utra,
            geran,
            cdma2000_1XRTT,
            cdma2000_HRPD,
            spare4,
            spare3,
            spare2,
            spare1
        }
    }

    [Serializable]
    public class HandoverCommand
    {
        public void InitDefaults()
        {
        }

        public criticalExtensions_Type criticalExtensions { get; set; }

        [Serializable]
        public class criticalExtensions_Type
        {
            public void InitDefaults()
            {
            }

            public c1_Type c1 { get; set; }

            public criticalExtensionsFuture_Type criticalExtensionsFuture { get; set; }

            [Serializable]
            public class c1_Type
            {
                public void InitDefaults()
                {
                }

                public HandoverCommand_r8_IEs handoverCommand_r8 { get; set; }

                public object spare1 { get; set; }

                public object spare2 { get; set; }

                public object spare3 { get; set; }

                public object spare4 { get; set; }

                public object spare5 { get; set; }

                public object spare6 { get; set; }

                public object spare7 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public c1_Type Decode(BitArrayInputStream input)
                    {
                        c1_Type type = new c1_Type();
                        type.InitDefaults();
                        switch (input.readBits(3))
                        {
                            case 0:
                                type.handoverCommand_r8 = HandoverCommand_r8_IEs.PerDecoder.Instance.Decode(input);
                                return type;

                            case 1:
                                return type;

                            case 2:
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
                        }
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                }
            }

            [Serializable]
            public class criticalExtensionsFuture_Type
            {
                public void InitDefaults()
                {
                }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public criticalExtensionsFuture_Type Decode(BitArrayInputStream input)
                    {
                        criticalExtensionsFuture_Type type = new criticalExtensionsFuture_Type();
                        type.InitDefaults();
                        return type;
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public criticalExtensions_Type Decode(BitArrayInputStream input)
                {
                    criticalExtensions_Type type = new criticalExtensions_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.c1 = c1_Type.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.criticalExtensionsFuture = criticalExtensionsFuture_Type.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverCommand Decode(BitArrayInputStream input)
            {
                HandoverCommand command = new HandoverCommand();
                command.InitDefaults();
                command.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return command;
            }
        }
    }

    [Serializable]
    public class HandoverCommand_r8_IEs
    {
        public void InitDefaults()
        {
        }

        public string handoverCommandMessage { get; set; }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        [Serializable]
        public class nonCriticalExtension_Type
        {
            public void InitDefaults()
            {
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public nonCriticalExtension_Type Decode(BitArrayInputStream input)
                {
                    nonCriticalExtension_Type type = new nonCriticalExtension_Type();
                    type.InitDefaults();
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverCommand_r8_IEs Decode(BitArrayInputStream input)
            {
                HandoverCommand_r8_IEs es = new HandoverCommand_r8_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                int nBits = input.readBits(8);
                es.handoverCommandMessage = input.readOctetString(nBits);
                if (stream.Read())
                {
                    es.nonCriticalExtension = nonCriticalExtension_Type.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class HandoverFromEUTRAPreparationRequest
    {
        public void InitDefaults()
        {
        }

        public criticalExtensions_Type criticalExtensions { get; set; }

        public long rrc_TransactionIdentifier { get; set; }

        [Serializable]
        public class criticalExtensions_Type
        {
            public void InitDefaults()
            {
            }

            public c1_Type c1 { get; set; }

            public criticalExtensionsFuture_Type criticalExtensionsFuture { get; set; }

            [Serializable]
            public class c1_Type
            {
                public void InitDefaults()
                {
                }

                public HandoverFromEUTRAPreparationRequest_r8_IEs handoverFromEUTRAPreparationRequest_r8 { get; set; }

                public object spare1 { get; set; }

                public object spare2 { get; set; }

                public object spare3 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public c1_Type Decode(BitArrayInputStream input)
                    {
                        c1_Type type = new c1_Type();
                        type.InitDefaults();
                        switch (input.readBits(2))
                        {
                            case 0:
                                type.handoverFromEUTRAPreparationRequest_r8 = HandoverFromEUTRAPreparationRequest_r8_IEs.PerDecoder.Instance.Decode(input);
                                return type;

                            case 1:
                                return type;

                            case 2:
                                return type;

                            case 3:
                                return type;
                        }
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                }
            }

            [Serializable]
            public class criticalExtensionsFuture_Type
            {
                public void InitDefaults()
                {
                }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public criticalExtensionsFuture_Type Decode(BitArrayInputStream input)
                    {
                        criticalExtensionsFuture_Type type = new criticalExtensionsFuture_Type();
                        type.InitDefaults();
                        return type;
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public criticalExtensions_Type Decode(BitArrayInputStream input)
                {
                    criticalExtensions_Type type = new criticalExtensions_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.c1 = c1_Type.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.criticalExtensionsFuture = criticalExtensionsFuture_Type.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverFromEUTRAPreparationRequest Decode(BitArrayInputStream input)
            {
                HandoverFromEUTRAPreparationRequest request = new HandoverFromEUTRAPreparationRequest();
                request.InitDefaults();
                request.rrc_TransactionIdentifier = input.readBits(2);
                request.criticalExtensions = criticalExtensions_Type.PerDecoder.Instance.Decode(input);
                return request;
            }
        }
    }

    [Serializable]
    public class HandoverFromEUTRAPreparationRequest_r8_IEs
    {
        public void InitDefaults()
        {
        }

        public CDMA2000_Type cdma2000_Type { get; set; }

        public string mobilityParameters { get; set; }

        public HandoverFromEUTRAPreparationRequest_v890_IEs nonCriticalExtension { get; set; }

        public string rand { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverFromEUTRAPreparationRequest_r8_IEs Decode(BitArrayInputStream input)
            {
                HandoverFromEUTRAPreparationRequest_r8_IEs es = new HandoverFromEUTRAPreparationRequest_r8_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 3);
                const int num2 = 1;
                es.cdma2000_Type = (CDMA2000_Type)input.readBits(num2);
                if (stream.Read())
                {
                    es.rand = input.readBitString(0x20);
                }
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    es.mobilityParameters = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = HandoverFromEUTRAPreparationRequest_v890_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class HandoverFromEUTRAPreparationRequest_v1020_IEs
    {
        public void InitDefaults()
        {
        }

        public dualRxTxRedirectIndicator_r10_Enum? dualRxTxRedirectIndicator_r10 { get; set; }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        public CarrierFreqCDMA2000 redirectCarrierCDMA2000_1XRTT_r10 { get; set; }

        public enum dualRxTxRedirectIndicator_r10_Enum
        {
            _true
        }

        [Serializable]
        public class nonCriticalExtension_Type
        {
            public void InitDefaults()
            {
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public nonCriticalExtension_Type Decode(BitArrayInputStream input)
                {
                    nonCriticalExtension_Type type = new nonCriticalExtension_Type();
                    type.InitDefaults();
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverFromEUTRAPreparationRequest_v1020_IEs Decode(BitArrayInputStream input)
            {
                HandoverFromEUTRAPreparationRequest_v1020_IEs es = new HandoverFromEUTRAPreparationRequest_v1020_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    int nBits = 1;
                    es.dualRxTxRedirectIndicator_r10 = (dualRxTxRedirectIndicator_r10_Enum)input.readBits(nBits);
                }
                if (stream.Read())
                {
                    es.redirectCarrierCDMA2000_1XRTT_r10 = CarrierFreqCDMA2000.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = nonCriticalExtension_Type.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class HandoverFromEUTRAPreparationRequest_v890_IEs
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public HandoverFromEUTRAPreparationRequest_v920_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverFromEUTRAPreparationRequest_v890_IEs Decode(BitArrayInputStream input)
            {
                HandoverFromEUTRAPreparationRequest_v890_IEs es = new HandoverFromEUTRAPreparationRequest_v890_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    es.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = HandoverFromEUTRAPreparationRequest_v920_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class HandoverFromEUTRAPreparationRequest_v920_IEs
    {
        public void InitDefaults()
        {
        }

        public bool? concurrPrepCDMA2000_HRPD_r9 { get; set; }

        public HandoverFromEUTRAPreparationRequest_v1020_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverFromEUTRAPreparationRequest_v920_IEs Decode(BitArrayInputStream input)
            {
                HandoverFromEUTRAPreparationRequest_v920_IEs es = new HandoverFromEUTRAPreparationRequest_v920_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    es.concurrPrepCDMA2000_HRPD_r9 = input.readBit() == 1;
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = HandoverFromEUTRAPreparationRequest_v1020_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

}
