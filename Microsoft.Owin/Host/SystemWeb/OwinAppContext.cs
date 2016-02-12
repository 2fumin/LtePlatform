using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Properties;

namespace Microsoft.Owin.Host.SystemWeb
{
    using Microsoft.Owin.Builder;
    using Microsoft.Owin.Host.SystemWeb.CallEnvironment;
    using Microsoft.Owin.Host.SystemWeb.DataProtection;
    using Microsoft.Owin.Host.SystemWeb.Infrastructure;
    using Microsoft.Owin.Logging;
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
        private readonly ITrace _trace = TraceFactory.Create("Microsoft.Owin.Host.SystemWeb.OwinAppContext");
        private const string TraceName = "Microsoft.Owin.Host.SystemWeb.OwinAppContext";

        public OwinAppContext()
        {
            this.AppName = HostingEnvironment.SiteName + HostingEnvironment.ApplicationID;
            if (string.IsNullOrWhiteSpace(this.AppName))
            {
                this.AppName = Guid.NewGuid().ToString();
            }
        }

        public OwinCallContext CreateCallContext(RequestContext requestContext, string requestPathBase, string requestPath, AsyncCallback callback, object extraData)
        {
            this.DetectWebSocketSupportStageTwo(requestContext);
            return new OwinCallContext(this, requestContext, requestPathBase, requestPath, callback, extraData);
        }

        private void DetectWebSocketSupportStageOne()
        {
            if ((HttpRuntime.IISVersion != null) && (HttpRuntime.IISVersion.Major >= 8))
            {
                this.WebSocketSupport = true;
                this.Capabilities["websocket.Version"] = "1.0";
            }
            else
            {
                this._trace.Write(TraceEventType.Information, Resources.Trace_WebSocketsSupportNotDetected, new object[0]);
            }
        }

        private void DetectWebSocketSupportStageTwo(RequestContext requestContext)
        {
            Func<object> valueFactory = null;
            object target = null;
            if (this.WebSocketSupport)
            {
                if (valueFactory == null)
                {
                    valueFactory = delegate {
                        string str = requestContext.HttpContext.Request.ServerVariables["WEBSOCKET_VERSION"];
                        if (string.IsNullOrEmpty(str))
                        {
                            this.Capabilities.Remove("websocket.Version");
                            this.WebSocketSupport = false;
                            this._trace.Write(TraceEventType.Information, Resources.Trace_WebSocketsSupportNotDetected, new object[0]);
                        }
                        else
                        {
                            this._trace.Write(TraceEventType.Information, Resources.Trace_WebSocketsSupportDetected, new object[0]);
                        }
                        return null;
                    };
                }
                LazyInitializer.EnsureInitialized<object>(ref target, ref this._detectWebSocketSupportStageTwoExecuted, ref this._detectWebSocketSupportStageTwoLock, valueFactory);
            }
        }

        internal void Initialize(Action<IAppBuilder> startup)
        {
            this.Capabilities = new ConcurrentDictionary<string, object>();
            AppBuilder builder = new AppBuilder();
            builder.Properties[OwinConstants.OwinVersion] = "1.0";
            builder.Properties["host.TraceOutput"] = TraceTextWriter.Instance;
            builder.Properties[OwinConstants.CommonKeys.AppName] = this.AppName;
            builder.Properties["host.OnAppDisposing"] = OwinApplication.ShutdownToken;
            builder.Properties["host.ReferencedAssemblies"] = new ReferencedAssembliesWrapper();
            builder.Properties["server.Capabilities"] = this.Capabilities;
            builder.Properties["security.DataProtectionProvider"] = new MachineKeyDataProtectionProvider().ToOwinFunction();
            AppBuilderLoggerExtensions.SetLoggerFactory((IAppBuilder)builder, new DiagnosticsLoggerFactory());
            this.Capabilities["sendfile.Version"] = "1.0";
            CompilationSection section = (CompilationSection)ConfigurationManager.GetSection("system.web/compilation");
            if (section.Debug)
            {
                builder.Properties[OwinConstants.CommonKeys.AppMode] = "development";
            }
            this.DetectWebSocketSupportStageOne();
            try
            {
                startup((IAppBuilder)builder);
            }
            catch (Exception exception)
            {
                this._trace.WriteError(Resources.Trace_EntryPointException, exception);
                throw;
            }
            this.AppFunc = (Func<IDictionary<string, object>, Task>)builder.Build(typeof(Func<IDictionary<string, object>, Task>));
        }

        internal Func<IDictionary<string, object>, Task> AppFunc { get; set; }

        internal string AppName { get; private set; }

        internal IDictionary<string, object> Capabilities { get; private set; }

        internal bool WebSocketSupport { get; set; }
    }
}
