using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceParser.Common
{
    public class AllTripObject
    {
        public List<TripS1Body> ls_trips1body { get; set; }

        public List<TripX2Body> ls_tripx2body { get; set; }

        public TripHead triphead { get; set; }

        public TripS1End trips1end { get; set; }

        public TripX2End tripx2end { get; set; }
    }

    public class Trip
    {
        public string ACCESS_GlobalCellId { get; set; }

        public DateTime CONNECTION_TIME { get; set; }

        public string CONNECTION_TYPE { get; set; }

        public int FIRST_ENBID_1 { get; set; }

        public int FIRST_ENBID_2 { get; set; }

        public string FIRST_FIRST_ENBID_3 { get; set; }

        public string FIRST_HANDOVER_EVENT_2 { get; set; }

        public string FIRST_HANDOVER_EVENT_3 { get; set; }

        public string FIRST_HANDOVER_TYPE_2 { get; set; }

        public string FIRST_HANDOVER_TYPE_3 { get; set; }

        public string FIRST_MME_UE_S1AP_ID { get; set; }

        public string FIRST_MMEID { get; set; }

        public decimal FIRST_RSRP_AVG_1 { get; set; }

        public decimal FIRST_RSRP_AVG_2 { get; set; }

        public decimal FIRST_RSRP_AVG_3 { get; set; }

        public decimal FIRST_RSRQ_AVG_1 { get; set; }

        public decimal FIRST_RSRQ_AVG_2 { get; set; }

        public decimal FIRST_RSRQ_AVG_3 { get; set; }

        public int FIRST_SECID_1 { get; set; }

        public int FIRST_SECID_2 { get; set; }

        public string FIRST_SECID_3 { get; set; }

        public string FirstGlobalCellId1 { get; set; }

        public string FirstGlobalCellId2 { get; set; }

        public string FirstGlobalCellId3 { get; set; }

        public string GUTI { get; set; }

        public string IMEI { get; set; }

        public string IMSI { get; set; }

        public int LAST_ENBID_1 { get; set; }

        public string LAST_ENBID_2 { get; set; }

        public string LAST_ENBID_3 { get; set; }

        public string LAST_HANDOVER_EVENT_1 { get; set; }

        public string LAST_HANDOVER_EVENT_2 { get; set; }

        public string LAST_HANDOVER_EVENT_3 { get; set; }

        public string LAST_HANDOVER_TYPE_1 { get; set; }

        public string LAST_HANDOVER_TYPE_2 { get; set; }

        public string LAST_HANDOVER_TYPE_3 { get; set; }

        public string LAST_MME_UE_S1AP_ID { get; set; }

        public string LAST_MMEID { get; set; }

        public decimal LAST_RSRP_AVG_1 { get; set; }

        public decimal LAST_RSRP_AVG_2 { get; set; }

        public string LAST_RSRP_AVG_3 { get; set; }

        public decimal LAST_RSRQ_AVG_1 { get; set; }

        public decimal LAST_RSRQ_AVG_2 { get; set; }

        public string LAST_RSRQ_AVG_3 { get; set; }

        public int LAST_SECID_1 { get; set; }

        public string LAST_SECID_2 { get; set; }

        public string LAST_SECID_3 { get; set; }

        public string LastGlobalCellId1 { get; set; }

        public string LastGlobalCellId2 { get; set; }

        public string LastGlobalCellId3 { get; set; }

        public string MCC { get; set; }

        public string MNC { get; set; }

        public string RELEASE_GlobalCellId { get; set; }

        public DateTime RELEASE_TIME { get; set; }

        public string RELEASECAUSE { get; set; }

        public string TAC { get; set; }
    }

    public class TripHead
    {
        public EventRRCConnectionRequest eventrrcconnectionrequest { get; set; }

        public EventRRCConnectionSetupComplete eventrrcconnectionsetupcomplete { get; set; }

        public EventS1InitialContextSetupRequest events1initialcontextsetuprequest { get; set; }

        public EventS1InitialUEMessage events1initialuemessage { get; set; }

        public string GlobalCellId { get; set; }

        public string Imsi { get; set; }

        public List<EventMeasurementReport> ls_eventmeasurementreport { get; set; }

        public List<EventRRCConnectionReconfiguration> ls_eventrrcconnectionreconfiguration { get; set; }

        public List<EventRRCConnectionReconfigurationComplete> ls_eventrrcconnectionreconfigurationcomplete { get; set; }

        public string MmeUeS1apid { get; set; }

        public string RacUeRef { get; set; }
    }

    public class TripOrder
    {
        public DateTime EventTime { get; set; }

        public string GlobalCellId { get; set; }

        public string Imsi { get; set; }

        public string MmeUeS1apid { get; set; }

        public string RacUeRef { get; set; }

        public string TripStatus { get; set; }
    }

    public class TripS1Body
    {
        public EventS1HandoverRequest events1handoverrequest { get; set; }

        public EventS1HandoverRequestAcknowledge events1handoverrequestacknowledge { get; set; }

        public string GlobalCellId { get; set; }

        public string Imsi { get; set; }

        public List<EventMeasurementReport> ls_eventmeasurementreport { get; set; }

        public List<EventRRCConnectionReconfiguration> ls_eventrrcconnectionreconfiguration { get; set; }

        public List<EventRRCConnectionReconfigurationComplete> ls_eventrrcconnectionreconfigurationcomplete { get; set; }

        public string MmeUeS1apid { get; set; }

        public string RacUeRef { get; set; }
    }

    public class TripS1End
    {
        public EventRRCConnectionRelease eventrrcconnectionrelease { get; set; }

        public EventS1HandoverRequest events1handoverrequest { get; set; }

        public EventS1HandoverRequestAcknowledge events1handoverrequestacknowledge { get; set; }

        public EventS1UEContextReleaseCommand events1uecontextreleasecommand { get; set; }

        public string GlobalCellId { get; set; }

        public string Imsi { get; set; }

        public List<EventMeasurementReport> ls_eventmeasurementreport { get; set; }

        public List<EventRRCConnectionReconfiguration> ls_eventrrcconnectionreconfiguration { get; set; }

        public List<EventRRCConnectionReconfigurationComplete> ls_eventrrcconnectionreconfigurationcomplete { get; set; }

        public string MmeUeS1apid { get; set; }

        public string RacUeRef { get; set; }
    }

    public class TripX2Body
    {
        public EventX2HandoverRequest eventx2handoverrequest { get; set; }

        public EventX2HandoverRequestAcknowledge eventx2handoverrequestacknowledge { get; set; }

        public EventX2UEContextRelease eventx2uecontextrelease { get; set; }

        public string GlobalCellId { get; set; }

        public string Imsi { get; set; }

        public List<EventMeasurementReport> ls_eventmeasurementreport { get; set; }

        public List<EventRRCConnectionReconfiguration> ls_eventrrcconnectionreconfiguration { get; set; }

        public List<EventRRCConnectionReconfigurationComplete> ls_eventrrcconnectionreconfigurationcomplete { get; set; }

        public string MmeUeS1apid { get; set; }

        public string RacUeRef { get; set; }
    }

    public class TripX2End
    {
        public EventRRCConnectionRelease eventrrcconnectionrelease { get; set; }

        public EventS1UEContextReleaseCommand events1uecontextreleasecommand { get; set; }

        public EventX2HandoverRequest eventx2handoverrequest { get; set; }

        public EventX2HandoverRequestAcknowledge eventx2handoverrequestacknowledge { get; set; }

        public EventX2UEContextRelease eventx2uecontextrelease { get; set; }

        public string GlobalCellId { get; set; }

        public string Imsi { get; set; }

        public List<EventMeasurementReport> ls_eventmeasurementreport { get; set; }

        public List<EventRRCConnectionReconfiguration> ls_eventrrcconnectionreconfiguration { get; set; }

        public List<EventRRCConnectionReconfigurationComplete> ls_eventrrcconnectionreconfigurationcomplete { get; set; }

        public string MmeUeS1apid { get; set; }

        public string RacUeRef { get; set; }
    }

}
