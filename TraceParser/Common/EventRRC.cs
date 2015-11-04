using TraceParser.Eutra;

namespace TraceParser.Common
{
    public class EventMeasurementReport
    {
        public EventHead eventhead { get; set; }

        public MeasurementReport MeasurementReport { get; set; }
    }

    public class EventRRCConnectionReconfiguration
    {
        public EventHead eventhead { get; set; }

        public RRCConnectionReconfiguration RRCConnectionReconfiguration { get; set; }
    }

    public class EventRRCConnectionReconfigurationComplete
    {
        public EventHead eventhead { get; set; }

        public RRCConnectionReconfigurationComplete RRCConnectionReconfigurationComplete { get; set; }
    }

    public class EventRRCConnectionReject
    {
        public EventHead eventhead { get; set; }

        public RRCConnectionReject RRCConnectionReject { get; set; }
    }

    public class EventRRCConnectionRelease
    {
        public EventHead eventhead { get; set; }

        public RRCConnectionRelease RRCConnectionRelease { get; set; }
    }

    public class EventRRCConnectionRequest
    {
        public EventHead eventhead { get; set; }

        public RRCConnectionRequest RRCConnectionRequest { get; set; }
    }

    public class EventRRCConnectionSetup
    {
        public EventHead eventhead { get; set; }

        public RRCConnectionSetup RRCConnectionSetup { get; set; }
    }

    public class EventRRCConnectionSetupComplete
    {
        public EventHead eventhead { get; set; }

        public RRCConnectionSetupComplete RRCConnectionSetupComplete { get; set; }
    }

}
