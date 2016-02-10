using System;

namespace Microsoft.Owin.Infrastructure
{
    public class SystemClock : ISystemClock
    {
        public DateTimeOffset UtcNow
        {
            get
            {
                DateTimeOffset utcNow = DateTimeOffset.UtcNow;
                return utcNow.AddMilliseconds((double)-utcNow.Millisecond);
            }
        }
    }
}
