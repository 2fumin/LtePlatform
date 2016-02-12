using System;
using System.Threading;
using System.Web;
using Microsoft.Owin.Host.SystemWeb.Infrastructure;
using Microsoft.Owin.Properties;

namespace Microsoft.Owin.Host.SystemWeb
{
    internal class DisconnectWatcher : IDisposable
    {
        private CancellationTokenSource _callCancelledSource;
        private IDisposable _connectionCheckTimer;
        private readonly HttpResponseBase _httpResponse;
        private static readonly TimerCallback ConnectionTimerCallback = CheckIsClientConnected;
        private static readonly bool IsClientDisconnectedTokenAvailable = CheckIsClientDisconnectedTokenAvailable();
        private static readonly bool IsSystemWebVersion451OrGreater = CheckIsSystemWebVersion451OrGreater();
        private static readonly ITrace Trace = TraceFactory.Create(TraceName);
        private const string TraceName = "Microsoft.Owin.Host.SystemWeb.DisconnectWatcher";

        internal DisconnectWatcher(HttpResponseBase httpResponse)
        {
            _httpResponse = httpResponse;
        }

        internal CancellationToken BindDisconnectNotification()
        {
            if (IsClientDisconnectedTokenAvailable && IsSystemWebVersion451OrGreater)
            {
                return _httpResponse.ClientDisconnectedToken;
            }
            _callCancelledSource = new CancellationTokenSource();
            _connectionCheckTimer = new Timer(ConnectionTimerCallback, this, TimeSpan.FromSeconds(10.0), TimeSpan.FromSeconds(10.0));
            return _callCancelledSource.Token;
        }

        private static void CheckIsClientConnected(object obj)
        {
            DisconnectWatcher watcher = (DisconnectWatcher)obj;
            if (watcher._httpResponse.IsClientConnected) return;
            watcher._connectionCheckTimer.Dispose();
            SetDisconnected(watcher);
        }

        private static bool CheckIsClientDisconnectedTokenAvailable()
        {
            Version version = new Version(7, 5);
            Version iISVersion = HttpRuntime.IISVersion;
            return (((iISVersion != null) && (iISVersion >= version)) && HttpRuntime.UsingIntegratedPipeline);
        }

        private static bool CheckIsSystemWebVersion451OrGreater()
        {
            return (typeof(HttpContextBase).Assembly.GetType("System.Web.AspNetEventSource") != null);
        }

        public void Dispose()
        {
            UnbindDisconnectNotification();
        }

        internal void OnFaulted()
        {
            SetDisconnected(this);
        }

        private static void SetDisconnected(object obj)
        {
            DisconnectWatcher watcher = (DisconnectWatcher)obj;
            CancellationTokenSource source = watcher._callCancelledSource;
            if (source != null)
            {
                try
                {
                    source.Cancel(false);
                }
                catch (ObjectDisposedException)
                {
                }
                catch (AggregateException exception)
                {
                    Trace.WriteError(Resources.Trace_RequestDisconnectCallbackExceptions, exception);
                }
            }
        }

        private void UnbindDisconnectNotification()
        {
            _callCancelledSource?.Dispose();
            _connectionCheckTimer?.Dispose();
        }
    }
}
