using System;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using Microsoft.Owin.Host.SystemWeb.Infrastructure;
using Microsoft.Owin.Properties;

namespace Microsoft.Owin.Host.SystemWeb
{
    internal class ShutdownDetector : IRegisteredObject, IDisposable
    {
        private IDisposable _checkAppPoolTimer;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private const string TraceName = "Microsoft.Owin.Host.SystemWeb.ShutdownDetector";
        private readonly ITrace _trace = TraceFactory.Create(TraceName);

        private void Cancel()
        {
            _checkAppPoolTimer?.Dispose();
            try
            {
                _cts.Cancel(false);
            }
            catch (ObjectDisposedException)
            {
            }
            catch (AggregateException exception)
            {
                _trace.WriteError(Resources.Trace_ShutdownException, exception);
            }
        }

        private void CheckForAppDomainRestart(object state)
        {
            if (SystemWeb.UnsafeIISMethods.RequestedAppDomainRestart)
            {
                Cancel();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cts.Dispose();
                if (_checkAppPoolTimer != null)
                {
                    _checkAppPoolTimer.Dispose();
                }
            }
        }

        internal void Initialize()
        {
            try
            {
                HostingEnvironment.RegisterObject(this);
                if ((HttpRuntime.UsingIntegratedPipeline && !RegisterForStopListeningEvent()) && SystemWeb.UnsafeIISMethods.CanDetectAppDomainRestart)
                {
                    _checkAppPoolTimer = new Timer(new TimerCallback(CheckForAppDomainRestart), null, TimeSpan.FromSeconds(10.0), TimeSpan.FromSeconds(10.0));
                }
            }
            catch (Exception exception)
            {
                _trace.WriteError(Resources.Trace_ShutdownDetectionSetupException, exception);
            }
        }

        private bool RegisterForStopListeningEvent()
        {
            EventInfo info = typeof(HostingEnvironment).GetEvent("StopListening");
            if (info == null)
            {
                return false;
            }
            info.AddEventHandler(null, new EventHandler(StopListening));
            return true;
        }

        public void Stop(bool immediate)
        {
            Cancel();
            HostingEnvironment.UnregisterObject(this);
        }

        private void StopListening(object sender, EventArgs e)
        {
            Cancel();
        }

        internal CancellationToken Token
        {
            get
            {
                return _cts.Token;
            }
        }
    }
}
