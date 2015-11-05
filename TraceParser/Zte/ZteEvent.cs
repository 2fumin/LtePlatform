using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceParser.Zte
{
    public class ZteEvent
    {
        public static List<ZteEvent> GetZtePmEvents()
        {
            List<ZteEvent> list = new List<ZteEvent>();
            ZteEvent item = new ZteEvent
            {
                EventName = "BCCH_BCH_MessageType",
                EventType = "RRC",
                MsgDepend = "BCCH_BCH_Message"
            };
            list.Add(item);
            ZteEvent event3 = new ZteEvent
            {
                EventName = "MasterInformationBlock",
                EventType = "RRC",
                MsgDepend = "BCCH_BCH_Message"
            };
            list.Add(event3);
            ZteEvent event4 = new ZteEvent
            {
                EventName = "SystemInformation",
                EventType = "RRC",
                MsgDepend = "BCCH_DL_SCH_Message"
            };
            list.Add(event4);
            ZteEvent event5 = new ZteEvent
            {
                EventName = "SystemInformationBlockType1",
                EventType = "RRC",
                MsgDepend = "BCCH_DL_SCH_Message"
            };
            list.Add(event5);
            ZteEvent event6 = new ZteEvent
            {
                EventName = "MBSFNAreaConfiguration",
                EventType = "RRC",
                MsgDepend = "MCCH_Message"
            };
            list.Add(event6);
            ZteEvent event7 = new ZteEvent
            {
                EventName = "Paging",
                EventType = "RRC",
                MsgDepend = "PCCH_Message"
            };
            list.Add(event7);
            ZteEvent event8 = new ZteEvent
            {
                EventName = "RRCConnectionReestablishment",
                EventType = "RRC",
                MsgDepend = "DL_CCCH_Message"
            };
            list.Add(event8);
            ZteEvent event9 = new ZteEvent
            {
                EventName = "RRCConnectionReestablishmentReject",
                EventType = "RRC",
                MsgDepend = "DL_CCCH_Message"
            };
            list.Add(event9);
            ZteEvent event10 = new ZteEvent
            {
                EventName = "RRCConnectionReject",
                EventType = "RRC",
                MsgDepend = "DL_CCCH_Message"
            };
            list.Add(event10);
            ZteEvent event11 = new ZteEvent
            {
                EventName = "RRCConnectionSetup",
                EventType = "RRC",
                MsgDepend = "DL_CCCH_Message"
            };
            list.Add(event11);
            ZteEvent event12 = new ZteEvent
            {
                EventName = "CSFBParametersResponseCDMA2000",
                EventType = "RRC",
                MsgDepend = "DL_DCCH_Message"
            };
            list.Add(event12);
            ZteEvent event13 = new ZteEvent
            {
                EventName = "DLInformationTransfer",
                EventType = "RRC",
                MsgDepend = "DL_DCCH_Message"
            };
            list.Add(event13);
            ZteEvent event14 = new ZteEvent
            {
                EventName = "HandoverFromEUTRAPreparationRequest",
                EventType = "RRC",
                MsgDepend = "DL_DCCH_Message"
            };
            list.Add(event14);
            ZteEvent event15 = new ZteEvent
            {
                EventName = "MobilityFromEUTRACommand",
                EventType = "RRC",
                MsgDepend = "DL_DCCH_Message"
            };
            list.Add(event15);
            ZteEvent event16 = new ZteEvent
            {
                EventName = "RRCConnectionReconfiguration",
                EventType = "RRC",
                MsgDepend = "DL_DCCH_Message"
            };
            list.Add(event16);
            ZteEvent event17 = new ZteEvent
            {
                EventName = "RRCConnectionRelease",
                EventType = "RRC",
                MsgDepend = "DL_DCCH_Message"
            };
            list.Add(event17);
            ZteEvent event18 = new ZteEvent
            {
                EventName = "SecurityModeCommand",
                EventType = "RRC",
                MsgDepend = "DL_DCCH_Message"
            };
            list.Add(event18);
            ZteEvent event19 = new ZteEvent
            {
                EventName = "UECapabilityEnquiry",
                EventType = "RRC",
                MsgDepend = "DL_DCCH_Message"
            };
            list.Add(event19);
            ZteEvent event20 = new ZteEvent
            {
                EventName = "CounterCheck",
                EventType = "RRC",
                MsgDepend = "DL_DCCH_Message"
            };
            list.Add(event20);
            ZteEvent event21 = new ZteEvent
            {
                EventName = "UEInformationRequest",
                EventType = "RRC",
                MsgDepend = "DL_DCCH_Message"
            };
            list.Add(event21);
            ZteEvent event22 = new ZteEvent
            {
                EventName = "LoggedMeasurementConfiguration",
                EventType = "RRC",
                MsgDepend = "DL_DCCH_Message"
            };
            list.Add(event22);
            ZteEvent event23 = new ZteEvent
            {
                EventName = "RNReconfiguration",
                EventType = "RRC",
                MsgDepend = "DL_DCCH_Message"
            };
            list.Add(event23);
            ZteEvent event24 = new ZteEvent
            {
                EventName = "Object",
                EventType = "RRC",
                MsgDepend = "DL_DCCH_Message"
            };
            list.Add(event24);
            ZteEvent event25 = new ZteEvent
            {
                EventName = "Object",
                EventType = "RRC",
                MsgDepend = "DL_DCCH_Message"
            };
            list.Add(event25);
            ZteEvent event26 = new ZteEvent
            {
                EventName = "Object",
                EventType = "RRC",
                MsgDepend = "DL_DCCH_Message"
            };
            list.Add(event26);
            ZteEvent event27 = new ZteEvent
            {
                EventName = "Object",
                EventType = "RRC",
                MsgDepend = "DL_DCCH_Message"
            };
            list.Add(event27);
            ZteEvent event28 = new ZteEvent
            {
                EventName = "RRCConnectionReestablishmentRequest",
                EventType = "RRC",
                MsgDepend = "UL_CCCH_Message"
            };
            list.Add(event28);
            ZteEvent event29 = new ZteEvent
            {
                EventName = "RRCConnectionRequest",
                EventType = "RRC",
                MsgDepend = "UL_CCCH_Message"
            };
            list.Add(event29);
            ZteEvent event30 = new ZteEvent
            {
                EventName = "CSFBParametersRequestCDMA2000",
                EventType = "RRC",
                MsgDepend = "UL_DCCH_Message"
            };
            list.Add(event30);
            ZteEvent event31 = new ZteEvent
            {
                EventName = "MeasurementReport",
                EventType = "RRC",
                MsgDepend = "UL_DCCH_Message"
            };
            list.Add(event31);
            ZteEvent event32 = new ZteEvent
            {
                EventName = "RRCConnectionReconfigurationComplete",
                EventType = "RRC",
                MsgDepend = "UL_DCCH_Message"
            };
            list.Add(event32);
            ZteEvent event33 = new ZteEvent
            {
                EventName = "RRCConnectionReestablishmentComplete",
                EventType = "RRC",
                MsgDepend = "UL_DCCH_Message"
            };
            list.Add(event33);
            ZteEvent event34 = new ZteEvent
            {
                EventName = "RRCConnectionSetupComplete",
                EventType = "RRC",
                MsgDepend = "UL_DCCH_Message"
            };
            list.Add(event34);
            ZteEvent event35 = new ZteEvent
            {
                EventName = "SecurityModeComplete",
                EventType = "RRC",
                MsgDepend = "UL_DCCH_Message"
            };
            list.Add(event35);
            ZteEvent event36 = new ZteEvent
            {
                EventName = "SecurityModeFailure",
                EventType = "RRC",
                MsgDepend = "UL_DCCH_Message"
            };
            list.Add(event36);
            ZteEvent event37 = new ZteEvent
            {
                EventName = "UECapabilityInformation",
                EventType = "RRC",
                MsgDepend = "UL_DCCH_Message"
            };
            list.Add(event37);
            ZteEvent event38 = new ZteEvent
            {
                EventName = "ULHandoverPreparationTransfer",
                EventType = "RRC",
                MsgDepend = "UL_DCCH_Message"
            };
            list.Add(event38);
            ZteEvent event39 = new ZteEvent
            {
                EventName = "ULInformationTransfer",
                EventType = "RRC",
                MsgDepend = "UL_DCCH_Message"
            };
            list.Add(event39);
            ZteEvent event40 = new ZteEvent
            {
                EventName = "CounterCheckResponse",
                EventType = "RRC",
                MsgDepend = "UL_DCCH_Message"
            };
            list.Add(event40);
            ZteEvent event41 = new ZteEvent
            {
                EventName = "UEInformationResponse",
                EventType = "RRC",
                MsgDepend = "UL_DCCH_Message"
            };
            list.Add(event41);
            ZteEvent event42 = new ZteEvent
            {
                EventName = "ProximityIndication",
                EventType = "RRC",
                MsgDepend = "UL_DCCH_Message"
            };
            list.Add(event42);
            ZteEvent event43 = new ZteEvent
            {
                EventName = "RNReconfigurationComplete",
                EventType = "RRC",
                MsgDepend = "UL_DCCH_Message"
            };
            list.Add(event43);
            ZteEvent event44 = new ZteEvent
            {
                EventName = "MBMSCountingResponse",
                EventType = "RRC",
                MsgDepend = "UL_DCCH_Message"
            };
            list.Add(event44);
            ZteEvent event45 = new ZteEvent
            {
                EventName = "InterFreqRSTDMeasurementIndication",
                EventType = "RRC",
                MsgDepend = "UL_DCCH_Message"
            };
            list.Add(event45);
            ZteEvent event46 = new ZteEvent
            {
                EventName = "X2AP",
                EventType = "X2AP",
                MsgDepend = "X2AP_PDU"
            };
            list.Add(event46);
            ZteEvent event47 = new ZteEvent
            {
                EventName = "S1AP",
                EventType = "S1AP",
                MsgDepend = "S1AP_PDU"
            };
            list.Add(event47);
            return list;
        }

        public string EventName { get; set; }

        public string EventType { get; set; }

        public string MsgDepend { get; set; }
    }
}
