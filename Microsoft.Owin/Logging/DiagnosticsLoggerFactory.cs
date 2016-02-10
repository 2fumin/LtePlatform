using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Microsoft.Owin.Logging
{
    public class DiagnosticsLoggerFactory : ILoggerFactory
    {
        private readonly SourceSwitch _rootSourceSwitch;
        private readonly TraceListener _rootTraceListener;
        private readonly ConcurrentDictionary<string, TraceSource> _sources;
        private const string RootTraceName = "Microsoft.Owin";

        public DiagnosticsLoggerFactory()
        {
            _sources = new ConcurrentDictionary<string, TraceSource>(StringComparer.OrdinalIgnoreCase);
            _rootSourceSwitch = new SourceSwitch(RootTraceName);
            _rootTraceListener = null;
        }

        public DiagnosticsLoggerFactory(SourceSwitch rootSourceSwitch, TraceListener rootTraceListener)
        {
            _sources = new ConcurrentDictionary<string, TraceSource>(StringComparer.OrdinalIgnoreCase);
            _rootSourceSwitch = rootSourceSwitch ?? new SourceSwitch(RootTraceName);
            _rootTraceListener = rootTraceListener;
        }

        public ILogger Create(string name)
        {
            return new DiagnosticsLogger(GetOrAddTraceSource(name));
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

        private TraceSource InitializeTraceSource(string traceSourceName)
        {
            var traceSource = new TraceSource(traceSourceName);
            if (traceSourceName == RootTraceName)
            {
                if (HasDefaultSwitch(traceSource))
                {
                    traceSource.Switch = _rootSourceSwitch;
                }
                if (_rootTraceListener != null)
                {
                    traceSource.Listeners.Add(_rootTraceListener);
                }
                return traceSource;
            }
            var name = ParentSourceName(traceSourceName);
            if (HasDefaultListeners(traceSource))
            {
                var orAddTraceSource = GetOrAddTraceSource(name);
                traceSource.Listeners.Clear();
                traceSource.Listeners.AddRange(orAddTraceSource.Listeners);
            }
            if (!HasDefaultSwitch(traceSource)) return traceSource;
            var source3 = GetOrAddTraceSource(name);
            traceSource.Switch = source3.Switch;
            return traceSource;
        }

        private static string ParentSourceName(string traceSourceName)
        {
            var length = traceSourceName.LastIndexOf('.');
            return length != -1 ? traceSourceName.Substring(0, length) : RootTraceName;
        }
    }
}
