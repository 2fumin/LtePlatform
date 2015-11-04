using TraceParser.S1ap;

namespace TraceParser.Common
{
    public class EventS1HandoverCommand
    {
        public EventHead eventhead { get; set; }

        public HandoverCommand HandoverCommand { get; set; }
    }

    public class EventS1HandoverRequest
    {
        public EventHead eventhead { get; set; }

        public HandoverRequest HandoverRequest { get; set; }
    }

    public class EventS1HandoverRequestAcknowledge
    {
        public EventHead eventhead { get; set; }

        public HandoverRequestAcknowledge HandoverRequestAcknowledge { get; set; }
    }

    public class EventS1HandoverRequired
    {
        public EventHead eventhead { get; set; }

        public HandoverRequired HandoverRequired { get; set; }
    }

    public class EventS1InitialContextSetupRequest
    {
        public EventHead eventhead { get; set; }

        public InitialContextSetupRequest InitialContextSetupRequest { get; set; }
    }

    public class EventS1InitialUEMessage
    {
        public EventHead eventhead { get; set; }

        public InitialUEMessage InitialUEMessage { get; set; }
    }

    public class EventS1UEContextReleaseCommand
    {
        public EventHead eventhead { get; set; }

        public UEContextReleaseCommand UEContextReleaseCommand { get; set; }
    }

}
