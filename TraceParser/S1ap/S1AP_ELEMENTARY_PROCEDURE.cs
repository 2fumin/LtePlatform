using System;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class S1AP_ELEMENTARY_PROCEDURE
    {
        public static object Switcher(long unique, string membername, BitArrayInputStream input)
        {
            long num = unique;
            if ((num <= 0x2fL) && (num >= 0L))
            {
                switch (((int)num))
                {
                    case 0:
                        return handoverPreparationClass.GetMemberObj(membername, input);

                    case 1:
                        return handoverResourceAllocationClass.GetMemberObj(membername, input);

                    case 2:
                        return handoverNotificationClass.GetMemberObj(membername, input);

                    case 3:
                        return pathSwitchRequestClass.GetMemberObj(membername, input);

                    case 4:
                        return handoverCancelClass.GetMemberObj(membername, input);

                    case 5:
                        return e_RABSetupClass.GetMemberObj(membername, input);

                    case 6:
                        return e_RABModifyClass.GetMemberObj(membername, input);

                    case 7:
                        return e_RABReleaseClass.GetMemberObj(membername, input);

                    case 8:
                        return e_RABReleaseIndicationClass.GetMemberObj(membername, input);

                    case 9:
                        return initialContextSetupClass.GetMemberObj(membername, input);

                    case 10:
                        return pagingClass.GetMemberObj(membername, input);

                    case 11:
                        return downlinkNASTransportClass.GetMemberObj(membername, input);

                    case 12:
                        return initialUEMessageClass.GetMemberObj(membername, input);

                    case 13:
                        return uplinkNASTransportClass.GetMemberObj(membername, input);

                    case 14:
                        return resetClass.GetMemberObj(membername, input);

                    case 15:
                        return errorIndicationClass.GetMemberObj(membername, input);

                    case 0x10:
                        return nASNonDeliveryIndicationClass.GetMemberObj(membername, input);

                    case 0x11:
                        return s1SetupClass.GetMemberObj(membername, input);

                    case 0x12:
                        return uEContextReleaseRequestClass.GetMemberObj(membername, input);

                    case 0x13:
                        return downlinkS1cdma2000tunnelingClass.GetMemberObj(membername, input);

                    case 20:
                        return uplinkS1cdma2000tunnelingClass.GetMemberObj(membername, input);

                    case 0x15:
                        return uEContextModificationClass.GetMemberObj(membername, input);

                    case 0x16:
                        return uECapabilityInfoIndicationClass.GetMemberObj(membername, input);

                    case 0x17:
                        return uEContextReleaseClass.GetMemberObj(membername, input);

                    case 0x18:
                        return eNBStatusTransferClass.GetMemberObj(membername, input);

                    case 0x19:
                        return mMEStatusTransferClass.GetMemberObj(membername, input);

                    case 0x1a:
                        return deactivateTraceClass.GetMemberObj(membername, input);

                    case 0x1b:
                        return traceStartClass.GetMemberObj(membername, input);

                    case 0x1c:
                        return traceFailureIndicationClass.GetMemberObj(membername, input);

                    case 0x1d:
                        return eNBConfigurationUpdateClass.GetMemberObj(membername, input);

                    case 30:
                        return mMEConfigurationUpdateClass.GetMemberObj(membername, input);

                    case 0x1f:
                        return locationReportingControlClass.GetMemberObj(membername, input);

                    case 0x20:
                        return locationReportingFailureIndicationClass.GetMemberObj(membername, input);

                    case 0x21:
                        return locationReportClass.GetMemberObj(membername, input);

                    case 0x22:
                        return overloadStartClass.GetMemberObj(membername, input);

                    case 0x23:
                        return overloadStopClass.GetMemberObj(membername, input);

                    case 0x24:
                        return writeReplaceWarningClass.GetMemberObj(membername, input);

                    case 0x25:
                        return eNBDirectInformationTransferClass.GetMemberObj(membername, input);

                    case 0x26:
                        return mMEDirectInformationTransferClass.GetMemberObj(membername, input);

                    case 0x27:
                        return privateMessageClass.GetMemberObj(membername, input);

                    case 40:
                        return eNBConfigurationTransferClass.GetMemberObj(membername, input);

                    case 0x29:
                        return mMEConfigurationTransferClass.GetMemberObj(membername, input);

                    case 0x2a:
                        return cellTrafficTraceClass.GetMemberObj(membername, input);

                    case 0x2b:
                        return killClass.GetMemberObj(membername, input);

                    case 0x2c:
                        return downlinkUEAssociatedLPPaTransportClass.GetMemberObj(membername, input);

                    case 0x2d:
                        return uplinkUEAssociatedLPPaTransportClass.GetMemberObj(membername, input);

                    case 0x2e:
                        return downlinkNonUEAssociatedLPPaTransportClass.GetMemberObj(membername, input);

                    case 0x2f:
                        return uplinkNonUEAssociatedLPPaTransportClass.GetMemberObj(membername, input);
                }
            }
            return null;
        }

        public Criticality criticality { get; set; }

        public object InitiatingMessage { get; set; }

        public long procedureCode { get; set; }

        public object SuccessfulOutcome { get; set; }

        public object UnsuccessfulOutcome { get; set; }

        public class cellTrafficTraceClass
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

            public static CellTrafficTrace InitiatingMessage(BitArrayInputStream input)
            {
                return CellTrafficTrace.PerDecoder.Instance.Decode(input);
            }
        }

        public class deactivateTraceClass
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

            public static DeactivateTrace InitiatingMessage(BitArrayInputStream input)
            {
                return DeactivateTrace.PerDecoder.Instance.Decode(input);
            }
        }

        public class downlinkNASTransportClass
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

            public static DownlinkNASTransport InitiatingMessage(BitArrayInputStream input)
            {
                return DownlinkNASTransport.PerDecoder.Instance.Decode(input);
            }
        }

        public class downlinkNonUEAssociatedLPPaTransportClass
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

            public static DownlinkNonUEAssociatedLPPaTransport InitiatingMessage(BitArrayInputStream input)
            {
                return DownlinkNonUEAssociatedLPPaTransport.PerDecoder.Instance.Decode(input);
            }
        }

        public class downlinkS1cdma2000tunnelingClass
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

            public static DownlinkS1cdma2000tunneling InitiatingMessage(BitArrayInputStream input)
            {
                return DownlinkS1cdma2000tunneling.PerDecoder.Instance.Decode(input);
            }
        }

        public class downlinkUEAssociatedLPPaTransportClass
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

            public static DownlinkUEAssociatedLPPaTransport InitiatingMessage(BitArrayInputStream input)
            {
                return DownlinkUEAssociatedLPPaTransport.PerDecoder.Instance.Decode(input);
            }
        }

        public class e_RABModifyClass
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

            public static E_RABModifyRequest InitiatingMessage(BitArrayInputStream input)
            {
                return E_RABModifyRequest.PerDecoder.Instance.Decode(input);
            }

            public static E_RABModifyResponse SuccessfulOutcome(BitArrayInputStream input)
            {
                return E_RABModifyResponse.PerDecoder.Instance.Decode(input);
            }
        }

        public class e_RABReleaseClass
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

            public static E_RABReleaseCommand InitiatingMessage(BitArrayInputStream input)
            {
                return E_RABReleaseCommand.PerDecoder.Instance.Decode(input);
            }

            public static E_RABReleaseResponse SuccessfulOutcome(BitArrayInputStream input)
            {
                return E_RABReleaseResponse.PerDecoder.Instance.Decode(input);
            }
        }

        public class e_RABReleaseIndicationClass
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

            public static E_RABReleaseIndication InitiatingMessage(BitArrayInputStream input)
            {
                return E_RABReleaseIndication.PerDecoder.Instance.Decode(input);
            }
        }

        public class e_RABSetupClass
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

            public static E_RABSetupRequest InitiatingMessage(BitArrayInputStream input)
            {
                return E_RABSetupRequest.PerDecoder.Instance.Decode(input);
            }

            public static E_RABSetupResponse SuccessfulOutcome(BitArrayInputStream input)
            {
                return E_RABSetupResponse.PerDecoder.Instance.Decode(input);
            }
        }

        public class eNBConfigurationTransferClass
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

            public static ENBConfigurationTransfer InitiatingMessage(BitArrayInputStream input)
            {
                return ENBConfigurationTransfer.PerDecoder.Instance.Decode(input);
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

        public class eNBDirectInformationTransferClass
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

            public static ENBDirectInformationTransfer InitiatingMessage(BitArrayInputStream input)
            {
                return ENBDirectInformationTransfer.PerDecoder.Instance.Decode(input);
            }
        }

        public class eNBStatusTransferClass
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

            public static ENBStatusTransfer InitiatingMessage(BitArrayInputStream input)
            {
                return ENBStatusTransfer.PerDecoder.Instance.Decode(input);
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
                switch (member)
                {
                    case "InitiatingMessage":
                        return InitiatingMessage(input);

                    case "SuccessfulOutcome":
                        return SuccessfulOutcome(input);
                }
                return null;
            }

            public static HandoverCancel InitiatingMessage(BitArrayInputStream input)
            {
                return HandoverCancel.PerDecoder.Instance.Decode(input);
            }

            public static HandoverCancelAcknowledge SuccessfulOutcome(BitArrayInputStream input)
            {
                return HandoverCancelAcknowledge.PerDecoder.Instance.Decode(input);
            }
        }

        public class handoverNotificationClass
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

            public static HandoverNotify InitiatingMessage(BitArrayInputStream input)
            {
                return HandoverNotify.PerDecoder.Instance.Decode(input);
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

            public static HandoverRequired InitiatingMessage(BitArrayInputStream input)
            {
                return HandoverRequired.PerDecoder.Instance.Decode(input);
            }

            public static HandoverCommand SuccessfulOutcome(BitArrayInputStream input)
            {
                return HandoverCommand.PerDecoder.Instance.Decode(input);
            }

            public static HandoverPreparationFailure UnsuccessfulOutcome(BitArrayInputStream input)
            {
                return HandoverPreparationFailure.PerDecoder.Instance.Decode(input);
            }
        }

        public class handoverResourceAllocationClass
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

            public static HandoverFailure UnsuccessfulOutcome(BitArrayInputStream input)
            {
                return HandoverFailure.PerDecoder.Instance.Decode(input);
            }
        }

        public class initialContextSetupClass
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

            public static InitialContextSetupRequest InitiatingMessage(BitArrayInputStream input)
            {
                return InitialContextSetupRequest.PerDecoder.Instance.Decode(input);
            }

            public static InitialContextSetupResponse SuccessfulOutcome(BitArrayInputStream input)
            {
                return InitialContextSetupResponse.PerDecoder.Instance.Decode(input);
            }

            public static InitialContextSetupFailure UnsuccessfulOutcome(BitArrayInputStream input)
            {
                return InitialContextSetupFailure.PerDecoder.Instance.Decode(input);
            }
        }

        public class initialUEMessageClass
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

            public static InitialUEMessage InitiatingMessage(BitArrayInputStream input)
            {
                return InitialUEMessage.PerDecoder.Instance.Decode(input);
            }
        }

        public class killClass
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

            public static KillRequest InitiatingMessage(BitArrayInputStream input)
            {
                return KillRequest.PerDecoder.Instance.Decode(input);
            }

            public static KillResponse SuccessfulOutcome(BitArrayInputStream input)
            {
                return KillResponse.PerDecoder.Instance.Decode(input);
            }
        }

        public class locationReportClass
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

            public static LocationReport InitiatingMessage(BitArrayInputStream input)
            {
                return LocationReport.PerDecoder.Instance.Decode(input);
            }
        }

        public class locationReportingControlClass
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

            public static LocationReportingControl InitiatingMessage(BitArrayInputStream input)
            {
                return LocationReportingControl.PerDecoder.Instance.Decode(input);
            }
        }

        public class locationReportingFailureIndicationClass
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

            public static LocationReportingFailureIndication InitiatingMessage(BitArrayInputStream input)
            {
                return LocationReportingFailureIndication.PerDecoder.Instance.Decode(input);
            }
        }

        public class mMEConfigurationTransferClass
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

            public static MMEConfigurationTransfer InitiatingMessage(BitArrayInputStream input)
            {
                return MMEConfigurationTransfer.PerDecoder.Instance.Decode(input);
            }
        }

        public class mMEConfigurationUpdateClass
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

            public static MMEConfigurationUpdate InitiatingMessage(BitArrayInputStream input)
            {
                return MMEConfigurationUpdate.PerDecoder.Instance.Decode(input);
            }

            public static MMEConfigurationUpdateAcknowledge SuccessfulOutcome(BitArrayInputStream input)
            {
                return MMEConfigurationUpdateAcknowledge.PerDecoder.Instance.Decode(input);
            }

            public static MMEConfigurationUpdateFailure UnsuccessfulOutcome(BitArrayInputStream input)
            {
                return MMEConfigurationUpdateFailure.PerDecoder.Instance.Decode(input);
            }
        }

        public class mMEDirectInformationTransferClass
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

            public static MMEDirectInformationTransfer InitiatingMessage(BitArrayInputStream input)
            {
                return MMEDirectInformationTransfer.PerDecoder.Instance.Decode(input);
            }
        }

        public class mMEStatusTransferClass
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

            public static MMEStatusTransfer InitiatingMessage(BitArrayInputStream input)
            {
                return MMEStatusTransfer.PerDecoder.Instance.Decode(input);
            }
        }

        public class nASNonDeliveryIndicationClass
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

            public static NASNonDeliveryIndication InitiatingMessage(BitArrayInputStream input)
            {
                return NASNonDeliveryIndication.PerDecoder.Instance.Decode(input);
            }
        }

        public class overloadStartClass
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

            public static OverloadStart InitiatingMessage(BitArrayInputStream input)
            {
                return OverloadStart.PerDecoder.Instance.Decode(input);
            }
        }

        public class overloadStopClass
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

            public static OverloadStop InitiatingMessage(BitArrayInputStream input)
            {
                return OverloadStop.PerDecoder.Instance.Decode(input);
            }
        }

        public class pagingClass
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

            public static Paging InitiatingMessage(BitArrayInputStream input)
            {
                return Paging.PerDecoder.Instance.Decode(input);
            }
        }

        public class pathSwitchRequestClass
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

            public static PathSwitchRequest InitiatingMessage(BitArrayInputStream input)
            {
                return PathSwitchRequest.PerDecoder.Instance.Decode(input);
            }

            public static PathSwitchRequestAcknowledge SuccessfulOutcome(BitArrayInputStream input)
            {
                return PathSwitchRequestAcknowledge.PerDecoder.Instance.Decode(input);
            }

            public static PathSwitchRequestFailure UnsuccessfulOutcome(BitArrayInputStream input)
            {
                return PathSwitchRequestFailure.PerDecoder.Instance.Decode(input);
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

            public static Reset InitiatingMessage(BitArrayInputStream input)
            {
                return Reset.PerDecoder.Instance.Decode(input);
            }

            public static ResetAcknowledge SuccessfulOutcome(BitArrayInputStream input)
            {
                return ResetAcknowledge.PerDecoder.Instance.Decode(input);
            }
        }

        public class s1SetupClass
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

            public static S1SetupRequest InitiatingMessage(BitArrayInputStream input)
            {
                return S1SetupRequest.PerDecoder.Instance.Decode(input);
            }

            public static S1SetupResponse SuccessfulOutcome(BitArrayInputStream input)
            {
                return S1SetupResponse.PerDecoder.Instance.Decode(input);
            }

            public static S1SetupFailure UnsuccessfulOutcome(BitArrayInputStream input)
            {
                return S1SetupFailure.PerDecoder.Instance.Decode(input);
            }
        }

        public class traceFailureIndicationClass
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

            public static TraceFailureIndication InitiatingMessage(BitArrayInputStream input)
            {
                return TraceFailureIndication.PerDecoder.Instance.Decode(input);
            }
        }

        public class traceStartClass
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

            public static TraceStart InitiatingMessage(BitArrayInputStream input)
            {
                return TraceStart.PerDecoder.Instance.Decode(input);
            }
        }

        public class uECapabilityInfoIndicationClass
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

            public static UECapabilityInfoIndication InitiatingMessage(BitArrayInputStream input)
            {
                return UECapabilityInfoIndication.PerDecoder.Instance.Decode(input);
            }
        }

        public class uEContextModificationClass
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

            public static UEContextModificationRequest InitiatingMessage(BitArrayInputStream input)
            {
                return UEContextModificationRequest.PerDecoder.Instance.Decode(input);
            }

            public static UEContextModificationResponse SuccessfulOutcome(BitArrayInputStream input)
            {
                return UEContextModificationResponse.PerDecoder.Instance.Decode(input);
            }

            public static UEContextModificationFailure UnsuccessfulOutcome(BitArrayInputStream input)
            {
                return UEContextModificationFailure.PerDecoder.Instance.Decode(input);
            }
        }

        public class uEContextReleaseClass
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

            public static UEContextReleaseCommand InitiatingMessage(BitArrayInputStream input)
            {
                return UEContextReleaseCommand.PerDecoder.Instance.Decode(input);
            }

            public static UEContextReleaseComplete SuccessfulOutcome(BitArrayInputStream input)
            {
                return UEContextReleaseComplete.PerDecoder.Instance.Decode(input);
            }
        }

        public class uEContextReleaseRequestClass
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

            public static UEContextReleaseRequest InitiatingMessage(BitArrayInputStream input)
            {
                return UEContextReleaseRequest.PerDecoder.Instance.Decode(input);
            }
        }

        public class uplinkNASTransportClass
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

            public static UplinkNASTransport InitiatingMessage(BitArrayInputStream input)
            {
                return UplinkNASTransport.PerDecoder.Instance.Decode(input);
            }
        }

        public class uplinkNonUEAssociatedLPPaTransportClass
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

            public static UplinkNonUEAssociatedLPPaTransport InitiatingMessage(BitArrayInputStream input)
            {
                return UplinkNonUEAssociatedLPPaTransport.PerDecoder.Instance.Decode(input);
            }
        }

        public class uplinkS1cdma2000tunnelingClass
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

            public static UplinkS1cdma2000tunneling InitiatingMessage(BitArrayInputStream input)
            {
                return UplinkS1cdma2000tunneling.PerDecoder.Instance.Decode(input);
            }
        }

        public class uplinkUEAssociatedLPPaTransportClass
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

            public static UplinkUEAssociatedLPPaTransport InitiatingMessage(BitArrayInputStream input)
            {
                return UplinkUEAssociatedLPPaTransport.PerDecoder.Instance.Decode(input);
            }
        }

        public class writeReplaceWarningClass
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

            public static WriteReplaceWarningRequest InitiatingMessage(BitArrayInputStream input)
            {
                return WriteReplaceWarningRequest.PerDecoder.Instance.Decode(input);
            }

            public static WriteReplaceWarningResponse SuccessfulOutcome(BitArrayInputStream input)
            {
                return WriteReplaceWarningResponse.PerDecoder.Instance.Decode(input);
            }
        }
    }
}