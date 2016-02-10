using System;
using System.Diagnostics;

namespace Microsoft.Owin.Logging
{
    public interface ILogger
    {
        bool WriteCore(TraceEventType eventType, int eventId, object state, Exception exception, Func<object, Exception, string> formatter);
    }
}
