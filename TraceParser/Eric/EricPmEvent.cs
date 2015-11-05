using System.Collections;

namespace TraceParser.Eric
{
    public class EricPmEvent
    {
        public static Hashtable GetEricPmEvents()
        {
            Hashtable hashtable = new Hashtable();
            EricPmEvent event2 = new EricPmEvent
            {
                PmEventName = "INTERNAL_EVENT_MEAS_CONFIG_A1",
                EventId = 0x1436,
                EventType = "UE",
                MsgDepend = "UL_DCCH_Message"
            };
            hashtable.Add(0x1436, event2);
            EricPmEvent event3 = new EricPmEvent
            {
                PmEventName = "INTERNAL_EVENT_MEAS_CONFIG_A2",
                EventId = 0x1437,
                EventType = "UE",
                MsgDepend = "UL_DCCH_Message"
            };
            hashtable.Add(0x1437, event3);
            EricPmEvent event4 = new EricPmEvent
            {
                PmEventName = "INTERNAL_EVENT_MEAS_CONFIG_A3",
                EventId = 0x1438,
                EventType = "UE",
                MsgDepend = "UL_DCCH_Message"
            };
            hashtable.Add(0x1438, event4);
            EricPmEvent event5 = new EricPmEvent
            {
                PmEventName = "INTERNAL_EVENT_MEAS_CONFIG_A4",
                EventId = 0x1439,
                EventType = "UE",
                MsgDepend = "UL_DCCH_Message"
            };
            hashtable.Add(0x1439, event5);
            EricPmEvent event6 = new EricPmEvent
            {
                PmEventName = "INTERNAL_EVENT_MEAS_CONFIG_A5",
                EventId = 0x143a,
                EventType = "UE",
                MsgDepend = "UL_DCCH_Message"
            };
            hashtable.Add(0x143a, event6);
            EricPmEvent event7 = new EricPmEvent
            {
                PmEventName = "INTERNAL_EVENT_MEAS_CONFIG_B1_CDMA2000",
                EventId = 0x1473,
                EventType = "UE",
                MsgDepend = "UL_DCCH_Message"
            };
            hashtable.Add(0x1473, event7);
            EricPmEvent event8 = new EricPmEvent
            {
                PmEventName = "INTERNAL_EVENT_MEAS_CONFIG_B1_UTRA",
                EventId = 0x1472,
                EventType = "UE",
                MsgDepend = "UL_DCCH_Message"
            };
            hashtable.Add(0x1472, event8);
            EricPmEvent event9 = new EricPmEvent
            {
                PmEventName = "INTERNAL_EVENT_MEAS_CONFIG_B2_CDMA2000",
                EventId = 0x143e,
                EventType = "UE",
                MsgDepend = "UL_DCCH_Message"
            };
            hashtable.Add(0x143e, event9);
            EricPmEvent event10 = new EricPmEvent
            {
                PmEventName = "INTERNAL_EVENT_MEAS_CONFIG_B2_GERAN",
                EventId = 0x143c,
                EventType = "UE",
                MsgDepend = "UL_DCCH_Message"
            };
            hashtable.Add(0x143c, event10);
            EricPmEvent event11 = new EricPmEvent
            {
                PmEventName = "INTERNAL_EVENT_MEAS_CONFIG_B2_UTRA",
                EventId = 0x143d,
                EventType = "UE",
                MsgDepend = "UL_DCCH_Message"
            };
            hashtable.Add(0x143d, event11);
            EricPmEvent event12 = new EricPmEvent
            {
                PmEventName = "INTERNAL_EVENT_MEAS_CONFIG_PERIODICAL_EUTRA",
                EventId = 0x143b,
                EventType = "UE",
                MsgDepend = "UL_DCCH_Message"
            };
            hashtable.Add(0x143b, event12);
            EricPmEvent event13 = new EricPmEvent
            {
                PmEventName = "INTERNAL_EVENT_MEAS_CONFIG_PERIODICAL_GERAN",
                EventId = 0x143f,
                EventType = "UE",
                MsgDepend = "UL_DCCH_Message"
            };
            hashtable.Add(0x143f, event13);
            EricPmEvent event14 = new EricPmEvent
            {
                PmEventName = "INTERNAL_EVENT_MEAS_CONFIG_PERIODICAL_UTRA",
                EventId = 0x1440,
                EventType = "UE",
                MsgDepend = "UL_DCCH_Message"
            };
            hashtable.Add(0x1440, event14);
            EricPmEvent event15 = new EricPmEvent
            {
                PmEventName = "RRC_CONNECTION_RE_ESTABLISHMENT_COMPLETE",
                EventId = 0x19,
                EventType = "EXTERNAL",
                MsgDepend = "UL_DCCH_Message"
            };
            hashtable.Add(0x19, event15);
            EricPmEvent event16 = new EricPmEvent
            {
                PmEventName = "RRC_CONNECTION_RE_ESTABLISHMENT",
                EventId = 0x18,
                EventType = "EXTERNAL",
                MsgDepend = "DL_CCCH_Message"
            };
            hashtable.Add(0x18, event16);
            EricPmEvent event17 = new EricPmEvent
            {
                PmEventName = "RRC_CSFB_PARAMETERS_REQUEST_CDMA2000",
                EventId = 0x1d,
                EventType = "EXTERNAL",
                MsgDepend = "UL_DCCH_Message"
            };
            hashtable.Add(0x1d, event17);
            EricPmEvent event18 = new EricPmEvent
            {
                PmEventName = "RRC_CSFB_PARAMETERS_RESPONSE_CDMA2000",
                EventId = 30,
                EventType = "EXTERNAL",
                MsgDepend = "DL_DCCH_Message"
            };
            hashtable.Add(30, event18);
            EricPmEvent event19 = new EricPmEvent
            {
                PmEventName = "RRC_DL_INFORMATION_TRANSFER",
                EventId = 6,
                EventType = "EXTERNAL",
                MsgDepend = "DL_DCCH_Message"
            };
            hashtable.Add(6, event19);
            EricPmEvent event20 = new EricPmEvent
            {
                PmEventName = "RRC_HANDOVER_FROM_EUTRA_PREPARATION_REQUEST",
                EventId = 0x1f,
                EventType = "EXTERNAL",
                MsgDepend = "DL_DCCH_Message"
            };
            hashtable.Add(0x1f, event20);
            EricPmEvent event21 = new EricPmEvent
            {
                PmEventName = "RRC_MASTER_INFORMATION_BLOCK",
                EventId = 0x15,
                EventType = "EXTERNAL",
                MsgDepend = "BCCH_BCH_Message"
            };
            hashtable.Add(0x15, event21);
            EricPmEvent event22 = new EricPmEvent
            {
                PmEventName = "RRC_MBSFNAREA_CONFIGURATION",
                EventId = 0x1c,
                EventType = "EXTERNAL",
                MsgDepend = "MCCH_Message"
            };
            hashtable.Add(0x1c, event22);
            EricPmEvent event23 = new EricPmEvent
            {
                PmEventName = "RRC_MEASUREMENT_REPORT",
                EventId = 11,
                EventType = "EXTERNAL",
                MsgDepend = "UL_DCCH_Message"
            };
            hashtable.Add(11, event23);
            EricPmEvent event24 = new EricPmEvent
            {
                PmEventName = "RRC_RRC_CONNECTION_RE_ESTABLISHMENT_REJECT",
                EventId = 4,
                EventType = "EXTERNAL",
                MsgDepend = "DL_CCCH_Message"
            };
            hashtable.Add(4, event24);
            EricPmEvent event25 = new EricPmEvent
            {
                PmEventName = "RRC_RRC_CONNECTION_RE_ESTABLISHMENT_REQUEST",
                EventId = 3,
                EventType = "EXTERNAL",
                MsgDepend = "UL_CCCH_Message"
            };
            hashtable.Add(3, event25);
            EricPmEvent event26 = new EricPmEvent
            {
                PmEventName = "RRC_RRC_CONNECTION_RECONFIGURATION_COMPLETE",
                EventId = 13,
                EventType = "EXTERNAL",
                MsgDepend = "UL_DCCH_Message"
            };
            hashtable.Add(13, event26);
            EricPmEvent event27 = new EricPmEvent
            {
                PmEventName = "RRC_RRC_CONNECTION_RECONFIGURATION",
                EventId = 8,
                EventType = "EXTERNAL",
                MsgDepend = "DL_DCCH_Message"
            };
            hashtable.Add(8, event27);
            EricPmEvent event28 = new EricPmEvent
            {
                PmEventName = "RRC_RRC_CONNECTION_REJECT",
                EventId = 1,
                EventType = "EXTERNAL",
                MsgDepend = "DL_CCCH_Message"
            };
            hashtable.Add(1, event28);
            EricPmEvent event29 = new EricPmEvent
            {
                PmEventName = "RRC_RRC_CONNECTION_RELEASE",
                EventId = 5,
                EventType = "EXTERNAL",
                MsgDepend = "DL_DCCH_Message"
            };
            hashtable.Add(5, event29);
            EricPmEvent event30 = new EricPmEvent
            {
                PmEventName = "RRC_RRC_CONNECTION_REQUEST",
                EventId = 2,
                EventType = "EXTERNAL",
                MsgDepend = "UL_CCCH_Message"
            };
            hashtable.Add(2, event30);
            EricPmEvent event31 = new EricPmEvent
            {
                PmEventName = "RRC_RRC_CONNECTION_SETUP_COMPLETE",
                EventId = 12,
                EventType = "EXTERNAL",
                MsgDepend = "UL_DCCH_Message"
            };
            hashtable.Add(12, event31);
            EricPmEvent event32 = new EricPmEvent
            {
                PmEventName = "RRC_RRC_CONNECTION_SETUP",
                EventId = 0,
                EventType = "EXTERNAL",
                MsgDepend = "DL_CCCH_Message"
            };
            hashtable.Add(0, event32);
            EricPmEvent event33 = new EricPmEvent
            {
                PmEventName = "RRC_SECURITY_MODE_COMMAND",
                EventId = 9,
                EventType = "EXTERNAL",
                MsgDepend = "DL_DCCH_Message"
            };
            hashtable.Add(9, event33);
            EricPmEvent event34 = new EricPmEvent
            {
                PmEventName = "RRC_SECURITY_MODE_COMPLETE",
                EventId = 0x11,
                EventType = "EXTERNAL",
                MsgDepend = "UL_DCCH_Message"
            };
            hashtable.Add(0x11, event34);
            EricPmEvent event35 = new EricPmEvent
            {
                PmEventName = "RRC_SECURITY_MODE_FAILURE",
                EventId = 0x12,
                EventType = "EXTERNAL",
                MsgDepend = "UL_DCCH_Message"
            };
            hashtable.Add(0x12, event35);
            EricPmEvent event36 = new EricPmEvent
            {
                PmEventName = "RRC_SYSTEM_INFORMATION_BLOCK_TYPE_1",
                EventId = 0x17,
                EventType = "EXTERNAL",
                MsgDepend = "BCCH_DL_SCH_Message"
            };
            hashtable.Add(0x17, event36);
            EricPmEvent event37 = new EricPmEvent
            {
                PmEventName = "RRC_SYSTEM_INFORMATION",
                EventId = 0x16,
                EventType = "EXTERNAL",
                MsgDepend = "BCCH_DL_SCH_Message"
            };
            hashtable.Add(0x16, event37);
            EricPmEvent event38 = new EricPmEvent
            {
                PmEventName = "RRC_UE_CAPABILITY_ENQUIRY",
                EventId = 10,
                EventType = "EXTERNAL",
                MsgDepend = "DL_DCCH_Message"
            };
            hashtable.Add(10, event38);
            EricPmEvent event39 = new EricPmEvent
            {
                PmEventName = "RRC_UE_CAPABILITY_INFORMATION",
                EventId = 0x13,
                EventType = "EXTERNAL",
                MsgDepend = "UL_DCCH_Message"
            };
            hashtable.Add(0x13, event39);
            EricPmEvent event40 = new EricPmEvent
            {
                PmEventName = "RRC_UL_INFORMATION_TRANSFER",
                EventId = 0x10,
                EventType = "EXTERNAL",
                MsgDepend = "UL_DCCH_Message"
            };
            hashtable.Add(0x10, event40);
            EricPmEvent event41 = new EricPmEvent
            {
                PmEventName = "INTERNAL_EVENT_CMAS_REQ",
                EventId = 0x1467,
                EventType = "Cell",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x1467, event41);
            EricPmEvent event42 = new EricPmEvent
            {
                PmEventName = "INTERNAL_EVENT_CMAS_RESP",
                EventId = 0x1468,
                EventType = "Cell",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x1468, event42);
            EricPmEvent event43 = new EricPmEvent
            {
                PmEventName = "INTERNAL_EVENT_ETWS_REQ",
                EventId = 0x1465,
                EventType = "Cell",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x1465, event43);
            EricPmEvent event44 = new EricPmEvent
            {
                PmEventName = "INTERNAL_EVENT_ETWS_RESP",
                EventId = 0x1466,
                EventType = "Cell",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x1466, event44);
            EricPmEvent event45 = new EricPmEvent
            {
                PmEventName = "INTERNAL_EVENT_S1_ERROR_INDICATION",
                EventId = 0x1459,
                EventType = "RBS",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x1459, event45);
            EricPmEvent event46 = new EricPmEvent
            {
                PmEventName = "INTERNAL_EVENT_S1_NAS_NON_DELIVERY_INDICATION",
                EventId = 0x1458,
                EventType = "UE",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x1458, event46);
            EricPmEvent event47 = new EricPmEvent
            {
                PmEventName = "INTERNAL_PROC_ERAB_MODIFY",
                EventId = 0x101a,
                EventType = "UE",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x101a, event47);
            EricPmEvent event48 = new EricPmEvent
            {
                PmEventName = "INTERNAL_PROC_ERAB_RELEASE",
                EventId = 0x1012,
                EventType = "UE",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x1012, event48);
            EricPmEvent event49 = new EricPmEvent
            {
                PmEventName = "INTERNAL_PROC_ERAB_SETUP",
                EventId = 0x1003,
                EventType = "UE",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x1003, event49);
            EricPmEvent event50 = new EricPmEvent
            {
                PmEventName = "S1_DOWNLINK_NAS_TRANSPORT",
                EventId = 0x401,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x401, event50);
            EricPmEvent event51 = new EricPmEvent
            {
                PmEventName = "S1_DOWNLINK_NON_UE_ASSOCIATED_LPPA_TRANSPORT",
                EventId = 0x43b,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x43b, event51);
            EricPmEvent event52 = new EricPmEvent
            {
                PmEventName = "S1_DOWNLINK_S1_CDMA2000_TUNNELING",
                EventId = 0x400,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x400, event52);
            EricPmEvent event53 = new EricPmEvent
            {
                PmEventName = "S1_DOWNLINK_UE_ASSOCIATED_LPPA_TRANSPORT",
                EventId = 0x439,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x439, event53);
            EricPmEvent event54 = new EricPmEvent
            {
                PmEventName = "S1_ENB_CONFIGURATION_TRANSFER",
                EventId = 0x813,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x813, event54);
            EricPmEvent event55 = new EricPmEvent
            {
                PmEventName = "S1_ENB_CONFIGURATION_UPDATE_ACKNOWLEDGE",
                EventId = 0x42d,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x42d, event55);
            EricPmEvent event56 = new EricPmEvent
            {
                PmEventName = "S1_ENB_CONFIGURATION_UPDATE_FAILURE",
                EventId = 0x42e,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x42e, event56);
            EricPmEvent event57 = new EricPmEvent
            {
                PmEventName = "S1_ENB_CONFIGURATION_UPDATE",
                EventId = 0x42c,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x42c, event57);
            EricPmEvent event58 = new EricPmEvent
            {
                PmEventName = "S1_ENB_STATUS_TRANSFER",
                EventId = 0x402,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x402, event58);
            EricPmEvent event59 = new EricPmEvent
            {
                PmEventName = "S1_ERAB_MODIFY_REQUEST",
                EventId = 0x419,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x419, event59);
            EricPmEvent event60 = new EricPmEvent
            {
                PmEventName = "S1_ERAB_MODIFY_RESPONSE",
                EventId = 0x41a,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x41a, event60);
            EricPmEvent event61 = new EricPmEvent
            {
                PmEventName = "S1_ERAB_RELEASE_COMMAND",
                EventId = 0x41b,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x41b, event61);
            EricPmEvent event62 = new EricPmEvent
            {
                PmEventName = "S1_ERAB_RELEASE_INDICATION",
                EventId = 0x438,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x438, event62);
            EricPmEvent event63 = new EricPmEvent
            {
                PmEventName = "S1_ERAB_RELEASE_REQUEST",
                EventId = 0x41d,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x41d, event63);
            EricPmEvent event64 = new EricPmEvent
            {
                PmEventName = "S1_ERAB_RELEASE_RESPONSE",
                EventId = 0x41c,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x41c, event64);
            EricPmEvent event65 = new EricPmEvent
            {
                PmEventName = "S1_ERAB_SETUP_REQUEST",
                EventId = 0x41e,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x41e, event65);
            EricPmEvent event66 = new EricPmEvent
            {
                PmEventName = "S1_ERAB_SETUP_RESPONSE",
                EventId = 0x41f,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x41f, event66);
            EricPmEvent event67 = new EricPmEvent
            {
                PmEventName = "S1_ERROR_INDICATION",
                EventId = 0x403,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x403, event67);
            EricPmEvent event68 = new EricPmEvent
            {
                PmEventName = "S1_HANDOVER_CANCEL_ACKNOWLEDGE",
                EventId = 0x405,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x405, event68);
            EricPmEvent event69 = new EricPmEvent
            {
                PmEventName = "S1_HANDOVER_CANCEL",
                EventId = 0x404,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x404, event69);
            EricPmEvent event70 = new EricPmEvent
            {
                PmEventName = "S1_HANDOVER_COMMAND",
                EventId = 0x406,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x406, event70);
            EricPmEvent event71 = new EricPmEvent
            {
                PmEventName = "S1_HANDOVER_FAILURE",
                EventId = 0x407,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x407, event71);
            EricPmEvent event72 = new EricPmEvent
            {
                PmEventName = "S1_HANDOVER_NOTIFY",
                EventId = 0x408,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x408, event72);
            EricPmEvent event73 = new EricPmEvent
            {
                PmEventName = "S1_HANDOVER_PREPARATION_FAILURE",
                EventId = 0x409,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x409, event73);
            EricPmEvent event74 = new EricPmEvent
            {
                PmEventName = "S1_HANDOVER_REQUEST_ACKNOWLEDGE",
                EventId = 0x40b,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x40b, event74);
            EricPmEvent event75 = new EricPmEvent
            {
                PmEventName = "S1_HANDOVER_REQUEST",
                EventId = 0x40a,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x40a, event75);
            EricPmEvent event76 = new EricPmEvent
            {
                PmEventName = "S1_HANDOVER_REQUIRED",
                EventId = 0x40c,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x40c, event76);
            EricPmEvent event77 = new EricPmEvent
            {
                PmEventName = "S1_INITIAL_CONTEXT_SETUP_FAILURE",
                EventId = 0x40d,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x40d, event77);
            EricPmEvent event78 = new EricPmEvent
            {
                PmEventName = "S1_INITIAL_CONTEXT_SETUP_REQUEST",
                EventId = 0x40e,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x40e, event78);
            EricPmEvent event79 = new EricPmEvent
            {
                PmEventName = "S1_INITIAL_CONTEXT_SETUP_RESPONSE",
                EventId = 0x40f,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x40f, event79);
            EricPmEvent event80 = new EricPmEvent
            {
                PmEventName = "S1_INITIAL_UE_MESSAGE",
                EventId = 0x410,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x410, event80);
            EricPmEvent event81 = new EricPmEvent
            {
                PmEventName = "S1_KILL_REQUEST",
                EventId = 0x436,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x436, event81);
            EricPmEvent event82 = new EricPmEvent
            {
                PmEventName = "S1_KILL_RESPONSE",
                EventId = 0x437,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x437, event82);
            EricPmEvent event83 = new EricPmEvent
            {
                PmEventName = "S1_LOCATION_REPORT_FAILURE_INDICATION",
                EventId = 0x811,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x811, event83);
            EricPmEvent event84 = new EricPmEvent
            {
                PmEventName = "S1_LOCATION_REPORT",
                EventId = 0x810,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x810, event84);
            EricPmEvent event85 = new EricPmEvent
            {
                PmEventName = "S1_LOCATION_REPORTING_CONTROL",
                EventId = 0x80f,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x80f, event85);
            EricPmEvent event86 = new EricPmEvent
            {
                PmEventName = "S1_MME_CONFIGURATION_TRANSFER",
                EventId = 0x812,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x812, event86);
            EricPmEvent event87 = new EricPmEvent
            {
                PmEventName = "S1_MME_CONFIGURATION_UPDATE_ACKNOWLEDGE",
                EventId = 0x430,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x430, event87);
            EricPmEvent event88 = new EricPmEvent
            {
                PmEventName = "S1_MME_CONFIGURATION_UPDATE_FAILURE",
                EventId = 0x431,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x431, event88);
            EricPmEvent event89 = new EricPmEvent
            {
                PmEventName = "S1_MME_CONFIGURATION_UPDATE",
                EventId = 0x42f,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x42f, event89);
            EricPmEvent event90 = new EricPmEvent
            {
                PmEventName = "S1_MME_STATUS_TRANSFER",
                EventId = 0x411,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x411, event90);
            EricPmEvent event91 = new EricPmEvent
            {
                PmEventName = "S1_NAS_NON_DELIVERY_INDICATION",
                EventId = 0x412,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x412, event91);
            EricPmEvent event92 = new EricPmEvent
            {
                PmEventName = "S1_PAGING",
                EventId = 0x413,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x413, event92);
            EricPmEvent event93 = new EricPmEvent
            {
                PmEventName = "S1_PATH_SWITCH_REQUEST_ACKNOWLEDGE",
                EventId = 0x415,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x415, event93);
            EricPmEvent event94 = new EricPmEvent
            {
                PmEventName = "S1_PATH_SWITCH_REQUEST_FAILURE",
                EventId = 0x416,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x416, event94);
            EricPmEvent event95 = new EricPmEvent
            {
                PmEventName = "S1_PATH_SWITCH_REQUEST",
                EventId = 0x414,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x414, event95);
            EricPmEvent event96 = new EricPmEvent
            {
                PmEventName = "S1_RESET_ACKNOWLEDGE",
                EventId = 0x418,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x418, event96);
            EricPmEvent event97 = new EricPmEvent
            {
                PmEventName = "S1_RESET",
                EventId = 0x417,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x417, event97);
            EricPmEvent event98 = new EricPmEvent
            {
                PmEventName = "S1_S1_SETUP_FAILURE",
                EventId = 0x420,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x420, event98);
            EricPmEvent event99 = new EricPmEvent
            {
                PmEventName = "S1_S1_SETUP_REQUEST",
                EventId = 0x421,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x421, event99);
            EricPmEvent event100 = new EricPmEvent
            {
                PmEventName = "S1_S1_SETUP_RESPONSE",
                EventId = 0x422,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x422, event100);
            EricPmEvent event101 = new EricPmEvent
            {
                PmEventName = "S1_UE_CAPABILITY_INFO_INDICATION",
                EventId = 0x423,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x423, event101);
            EricPmEvent event102 = new EricPmEvent
            {
                PmEventName = "S1_UE_CONTEXT_MODIFICATION_FAILURE",
                EventId = 0x424,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x424, event102);
            EricPmEvent event103 = new EricPmEvent
            {
                PmEventName = "S1_UE_CONTEXT_MODIFICATION_REQUEST",
                EventId = 0x425,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x425, event103);
            EricPmEvent event104 = new EricPmEvent
            {
                PmEventName = "S1_UE_CONTEXT_MODIFICATION_RESPONSE",
                EventId = 0x426,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x426, event104);
            EricPmEvent event105 = new EricPmEvent
            {
                PmEventName = "S1_UE_CONTEXT_RELEASE_COMMAND",
                EventId = 0x427,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x427, event105);
            EricPmEvent event106 = new EricPmEvent
            {
                PmEventName = "S1_UE_CONTEXT_RELEASE_COMPLETE",
                EventId = 0x428,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x428, event106);
            EricPmEvent event107 = new EricPmEvent
            {
                PmEventName = "S1_UE_CONTEXT_RELEASE_REQUEST",
                EventId = 0x429,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x429, event107);
            EricPmEvent event108 = new EricPmEvent
            {
                PmEventName = "S1_UPLINK_NAS_TRANSPORT",
                EventId = 0x42b,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x42b, event108);
            EricPmEvent event109 = new EricPmEvent
            {
                PmEventName = "S1_UPLINK_NON_UE_ASSOCIATED_LPPA_TRANSPORT",
                EventId = 0x43c,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x43c, event109);
            EricPmEvent event110 = new EricPmEvent
            {
                PmEventName = "S1_UPLINK_S1_CDMA2000_TUNNELING",
                EventId = 0x42a,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x42a, event110);
            EricPmEvent event111 = new EricPmEvent
            {
                PmEventName = "S1_UPLINK_UE_ASSOCIATED_LPPA_TRANSPORT",
                EventId = 0x43a,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x43a, event111);
            EricPmEvent event112 = new EricPmEvent
            {
                PmEventName = "S1_WRITE_REPLACE_WARNING_REQUEST",
                EventId = 0x434,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x434, event112);
            EricPmEvent event113 = new EricPmEvent
            {
                PmEventName = "S1_WRITE_REPLACE_WARNING_RESPONSE",
                EventId = 0x435,
                EventType = "EXTERNAL",
                MsgDepend = "S1AP_PDU"
            };
            hashtable.Add(0x435, event113);
            EricPmEvent event114 = new EricPmEvent
            {
                PmEventName = "INTERNAL_EVENT_X2_ERROR_INDICATION",
                EventId = 0x145a,
                EventType = "RBS",
                MsgDepend = "X2AP_PDU"
            };
            hashtable.Add(0x145a, event114);
            EricPmEvent event115 = new EricPmEvent
            {
                PmEventName = "X2_ENB_CONFIGURATION_UPDATE_ACKNOWLEDGE",
                EventId = 0x806,
                EventType = "EXTERNAL",
                MsgDepend = "X2AP_PDU"
            };
            hashtable.Add(0x806, event115);
            EricPmEvent event116 = new EricPmEvent
            {
                PmEventName = "X2_ENB_CONFIGURATION_UPDATE_FAILURE",
                EventId = 0x807,
                EventType = "EXTERNAL",
                MsgDepend = "X2AP_PDU"
            };
            hashtable.Add(0x807, event116);
            EricPmEvent event117 = new EricPmEvent
            {
                PmEventName = "X2_ENB_CONFIGURATION_UPDATE",
                EventId = 0x805,
                EventType = "EXTERNAL",
                MsgDepend = "X2AP_PDU"
            };
            hashtable.Add(0x805, event117);
            EricPmEvent event118 = new EricPmEvent
            {
                PmEventName = "X2_ERROR_INDICATION",
                EventId = 0x804,
                EventType = "EXTERNAL",
                MsgDepend = "X2AP_PDU"
            };
            hashtable.Add(0x804, event118);
            EricPmEvent event119 = new EricPmEvent
            {
                PmEventName = "X2_HANDOVER_CANCEL",
                EventId = 0x809,
                EventType = "EXTERNAL",
                MsgDepend = "X2AP_PDU"
            };
            hashtable.Add(0x809, event119);
            EricPmEvent event120 = new EricPmEvent
            {
                PmEventName = "X2_HANDOVER_PREPARATION_FAILURE",
                EventId = 0x80e,
                EventType = "EXTERNAL",
                MsgDepend = "X2AP_PDU"
            };
            hashtable.Add(0x80e, event120);
            EricPmEvent event121 = new EricPmEvent
            {
                PmEventName = "X2_HANDOVER_REQUEST_ACKNOWLEDGE",
                EventId = 0x80b,
                EventType = "EXTERNAL",
                MsgDepend = "X2AP_PDU"
            };
            hashtable.Add(0x80b, event121);
            EricPmEvent event122 = new EricPmEvent
            {
                PmEventName = "X2_HANDOVER_REQUEST",
                EventId = 0x80a,
                EventType = "EXTERNAL",
                MsgDepend = "X2AP_PDU"
            };
            hashtable.Add(0x80a, event122);
            EricPmEvent event123 = new EricPmEvent
            {
                PmEventName = "X2_PRIVATE_MESSAGE",
                EventId = 0x814,
                EventType = "EXTERNAL",
                MsgDepend = "X2AP_PDU"
            };
            hashtable.Add(0x814, event123);
            EricPmEvent event124 = new EricPmEvent
            {
                PmEventName = "X2_RESET_REQUEST",
                EventId = 0x800,
                EventType = "EXTERNAL",
                MsgDepend = "X2AP_PDU"
            };
            hashtable.Add(0x800, event124);
            EricPmEvent event125 = new EricPmEvent
            {
                PmEventName = "X2_RESET_RESPONSE",
                EventId = 0x801,
                EventType = "EXTERNAL",
                MsgDepend = "X2AP_PDU"
            };
            hashtable.Add(0x801, event125);
            EricPmEvent event126 = new EricPmEvent
            {
                PmEventName = "X2_SN_STATUS_TRANSFER",
                EventId = 0x80c,
                EventType = "EXTERNAL",
                MsgDepend = "X2AP_PDU"
            };
            hashtable.Add(0x80c, event126);
            EricPmEvent event127 = new EricPmEvent
            {
                PmEventName = "X2_UE_CONTEXT_RELEASE",
                EventId = 0x80d,
                EventType = "EXTERNAL",
                MsgDepend = "X2AP_PDU"
            };
            hashtable.Add(0x80d, event127);
            EricPmEvent event128 = new EricPmEvent
            {
                PmEventName = "X2_X2_SETUP_FAILURE",
                EventId = 0x808,
                EventType = "EXTERNAL",
                MsgDepend = "X2AP_PDU"
            };
            hashtable.Add(0x808, event128);
            EricPmEvent event129 = new EricPmEvent
            {
                PmEventName = "X2_X2_SETUP_REQUEST",
                EventId = 0x802,
                EventType = "EXTERNAL",
                MsgDepend = "X2AP_PDU"
            };
            hashtable.Add(0x802, event129);
            EricPmEvent event130 = new EricPmEvent
            {
                PmEventName = "X2_X2_SETUP_RESPONSE",
                EventId = 0x803,
                EventType = "EXTERNAL",
                MsgDepend = "X2AP_PDU"
            };
            hashtable.Add(0x803, event130);
            return hashtable;
        }

        public uint EventId { get; set; }

        public string EventType { get; set; }

        public string MsgDepend { get; set; }

        public string PmEventName { get; set; }
    }
}
