using System;
using Lte.Domain.Common;

namespace TraceParser.X2ap
{
    [Serializable]
    public class X2AP_ELEMENTARY_PROCEDURE
    {
        public static object Switcher(long unique, string membername, BitArrayInputStream input)
        {
            long num = unique;
            if ((num <= 15L) && (num >= 0L))
            {
                switch (((int)num))
                {
                    case 0:
                        return handoverPreparationClass.GetMemberObj(membername, input);

                    case 1:
                        return handoverCancelClass.GetMemberObj(membername, input);

                    case 2:
                        return loadIndicationClass.GetMemberObj(membername, input);

                    case 3:
                        return errorIndicationClass.GetMemberObj(membername, input);

                    case 4:
                        return snStatusTransferClass.GetMemberObj(membername, input);

                    case 5:
                        return uEContextReleaseClass.GetMemberObj(membername, input);

                    case 6:
                        return x2SetupClass.GetMemberObj(membername, input);

                    case 7:
                        return resetClass.GetMemberObj(membername, input);

                    case 8:
                        return eNBConfigurationUpdateClass.GetMemberObj(membername, input);

                    case 9:
                        return resourceStatusReportingInitiationClass.GetMemberObj(membername, input);

                    case 10:
                        return resourceStatusReportingClass.GetMemberObj(membername, input);

                    case 11:
                        return privateMessageClass.GetMemberObj(membername, input);

                    case 12:
                        return mobilitySettingsChangeClass.GetMemberObj(membername, input);

                    case 13:
                        return rLFIndicationClass.GetMemberObj(membername, input);

                    case 14:
                        return handoverReportClass.GetMemberObj(membername, input);

                    case 15:
                        return cellActivationClass.GetMemberObj(membername, input);
                }
            }
            return null;
        }

        public Criticality criticality { get; set; }

        public object InitiatingMessage { get; set; }

        public long procedureCode { get; set; }

        public object SuccessfulOutcome { get; set; }

        public object UnsuccessfulOutcome { get; set; }

        public class cellActivationClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                switch (member)
                {
                    case "InitiatingMessage":
                        return InitiatingMessage(input);

                    case "SuccessfulOutcome":
                        return SuccessfulOutcome(input);

                    case "UnsuccessfulOutcome":
                        return UnsuccessfulOutcome(input);
                }
                return null;
            }

            public static CellActivationRequest InitiatingMessage(BitArrayInputStream input)
            {
                return CellActivationRequest.PerDecoder.Instance.Decode(input);
            }

            public static CellActivationResponse SuccessfulOutcome(BitArrayInputStream input)
            {
                return CellActivationResponse.PerDecoder.Instance.Decode(input);
            }

