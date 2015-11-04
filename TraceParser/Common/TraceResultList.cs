using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceParser.Common
{
    public class TraceResultList
    {
        public List<ITraceMessage> Messages { get; set; }

        public TraceResultList()
        {
            Messages = new List<ITraceMessage>();
            FailCounts = 0;
        }

        public int FailCounts { get; set; }
    }
}
