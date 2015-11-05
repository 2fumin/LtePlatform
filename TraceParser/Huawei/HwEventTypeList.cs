using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceParser.Huawei
{
    public class HwEventTypeList
    {
        public HwEventTypeList()
        {
            ht_HwEventType = new Hashtable
            {
                {0x2020200, "S1_E_RAB_MODIFY_REQUEST"},
                {0x2020201, "S1_E_RAB_MODIFY_RESPONSE"},
                {0x2020202, "S1_E_RAB_RELEASE_COMMAND"},
                {0x2020203, "S1_E_RAB_RELEASE_INDICATION"},
                {0x2020204, "S1_E_RAB_RELEASE_RESPONSE"},
                {0x2020205, "S1_E_RAB_SETUP_REQUEST"},
                {0x2020206, "S1_E_RAB_SETUP_RESPONSE"},
                {0x2020207, "S1_DOWNLINK_NAS_TRANSPORT"},
                {0x2020208, "S1_DOWNLINK_S1_CDMA2000_TUNNELING"},
                {0x2020209, "S1_HANDOVER_CANCEL"},
                {0x202020a, "S1_HANDOVER_CANCEL_ACKNOWLEDGE"},
                {0x202020b, "S1_HANDOVER_COMMAND"},
                {0x202020c, "S1_HANDOVER_FAILURE"},
                {0x202020d, "S1_HANDOVER_NOTIFY"},
                {0x202020e, "S1_HANDOVER_PREPARATION_FAILURE"},
                {0x202020f, "S1_HANDOVER_REQUEST"},
                {0x2020210, "S1_HANDOVER_REQUEST_ACKNOWLEDGE"},
                {0x2020211, "S1_HANDOVER_REQUIRED"},
                {0x2020212, "S1_INITIAL_CONTEXT_SETUP_FAILURE"},
                {0x2020213, "S1_INITIAL_CONTEXT_SETUP_REQUEST"},
                {0x2020214, "S1_INITIAL_CONTEXT_SETUP_RESPONSE"},
                {0x2020215, "S1_INITIAL_UE_MESSAGE"},
                {0x2020216, "S1_NAS_NON_DELIVERY_INDICATION"},
                {0x2020217, "S1_PAGING"},
                {0x2020218, "S1_PATH_SWITCH_REQUEST"},
                {0x2020219, "S1_PATH_SWITCH_REQUEST_ACKNOWLEDGE"},
                {0x202021a, "S1_PATH_SWITCH_REQUEST_FAILURE"},
                {0x202021b, "S1_UE_CAPABILITY_INFO_INDICATION"},
                {0x202021c, "S1_UE_CONTEXT_MODIFICATION_FAILURE"},
                {0x202021d, "S1_UE_CONTEXT_MODIFICATION_REQUEST"},
                {0x202021e, "S1_UE_CONTEXT_MODIFICATION_RESPONSE"},
                {0x202021f, "S1_UE_CONTEXT_RELEASE_COMMAND"},
                {0x2020220, "S1_UE_CONTEXT_RELEASE_COMPLETE"},
                {0x2020221, "S1_UE_CONTEXT_RELEASE_REQUEST"},
                {0x2020222, "S1_UPLINK_NAS_TRANSPORT"},
                {0x2020223, "S1_UPLINK_S1_CDMA2000_TUNNELING"},
                {0x2020224, "S1_CELL_TRAFFIC_TRACE"},
                {0x2020225, "S1_eNB_STATUS_TRANSFER"},
                {0x2020226, "S1_MME_STATUS_TRANSFER"},
                {0x2020227, "S1_TRACE_START"},
                {0x2020228, "S1_TRACE_FAILURE_INDICATION"},
                {0x2020229, "S1_DEACTIVATE_TRACE"},
                {0x202022a, "S1_LOCATION_REPORTING_CONTROL"},
                {0x202022b, "S1_LOCATION_REPORT_FAILURE_INDICATION"},
                {0x202022c, "S1_LOCATION_REPORT "},
                {0x202022d, "S1_DOWNLINK_UE_ASSOCIATED_LPPA_TRANSPORT"},
                {0x202022e, "S1_UPLINK_UE_ASSOCIATED_LPPA_TRANSPORT"},
                {0x2010300, "UU_CSFB_PARAMETERS_REQUEST_CDMA2000"},
                {0x2010301, "UU_CSFB_PARAMETERS_RESPONSE_CDMA2000"},
                {0x2010302, "UU_HANDOVER_FROM_EUTRA_PREPARATION_REQUEST_CDMA2000"},
                {0x2010303, "UU_MEASUREMENT_REPORT"},
                {0x2010304, "UU_MOBILITY_FROM_EUTRA_COMMAND"},
                {0x2010305, "UU_PAGING"},
                {0x2010306, "UU_RRC_CONNECTION_RECONFIGURATION"},
                {0x2010307, "UU_RRC_CONNECTION_RECONFIGURATION_COMPLETE"},
                {0x2010308, "UU_RRC_CONNECTION_REESTABLISHMENT"},
                {0x2010309, "UU_RRC_CONNECTION_REESTABLISHMENT_COMPLETE"},
                {0x201030a, "UU_RRC_CONNECTION_REESTABLISHMENT_REJECT"},
                {0x201030b, "UU_RRC_CONNECTION_REESTABLISHMENT_REQUEST"},
                {0x201030c, "UU_RRC_CONNECTION_REJECT"},
                {0x201030d, "UU_RRC_CONNECTION_RELEASE"},
                {0x201030e, "UU_RRC_CONNECTION_REQUEST"},
                {0x201030f, "UU_RRC_CONNECTION_SETUP"},
                {0x2010310, "UU_RRC_CONNECTION_SETUP_COMPLETE"},
                {0x2010311, "UU_SECURITY_MODE_COMMAND"},
                {0x2010312, "UU_SECURITY_MODE_COMPLETE"},
                {0x2010313, "UU_SECURITY_MODE_FAILURE"},
                {0x2010314, "UU_UE_CAPABILITY_ENQUIRY"},
                {0x2010315, "UU_UE_CAPABILITY_INFORMATION"},
                {0x2010316, "UU_UL_HANDOVER_PREPARATION_TRANSFER_CDMA2000"},
                {0x2010317, "UU_UL_INFORMATION_TRANSFER"},
                {0x2010318, "RRC_DL_INFORMATION_TRANSFER"},
                {0x2010319, "RRC_LOGGED_MEASUREMENT_CONFIGURATION"},
                {0x201031a, "RRC_UE_INFORMATION_REQUEST"},
                {0x201031b, "RRC_UE_INFORMATION_RESPONSE"},
                {0x2030400, "X2_HANDOVER_CANCEL"},
                {0x2030401, "X2_HANDOVER_PREPARATION_FAILURE"},
                {0x2030402, "X2_HANDOVER_REQUEST"},
                {0x2030403, "X2_HANDOVER_REQUEST_ACKNOWLEDGE"},
                {0x2030404, "X2_UE_CONTEXT_RELEASE"},
                {0x2030405, "X2_SN_STATUS_TRANSFER"}
            };
        }

        public Hashtable ht_HwEventType { get; set; }
    }
}