            public static CellActivationFailure UnsuccessfulOutcome(BitArrayInputStream input)
            {
                return CellActivationFailure.PerDecoder.Instance.Decode(input);
            }
        }

        public class eNBConfigurationUpdateClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                switch (member)
                {
                    case "InitiatingMessage":
                        return InitiatingMessage(input);

                    case "SuccessfulOutcome":
                        return SuccessfulOutcome(input);

                    case "UnsuccessfulOutcome":
                        return UnsuccessfulOutcome(input);
                }
                return null;
            }

            public static ENBConfigurationUpdate InitiatingMessage(BitArrayInputStream input)
            {
                return ENBConfigurationUpdate.PerDecoder.Instance.Decode(input);
            }

            public static ENBConfigurationUpdateAcknowledge SuccessfulOutcome(BitArrayInputStream input)
            {
                return ENBConfigurationUpdateAcknowledge.PerDecoder.Instance.Decode(input);
            }

            public static ENBConfigurationUpdateFailure UnsuccessfulOutcome(BitArrayInputStream input)
            {
                return ENBConfigurationUpdateFailure.PerDecoder.Instance.Decode(input);
            }
        }

        public class errorIndicationClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "InitiatingMessage"))
                {
                    return InitiatingMessage(input);
                }
                return null;
            }

            public static ErrorIndication InitiatingMessage(BitArrayInputStream input)
            {
                return ErrorIndication.PerDecoder.Instance.Decode(input);
            }
        }

        public class handoverCancelClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "InitiatingMessage"))
                {
                    return InitiatingMessage(input);
                }
                return null;
            }

            public static HandoverCancel InitiatingMessage(BitArrayInputStream input)
            {
                return HandoverCancel.PerDecoder.Instance.Decode(input);
            }
        }

        public class handoverPreparationClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                switch (member)
                {
                    case "InitiatingMessage":
                        return InitiatingMessage(input);

                    case "SuccessfulOutcome":
                        return SuccessfulOutcome(input);

                    case "UnsuccessfulOutcome":
                        return UnsuccessfulOutcome(input);
                }
                return null;
            }

            public static HandoverRequest InitiatingMessage(BitArrayInputStream input)
            {
                return HandoverRequest.PerDecoder.Instance.Decode(input);
            }

            public static HandoverRequestAcknowledge SuccessfulOutcome(BitArrayInputStream input)
            {
                return HandoverRequestAcknowledge.PerDecoder.Instance.Decode(input);
            }

            public static HandoverPreparationFailure UnsuccessfulOutcome(BitArrayInputStream input)
            {
                return HandoverPreparationFailure.PerDecoder.Instance.Decode(input);
            }
        }

        public class handoverReportClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "InitiatingMessage"))
                {
                    return InitiatingMessage(input);
                }
                return null;
            }

            public static HandoverReport InitiatingMessage(BitArrayInputStream input)
            {
                return HandoverReport.PerDecoder.Instance.Decode(input);
            }
        }

        public class loadIndicationClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "InitiatingMessage"))
                {
                    return InitiatingMessage(input);
                }
                return null;
            }

            public static LoadInformation InitiatingMessage(BitArrayInputStream input)
            {
                return LoadInformation.PerDecoder.Instance.Decode(input);
            }
        }

        public class mobilitySettingsChangeClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                switch (member)
                {
                    case "InitiatingMessage":
                        return InitiatingMessage(input);

                    case "SuccessfulOutcome":
                        return SuccessfulOutcome(input);

                    case "UnsuccessfulOutcome":
                        return UnsuccessfulOutcome(input);
                }
                return null;
            }

            public static MobilityChangeRequest InitiatingMessage(BitArrayInputStream input)
            {
                return MobilityChangeRequest.PerDecoder.Instance.Decode(input);
            }

            public static MobilityChangeAcknowledge SuccessfulOutcome(BitArrayInputStream input)
            {
                return MobilityChangeAcknowledge.PerDecoder.Instance.Decode(input);
            }

            public static MobilityChangeFailure UnsuccessfulOutcome(BitArrayInputStream input)
            {
                return MobilityChangeFailure.PerDecoder.Instance.Decode(input);
            }
        }

        public class privateMessageClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "InitiatingMessage"))
                {
                    return InitiatingMessage(input);
                }
                return null;
            }

            public static PrivateMessage InitiatingMessage(BitArrayInputStream input)
            {
                return PrivateMessage.PerDecoder.Instance.Decode(input);
            }
        }

        public class resetClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                switch (member)
                {
                    case "InitiatingMessage":
                        return InitiatingMessage(input);

                    case "SuccessfulOutcome":
                        return SuccessfulOutcome(input);
                }
                return null;
            }

            public static ResetRequest InitiatingMessage(BitArrayInputStream input)
            {
                return ResetRequest.PerDecoder.Instance.Decode(input);
            }

            public static ResetResponse SuccessfulOutcome(BitArrayInputStream input)
            {
                return ResetResponse.PerDecoder.Instance.Decode(input);
            }
        }

        public class resourceStatusReportingClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "InitiatingMessage"))
                {
                    return InitiatingMessage(input);
                }
                return null;
            }

            public static ResourceStatusUpdate InitiatingMessage(BitArrayInputStream input)
            {
                return ResourceStatusUpdate.PerDecoder.Instance.Decode(input);
            }
        }

        public class resourceStatusReportingInitiationClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                switch (member)
                {
                    case "InitiatingMessage":
                        return InitiatingMessage(input);

                    case "SuccessfulOutcome":
                        return SuccessfulOutcome(input);

                    case "UnsuccessfulOutcome":
                        return UnsuccessfulOutcome(input);
                }
                return null;
            }

            public static ResourceStatusRequest InitiatingMessage(BitArrayInputStream input)
            {
                return ResourceStatusRequest.PerDecoder.Instance.Decode(input);
            }

            public static ResourceStatusResponse SuccessfulOutcome(BitArrayInputStream input)
            {
                return ResourceStatusResponse.PerDecoder.Instance.Decode(input);
            }

            public static ResourceStatusFailure UnsuccessfulOutcome(BitArrayInputStream input)
            {
                return ResourceStatusFailure.PerDecoder.Instance.Decode(input);
            }
        }

        public class rLFIndicationClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "InitiatingMessage"))
                {
                    return InitiatingMessage(input);
                }
                return null;
            }

            public static RLFIndication InitiatingMessage(BitArrayInputStream input)
            {
                return RLFIndication.PerDecoder.Instance.Decode(input);
            }
        }

        public class snStatusTransferClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "InitiatingMessage"))
                {
                    return InitiatingMessage(input);
                }
                return null;
            }

            public static SNStatusTransfer InitiatingMessage(BitArrayInputStream input)
            {
                return SNStatusTransfer.PerDecoder.Instance.Decode(input);
            }
        }

        public class uEContextReleaseClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "InitiatingMessage"))
                {
                    return InitiatingMessage(input);
                }
                return null;
            }

            public static UEContextRelease InitiatingMessage(BitArrayInputStream input)
            {
                return UEContextRelease.PerDecoder.Instance.Decode(input);
            }
        }

        public class x2SetupClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                switch (member)
                {
                    case "InitiatingMessage":
                        return InitiatingMessage(input);

                    case "SuccessfulOutcome":
                        return SuccessfulOutcome(input);

                    case "UnsuccessfulOutcome":
                        return UnsuccessfulOutcome(input);
                }
                return null;
            }

            public static X2SetupRequest InitiatingMessage(BitArrayInputStream input)
            {
                return X2SetupRequest.PerDecoder.Instance.Decode(input);
            }

            public static X2SetupResponse SuccessfulOutcome(BitArrayInputStream input)
            {
                return X2SetupResponse.PerDecoder.Instance.Decode(input);
            }

            public static X2SetupFailure UnsuccessfulOutcome(BitArrayInputStream input)
            {
                return X2SetupFailure.PerDecoder.Instance.Decode(input);
            }
        }
    }
}
