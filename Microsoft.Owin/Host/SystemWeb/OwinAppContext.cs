using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Properties;

namespace Microsoft.Owin.Host.SystemWeb
{
    using Builder;
    using CallEnvironment;
    using SystemWeb.DataProtection;
    using Infrastructure;
    using Logging;
    using Owin;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Hosting;
    using System.Web.Routing;

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
            builder.Properties[OwinConstants.OwinVersion] = "1.0";
            builder.Properties["host.TraceOutput"] = TraceTextWriter.Instance;
            builder.Properties[OwinConstants.CommonKeys.AppName] = AppName;
            builder.Properties["host.OnAppDisposing"] = OwinApplication.ShutdownToken;
            builder.Properties["host.ReferencedAssemblies"] = new ReferencedAssembliesWrapper();
            builder.Properties["server.Capabilities"] = Capabilities;
            builder.Properties["security.DataProtectionProvider"] = new MachineKeyDataProtectionProvider().ToOwinFunction();
            AppBuilderLoggerExtensions.SetLoggerFactory((IAppBuilder)builder, new DiagnosticsLoggerFactory());
            Capabilities["sendfile.Version"] = "1.0";
            var section = (CompilationSection)ConfigurationManager.GetSection("system.web/compilation");
            if (section.Debug)
            {
                builder.Properties[OwinConstants.CommonKeys.AppMode] = "development";
            }
            DetectWebSocketSupportStageOne();
            try
            {
                startup((IAppBuilder)builder);
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
