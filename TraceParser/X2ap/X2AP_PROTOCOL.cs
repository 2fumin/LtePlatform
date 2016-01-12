using System;
using System.Collections.Generic;
using Lte.Domain.Common;
using TraceParser.Common;

namespace TraceParser.X2ap
{
    [Serializable]
    public class X2AP_PROTOCOL_EXTENSION
    {
        public static object Switcher(long unique, string membername, BitArrayInputStream input)
        {
            long num = unique;
            if (num <= 0x2aL)
            {
                if (num < 0x29L)
                {
                    goto Label_0081;
                }
                switch (((int)(num - 0x29L)))
                {
                    case 0:
                        return id_Number_of_AntennaportsClass.GetMemberObj(membername, input);

                    case 1:
                        return id_CompositeAvailableCapacityGroupClass.GetMemberObj(membername, input);
                }
            }
            if ((num <= 0x3bL) && (num >= 0x37L))
            {
                switch (((int)(num - 0x37L)))
                {
                    case 0:
                        return id_PRACH_ConfigurationClass.GetMemberObj(membername, input);

                    case 1:
                        return id_MBSFN_Subframe_InfoClass.GetMemberObj(membername, input);

                    case 4:
                        return id_DeactivationIndicationClass.GetMemberObj(membername, input);
                }
            }
        Label_0081:
            return null;
        }

        public Criticality criticality { get; set; }

        public object Extension { get; set; }

        public long id { get; set; }

        public Presence presence { get; set; }

        public class id_CompositeAvailableCapacityGroupClass
        {
            public static CompositeAvailableCapacityGroup Extension(BitArrayInputStream input)
            {
                return CompositeAvailableCapacityGroup.PerDecoder.Instance.Decode(input);
            }

            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Extension"))
                {
                    return Extension(input);
                }
                return null;
            }
        }

