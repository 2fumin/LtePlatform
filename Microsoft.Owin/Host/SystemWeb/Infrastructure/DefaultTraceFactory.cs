using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Microsoft.Owin.Host.SystemWeb.Infrastructure
{
    internal class DefaultTraceFactory : ITraceFactory
    {
        private readonly ConcurrentDictionary<string, TraceSource> _sources = new ConcurrentDictionary<string, TraceSource>(StringComparer.OrdinalIgnoreCase);
        private readonly SourceSwitch _switch = new SourceSwitch("Microsoft.Owin.Host.SystemWeb");
        private const string RootTraceName = "Microsoft.Owin.Host.SystemWeb";

        public ITrace Create(string name)
        {
            return new DefaultTrace(GetOrAddTraceSource(name));
        }

        private TraceSource GetOrAddTraceSource(string name)
        {
            return _sources.GetOrAdd(name, InitializeTraceSource);
        }

        private static bool HasDefaultListeners(TraceSource traceSource)
        {
            return ((traceSource.Listeners.Count == 1) && (traceSource.Listeners[0] is DefaultTraceListener));
        }

        private static bool HasDefaultSwitch(TraceSource traceSource)
        {
            return ((string.IsNullOrEmpty(traceSource.Switch.DisplayName) == string.IsNullOrEmpty(traceSource.Name)) &&
                    (traceSource.Switch.Level == SourceLevels.Off));
        }

        private TraceSource InitializeTraceSource(string key)
        {
            var traceSource = new TraceSource(key);
            if (key == RootTraceName)
            {
                if (HasDefaultSwitch(traceSource))
                {
                    traceSource.Switch = _switch;
                }
                return traceSource;
            }
            if (HasDefaultListeners(traceSource))
            {
                var orAddTraceSource = GetOrAddTraceSource(RootTraceName);
                traceSource.Listeners.Clear();
                traceSource.Listeners.AddRange(orAddTraceSource.Listeners);
            }
            if (!HasDefaultSwitch(traceSource)) return traceSource;
            var source3 = GetOrAddTraceSource(RootTraceName);
            traceSource.Switch = source3.Switch;
            return traceSource;
        }
    }
}
