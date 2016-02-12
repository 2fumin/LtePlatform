using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Routing;
using Microsoft.Owin.Builder;
using Microsoft.Owin.Host.SystemWeb.CallEnvironment;
using Microsoft.Owin.Host.SystemWeb.DataProtection;
using Microsoft.Owin.Host.SystemWeb.Infrastructure;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Properties;

namespace Microsoft.Owin.Host.SystemWeb
{
    internal class OwinAppContext
    {
        private bool _detectWebSocketSupportStageTwoExecuted;
        private object _detectWebSocketSupportStageTwoLock;
        private const string TraceName = "Microsoft.Owin.Host.SystemWeb.OwinAppContext";
        private readonly ITrace _trace = TraceFactory.Create(TraceName);

        public OwinAppContext()
        {
            AppName = HostingEnvironment.SiteName + HostingEnvironment.ApplicationID;
            if (string.IsNullOrWhiteSpace(AppName))
            {
                AppName = Guid.NewGuid().ToString();
            }
        }

        public OwinCallContext CreateCallContext(RequestContext requestContext, string requestPathBase, string requestPath, 
            AsyncCallback callback, object extraData)
        {
            DetectWebSocketSupportStageTwo(requestContext);
            return new OwinCallContext(this, requestContext, requestPathBase, requestPath, callback, extraData);
        }

        private void DetectWebSocketSupportStageOne()
        {
            if ((HttpRuntime.IISVersion != null) && (HttpRuntime.IISVersion.Major >= 8))
            {
                WebSocketSupport = true;
                Capabilities[Constants.WebSocketVersionKey] = Constants.WebSocketVersion;
            }
            else
            {
                _trace.Write(TraceEventType.Information, Resources.Trace_WebSocketsSupportNotDetected);
            }
        }

        private void DetectWebSocketSupportStageTwo(RequestContext requestContext)
        {
            object target = null;
            if (!WebSocketSupport) return;
            Func<object> valueFactory = delegate
            {
                var str = requestContext.HttpContext.Request.ServerVariables["WEBSOCKET_VERSION"];
                if (string.IsNullOrEmpty(str))
                {
                    Capabilities.Remove(Constants.WebSocketVersionKey);
                    WebSocketSupport = false;
                    _trace.Write(TraceEventType.Information, Resources.Trace_WebSocketsSupportNotDetected);
                }
                else
                {
                    _trace.Write(TraceEventType.Information, Resources.Trace_WebSocketsSupportDetected);
                }
                return null;
            };
            LazyInitializer.EnsureInitialized(ref target, ref _detectWebSocketSupportStageTwoExecuted, 
                ref _detectWebSocketSupportStageTwoLock, valueFactory);
        }

        internal void Initialize(Action<IAppBuilder> startup)
        {
            Capabilities = new ConcurrentDictionary<string, object>();
            var builder = new AppBuilder();
            builder.Properties[Constants.OwinVersionKey] = Constants.OwinVersion;
            builder.Properties[Constants.HostTraceOutputKey] = TraceTextWriter.Instance;
            builder.Properties[Constants.HostAppNameKey] = AppName;
            builder.Properties[Constants.HostOnAppDisposingKey] = OwinApplication.ShutdownToken;
            builder.Properties[Constants.HostReferencedAssemblies] = new ReferencedAssembliesWrapper();
            builder.Properties[Constants.ServerCapabilitiesKey] = Capabilities;
            builder.Properties[Constants.SecurityDataProtectionProvider] = new MachineKeyDataProtectionProvider().ToOwinFunction();
            builder.SetLoggerFactory(new DiagnosticsLoggerFactory());
            Capabilities[Constants.SendFileVersionKey] = Constants.SendFileVersion;
            var section = (CompilationSection)ConfigurationManager.GetSection("system.web/compilation");
            if (section.Debug)
            {
                builder.Properties[Constants.HostAppModeKey] = "development";
            }
            DetectWebSocketSupportStageOne();
            try
            {
                startup(builder);
            }
            catch (Exception exception)
            {
                _trace.WriteError(Resources.Trace_EntryPointException, exception);
                throw;
            }
            AppFunc = (Func<IDictionary<string, object>, Task>)builder.Build(typeof(Func<IDictionary<string, object>, Task>));
        }

        internal Func<IDictionary<string, object>, Task> AppFunc { get; set; }

        internal string AppName { get; }

        internal IDictionary<string, object> Capabilities { get; private set; }

        internal bool WebSocketSupport { get; set; }
    }
}
