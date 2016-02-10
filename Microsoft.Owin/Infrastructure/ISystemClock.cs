using System;

namespace Microsoft.Owin.Infrastructure
{
    public interface ISystemClock
    {
        DateTimeOffset UtcNow { get; }
    }
}