        public class id_DeactivationIndicationClass
        {
            public static DeactivationIndication Extension(BitArrayInputStream input)
            {
                int nBits = 1;
                return (DeactivationIndication)input.readBits(nBits);
            }

            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Extension"))
                {
                    return Extension(input);
                }
                return null;
            }
        }

        public class id_MBSFN_Subframe_InfoClass
        {
            public static List<MBSFN_Subframe_Info> Extension(BitArrayInputStream input)
            {
                List<MBSFN_Subframe_Info> list = new List<MBSFN_Subframe_Info>();
                const int nBits = 3;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    MBSFN_Subframe_Info item = MBSFN_Subframe_Info.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }

            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Extension"))
                {
                    return Extension(input);
                }
                return null;
            }
        }

        public class id_Number_of_AntennaportsClass
        {
            public static Number_of_Antennaports Extension(BitArrayInputStream input)
            {
                int nBits = (input.readBit() == 0) ? 2 : 2;
                return (Number_of_Antennaports)input.readBits(nBits);
            }

            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Extension"))
                {
                    return Extension(input);
                }
                return null;
            }
        }

        public class id_PRACH_ConfigurationClass
        {
            public static PRACH_Configuration Extension(BitArrayInputStream input)
            {
                return PRACH_Configuration.PerDecoder.Instance.Decode(input);
            }

            public static object GetMemberObj(string member, BitArrayInputStream input)
            {
                string str = member;
                if ((str != null) && (str == "Extension"))
                {
                    return Extension(input);
                }
                return null;
            }
        }
    }

    [Serializable]
    public class X2AP_PROTOCOL_IES
    {
        public static object Switcher(long unique, string membername, BitArrayInputStream input)
        {
            long num = unique;
            if ((num <= 60L) && (num >= 0L))
            {
                switch (((int)num))
                {
                    case 0:
                        return id_E_RABs_Admitted_ItemClass.GetMemberObj(membername, input);

                    case 1:
                        return id_E_RABs_Admitted_ListClass.GetMemberObj(membername, input);

                    case 2:
                        return id_E_RAB_ItemClass.GetMemberObj(membername, input);

                    case 3:
                        return id_E_RABs_NotAdmitted_ListClass.GetMemberObj(membername, input);

                    case 4:
                        return id_E_RABs_ToBeSetup_ItemClass.GetMemberObj(membername, input);

                    case 5:
                        return id_CauseClass.GetMemberObj(membername, input);

                    case 6:
                        return id_CellInformationClass.GetMemberObj(membername, input);

                    case 7:
                        return id_CellInformation_ItemClass.GetMemberObj(membername, input);

                    case 9:
                        return id_New_eNB_UE_X2AP_IDClass.GetMemberObj(membername, input);

                    case 10:
                        return id_Old_eNB_UE_X2AP_IDClass.GetMemberObj(membername, input);

                    case 11:
                        return id_TargetCell_IDClass.GetMemberObj(membername, input);

                    case 12:
                        return id_TargeteNBtoSource_eNBTransparentContainerClass.GetMemberObj(membername, input);

                    case 13:
                        return id_TraceActivationClass.GetMemberObj(membername, input);

                    case 14:
                        return id_UE_ContextInformationClass.GetMemberObj(membername, input);

                    case 15:
                        return id_UE_HistoryInformationClass.GetMemberObj(membername, input);

                    case 0x11:
                        return id_CriticalityDiagnosticsClass.GetMemberObj(membername, input);

                    case 0x12:
                        return id_E_RABs_SubjectToStatusTransfer_ListClass.GetMemberObj(membername, input);

                    case 0x13:
                        return id_E_RABs_SubjectToStatusTransfer_ItemClass.GetMemberObj(membername, input);

                    case 20:
                        return id_ServedCellsClass.GetMemberObj(membername, input);

                    case 0x15:
                        return id_GlobalENB_IDClass.GetMemberObj(membername, input);

                    case 0x16:
                        return id_TimeToWaitClass.GetMemberObj(membername, input);

                    case 0x17:
                        return id_GUMMEI_IDClass.GetMemberObj(membername, input);

                    case 0x18:
                        return id_GUGroupIDListClass.GetMemberObj(membername, input);

                    case 0x19:
                        return id_ServedCellsToAddClass.GetMemberObj(membername, input);

                    case 0x1a:
                        return id_ServedCellsToModifyClass.GetMemberObj(membername, input);

                    case 0x1b:
                        return id_ServedCellsToDeleteClass.GetMemberObj(membername, input);

                    case 0x1c:
                        return id_Registration_RequestClass.GetMemberObj(membername, input);

                    case 0x1d:
                        return id_CellToReportClass.GetMemberObj(membername, input);

                    case 30:
                        return id_ReportingPeriodicityClass.GetMemberObj(membername, input);

                    case 0x1f:
                        return id_CellToReport_ItemClass.GetMemberObj(membername, input);

                    case 0x20:
                        return id_CellMeasurementResultClass.GetMemberObj(membername, input);

                    case 0x21:
                        return id_CellMeasurementResult_ItemClass.GetMemberObj(membername, input);

                    case 0x22:
                        return id_GUGroupIDToAddListClass.GetMemberObj(membername, input);

                    case 0x23:
                        return id_GUGroupIDToDeleteListClass.GetMemberObj(membername, input);

                    case 0x24:
                        return id_SRVCCOperationPossibleClass.GetMemberObj(membername, input);

                    case 0x26:
                        return id_ReportCharacteristicsClass.GetMemberObj(membername, input);

                    case 0x27:
                        return id_ENB1_Measurement_IDClass.GetMemberObj(membername, input);

                    case 40:
                        return id_ENB2_Measurement_IDClass.GetMemberObj(membername, input);

                    case 0x2b:
                        return id_ENB1_Cell_IDClass.GetMemberObj(membername, input);

                    case 0x2c:
                        return id_ENB2_Cell_IDClass.GetMemberObj(membername, input);

                    case 0x2d:
                        return id_ENB2_Proposed_Mobility_ParametersClass.GetMemberObj(membername, input);

                    case 0x2e:
                        return id_ENB1_Mobility_ParametersClass.GetMemberObj(membername, input);

                    case 0x2f:
                        return id_ENB2_Mobility_Parameters_Modification_RangeClass.GetMemberObj(membername, input);

                    case 0x30:
                        return id_FailureCellPCIClass.GetMemberObj(membername, input);

                    case 0x31:
                        return id_Re_establishmentCellECGIClass.GetMemberObj(membername, input);

                    case 50:
                        return id_FailureCellCRNTIClass.GetMemberObj(membername, input);

                    case 0x33:
                        return id_ShortMAC_IClass.GetMemberObj(membername, input);

                    case 0x34:
                        return id_SourceCellECGIClass.GetMemberObj(membername, input);

                    case 0x35:
                        return id_FailureCellECGIClass.GetMemberObj(membername, input);

                    case 0x36:
                        return id_HandoverReportTypeClass.GetMemberObj(membername, input);

                    case 0x39:
                        return id_ServedCellsToActivateClass.GetMemberObj(membername, input);

                    case 0x3a:
                        return id_ActivatedCellListClass.GetMemberObj(membername, input);

                    case 60:
                        return id_UE_RLF_Report_ContainerClass.GetMemberObj(membername, input);
                }
            }
            return null;
        }

        public Criticality criticality { get; set; }

        public long id { get; set; }

        public Presence presence { get; set; }

        public object Value { get; set; }

        public class id_ActivatedCellListClass
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

            public static List<ActivatedCellList_Item> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ActivatedCellList_Item> list = new List<ActivatedCellList_Item>();
                const int nBits = 8;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    ActivatedCellList_Item item = ActivatedCellList_Item.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
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

        public class id_CellInformation_ItemClass
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

            public static CellInformation_Item Value(BitArrayInputStream input)
            {
                return CellInformation_Item.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_CellInformationClass
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

        public class id_CellMeasurementResult_ItemClass
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

            public static CellMeasurementResult_Item Value(BitArrayInputStream input)
            {
                return CellMeasurementResult_Item.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_CellMeasurementResultClass
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

        public class id_CellToReport_ItemClass
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

            public static CellToReport_Item Value(BitArrayInputStream input)
            {
                return CellToReport_Item.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_CellToReportClass
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

        public class id_E_RAB_ItemClass
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

            public static E_RAB_Item Value(BitArrayInputStream input)
            {
                return E_RAB_Item.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_E_RABs_Admitted_ItemClass
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

            public static E_RABs_Admitted_Item Value(BitArrayInputStream input)
            {
                return E_RABs_Admitted_Item.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_E_RABs_Admitted_ListClass
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

        public class id_E_RABs_NotAdmitted_ListClass
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

        public class id_E_RABs_SubjectToStatusTransfer_ItemClass
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

            public static E_RABs_SubjectToStatusTransfer_Item Value(BitArrayInputStream input)
            {
                return E_RABs_SubjectToStatusTransfer_Item.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_E_RABs_SubjectToStatusTransfer_ListClass
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

        public class id_E_RABs_ToBeSetup_ItemClass
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

            public static E_RABs_ToBeSetup_Item Value(BitArrayInputStream input)
            {
                return E_RABs_ToBeSetup_Item.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_ENB1_Cell_IDClass
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

            public static ECGI Value(BitArrayInputStream input)
            {
                return ECGI.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_ENB1_Measurement_IDClass
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
                return Measurement_ID.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_ENB1_Mobility_ParametersClass
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

            public static MobilityParametersInformation Value(BitArrayInputStream input)
            {
                return MobilityParametersInformation.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_ENB2_Cell_IDClass
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

            public static ECGI Value(BitArrayInputStream input)
            {
                return ECGI.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_ENB2_Measurement_IDClass
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
                return Measurement_ID.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_ENB2_Mobility_Parameters_Modification_RangeClass
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

            public static MobilityParametersModificationRange Value(BitArrayInputStream input)
            {
                return MobilityParametersModificationRange.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_ENB2_Proposed_Mobility_ParametersClass
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

            public static MobilityParametersInformation Value(BitArrayInputStream input)
            {
                return MobilityParametersInformation.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_FailureCellCRNTIClass
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
                return CRNTI.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_FailureCellECGIClass
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

            public static ECGI Value(BitArrayInputStream input)
            {
                return ECGI.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_FailureCellPCIClass
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
                return PCI.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_GlobalENB_IDClass
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

        public class id_GUGroupIDListClass
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

            public static List<GU_Group_ID> Value(BitArrayInputStream input)
            {
                List<GU_Group_ID> list = new List<GU_Group_ID>();
                int nBits = 4;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    GU_Group_ID item = GU_Group_ID.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_GUGroupIDToAddListClass
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

            public static List<GU_Group_ID> Value(BitArrayInputStream input)
            {
                List<GU_Group_ID> list = new List<GU_Group_ID>();
                int nBits = 4;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    GU_Group_ID item = GU_Group_ID.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_GUGroupIDToDeleteListClass
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

            public static List<GU_Group_ID> Value(BitArrayInputStream input)
            {
                List<GU_Group_ID> list = new List<GU_Group_ID>();
                int nBits = 4;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    GU_Group_ID item = GU_Group_ID.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
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

        public class id_HandoverReportTypeClass
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

            public static HandoverReportType Value(BitArrayInputStream input)
            {
                int nBits = 1;
                return (HandoverReportType)input.readBits(nBits);
            }
        }

        public class id_New_eNB_UE_X2AP_IDClass
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
                return UE_X2AP_ID.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_Old_eNB_UE_X2AP_IDClass
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
                return UE_X2AP_ID.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_Re_establishmentCellECGIClass
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

            public static ECGI Value(BitArrayInputStream input)
            {
                return ECGI.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_Registration_RequestClass
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

            public static Registration_Request Value(BitArrayInputStream input)
            {
                int nBits = 1;
                return (Registration_Request)input.readBits(nBits);
            }
        }

        public class id_ReportCharacteristicsClass
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
                return ReportCharacteristics.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_ReportingPeriodicityClass
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

            public static ReportingPeriodicity Value(BitArrayInputStream input)
            {
                int nBits = (input.readBit() == 0) ? 2 : 2;
                return (ReportingPeriodicity)input.readBits(nBits);
            }
        }

        public class id_ServedCellsClass
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

            public static List<ServedCells_Element> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ServedCells_Element> list = new List<ServedCells_Element>();
                int nBits = 8;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    ServedCells_Element item = ServedCells_Element.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_ServedCellsToActivateClass
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

            public static List<ServedCellsToActivate_Item> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ServedCellsToActivate_Item> list = new List<ServedCellsToActivate_Item>();
                int nBits = 8;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    ServedCellsToActivate_Item item = ServedCellsToActivate_Item.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_ServedCellsToAddClass
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

            public static List<ServedCells_Element> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ServedCells_Element> list = new List<ServedCells_Element>();
                int nBits = 8;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    ServedCells_Element item = ServedCells_Element.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_ServedCellsToDeleteClass
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

            public static List<ECGI> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ECGI> list = new List<ECGI>();
                int nBits = 8;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    ECGI item = ECGI.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_ServedCellsToModifyClass
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

            public static List<ServedCellsToModify_Item> Value(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                List<ServedCellsToModify_Item> list = new List<ServedCellsToModify_Item>();
                int nBits = 8;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    ServedCellsToModify_Item item = ServedCellsToModify_Item.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_ShortMAC_IClass
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
                return ShortMAC_I.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_SourceCellECGIClass
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

            public static ECGI Value(BitArrayInputStream input)
            {
                return ECGI.PerDecoder.Instance.Decode(input);
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

        public class id_TargetCell_IDClass
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

            public static ECGI Value(BitArrayInputStream input)
            {
                return ECGI.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_TargeteNBtoSource_eNBTransparentContainerClass
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
                return TargeteNBtoSource_eNBTransparentContainer.PerDecoder.Instance.Decode(input);
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

        public class id_UE_ContextInformationClass
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

            public static UE_ContextInformation Value(BitArrayInputStream input)
            {
                return UE_ContextInformation.PerDecoder.Instance.Decode(input);
            }
        }

        public class id_UE_HistoryInformationClass
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

            public static List<LastVisitedCell_Item> Value(BitArrayInputStream input)
            {
                List<LastVisitedCell_Item> list = new List<LastVisitedCell_Item>();
                const int nBits = 4;
                int num2 = input.readBits(nBits) + 1;
                for (int i = 0; i < num2; i++)
                {
                    LastVisitedCell_Item item = LastVisitedCell_Item.PerDecoder.Instance.Decode(input);
                    list.Add(item);
                }
                return list;
            }
        }

        public class id_UE_RLF_Report_ContainerClass
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
                return UE_RLF_Report_Container.PerDecoder.Instance.Decode(input);
            }
        }
    }

    [Serializable]
    public class X2AP_PROTOCOL_IES_PAIR
    {
        public static object Switcher(object unique, string membername, BitArrayInputStream input)
        {
            return null;
        }

        public Criticality firstCriticality { get; set; }

        public object FirstValue { get; set; }

        public long id { get; set; }

        public Presence presence { get; set; }

        public Criticality secondCriticality { get; set; }

        public object SecondValue { get; set; }
    }

}
