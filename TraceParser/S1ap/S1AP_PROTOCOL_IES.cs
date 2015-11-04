using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class S1AP_PROTOCOL_IES
    {
        public static object Switcher(long unique, string membername, BitArrayInputStream input)
        {
            long num = unique;
            if ((num <= 0x9fL) && (num >= 0L))
            {
                switch (((int)num))
                {
                    case 0:
                        return id_MME_UE_S1AP_IDClass.GetMemberObj(membername, input);

                    case 1:
                        return id_HandoverTypeClass.GetMemberObj(membername, input);

                    case 2:
                        return id_CauseClass.GetMemberObj(membername, input);

                    case 4:
                        return id_TargetIDClass.GetMemberObj(membername, input);

                    case 8:
                        return id_eNB_UE_S1AP_IDClass.GetMemberObj(membername, input);

                    case 12:
                        return id_E_RABSubjecttoDataForwardingListClass.GetMemberObj(membername, input);

                    case 13:
                        return id_E_RABtoReleaseListHOCmdClass.GetMemberObj(membername, input);

                    case 14:
                        return id_E_RABDataForwardingItemClass.GetMemberObj(membername, input);

                    case 15:
                        return id_E_RABReleaseItemBearerRelCompClass.GetMemberObj(membername, input);

                    case 0x10:
                        return id_E_RABToBeSetupListBearerSUReqClass.GetMemberObj(membername, input);

                    case 0x11:
                        return id_E_RABToBeSetupItemBearerSUReqClass.GetMemberObj(membername, input);

                    case 0x12:
                        return id_E_RABAdmittedListClass.GetMemberObj(membername, input);

                    case 0x13:
                        return id_E_RABFailedToSetupListHOReqAckClass.GetMemberObj(membername, input);

                    case 20:
                        return id_E_RABAdmittedItemClass.GetMemberObj(membername, input);

                    case 0x15:
                        return id_E_RABFailedtoSetupItemHOReqAckClass.GetMemberObj(membername, input);

                    case 0x16:
                        return id_E_RABToBeSwitchedDLListClass.GetMemberObj(membername, input);

                    case 0x17:
                        return id_E_RABToBeSwitchedDLItemClass.GetMemberObj(membername, input);

                    case 0x18:
                        return id_E_RABToBeSetupListCtxtSUReqClass.GetMemberObj(membername, input);

                    case 0x19:
                        return id_TraceActivationClass.GetMemberObj(membername, input);

                    case 0x1a:
                        return id_NAS_PDUClass.GetMemberObj(membername, input);

                    case 0x1b:
                        return id_E_RABToBeSetupItemHOReqClass.GetMemberObj(membername, input);

                    case 0x1c:
                        return id_E_RABSetupListBearerSUResClass.GetMemberObj(membername, input);

                    case 0x1d:
                        return id_E_RABFailedToSetupListBearerSUResClass.GetMemberObj(membername, input);

                    case 30:
                        return id_E_RABToBeModifiedListBearerModReqClass.GetMemberObj(membername, input);

                    case 0x1f:
                        return id_E_RABModifyListBearerModResClass.GetMemberObj(membername, input);

                    case 0x20:
                        return id_E_RABFailedToModifyListClass.GetMemberObj(membername, input);

                    case 0x21:
                        return id_E_RABToBeReleasedListClass.GetMemberObj(membername, input);

                    case 0x22:
                        return id_E_RABFailedToReleaseListClass.GetMemberObj(membername, input);

                    case 0x23:
                        return id_E_RABItemClass.GetMemberObj(membername, input);

                    case 0x24:
                        return id_E_RABToBeModifiedItemBearerModReqClass.GetMemberObj(membername, input);

                    case 0x25:
                        return id_E_RABModifyItemBearerModResClass.GetMemberObj(membername, input);

                    case 0x27:
                        return id_E_RABSetupItemBearerSUResClass.GetMemberObj(membername, input);

                    case 40:
                        return id_SecurityContextClass.GetMemberObj(membername, input);

                    case 0x29:
                        return id_HandoverRestrictionListClass.GetMemberObj(membername, input);

                    case 0x2b:
                        return id_UEPagingIDClass.GetMemberObj(membername, input);

                    case 0x2c:
                        return id_pagingDRXClass.GetMemberObj(membername, input);

                    case 0x2e:
                        return id_TAIListClass.GetMemberObj(membername, input);

                    case 0x2f:
                        return id_TAIItemClass.GetMemberObj(membername, input);

                    case 0x30:
                        return id_E_RABFailedToSetupListCtxtSUResClass.GetMemberObj(membername, input);

                    case 50:
                        return id_E_RABSetupItemCtxtSUResClass.GetMemberObj(membername, input);

                    case 0x33:
                        return id_E_RABSetupListCtxtSUResClass.GetMemberObj(membername, input);

                    case 0x34:
                        return id_E_RABToBeSetupItemCtxtSUReqClass.GetMemberObj(membername, input);

                    case 0x35:
                        return id_E_RABToBeSetupListHOReqClass.GetMemberObj(membername, input);

                    case 0x3a:
                        return id_CriticalityDiagnosticsClass.GetMemberObj(membername, input);

                    case 0x3b:
                        return id_Global_ENB_IDClass.GetMemberObj(membername, input);

                    case 60:
                        return id_eNBnameClass.GetMemberObj(membername, input);

                    case 0x3d:
                        return id_MMEnameClass.GetMemberObj(membername, input);

                    case 0x40:
                        return id_SupportedTAsClass.GetMemberObj(membername, input);

                    case 0x41:
                        return id_TimeToWaitClass.GetMemberObj(membername, input);

                    case 0x42:
                        return id_uEaggregateMaximumBitrateClass.GetMemberObj(membername, input);

                    case 0x43:
                        return id_TAIClass.GetMemberObj(membername, input);

                    case 0x45:
                        return id_E_RABReleaseListBearerRelCompClass.GetMemberObj(membername, input);

                    case 70:
                        return id_cdma2000PDUClass.GetMemberObj(membername, input);

                    case 0x47:
                        return id_cdma2000RATTypeClass.GetMemberObj(membername, input);

                    case 0x48:
                        return id_cdma2000SectorIDClass.GetMemberObj(membername, input);

                    case 0x49:
                        return id_SecurityKeyClass.GetMemberObj(membername, input);

                    case 0x4a:
                        return id_UERadioCapabilityClass.GetMemberObj(membername, input);

                    case 0x4b:
                        return id_GUMMEI_IDClass.GetMemberObj(membername, input);

                    case 0x4e:
                        return id_E_RABInformationListItemClass.GetMemberObj(membername, input);

                    case 0x4f:
                        return id_Direct_Forwarding_Path_AvailabilityClass.GetMemberObj(membername, input);

                    case 80:
                        return id_UEIdentityIndexValueClass.GetMemberObj(membername, input);

                    case 0x53:
                        return id_cdma2000HOStatusClass.GetMemberObj(membername, input);

                    case 0x54:
                        return id_cdma2000HORequiredIndicationClass.GetMemberObj(membername, input);

                    case 0x56:
                        return id_E_UTRAN_Trace_IDClass.GetMemberObj(membername, input);

                    case 0x57:
                        return id_RelativeMMECapacityClass.GetMemberObj(membername, input);

                    case 0x58:
                        return id_SourceMME_UE_S1AP_IDClass.GetMemberObj(membername, input);

                    case 0x59:
                        return id_Bearers_SubjectToStatusTransfer_ItemClass.GetMemberObj(membername, input);

                    case 90:
                        return id_eNB_StatusTransfer_TransparentContainerClass.GetMemberObj(membername, input);

                    case 0x5b:
                        return id_UE_associatedLogicalS1_ConnectionItemClass.GetMemberObj(membername, input);

                    case 0x5c:
                        return id_ResetTypeClass.GetMemberObj(membername, input);

                    case 0x5d:
                        return id_UE_associatedLogicalS1_ConnectionListResAckClass.GetMemberObj(membername, input);

                    case 0x5e:
                        return id_E_RABToBeSwitchedULItemClass.GetMemberObj(membername, input);

                    case 0x5f:
                        return id_E_RABToBeSwitchedULListClass.GetMemberObj(membername, input);

                    case 0x60:
                        return id_S_TMSIClass.GetMemberObj(membername, input);

                    case 0x61:
                        return id_cdma2000OneXRANDClass.GetMemberObj(membername, input);

                    case 0x62:
                        return id_RequestTypeClass.GetMemberObj(membername, input);

                    case 0x63:
                        return id_UE_S1AP_IDsClass.GetMemberObj(membername, input);

                    case 100:
                        return id_EUTRAN_CGIClass.GetMemberObj(membername, input);

                    case 0x65:
                        return id_OverloadResponseClass.GetMemberObj(membername, input);

                    case 0x66:
                        return id_cdma2000OneXSRVCCInfoClass.GetMemberObj(membername, input);

                    case 0x68:
                        return id_Source_ToTarget_TransparentContainerClass.GetMemberObj(membername, input);

                    case 0x69:
                        return id_ServedGUMMEIsClass.GetMemberObj(membername, input);

                    case 0x6a:
                        return id_SubscriberProfileIDforRFPClass.GetMemberObj(membername, input);

                    case 0x6b:
                        return id_UESecurityCapabilitiesClass.GetMemberObj(membername, input);

                    case 0x6c:
                        return id_CSFallbackIndicatorClass.GetMemberObj(membername, input);

                    case 0x6d:
                        return id_CNDomainClass.GetMemberObj(membername, input);

                    case 110:
                        return id_E_RABReleasedListClass.GetMemberObj(membername, input);

                    case 0x6f:
                        return id_MessageIdentifierClass.GetMemberObj(membername, input);

                    case 0x70:
                        return id_SerialNumberClass.GetMemberObj(membername, input);

                    case 0x71:
                        return id_WarningAreaListClass.GetMemberObj(membername, input);

                    case 0x72:
                        return id_RepetitionPeriodClass.GetMemberObj(membername, input);

                    case 0x73:
                        return id_NumberofBroadcastRequestClass.GetMemberObj(membername, input);

                    case 0x74:
                        return id_WarningTypeClass.GetMemberObj(membername, input);

                    case 0x75:
                        return id_WarningSecurityInfoClass.GetMemberObj(membername, input);

                    case 0x76:
                        return id_DataCodingSchemeClass.GetMemberObj(membername, input);

                    case 0x77:
                        return id_WarningMessageContentsClass.GetMemberObj(membername, input);

                    case 120:
                        return id_BroadcastCompletedAreaListClass.GetMemberObj(membername, input);

                    case 0x79:
                        return id_Inter_SystemInformationTransferTypeEDTClass.GetMemberObj(membername, input);

                    case 0x7a:
                        return id_Inter_SystemInformationTransferTypeMDTClass.GetMemberObj(membername, input);

                    case 0x7b:
                        return id_Target_ToSource_TransparentContainerClass.GetMemberObj(membername, input);

                    case 0x7c:
                        return id_SRVCCOperationPossibleClass.GetMemberObj(membername, input);

                    case 0x7d:
                        return id_SRVCCHOIndicationClass.GetMemberObj(membername, input);

                    case 0x7f:
                        return id_CSG_IdClass.GetMemberObj(membername, input);

                    case 0x80:
                        return id_CSG_IdListClass.GetMemberObj(membername, input);

                    case 0x81:
                        return id_SONConfigurationTransferECTClass.GetMemberObj(membername, input);

                    case 130:
                        return id_SONConfigurationTransferMCTClass.GetMemberObj(membername, input);

                    case 0x83:
                        return id_TraceCollectionEntityIPAddressClass.GetMemberObj(membername, input);

                    case 0x84:
                        return id_MSClassmark2Class.GetMemberObj(membername, input);

                    case 0x85:
                        return id_MSClassmark3Class.GetMemberObj(membername, input);

                    case 0x86:
                        return id_RRC_Establishment_CauseClass.GetMemberObj(membername, input);

                    case 0x87:
                        return id_NASSecurityParametersfromE_UTRANClass.GetMemberObj(membername, input);

                    case 0x88:
                        return id_NASSecurityParameterstoE_UTRANClass.GetMemberObj(membername, input);

                    case 0x89:
                        return id_DefaultPagingDRXClass.GetMemberObj(membername, input);

                    case 0x8a:
                        return id_Source_ToTarget_TransparentContainer_SecondaryClass.GetMemberObj(membername, input);

                    case 0x8b:
                        return id_Target_ToSource_TransparentContainer_SecondaryClass.GetMemberObj(membername, input);

                    case 140:
                        return id_EUTRANRoundTripDelayEstimationInfoClass.GetMemberObj(membername, input);

                    case 0x8d:
                        return id_BroadcastCancelledAreaListClass.GetMemberObj(membername, input);

                    case 0x8e:
                        return id_ConcurrentWarningMessageIndicatorClass.GetMemberObj(membername, input);

                    case 0x90:
                        return id_ExtendedRepetitionPeriodClass.GetMemberObj(membername, input);

                    case 0x91:
                        return id_CellAccessModeClass.GetMemberObj(membername, input);

                    case 0x92:
                        return id_CSGMembershipStatusClass.GetMemberObj(membername, input);

                    case 0x93:
                        return id_LPPa_PDUClass.GetMemberObj(membername, input);

                    case 0x94:
                        return id_Routing_IDClass.GetMemberObj(membername, input);

                    case 150:
                        return id_PS_ServiceNotAvailableClass.GetMemberObj(membername, input);

                    case 0x9f:
                        return id_RegisteredLAIClass.GetMemberObj(membername, input);
                }
            }
            return null;
        }

        public Criticality criticality { get; set; }

        public long id { get; set; }

        public Presence presence { get; set; }

        public object Value { get; set; }

        public class id_Bearers_SubjectToStatusTransfer_ItemClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static Bearers_SubjectToStatusTransfer_Item Value(BitArrayInputStream input)
            {
                return Bearers_SubjectToStatusTransfer_Item.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_BroadcastCancelledAreaListClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static BroadcastCancelledAreaList Value(BitArrayInputStream input)
            {
                return BroadcastCancelledAreaList.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_BroadcastCompletedAreaListClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static BroadcastCompletedAreaList Value(BitArrayInputStream input)
            {
                return BroadcastCompletedAreaList.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_CauseClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static Cause Value(BitArrayInputStream input)
            {
                return Cause.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_cdma2000HORequiredIndicationClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static Cdma2000HORequiredIndication Value(BitArrayInputStream input)
            {
                const int nBits = 1;
                return (Cdma2000HORequiredIndication)input.readBits(nBits);
            }
        }

        public class id_cdma2000HOStatusClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static Cdma2000HOStatus Value(BitArrayInputStream input)
            {
                const int nBits = 1;
                return (Cdma2000HOStatus)input.readBits(nBits);
            }
        }

        public class id_cdma2000OneXRANDClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return Cdma2000OneXRAND.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_cdma2000OneXSRVCCInfoClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static Cdma2000OneXSRVCCInfo Value(BitArrayInputStream input)
            {
                return Cdma2000OneXSRVCCInfo.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_cdma2000PDUClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return Cdma2000PDU.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_cdma2000RATTypeClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static Cdma2000RATType Value(BitArrayInputStream input)
            {
                const int nBits = 1;
                return (Cdma2000RATType)input.readBits(nBits);
            }
        }

        public class id_cdma2000SectorIDClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return Cdma2000SectorID.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_CellAccessModeClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static CellAccessMode Value(BitArrayInputStream input)
            {
                const int nBits = 1;
                return (CellAccessMode)input.readBits(nBits);
            }
        }

        public class id_CNDomainClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static CNDomain Value(BitArrayInputStream input)
            {
                const int nBits = 1;
                return (CNDomain)input.readBits(nBits);
            }
        }

        public class id_ConcurrentWarningMessageIndicatorClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static ConcurrentWarningMessageIndicator Value(BitArrayInputStream input)
            {
                const int nBits = 1;
                return (ConcurrentWarningMessageIndicator)input.readBits(nBits);
            }
        }

        public class id_CriticalityDiagnosticsClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static CriticalityDiagnostics Value(BitArrayInputStream input)
            {
                return CriticalityDiagnostics.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_CSFallbackIndicatorClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static CSFallbackIndicator Value(BitArrayInputStream input)
            {
                const int nBits = 1;
                return (CSFallbackIndicator)input.readBits(nBits);
            }
        }

        public class id_CSG_IdClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return CSG_Id.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_CSG_IdListClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<CSG_IdList_Item> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<CSG_IdList_Item> list = new List<CSG_IdList_Item>();
                const int nBits = 8;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    CSG_IdList_Item item = CSG_IdList_Item.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_CSGMembershipStatusClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static CSGMembershipStatus Value(BitArrayInputStream input)
            {
                const int nBits = 1;
                return (CSGMembershipStatus)input.readBits(nBits);
            }
        }

        public class id_DataCodingSchemeClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return DataCodingScheme.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_DefaultPagingDRXClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static PagingDRX Value(BitArrayInputStream input)
            {
                int nBits = (input.readBit() == 0) ? 2 : 2;
                return (PagingDRX)input.readBits(nBits);
            }
        }

        public class id_Direct_Forwarding_Path_AvailabilityClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static Direct_Forwarding_Path_Availability Value(BitArrayInputStream input)
            {
                const int nBits = 1;
                return (Direct_Forwarding_Path_Availability)input.readBits(nBits);
            }
        }

        public class id_E_RABAdmittedItemClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static E_RABAdmittedItem Value(BitArrayInputStream input)
            {
                return E_RABAdmittedItem.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_E_RABAdmittedListClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<ProtocolIE_Field> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ProtocolIE_Field> list = new List<ProtocolIE_Field>();
                const int nBits = 0;
                int num2 = input.readBits(nBits);
                for (int i = 0; i < num2; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_E_RABDataForwardingItemClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static E_RABDataForwardingItem Value(BitArrayInputStream input)
            {
                return E_RABDataForwardingItem.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_E_RABFailedToModifyListClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<ProtocolIE_Field> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ProtocolIE_Field> list = new List<ProtocolIE_Field>();
                const int nBits = 8;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_E_RABFailedToReleaseListClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<ProtocolIE_Field> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ProtocolIE_Field> list = new List<ProtocolIE_Field>();
                const int nBits = 8;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_E_RABFailedtoSetupItemHOReqAckClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static E_RABFailedToSetupItemHOReqAck Value(BitArrayInputStream input)
            {
                return E_RABFailedToSetupItemHOReqAck.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_E_RABFailedToSetupListBearerSUResClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<ProtocolIE_Field> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ProtocolIE_Field> list = new List<ProtocolIE_Field>();
                int nBits = 8;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_E_RABFailedToSetupListCtxtSUResClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<ProtocolIE_Field> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ProtocolIE_Field> list = new List<ProtocolIE_Field>();
                int nBits = 8;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_E_RABFailedToSetupListHOReqAckClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<ProtocolIE_Field> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ProtocolIE_Field> list = new List<ProtocolIE_Field>();
                int nBits = 0;
                int num2 = input.readBits(nBits);
                for (int i = 0; i < num2; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_E_RABInformationListItemClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static E_RABInformationListItem Value(BitArrayInputStream input)
            {
                return E_RABInformationListItem.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_E_RABItemClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static E_RABItem Value(BitArrayInputStream input)
            {
                return E_RABItem.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_E_RABModifyItemBearerModResClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static E_RABModifyItemBearerModRes Value(BitArrayInputStream input)
            {
                return E_RABModifyItemBearerModRes.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_E_RABModifyListBearerModResClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<ProtocolIE_Field> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ProtocolIE_Field> list = new List<ProtocolIE_Field>();
                int nBits = 8;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_E_RABReleasedListClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<ProtocolIE_Field> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ProtocolIE_Field> list = new List<ProtocolIE_Field>();
                int nBits = 8;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_E_RABReleaseItemBearerRelCompClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static E_RABReleaseItemBearerRelComp Value(BitArrayInputStream input)
            {
                return E_RABReleaseItemBearerRelComp.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_E_RABReleaseListBearerRelCompClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<ProtocolIE_Field> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ProtocolIE_Field> list = new List<ProtocolIE_Field>();
                int nBits = 8;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_E_RABSetupItemBearerSUResClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static E_RABSetupItemBearerSURes Value(BitArrayInputStream input)
            {
                return E_RABSetupItemBearerSURes.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_E_RABSetupItemCtxtSUResClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static E_RABSetupItemCtxtSURes Value(BitArrayInputStream input)
            {
                return E_RABSetupItemCtxtSURes.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_E_RABSetupListBearerSUResClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<ProtocolIE_Field> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ProtocolIE_Field> list = new List<ProtocolIE_Field>();
                int nBits = 8;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_E_RABSetupListCtxtSUResClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<ProtocolIE_Field> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ProtocolIE_Field> list = new List<ProtocolIE_Field>();
                int nBits = 8;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_E_RABSubjecttoDataForwardingListClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<ProtocolIE_Field> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ProtocolIE_Field> list = new List<ProtocolIE_Field>();
                int nBits = 0;
                int num2 = input.readBits(nBits);
                for (int i = 0; i < num2; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_E_RABToBeModifiedItemBearerModReqClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static E_RABToBeModifiedItemBearerModReq Value(BitArrayInputStream input)
            {
                return E_RABToBeModifiedItemBearerModReq.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_E_RABToBeModifiedListBearerModReqClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<ProtocolIE_Field> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ProtocolIE_Field> list = new List<ProtocolIE_Field>();
                int nBits = 8;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_E_RABToBeReleasedListClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<ProtocolIE_Field> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ProtocolIE_Field> list = new List<ProtocolIE_Field>();
                int nBits = 8;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_E_RABToBeSetupItemBearerSUReqClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static E_RABToBeSetupItemBearerSUReq Value(BitArrayInputStream input)
            {
                return E_RABToBeSetupItemBearerSUReq.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_E_RABToBeSetupItemCtxtSUReqClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static E_RABToBeSetupItemCtxtSUReq Value(BitArrayInputStream input)
            {
                return E_RABToBeSetupItemCtxtSUReq.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_E_RABToBeSetupItemHOReqClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static E_RABToBeSetupItemHOReq Value(BitArrayInputStream input)
            {
                return E_RABToBeSetupItemHOReq.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_E_RABToBeSetupListBearerSUReqClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<ProtocolIE_Field> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ProtocolIE_Field> list = new List<ProtocolIE_Field>();
                int nBits = 8;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_E_RABToBeSetupListCtxtSUReqClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<ProtocolIE_Field> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ProtocolIE_Field> list = new List<ProtocolIE_Field>();
                int nBits = 8;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_E_RABToBeSetupListHOReqClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<ProtocolIE_Field> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ProtocolIE_Field> list = new List<ProtocolIE_Field>();
                int nBits = 0;
                int num2 = input.readBits(nBits);
                for (int i = 0; i < num2; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_E_RABToBeSwitchedDLItemClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static E_RABToBeSwitchedDLItem Value(BitArrayInputStream input)
            {
                return E_RABToBeSwitchedDLItem.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_E_RABToBeSwitchedDLListClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<ProtocolIE_Field> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ProtocolIE_Field> list = new List<ProtocolIE_Field>();
                int nBits = 0;
                int num2 = input.readBits(nBits);
                for (int i = 0; i < num2; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_E_RABToBeSwitchedULItemClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static E_RABToBeSwitchedULItem Value(BitArrayInputStream input)
            {
                return E_RABToBeSwitchedULItem.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_E_RABToBeSwitchedULListClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<ProtocolIE_Field> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ProtocolIE_Field> list = new List<ProtocolIE_Field>();
                int nBits = 0;
                int num2 = input.readBits(nBits);
                for (int i = 0; i < num2; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_E_RABtoReleaseListHOCmdClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<ProtocolIE_Field> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ProtocolIE_Field> list = new List<ProtocolIE_Field>();
                int nBits = 8;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_E_UTRAN_Trace_IDClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return E_UTRAN_Trace_ID.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_eNB_StatusTransfer_TransparentContainerClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static ENB_StatusTransfer_TransparentContainer Value(BitArrayInputStream input)
            {
                return ENB_StatusTransfer_TransparentContainer.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_eNB_UE_S1AP_IDClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static long Value(BitArrayInputStream input)
            {
                return ENB_UE_S1AP_ID.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_eNBnameClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return ENBname.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_EUTRAN_CGIClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static EUTRAN_CGI Value(BitArrayInputStream input)
            {
                return EUTRAN_CGI.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_EUTRANRoundTripDelayEstimationInfoClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static long Value(BitArrayInputStream input)
            {
                return EUTRANRoundTripDelayEstimationInfo.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_ExtendedRepetitionPeriodClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static long Value(BitArrayInputStream input)
            {
                return ExtendedRepetitionPeriod.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_Global_ENB_IDClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static Global_ENB_ID Value(BitArrayInputStream input)
            {
                return Global_ENB_ID.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_GUMMEI_IDClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static GUMMEI Value(BitArrayInputStream input)
            {
                return GUMMEI.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_HandoverRestrictionListClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static HandoverRestrictionList Value(BitArrayInputStream input)
            {
                return HandoverRestrictionList.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_HandoverTypeClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static HandoverType Value(BitArrayInputStream input)
            {
                int nBits = (input.readBit() == 0) ? 3 : 3;
                return (HandoverType)input.readBits(nBits);
            }
        }

        public class id_Inter_SystemInformationTransferTypeEDTClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static Inter_SystemInformationTransferType Value(BitArrayInputStream input)
            {
                return Inter_SystemInformationTransferType.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_Inter_SystemInformationTransferTypeMDTClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static Inter_SystemInformationTransferType Value(BitArrayInputStream input)
            {
                return Inter_SystemInformationTransferType.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_LPPa_PDUClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return LPPa_PDU.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_MessageIdentifierClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return MessageIdentifier.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_MME_UE_S1AP_IDClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static long Value(BitArrayInputStream input)
            {
                return MME_UE_S1AP_ID.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_MMEnameClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return MMEname.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_MSClassmark2Class
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return MSClassmark2.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_MSClassmark3Class
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return MSClassmark3.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_NAS_PDUClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return NAS_PDU.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_NASSecurityParametersfromE_UTRANClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return NASSecurityParametersfromE_UTRAN.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_NASSecurityParameterstoE_UTRANClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return NASSecurityParameterstoE_UTRAN.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_NumberofBroadcastRequestClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static long Value(BitArrayInputStream input)
            {
                return NumberofBroadcastRequest.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_OverloadResponseClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static OverloadResponse Value(BitArrayInputStream input)
            {
                return OverloadResponse.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_pagingDRXClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static PagingDRX Value(BitArrayInputStream input)
            {
                int nBits = (input.readBit() == 0) ? 2 : 2;
                return (PagingDRX)input.readBits(nBits);
            }
        }

        public class id_PS_ServiceNotAvailableClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static PS_ServiceNotAvailable Value(BitArrayInputStream input)
            {
                int nBits = 1;
                return (PS_ServiceNotAvailable)input.readBits(nBits);
            }
        }

        public class id_RegisteredLAIClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static LAI Value(BitArrayInputStream input)
            {
                return LAI.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_RelativeMMECapacityClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static long Value(BitArrayInputStream input)
            {
                return RelativeMMECapacity.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_RepetitionPeriodClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static long Value(BitArrayInputStream input)
            {
                return RepetitionPeriod.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_RequestTypeClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static RequestType Value(BitArrayInputStream input)
            {
                return RequestType.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_ResetTypeClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static ResetType Value(BitArrayInputStream input)
            {
                return ResetType.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_Routing_IDClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static long Value(BitArrayInputStream input)
            {
                return Routing_ID.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_RRC_Establishment_CauseClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static RRC_Establishment_Cause Value(BitArrayInputStream input)
            {
                int nBits = (input.readBit() == 0) ? 3 : 3;
                return (RRC_Establishment_Cause)input.readBits(nBits);
            }
        }

        public class id_S_TMSIClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static S_TMSI Value(BitArrayInputStream input)
            {
                return S_TMSI.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_SecurityContextClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static SecurityContext Value(BitArrayInputStream input)
            {
                return SecurityContext.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_SecurityKeyClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return SecurityKey.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_SerialNumberClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return SerialNumber.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_ServedGUMMEIsClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<ServedGUMMEIsItem> Value(BitArrayInputStream input)
            {
                List<ServedGUMMEIsItem> list = new List<ServedGUMMEIsItem>();
                int nBits = 3;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    ServedGUMMEIsItem item = ServedGUMMEIsItem.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_SONConfigurationTransferECTClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static SONConfigurationTransfer Value(BitArrayInputStream input)
            {
                return SONConfigurationTransfer.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_SONConfigurationTransferMCTClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static SONConfigurationTransfer Value(BitArrayInputStream input)
            {
                return SONConfigurationTransfer.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_Source_ToTarget_TransparentContainer_SecondaryClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return Source_ToTarget_TransparentContainer.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_Source_ToTarget_TransparentContainerClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return Source_ToTarget_TransparentContainer.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_SourceMME_UE_S1AP_IDClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static long Value(BitArrayInputStream input)
            {
                return MME_UE_S1AP_ID.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_SRVCCHOIndicationClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static SRVCCHOIndication Value(BitArrayInputStream input)
            {
                int nBits = 1;
                return (SRVCCHOIndication)input.readBits(nBits);
            }
        }

        public class id_SRVCCOperationPossibleClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static SRVCCOperationPossible Value(BitArrayInputStream input)
            {
                int nBits = 1;
                return (SRVCCOperationPossible)input.readBits(nBits);
            }
        }

        public class id_SubscriberProfileIDforRFPClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static long Value(BitArrayInputStream input)
            {
                return SubscriberProfileIDforRFP.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_SupportedTAsClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<SupportedTAs_Item> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<SupportedTAs_Item> list = new List<SupportedTAs_Item>();
                int nBits = 8;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    SupportedTAs_Item item = SupportedTAs_Item.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_TAIClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static TAI Value(BitArrayInputStream input)
            {
                return TAI.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_TAIItemClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static TAIItem Value(BitArrayInputStream input)
            {
                return TAIItem.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_TAIListClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<ProtocolIE_Field> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ProtocolIE_Field> list = new List<ProtocolIE_Field>();
                int nBits = 8;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_Target_ToSource_TransparentContainer_SecondaryClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return Target_ToSource_TransparentContainer.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_Target_ToSource_TransparentContainerClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return Target_ToSource_TransparentContainer.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_TargetIDClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static TargetID Value(BitArrayInputStream input)
            {
                return TargetID.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_TimeToWaitClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static TimeToWait Value(BitArrayInputStream input)
            {
                int nBits = (input.readBit() == 0) ? 3 : 3;
                return (TimeToWait)input.readBits(nBits);
            }
        }

        public class id_TraceActivationClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static TraceActivation Value(BitArrayInputStream input)
            {
                return TraceActivation.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_TraceCollectionEntityIPAddressClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return TransportLayerAddress.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_UE_associatedLogicalS1_ConnectionItemClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static UE_associatedLogicalS1_ConnectionItem Value(BitArrayInputStream input)
            {
                return UE_associatedLogicalS1_ConnectionItem.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_UE_associatedLogicalS1_ConnectionListResAckClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static List<ProtocolIE_Field> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ProtocolIE_Field> list = new List<ProtocolIE_Field>();
                const int nBits = 8;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_UE_S1AP_IDsClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static UE_S1AP_IDs Value(BitArrayInputStream input)
            {
                return UE_S1AP_IDs.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_uEaggregateMaximumBitrateClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static UEAggregateMaximumBitrate Value(BitArrayInputStream input)
            {
                return UEAggregateMaximumBitrate.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_UEIdentityIndexValueClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return UEIdentityIndexValue.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_UEPagingIDClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static UEPagingID Value(BitArrayInputStream input)
            {
                return UEPagingID.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_UERadioCapabilityClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return UERadioCapability.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_UESecurityCapabilitiesClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static UESecurityCapabilities Value(BitArrayInputStream input)
            {
                return UESecurityCapabilities.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_WarningAreaListClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static WarningAreaList Value(BitArrayInputStream input)
            {
                return WarningAreaList.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_WarningMessageContentsClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return WarningMessageContents.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_WarningSecurityInfoClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return WarningSecurityInfo.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_WarningTypeClass
        {
            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Value"))
                {
                    return Value(input);
                }
                return null;
            }

            public static string Value(BitArrayInputStream input)
            {
                return WarningType.PerDecoder.Instance.Decode(input);
            }
        }
    }
}