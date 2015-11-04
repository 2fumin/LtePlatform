using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceParser.X2ap;

namespace TraceParser.Common
{
    public class EventX2HandoverRequest
    {
        public EventHead eventhead { get; set; }

        public HandoverRequest HandoverRequest { get; set; }
    }

    public class EventX2HandoverRequestAcknowledge
    {
        public EventHead eventhead { get; set; }

        public HandoverRequestAcknowledge HandoverRequestAcknowledge { get; set; }
    }

    public class EventX2UEContextRelease
    {
        public EventHead eventhead { get; set; }

        public UEContextRelease UEContextRelease { get; set; }
    }

}
