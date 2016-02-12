using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using Microsoft.Owin.Host.SystemWeb.IntegratedPipeline;

namespace Microsoft.Owin.Host.SystemWeb.CallEnvironment
{
    [GeneratedCode("TextTemplatingFileGenerator", "")]
    internal sealed class AspNetDictionary : IDictionary<string, object>
    {
        private CancellationToken _CallCancelled;
        private X509Certificate _ClientCert;
        private Action _DisableRequestBuffering;
        private Action _DisableResponseBuffering;
        private Action _DisableResponseCompression;
        private IDictionary<string, object> _extra = WeakNilEnvironment;
        private uint _flag0 = 0xfe59be96;
        private uint _flag1 = 3;
        private string _HostAppMode;
        private string _HostAppName;
        private TextWriter _HostTraceOutput;
        private HttpContextBase _HttpContextBase;
        private uint _initFlag0 = 0xfe59be96;
        private uint _initFlag1 = 3;
        private IntegratedPipelineContext _IntegratedPipelineContext;
        private string _IntegratedPipelineStage;
        private Func<Task> _LoadClientCert;
        private CancellationToken _OnAppDisposing;
        private Action<Action<object>, object> _OnSendingHeaders;
        private string _OwinVersion;
        private readonly IPropertySource _propertySource;
        private Stream _RequestBody;
        private RequestContext _RequestContext;
        private IDictionary<string, string[]> _RequestHeaders;
        private string _RequestId;
        private string _RequestMethod;
        private string _RequestPath;
        private string _RequestPathBase;
        private string _RequestProtocol;
        private string _RequestQueryString;
        private string _RequestScheme;
        private Stream _ResponseBody;
        private IDictionary<string, string[]> _ResponseHeaders;
        private Func<string, long, long?, CancellationToken, Task> _SendFileAsync;
        private IDictionary<string, object> _ServerCapabilities;
        private bool _ServerIsLocal;
        private string _ServerLocalIpAddress;
        private string _ServerLocalPort;
        private string _ServerRemoteIpAddress;
        private string _ServerRemotePort;
        private Action<IDictionary<string, object>, Func<IDictionary<string, object>, Task>> _WebSocketAccept;
        private static readonly IDictionary<string, object> WeakNilEnvironment = new NilDictionary();

        internal AspNetDictionary(IPropertySource propertySource)
        {
            _propertySource = propertySource;
        }

        private bool InitPropertyClientCert()
        {
            if (!_propertySource.TryGetClientCert(ref _ClientCert))
            {
                _flag0 &= 0xbfffffff;
                _initFlag0 &= 0xbfffffff;
                return false;
            }
            _initFlag0 &= 0xbfffffff;
            return true;
        }

        private bool InitPropertyDisableRequestBuffering()
        {
            if (!_propertySource.TryGetDisableRequestBuffering(ref _DisableRequestBuffering))
            {
                _flag0 &= 0xfffff7ff;
                _initFlag0 &= 0xfffff7ff;
                return false;
            }
            _initFlag0 &= 0xfffff7ff;
            return true;
        }

        private bool InitPropertyHostAppMode()
        {
            if (!_propertySource.TryGetHostAppMode(ref _HostAppMode))
            {
                _flag0 &= 0xfff7ffff;
                _initFlag0 &= 0xfff7ffff;
                return false;
            }
            _initFlag0 &= 0xfff7ffff;
            return true;
        }

        private bool InitPropertyLoadClientCert()
        {
            if (!_propertySource.TryGetLoadClientCert(ref _LoadClientCert))
            {
                _flag0 &= 0x7fffffff;
                _initFlag0 &= 0x7fffffff;
                return false;
            }
            _initFlag0 &= 0x7fffffff;
            return true;
        }

        private bool InitPropertyWebSocketAccept()
        {
            if (!_propertySource.TryGetWebSocketAccept(ref _WebSocketAccept))
            {
                _flag1 &= 0xfffffffd;
                _initFlag1 &= 0xfffffffd;
                return false;
            }
            _initFlag1 &= 0xfffffffd;
            return true;
        }

        private bool PropertiesContainsKey(string key)
        {
            switch (key.Length)
            {
                case 11:
                    if (((_flag0 & 0x400000) == 0) || !string.Equals(key, OwinConstants.Security.User, StringComparison.Ordinal))
                    {
                        break;
                    }
                    return true;

                case 12:
                    if (((_flag0 & 1) == 0) || !string.Equals(key, OwinConstants.OwinVersion, StringComparison.Ordinal))
                    {
                        if ((((_flag0 & 0x40000) == 0) || !string.Equals(key, OwinConstants.CommonKeys.AppName, StringComparison.Ordinal)) &&
                            ((((_flag0 & 0x80000) == 0) || !string.Equals(key, OwinConstants.CommonKeys.AppMode, StringComparison.Ordinal)) ||
                             (((_initFlag0 & 0x80000) != 0) && !InitPropertyHostAppMode())))
                        {
                            break;
                        }
                        return true;
                    }
                    return true;

                case 14:
                    if (((_flag0 & 0x400) == 0) || !string.Equals(key, OwinConstants.RequestId, StringComparison.Ordinal))
                    {
                        if (((_flag0 & 0x20000000) == 0) || !string.Equals(key, OwinConstants.CommonKeys.IsLocal, StringComparison.Ordinal))
                        {
                            break;
                        }
                        return true;
                    }
                    return true;

                case 0x10:
                    if (((_flag0 & 0x40) == 0) || !string.Equals(key, OwinConstants.RequestPath, StringComparison.Ordinal))
                    {
                        if (((_flag0 & 0x200) == 0) || !string.Equals(key, OwinConstants.RequestBody, StringComparison.Ordinal))
                        {
                            if (((_flag0 & 0x20000) != 0) && string.Equals(key, "host.TraceOutput", StringComparison.Ordinal))
                            {
                                return true;
                            }
                            if (((_flag0 & 0x10000000) != 0) && string.Equals(key, "server.LocalPort", StringComparison.Ordinal))
                            {
                                return true;
                            }
                            if ((((_flag1 & 2) == 0) ||
                                 !string.Equals(key, "websocket.Accept", StringComparison.Ordinal)) ||
                                (((_initFlag1 & 2) != 0) && !InitPropertyWebSocketAccept()))
                            {
                                break;
                            }
                        }
                        return true;
                    }
                    return true;

                case 0x11:
                    if (((_flag0 & 0x8000) == 0) || !string.Equals(key, "owin.ResponseBody", StringComparison.Ordinal))
                    {
                        if (((_flag0 & 0x4000000) == 0) || !string.Equals(key, "server.RemotePort", StringComparison.Ordinal))
                        {
                            break;
                        }
                        return true;
                    }
                    return true;

                case 0x12:
                    if (((_flag0 & 2) == 0) || !string.Equals(key, "owin.CallCancelled", StringComparison.Ordinal))
                    {
                        if (((_flag0 & 8) != 0) &&
                            string.Equals(key, OwinConstants.RequestMethod, StringComparison.Ordinal)) return true;
                        if (((_flag0 & 0x10) != 0) && string.Equals(key, "owin.RequestScheme", StringComparison.Ordinal))
                        {
                            return true;
                        }
                        if (((_flag1 & 1) == 0) || !string.Equals(key, "sendfile.SendAsync", StringComparison.Ordinal))
                        {
                            break;
                        }
                        return true;
                    }
                    return true;

                case 0x13:
                    if (((_flag0 & 0x100) == 0) || !string.Equals(key, OwinConstants.RequestHeaders, StringComparison.Ordinal))
                    {
                        if ((((_flag0 & 0x100000) == 0) ||
                             !string.Equals(key, "host.OnAppDisposing", StringComparison.Ordinal)) &&
                            (((_flag0 & 0x1000000) == 0) ||
                             !string.Equals(key, "server.Capabilities", StringComparison.Ordinal)))
                        {
                            break;
                        }
                        return true;
                    }
                    return true;

                case 20:
                    if (((_flag0 & 4) == 0) || !string.Equals(key, OwinConstants.RequestProtocol, StringComparison.Ordinal))
                    {
                        if ((((_flag0 & 0x20) == 0) || !string.Equals(key, "owin.RequestPathBase", StringComparison.Ordinal)) && (((_flag0 & 0x4000) == 0) || !string.Equals(key, "owin.ResponseHeaders", StringComparison.Ordinal)))
                        {
                            break;
                        }
                        return true;
                    }
                    return true;

                case 0x15:
                    if (((_flag0 & 0x8000000) == 0) || !string.Equals(key, "server.LocalIpAddress", StringComparison.Ordinal))
                    {
                        if ((((_flag0 & 0x40000000) == 0) || !string.Equals(key, "ssl.ClientCertificate", StringComparison.Ordinal)) || (((_initFlag0 & 0x40000000) != 0) && !InitPropertyClientCert()))
                        {
                            break;
                        }
                        return true;
                    }
                    return true;

                case 0x16:
                    if (((_flag0 & 0x2000000) == 0) || !string.Equals(key, "server.RemoteIpAddress", StringComparison.Ordinal))
                    {
                        break;
                    }
                    return true;

                case 0x17:
                    if (((_flag0 & 0x80) == 0) || !string.Equals(key, "owin.RequestQueryString", StringComparison.Ordinal))
                    {
                        if (((_flag0 & 0x1000) == 0) || !string.Equals(key, "owin.ResponseStatusCode", StringComparison.Ordinal))
                        {
                            if (((_flag0 & 0x800000) != 0) && string.Equals(key, "server.OnSendingHeaders", StringComparison.Ordinal))
                            {
                                return true;
                            }
                            if ((((_flag0 & 0x80000000) == 0) || !string.Equals(key, "ssl.LoadClientCertAsync", StringComparison.Ordinal)) || (((_initFlag0 & 0x80000000) != 0) && !InitPropertyLoadClientCert()))
                            {
                                break;
                            }
                        }
                        return true;
                    }
                    return true;

                case 0x19:
                    if (((_flag0 & 0x2000) == 0) || !string.Equals(key, "owin.ResponseReasonPhrase", StringComparison.Ordinal))
                    {
                        break;
                    }
                    return true;

                case 0x1a:
                    if (((_flag1 & 4) == 0) || !string.Equals(key, Constants.IntegratedPipelineContext, StringComparison.Ordinal))
                    {
                        if (((_flag1 & 0x20) != 0) && string.Equals(key, "System.Web.HttpContextBase", StringComparison.Ordinal))
                        {
                            return true;
                        }
                        break;
                    }
                    return true;

                case 30:
                    if ((((_flag0 & 0x800) == 0) || !string.Equals(key, "server.DisableRequestBuffering", StringComparison.Ordinal)) || (((_initFlag0 & 0x800) != 0) && !InitPropertyDisableRequestBuffering()))
                    {
                        break;
                    }
                    return true;

                case 0x1f:
                    if (((_flag0 & 0x10000) == 0) || !string.Equals(key, "server.DisableResponseBuffering", StringComparison.Ordinal))
                    {
                        if (((_flag1 & 8) == 0) || !string.Equals(key, Constants.IntegratedPipelineCurrentStage, StringComparison.Ordinal))
                        {
                            break;
                        }
                        return true;
                    }
                    return true;

                case 0x21:
                    if (((_flag1 & 0x10) == 0) || !string.Equals(key, "System.Web.Routing.RequestContext", StringComparison.Ordinal))
                    {
                        break;
                    }
                    return true;

                case 0x24:
                    if (((_flag0 & 0x200000) == 0) || !string.Equals(key, "systemweb.DisableResponseCompression", StringComparison.Ordinal))
                    {
                        break;
                    }
                    return true;
            }
            return false;
        }

        private IEnumerable<KeyValuePair<string, object>> PropertiesEnumerable()
        {
            if ((_flag0 & 1) != 0)
            {
                yield return new KeyValuePair<string, object>(OwinConstants.OwinVersion, OwinVersion);
            }
            if ((_flag0 & 2) != 0)
            {
                yield return new KeyValuePair<string, object>("owin.CallCancelled", CallCancelled);
            }
            if ((_flag0 & 4) != 0)
            {
                yield return new KeyValuePair<string, object>(OwinConstants.RequestProtocol, RequestProtocol);
            }
            if ((_flag0 & 8) != 0)
            {
                yield return new KeyValuePair<string, object>(OwinConstants.RequestMethod, RequestMethod);
            }
            if ((_flag0 & 0x10) != 0)
            {
                yield return new KeyValuePair<string, object>("owin.RequestScheme", RequestScheme);
            }
            if ((_flag0 & 0x20) != 0)
            {
                yield return new KeyValuePair<string, object>("owin.RequestPathBase", RequestPathBase);
            }
            if ((_flag0 & 0x40) != 0)
            {
                yield return new KeyValuePair<string, object>(OwinConstants.RequestPath, RequestPath);
            }
            if ((_flag0 & 0x80) != 0)
            {
                yield return new KeyValuePair<string, object>("owin.RequestQueryString", RequestQueryString);
            }
            if ((_flag0 & 0x100) != 0)
            {
                yield return new KeyValuePair<string, object>(OwinConstants.RequestHeaders, RequestHeaders);
            }
            if ((_flag0 & 0x200) != 0)
            {
                yield return new KeyValuePair<string, object>(OwinConstants.RequestBody, RequestBody);
            }
            if ((_flag0 & 0x400) != 0)
            {
                yield return new KeyValuePair<string, object>(OwinConstants.RequestId, RequestId);
            }
            if (((_flag0 & 0x800) != 0) && (((_initFlag0 & 0x800) == 0) || InitPropertyDisableRequestBuffering()))
            {
                yield return new KeyValuePair<string, object>("server.DisableRequestBuffering", DisableRequestBuffering);
            }
            if ((_flag0 & 0x1000) != 0)
            {
                yield return new KeyValuePair<string, object>("owin.ResponseStatusCode", ResponseStatusCode);
            }
            if ((_flag0 & 0x2000) != 0)
            {
                yield return new KeyValuePair<string, object>("owin.ResponseReasonPhrase", ResponseReasonPhrase);
            }
            if ((_flag0 & 0x4000) != 0)
            {
                yield return new KeyValuePair<string, object>("owin.ResponseHeaders", ResponseHeaders);
            }
            if ((_flag0 & 0x8000) != 0)
            {
                yield return new KeyValuePair<string, object>("owin.ResponseBody", ResponseBody);
            }
            if ((_flag0 & 0x10000) != 0)
            {
                yield return new KeyValuePair<string, object>("server.DisableResponseBuffering", DisableResponseBuffering);
            }
            if ((_flag0 & 0x20000) != 0)
            {
                yield return new KeyValuePair<string, object>("host.TraceOutput", HostTraceOutput);
            }
            if ((_flag0 & 0x40000) != 0)
            {
                yield return new KeyValuePair<string, object>(OwinConstants.CommonKeys.AppName, HostAppName);
            }
            if (((_flag0 & 0x80000) != 0) && (((_initFlag0 & 0x80000) == 0) || InitPropertyHostAppMode()))
            {
                yield return new KeyValuePair<string, object>(OwinConstants.CommonKeys.AppMode, HostAppMode);
            }
            if ((_flag0 & 0x100000) != 0)
            {
                yield return new KeyValuePair<string, object>("host.OnAppDisposing", OnAppDisposing);
            }
            if ((_flag0 & 0x200000) != 0)
            {
                yield return new KeyValuePair<string, object>("systemweb.DisableResponseCompression", DisableResponseCompression);
            }
            if ((_flag0 & 0x400000) != 0)
            {
                yield return new KeyValuePair<string, object>(OwinConstants.Security.User, ServerUser);
            }
            if ((_flag0 & 0x800000) != 0)
            {
                yield return new KeyValuePair<string, object>("server.OnSendingHeaders", OnSendingHeaders);
            }
            if ((_flag0 & 0x1000000) != 0)
            {
                yield return new KeyValuePair<string, object>("server.Capabilities", ServerCapabilities);
            }
            if ((_flag0 & 0x2000000) != 0)
            {
                yield return new KeyValuePair<string, object>("server.RemoteIpAddress", ServerRemoteIpAddress);
            }
            if ((_flag0 & 0x4000000) != 0)
            {
                yield return new KeyValuePair<string, object>("server.RemotePort", ServerRemotePort);
            }
            if ((_flag0 & 0x8000000) != 0)
            {
                yield return new KeyValuePair<string, object>("server.LocalIpAddress", ServerLocalIpAddress);
            }
            if ((_flag0 & 0x10000000) != 0)
            {
                yield return new KeyValuePair<string, object>("server.LocalPort", ServerLocalPort);
            }
            if ((_flag0 & 0x20000000) != 0)
            {
                yield return new KeyValuePair<string, object>(OwinConstants.CommonKeys.IsLocal, ServerIsLocal);
            }
            if (((_flag0 & 0x40000000) != 0) && (((_initFlag0 & 0x40000000) == 0) || InitPropertyClientCert()))
            {
                yield return new KeyValuePair<string, object>("ssl.ClientCertificate", ClientCert);
            }
            if (((_flag0 & 0x80000000) != 0) && (((_initFlag0 & 0x80000000) == 0) || InitPropertyLoadClientCert()))
            {
                yield return new KeyValuePair<string, object>("ssl.LoadClientCertAsync", LoadClientCert);
            }
            if ((_flag1 & 1) != 0)
            {
                yield return new KeyValuePair<string, object>("sendfile.SendAsync", SendFileAsync);
            }
            if (((_flag1 & 2) != 0) && (((_initFlag1 & 2) == 0) || InitPropertyWebSocketAccept()))
            {
                yield return new KeyValuePair<string, object>("websocket.Accept", WebSocketAccept);
            }
            if ((_flag1 & 4) != 0)
            {
                yield return new KeyValuePair<string, object>(Constants.IntegratedPipelineContext, IntegratedPipelineContext);
            }
            if ((_flag1 & 8) != 0)
            {
                yield return new KeyValuePair<string, object>(Constants.IntegratedPipelineCurrentStage, IntegratedPipelineStage);
            }
            if ((_flag1 & 0x10) != 0)
            {
                yield return new KeyValuePair<string, object>("System.Web.Routing.RequestContext", RequestContext);
            }
            if ((_flag1 & 0x20) != 0)
            {
                yield return new KeyValuePair<string, object>("System.Web.HttpContextBase", HttpContextBase);
            }
        }

        private IEnumerable<string> PropertiesKeys()
        {
            if ((_flag0 & 1) != 0)
            {
                yield return OwinConstants.OwinVersion;
            }
            if ((_flag0 & 2) != 0)
            {
                yield return "owin.CallCancelled";
            }
            if ((_flag0 & 4) != 0)
            {
                yield return OwinConstants.RequestProtocol;
            }
            if ((_flag0 & 8) != 0)
            {
                yield return OwinConstants.RequestMethod;
            }
            if ((_flag0 & 0x10) != 0)
            {
                yield return "owin.RequestScheme";
            }
            if ((_flag0 & 0x20) != 0)
            {
                yield return "owin.RequestPathBase";
            }
            if ((_flag0 & 0x40) != 0)
            {
                yield return OwinConstants.RequestPath;
            }
            if ((_flag0 & 0x80) != 0)
            {
                yield return "owin.RequestQueryString";
            }
            if ((_flag0 & 0x100) != 0)
            {
                yield return OwinConstants.RequestHeaders;
            }
            if ((_flag0 & 0x200) != 0)
            {
                yield return OwinConstants.RequestBody;
            }
            if ((_flag0 & 0x400) != 0)
            {
                yield return OwinConstants.RequestId;
            }
            if (((_flag0 & 0x800) != 0) && (((_initFlag0 & 0x800) == 0) || InitPropertyDisableRequestBuffering()))
            {
                yield return "server.DisableRequestBuffering";
            }
            if ((_flag0 & 0x1000) != 0)
            {
                yield return "owin.ResponseStatusCode";
            }
            if ((_flag0 & 0x2000) != 0)
            {
                yield return "owin.ResponseReasonPhrase";
            }
            if ((_flag0 & 0x4000) != 0)
            {
                yield return "owin.ResponseHeaders";
            }
            if ((_flag0 & 0x8000) != 0)
            {
                yield return "owin.ResponseBody";
            }
            if ((_flag0 & 0x10000) != 0)
            {
                yield return "server.DisableResponseBuffering";
            }
            if ((_flag0 & 0x20000) != 0)
            {
                yield return "host.TraceOutput";
            }
            if ((_flag0 & 0x40000) != 0)
            {
                yield return OwinConstants.CommonKeys.AppName;
            }
            if (((_flag0 & 0x80000) != 0) && (((_initFlag0 & 0x80000) == 0) || InitPropertyHostAppMode()))
            {
                yield return OwinConstants.CommonKeys.AppMode;
            }
            if ((_flag0 & 0x100000) != 0)
            {
                yield return "host.OnAppDisposing";
            }
            if ((_flag0 & 0x200000) != 0)
            {
                yield return "systemweb.DisableResponseCompression";
            }
            if ((_flag0 & 0x400000) != 0)
            {
                yield return OwinConstants.Security.User;
            }
            if ((_flag0 & 0x800000) != 0)
            {
                yield return "server.OnSendingHeaders";
            }
            if ((_flag0 & 0x1000000) != 0)
            {
                yield return "server.Capabilities";
            }
            if ((_flag0 & 0x2000000) != 0)
            {
                yield return "server.RemoteIpAddress";
            }
            if ((_flag0 & 0x4000000) != 0)
            {
                yield return "server.RemotePort";
            }
            if ((_flag0 & 0x8000000) != 0)
            {
                yield return "server.LocalIpAddress";
            }
            if ((_flag0 & 0x10000000) != 0)
            {
                yield return "server.LocalPort";
            }
            if ((_flag0 & 0x20000000) != 0)
            {
                yield return OwinConstants.CommonKeys.IsLocal;
            }
            if (((_flag0 & 0x40000000) != 0) && (((_initFlag0 & 0x40000000) == 0) || InitPropertyClientCert()))
            {
                yield return "ssl.ClientCertificate";
            }
            if (((_flag0 & 0x80000000) != 0) && (((_initFlag0 & 0x80000000) == 0) || InitPropertyLoadClientCert()))
            {
                yield return "ssl.LoadClientCertAsync";
            }
            if ((_flag1 & 1) != 0)
            {
                yield return "sendfile.SendAsync";
            }
            if (((_flag1 & 2) != 0) && (((_initFlag1 & 2) == 0) || InitPropertyWebSocketAccept()))
            {
                yield return "websocket.Accept";
            }
            if ((_flag1 & 4) != 0)
            {
                yield return Constants.IntegratedPipelineContext;
            }
            if ((_flag1 & 8) != 0)
            {
                yield return Constants.IntegratedPipelineCurrentStage;
            }
            if ((_flag1 & 0x10) != 0)
            {
                yield return "System.Web.Routing.RequestContext";
            }
            if ((_flag1 & 0x20) != 0)
            {
                yield return "System.Web.HttpContextBase";
            }
        }

        private bool PropertiesTryGetValue(string key, out object value)
        {
            switch (key.Length)
            {
                case 11:
                    if (((_flag0 & 0x400000) == 0) || !string.Equals(key, OwinConstants.Security.User, StringComparison.Ordinal))
                    {
                        break;
                    }
                    value = ServerUser;
                    return true;

                case 12:
                    if (((_flag0 & 1) == 0) || !string.Equals(key, OwinConstants.OwinVersion, StringComparison.Ordinal))
                    {
                        if (((_flag0 & 0x40000) != 0) && string.Equals(key, OwinConstants.CommonKeys.AppName, StringComparison.Ordinal))
                        {
                            value = HostAppName;
                            return true;
                        }
                        if (((_flag0 & 0x80000) == 0) || !string.Equals(key, OwinConstants.CommonKeys.AppMode, StringComparison.Ordinal))
                        {
                            break;
                        }
                        value = HostAppMode;
                        if ((_flag0 & 0x80000) == 0)
                        {
                            value = null;
                            return false;
                        }
                        return true;
                    }
                    value = OwinVersion;
                    return true;

                case 14:
                    if (((_flag0 & 0x400) == 0) || !string.Equals(key, OwinConstants.RequestId, StringComparison.Ordinal))
                    {
                        if (((_flag0 & 0x20000000) == 0) || !string.Equals(key, OwinConstants.CommonKeys.IsLocal, StringComparison.Ordinal))
                        {
                            break;
                        }
                        value = ServerIsLocal;
                        return true;
                    }
                    value = RequestId;
                    return true;

                case 0x10:
                    if (((_flag0 & 0x40) == 0) || !string.Equals(key, OwinConstants.RequestPath, StringComparison.Ordinal))
                    {
                        if (((_flag0 & 0x200) != 0) && string.Equals(key, OwinConstants.RequestBody, StringComparison.Ordinal))
                        {
                            value = RequestBody;
                            return true;
                        }
                        if (((_flag0 & 0x20000) != 0) && string.Equals(key, "host.TraceOutput", StringComparison.Ordinal))
                        {
                            value = HostTraceOutput;
                            return true;
                        }
                        if (((_flag0 & 0x10000000) != 0) && string.Equals(key, "server.LocalPort", StringComparison.Ordinal))
                        {
                            value = ServerLocalPort;
                            return true;
                        }
                        if (((_flag1 & 2) == 0) || !string.Equals(key, "websocket.Accept", StringComparison.Ordinal))
                        {
                            break;
                        }
                        value = WebSocketAccept;
                        if ((_flag1 & 2) == 0)
                        {
                            value = null;
                            return false;
                        }
                        return true;
                    }
                    value = RequestPath;
                    return true;

                case 0x11:
                    if (((_flag0 & 0x8000) == 0) || !string.Equals(key, "owin.ResponseBody", StringComparison.Ordinal))
                    {
                        if (((_flag0 & 0x4000000) == 0) || !string.Equals(key, "server.RemotePort", StringComparison.Ordinal))
                        {
                            break;
                        }
                        value = ServerRemotePort;
                        return true;
                    }
                    value = ResponseBody;
                    return true;

                case 0x12:
                    if (((_flag0 & 2) == 0) || !string.Equals(key, "owin.CallCancelled", StringComparison.Ordinal))
                    {
                        if (((_flag0 & 8) != 0) && string.Equals(key, OwinConstants.RequestMethod, StringComparison.Ordinal))
                        {
                            value = RequestMethod;
                            return true;
                        }
                        if (((_flag0 & 0x10) != 0) && string.Equals(key, "owin.RequestScheme", StringComparison.Ordinal))
                        {
                            value = RequestScheme;
                            return true;
                        }
                        if (((_flag1 & 1) == 0) || !string.Equals(key, "sendfile.SendAsync", StringComparison.Ordinal))
                        {
                            break;
                        }
                        value = SendFileAsync;
                        return true;
                    }
                    value = CallCancelled;
                    return true;

                case 0x13:
                    if (((_flag0 & 0x100) == 0) || !string.Equals(key, OwinConstants.RequestHeaders, StringComparison.Ordinal))
                    {
                        if (((_flag0 & 0x100000) != 0) && string.Equals(key, "host.OnAppDisposing", StringComparison.Ordinal))
                        {
                            value = OnAppDisposing;
                            return true;
                        }
                        if (((_flag0 & 0x1000000) == 0) || !string.Equals(key, "server.Capabilities", StringComparison.Ordinal))
                        {
                            break;
                        }
                        value = ServerCapabilities;
                        return true;
                    }
                    value = RequestHeaders;
                    return true;

                case 20:
                    if (((_flag0 & 4) == 0) || !string.Equals(key, OwinConstants.RequestProtocol, StringComparison.Ordinal))
                    {
                        if (((_flag0 & 0x20) != 0) && string.Equals(key, "owin.RequestPathBase", StringComparison.Ordinal))
                        {
                            value = RequestPathBase;
                            return true;
                        }
                        if (((_flag0 & 0x4000) == 0) || !string.Equals(key, "owin.ResponseHeaders", StringComparison.Ordinal))
                        {
                            break;
                        }
                        value = ResponseHeaders;
                        return true;
                    }
                    value = RequestProtocol;
                    return true;

                case 0x15:
                    if (((_flag0 & 0x8000000) == 0) || !string.Equals(key, "server.LocalIpAddress", StringComparison.Ordinal))
                    {
                        if (((_flag0 & 0x40000000) == 0) || !string.Equals(key, "ssl.ClientCertificate", StringComparison.Ordinal))
                        {
                            break;
                        }
                        value = ClientCert;
                        if ((_flag0 & 0x40000000) == 0)
                        {
                            value = null;
                            return false;
                        }
                        return true;
                    }
                    value = ServerLocalIpAddress;
                    return true;

                case 0x16:
                    if (((_flag0 & 0x2000000) == 0) || !string.Equals(key, "server.RemoteIpAddress", StringComparison.Ordinal))
                    {
                        break;
                    }
                    value = ServerRemoteIpAddress;
                    return true;

                case 0x17:
                    if (((_flag0 & 0x80) == 0) || !string.Equals(key, "owin.RequestQueryString", StringComparison.Ordinal))
                    {
                        if (((_flag0 & 0x1000) != 0) && string.Equals(key, "owin.ResponseStatusCode", StringComparison.Ordinal))
                        {
                            value = ResponseStatusCode;
                            return true;
                        }
                        if (((_flag0 & 0x800000) != 0) && string.Equals(key, "server.OnSendingHeaders", StringComparison.Ordinal))
                        {
                            value = OnSendingHeaders;
                            return true;
                        }
                        if (((_flag0 & 0x80000000) == 0) || !string.Equals(key, "ssl.LoadClientCertAsync", StringComparison.Ordinal))
                        {
                            break;
                        }
                        value = LoadClientCert;
                        if ((_flag0 & 0x80000000) == 0)
                        {
                            value = null;
                            return false;
                        }
                        return true;
                    }
                    value = RequestQueryString;
                    return true;

                case 0x19:
                    if (((_flag0 & 0x2000) == 0) || !string.Equals(key, "owin.ResponseReasonPhrase", StringComparison.Ordinal))
                    {
                        break;
                    }
                    value = ResponseReasonPhrase;
                    return true;

                case 0x1a:
                    if (((_flag1 & 4) == 0) || !string.Equals(key, Constants.IntegratedPipelineContext, StringComparison.Ordinal))
                    {
                        if (((_flag1 & 0x20) != 0) && string.Equals(key, "System.Web.HttpContextBase", StringComparison.Ordinal))
                        {
                            value = HttpContextBase;
                            return true;
                        }
                        break;
                    }
                    value = IntegratedPipelineContext;
                    return true;

                case 30:
                    if (((_flag0 & 0x800) == 0) || !string.Equals(key, "server.DisableRequestBuffering", StringComparison.Ordinal))
                    {
                        break;
                    }
                    value = DisableRequestBuffering;
                    if ((_flag0 & 0x800) != 0)
                    {
                        return true;
                    }
                    value = null;
                    return false;

                case 0x1f:
                    if (((_flag0 & 0x10000) == 0) || !string.Equals(key, "server.DisableResponseBuffering", StringComparison.Ordinal))
                    {
                        if (((_flag1 & 8) == 0) || !string.Equals(key, Constants.IntegratedPipelineCurrentStage, StringComparison.Ordinal))
                        {
                            break;
                        }
                        value = IntegratedPipelineStage;
                        return true;
                    }
                    value = DisableResponseBuffering;
                    return true;

                case 0x21:
                    if (((_flag1 & 0x10) == 0) || !string.Equals(key, "System.Web.Routing.RequestContext", StringComparison.Ordinal))
                    {
                        break;
                    }
                    value = RequestContext;
                    return true;

                case 0x24:
                    if (((_flag0 & 0x200000) == 0) || !string.Equals(key, "systemweb.DisableResponseCompression", StringComparison.Ordinal))
                    {
                        break;
                    }
                    value = DisableResponseCompression;
                    return true;
            }
            value = null;
            return false;
        }

        private bool PropertiesTryRemove(string key)
        {
            switch (key.Length)
            {
                case 11:
                    if (((_flag0 & 0x400000) == 0) || !string.Equals(key, OwinConstants.Security.User, StringComparison.Ordinal))
                    {
                        break;
                    }
                    return true;

                case 12:
                    if (((_flag0 & 1) == 0) || !string.Equals(key, OwinConstants.OwinVersion, StringComparison.Ordinal))
                    {
                        if (((_flag0 & 0x40000) != 0) && string.Equals(key, OwinConstants.CommonKeys.AppName, StringComparison.Ordinal))
                        {
                            _flag0 &= 0xfffbffff;
                            _HostAppName = null;
                            return true;
                        }
                        if (((_flag0 & 0x80000) == 0) || !string.Equals(key, OwinConstants.CommonKeys.AppMode, StringComparison.Ordinal))
                        {
                            break;
                        }
                        _initFlag0 &= 0xfff7ffff;
                        _flag0 &= 0xfff7ffff;
                        _HostAppMode = null;
                        return true;
                    }
                    _flag0 &= 0xfffffffe;
                    _OwinVersion = null;
                    return true;

                case 14:
                    if (((_flag0 & 0x400) == 0) || !string.Equals(key, OwinConstants.RequestId, StringComparison.Ordinal))
                    {
                        if (((_flag0 & 0x20000000) == 0) || !string.Equals(key, OwinConstants.CommonKeys.IsLocal, StringComparison.Ordinal))
                        {
                            break;
                        }
                        _initFlag0 &= 0xdfffffff;
                        _flag0 &= 0xdfffffff;
                        _ServerIsLocal = false;
                        return true;
                    }
                    _initFlag0 &= 0xfffffbff;
                    _flag0 &= 0xfffffbff;
                    _RequestId = null;
                    return true;

                case 0x10:
                    if (((_flag0 & 0x40) == 0) || !string.Equals(key, OwinConstants.RequestPath, StringComparison.Ordinal))
                    {
                        if (((_flag0 & 0x200) != 0) && string.Equals(key, OwinConstants.RequestBody, StringComparison.Ordinal))
                        {
                            _initFlag0 &= 0xfffffdff;
                            _flag0 &= 0xfffffdff;
                            _RequestBody = null;
                            return true;
                        }
                        if (((_flag0 & 0x20000) != 0) && string.Equals(key, "host.TraceOutput", StringComparison.Ordinal))
                        {
                            _flag0 &= 0xfffdffff;
                            _HostTraceOutput = null;
                            return true;
                        }
                        if (((_flag0 & 0x10000000) != 0) && string.Equals(key, "server.LocalPort", StringComparison.Ordinal))
                        {
                            _initFlag0 &= 0xefffffff;
                            _flag0 &= 0xefffffff;
                            _ServerLocalPort = null;
                            return true;
                        }
                        if (((_flag1 & 2) == 0) || !string.Equals(key, "websocket.Accept", StringComparison.Ordinal))
                        {
                            break;
                        }
                        _initFlag1 &= 0xfffffffd;
                        _flag1 &= 0xfffffffd;
                        _WebSocketAccept = null;
                        return true;
                    }
                    _flag0 &= 0xffffffbf;
                    _RequestPath = null;
                    return true;

                case 0x11:
                    if (((_flag0 & 0x8000) == 0) || !string.Equals(key, "owin.ResponseBody", StringComparison.Ordinal))
                    {
                        if (((_flag0 & 0x4000000) == 0) || !string.Equals(key, "server.RemotePort", StringComparison.Ordinal))
                        {
                            break;
                        }
                        _initFlag0 &= 0xfbffffff;
                        _flag0 &= 0xfbffffff;
                        _ServerRemotePort = null;
                        return true;
                    }
                    _initFlag0 &= 0xffff7fff;
                    _flag0 &= 0xffff7fff;
                    _ResponseBody = null;
                    return true;

                case 0x12:
                    if (((_flag0 & 2) == 0) || !string.Equals(key, "owin.CallCancelled", StringComparison.Ordinal))
                    {
                        if (((_flag0 & 8) != 0) && string.Equals(key, OwinConstants.RequestMethod, StringComparison.Ordinal))
                        {
                            _flag0 &= 0xfffffff7;
                            _RequestMethod = null;
                            return true;
                        }
                        if (((_flag0 & 0x10) != 0) && string.Equals(key, "owin.RequestScheme", StringComparison.Ordinal))
                        {
                            _initFlag0 &= 0xffffffef;
                            _flag0 &= 0xffffffef;
                            _RequestScheme = null;
                            return true;
                        }
                        if (((_flag1 & 1) == 0) || !string.Equals(key, "sendfile.SendAsync", StringComparison.Ordinal))
                        {
                            break;
                        }
                        _initFlag1 &= 0xfffffffe;
                        _flag1 &= 0xfffffffe;
                        _SendFileAsync = null;
                        return true;
                    }
                    _initFlag0 &= 0xfffffffd;
                    _flag0 &= 0xfffffffd;
                    _CallCancelled = new CancellationToken();
                    return true;

                case 0x13:
                    if (((_flag0 & 0x100) == 0) || !string.Equals(key, OwinConstants.RequestHeaders, StringComparison.Ordinal))
                    {
                        if (((_flag0 & 0x100000) != 0) && string.Equals(key, "host.OnAppDisposing", StringComparison.Ordinal))
                        {
                            _initFlag0 &= 0xffefffff;
                            _flag0 &= 0xffefffff;
                            _OnAppDisposing = new CancellationToken();
                            return true;
                        }
                        if (((_flag0 & 0x1000000) == 0) || !string.Equals(key, "server.Capabilities", StringComparison.Ordinal))
                        {
                            break;
                        }
                        _flag0 &= 0xfeffffff;
                        _ServerCapabilities = null;
                        return true;
                    }
                    _flag0 &= 0xfffffeff;
                    _RequestHeaders = null;
                    return true;

                case 20:
                    if (((_flag0 & 4) == 0) || !string.Equals(key, OwinConstants.RequestProtocol, StringComparison.Ordinal))
                    {
                        if (((_flag0 & 0x20) != 0) && string.Equals(key, "owin.RequestPathBase", StringComparison.Ordinal))
                        {
                            _flag0 &= 0xffffffdf;
                            _RequestPathBase = null;
                            return true;
                        }
                        if (((_flag0 & 0x4000) == 0) || !string.Equals(key, "owin.ResponseHeaders", StringComparison.Ordinal))
                        {
                            break;
                        }
                        _flag0 &= 0xffffbfff;
                        _ResponseHeaders = null;
                        return true;
                    }
                    _initFlag0 &= 0xfffffffb;
                    _flag0 &= 0xfffffffb;
                    _RequestProtocol = null;
                    return true;

                case 0x15:
                    if (((_flag0 & 0x8000000) == 0) || !string.Equals(key, "server.LocalIpAddress", StringComparison.Ordinal))
                    {
                        if (((_flag0 & 0x40000000) == 0) || !string.Equals(key, "ssl.ClientCertificate", StringComparison.Ordinal))
                        {
                            break;
                        }
                        _initFlag0 &= 0xbfffffff;
                        _flag0 &= 0xbfffffff;
                        _ClientCert = null;
                        return true;
                    }
                    _initFlag0 &= 0xf7ffffff;
                    _flag0 &= 0xf7ffffff;
                    _ServerLocalIpAddress = null;
                    return true;

                case 0x16:
                    if (((_flag0 & 0x2000000) == 0) || !string.Equals(key, "server.RemoteIpAddress", StringComparison.Ordinal))
                    {
                        break;
                    }
                    _initFlag0 &= 0xfdffffff;
                    _flag0 &= 0xfdffffff;
                    _ServerRemoteIpAddress = null;
                    return true;

                case 0x17:
                    if (((_flag0 & 0x80) == 0) || !string.Equals(key, "owin.RequestQueryString", StringComparison.Ordinal))
                    {
                        if (((_flag0 & 0x1000) == 0) || !string.Equals(key, "owin.ResponseStatusCode", StringComparison.Ordinal))
                        {
                            if (((_flag0 & 0x800000) != 0) && string.Equals(key, "server.OnSendingHeaders", StringComparison.Ordinal))
                            {
                                _flag0 &= 0xff7fffff;
                                _OnSendingHeaders = null;
                                return true;
                            }
                            if (((_flag0 & 0x80000000) == 0) || !string.Equals(key, "ssl.LoadClientCertAsync", StringComparison.Ordinal))
                            {
                                break;
                            }
                            _initFlag0 &= 0x7fffffff;
                            _flag0 &= 0x7fffffff;
                            _LoadClientCert = null;
                        }
                        return true;
                    }
                    _initFlag0 &= 0xffffff7f;
                    _flag0 &= 0xffffff7f;
                    _RequestQueryString = null;
                    return true;

                case 0x19:
                    if (((_flag0 & 0x2000) == 0) || !string.Equals(key, "owin.ResponseReasonPhrase", StringComparison.Ordinal))
                    {
                        break;
                    }
                    return true;

                case 0x1a:
                    if (((_flag1 & 4) == 0) || !string.Equals(key, Constants.IntegratedPipelineContext, StringComparison.Ordinal))
                    {
                        if (((_flag1 & 0x20) != 0) && string.Equals(key, "System.Web.HttpContextBase", StringComparison.Ordinal))
                        {
                            _flag1 &= 0xffffffdf;
                            _HttpContextBase = null;
                            return true;
                        }
                        break;
                    }
                    _flag1 &= 0xfffffffb;
                    _IntegratedPipelineContext = null;
                    return true;

                case 30:
                    if (((_flag0 & 0x800) == 0) || !string.Equals(key, "server.DisableRequestBuffering", StringComparison.Ordinal))
                    {
                        break;
                    }
                    _initFlag0 &= 0xfffff7ff;
                    _flag0 &= 0xfffff7ff;
                    _DisableRequestBuffering = null;
                    return true;

                case 0x1f:
                    if (((_flag0 & 0x10000) == 0) || !string.Equals(key, "server.DisableResponseBuffering", StringComparison.Ordinal))
                    {
                        if (((_flag1 & 8) == 0) || !string.Equals(key, Constants.IntegratedPipelineCurrentStage, StringComparison.Ordinal))
                        {
                            break;
                        }
                        _flag1 &= 0xfffffff7;
                        _IntegratedPipelineStage = null;
                        return true;
                    }
                    _initFlag0 &= 0xfffeffff;
                    _flag0 &= 0xfffeffff;
                    _DisableResponseBuffering = null;
                    return true;

                case 0x21:
                    if (((_flag1 & 0x10) == 0) || !string.Equals(key, "System.Web.Routing.RequestContext", StringComparison.Ordinal))
                    {
                        break;
                    }
                    _flag1 &= 0xffffffef;
                    _RequestContext = null;
                    return true;

                case 0x24:
                    if (((_flag0 & 0x200000) == 0) || !string.Equals(key, "systemweb.DisableResponseCompression", StringComparison.Ordinal))
                    {
                        break;
                    }
                    _flag0 &= 0xffdfffff;
                    _DisableResponseCompression = null;
                    return true;
            }
            return false;
        }

        private bool PropertiesTrySetValue(string key, object value)
        {
            switch (key.Length)
            {
                case 11:
                    if (!string.Equals(key, OwinConstants.Security.User, StringComparison.Ordinal))
                    {
                        break;
                    }
                    ServerUser = (IPrincipal)value;
                    return true;

                case 12:
                    if (!string.Equals(key, OwinConstants.OwinVersion, StringComparison.Ordinal))
                    {
                        if (string.Equals(key, OwinConstants.CommonKeys.AppName, StringComparison.Ordinal))
                        {
                            HostAppName = (string)value;
                            return true;
                        }
                        if (!string.Equals(key, OwinConstants.CommonKeys.AppMode, StringComparison.Ordinal))
                        {
                            break;
                        }
                        HostAppMode = (string)value;
                        return true;
                    }
                    OwinVersion = (string)value;
                    return true;

                case 14:
                    if (!string.Equals(key, OwinConstants.RequestId, StringComparison.Ordinal))
                    {
                        if (!string.Equals(key, OwinConstants.CommonKeys.IsLocal, StringComparison.Ordinal))
                        {
                            break;
                        }
                        ServerIsLocal = (bool)value;
                        return true;
                    }
                    RequestId = (string)value;
                    return true;

                case 0x10:
                    if (!string.Equals(key, OwinConstants.RequestPath, StringComparison.Ordinal))
                    {
                        if (string.Equals(key, OwinConstants.RequestBody, StringComparison.Ordinal))
                        {
                            RequestBody = (Stream)value;
                            return true;
                        }
                        if (string.Equals(key, "host.TraceOutput", StringComparison.Ordinal))
                        {
                            HostTraceOutput = (TextWriter)value;
                            return true;
                        }
                        if (string.Equals(key, "server.LocalPort", StringComparison.Ordinal))
                        {
                            ServerLocalPort = (string)value;
                            return true;
                        }
                        if (!string.Equals(key, "websocket.Accept", StringComparison.Ordinal))
                        {
                            break;
                        }
                        WebSocketAccept = (Action<IDictionary<string, object>, Func<IDictionary<string, object>, Task>>)value;
                        return true;
                    }
                    RequestPath = (string)value;
                    return true;

                case 0x11:
                    if (!string.Equals(key, "owin.ResponseBody", StringComparison.Ordinal))
                    {
                        if (!string.Equals(key, "server.RemotePort", StringComparison.Ordinal))
                        {
                            break;
                        }
                        ServerRemotePort = (string)value;
                        return true;
                    }
                    ResponseBody = (Stream)value;
                    return true;

                case 0x12:
                    if (!string.Equals(key, "owin.CallCancelled", StringComparison.Ordinal))
                    {
                        if (string.Equals(key, OwinConstants.RequestMethod, StringComparison.Ordinal))
                        {
                            RequestMethod = (string)value;
                            return true;
                        }
                        if (string.Equals(key, "owin.RequestScheme", StringComparison.Ordinal))
                        {
                            RequestScheme = (string)value;
                            return true;
                        }
                        if (!string.Equals(key, "sendfile.SendAsync", StringComparison.Ordinal))
                        {
                            break;
                        }
                        SendFileAsync = (Func<string, long, long?, CancellationToken, Task>)value;
                        return true;
                    }
                    CallCancelled = (CancellationToken)value;
                    return true;

                case 0x13:
                    if (!string.Equals(key, OwinConstants.RequestHeaders, StringComparison.Ordinal))
                    {
                        if (string.Equals(key, "host.OnAppDisposing", StringComparison.Ordinal))
                        {
                            OnAppDisposing = (CancellationToken)value;
                            return true;
                        }
                        if (!string.Equals(key, "server.Capabilities", StringComparison.Ordinal))
                        {
                            break;
                        }
                        ServerCapabilities = (IDictionary<string, object>)value;
                        return true;
                    }
                    RequestHeaders = (IDictionary<string, string[]>)value;
                    return true;

                case 20:
                    if (!string.Equals(key, OwinConstants.RequestProtocol, StringComparison.Ordinal))
                    {
                        if (string.Equals(key, "owin.RequestPathBase", StringComparison.Ordinal))
                        {
                            RequestPathBase = (string)value;
                            return true;
                        }
                        if (!string.Equals(key, "owin.ResponseHeaders", StringComparison.Ordinal))
                        {
                            break;
                        }
                        ResponseHeaders = (IDictionary<string, string[]>)value;
                        return true;
                    }
                    RequestProtocol = (string)value;
                    return true;

                case 0x15:
                    if (!string.Equals(key, "server.LocalIpAddress", StringComparison.Ordinal))
                    {
                        if (!string.Equals(key, "ssl.ClientCertificate", StringComparison.Ordinal))
                        {
                            break;
                        }
                        ClientCert = (X509Certificate)value;
                        return true;
                    }
                    ServerLocalIpAddress = (string)value;
                    return true;

                case 0x16:
                    if (!string.Equals(key, "server.RemoteIpAddress", StringComparison.Ordinal))
                    {
                        break;
                    }
                    ServerRemoteIpAddress = (string)value;
                    return true;

                case 0x17:
                    if (!string.Equals(key, "owin.RequestQueryString", StringComparison.Ordinal))
                    {
                        if (string.Equals(key, "owin.ResponseStatusCode", StringComparison.Ordinal))
                        {
                            ResponseStatusCode = (int)value;
                            return true;
                        }
                        if (string.Equals(key, "server.OnSendingHeaders", StringComparison.Ordinal))
                        {
                            OnSendingHeaders = (Action<Action<object>, object>)value;
                            return true;
                        }
                        if (!string.Equals(key, "ssl.LoadClientCertAsync", StringComparison.Ordinal))
                        {
                            break;
                        }
                        LoadClientCert = (Func<Task>)value;
                        return true;
                    }
                    RequestQueryString = (string)value;
                    return true;

                case 0x19:
                    if (!string.Equals(key, "owin.ResponseReasonPhrase", StringComparison.Ordinal))
                    {
                        break;
                    }
                    ResponseReasonPhrase = (string)value;
                    return true;

                case 0x1a:
                    if (!string.Equals(key, Constants.IntegratedPipelineContext, StringComparison.Ordinal))
                    {
                        if (string.Equals(key, "System.Web.HttpContextBase", StringComparison.Ordinal))
                        {
                            HttpContextBase = (HttpContextBase)value;
                            return true;
                        }
                        break;
                    }
                    IntegratedPipelineContext = (IntegratedPipelineContext)value;
                    return true;

                case 30:
                    if (!string.Equals(key, "server.DisableRequestBuffering", StringComparison.Ordinal))
                    {
                        break;
                    }
                    DisableRequestBuffering = (Action)value;
                    return true;

                case 0x1f:
                    if (!string.Equals(key, "server.DisableResponseBuffering", StringComparison.Ordinal))
                    {
                        if (!string.Equals(key, Constants.IntegratedPipelineCurrentStage, StringComparison.Ordinal))
                        {
                            break;
                        }
                        IntegratedPipelineStage = (string)value;
                        return true;
                    }
                    DisableResponseBuffering = (Action)value;
                    return true;

                case 0x21:
                    if (!string.Equals(key, "System.Web.Routing.RequestContext", StringComparison.Ordinal))
                    {
                        break;
                    }
                    RequestContext = (RequestContext)value;
                    return true;

                case 0x24:
                    if (!string.Equals(key, "systemweb.DisableResponseCompression", StringComparison.Ordinal))
                    {
                        break;
                    }
                    DisableResponseCompression = (Action)value;
                    return true;
            }
            return false;
        }

        private IEnumerable<object> PropertiesValues()
        {
            if ((_flag0 & 1) != 0)
            {
                yield return OwinVersion;
            }
            if ((_flag0 & 2) != 0)
            {
                yield return CallCancelled;
            }
            if ((_flag0 & 4) != 0)
            {
                yield return RequestProtocol;
            }
            if ((_flag0 & 8) != 0)
            {
                yield return RequestMethod;
            }
            if ((_flag0 & 0x10) != 0)
            {
                yield return RequestScheme;
            }
            if ((_flag0 & 0x20) != 0)
            {
                yield return RequestPathBase;
            }
            if ((_flag0 & 0x40) != 0)
            {
                yield return RequestPath;
            }
            if ((_flag0 & 0x80) != 0)
            {
                yield return RequestQueryString;
            }
            if ((_flag0 & 0x100) != 0)
            {
                yield return RequestHeaders;
            }
            if ((_flag0 & 0x200) != 0)
            {
                yield return RequestBody;
            }
            if ((_flag0 & 0x400) != 0)
            {
                yield return RequestId;
            }
            if (((_flag0 & 0x800) != 0) && (((_initFlag0 & 0x800) == 0) || InitPropertyDisableRequestBuffering()))
            {
                yield return DisableRequestBuffering;
            }
            if ((_flag0 & 0x1000) != 0)
            {
                yield return ResponseStatusCode;
            }
            if ((_flag0 & 0x2000) != 0)
            {
                yield return ResponseReasonPhrase;
            }
            if ((_flag0 & 0x4000) != 0)
            {
                yield return ResponseHeaders;
            }
            if ((_flag0 & 0x8000) != 0)
            {
                yield return ResponseBody;
            }
            if ((_flag0 & 0x10000) != 0)
            {
                yield return DisableResponseBuffering;
            }
            if ((_flag0 & 0x20000) != 0)
            {
                yield return HostTraceOutput;
            }
            if ((_flag0 & 0x40000) != 0)
            {
                yield return HostAppName;
            }
            if (((_flag0 & 0x80000) != 0) && (((_initFlag0 & 0x80000) == 0) || InitPropertyHostAppMode()))
            {
                yield return HostAppMode;
            }
            if ((_flag0 & 0x100000) != 0)
            {
                yield return OnAppDisposing;
            }
            if ((_flag0 & 0x200000) != 0)
            {
                yield return DisableResponseCompression;
            }
            if ((_flag0 & 0x400000) != 0)
            {
                yield return ServerUser;
            }
            if ((_flag0 & 0x800000) != 0)
            {
                yield return OnSendingHeaders;
            }
            if ((_flag0 & 0x1000000) != 0)
            {
                yield return ServerCapabilities;
            }
            if ((_flag0 & 0x2000000) != 0)
            {
                yield return ServerRemoteIpAddress;
            }
            if ((_flag0 & 0x4000000) != 0)
            {
                yield return ServerRemotePort;
            }
            if ((_flag0 & 0x8000000) != 0)
            {
                yield return ServerLocalIpAddress;
            }
            if ((_flag0 & 0x10000000) != 0)
            {
                yield return ServerLocalPort;
            }
            if ((_flag0 & 0x20000000) != 0)
            {
                yield return ServerIsLocal;
            }
            if (((_flag0 & 0x40000000) != 0) && (((_initFlag0 & 0x40000000) == 0) || InitPropertyClientCert()))
            {
                yield return ClientCert;
            }
            if (((_flag0 & 0x80000000) != 0) && (((_initFlag0 & 0x80000000) == 0) || InitPropertyLoadClientCert()))
            {
                yield return LoadClientCert;
            }
            if ((_flag1 & 1) != 0)
            {
                yield return SendFileAsync;
            }
            if (((_flag1 & 2) != 0) && (((_initFlag1 & 2) == 0) || InitPropertyWebSocketAccept()))
            {
                yield return WebSocketAccept;
            }
            if ((_flag1 & 4) != 0)
            {
                yield return IntegratedPipelineContext;
            }
            if ((_flag1 & 8) != 0)
            {
                yield return IntegratedPipelineStage;
            }
            if ((_flag1 & 0x10) != 0)
            {
                yield return RequestContext;
            }
            if ((_flag1 & 0x20) != 0)
            {
                yield return HttpContextBase;
            }
        }

        void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
        {
            ((IDictionary<string, object>)this).Add(item.Key, item.Value);
        }

        void ICollection<KeyValuePair<string, object>>.Clear()
        {
            foreach (string str in PropertiesKeys())
            {
                PropertiesTryRemove(str);
            }
            Extra.Clear();
        }

        bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
        {
            object obj2;
            return (TryGetValue(item.Key, out obj2) && Equals(obj2, item.Value));
        }

        void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            PropertiesEnumerable().Concat(Extra).ToArray().CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
        {
            return (((ICollection<KeyValuePair<string, object>>)this).Contains(item) && ((IDictionary<string, object>)this).Remove(item.Key));
        }

        void IDictionary<string, object>.Add(string key, object value)
        {
            if (!PropertiesTrySetValue(key, value))
            {
                StrongExtra.Add(key, value);
            }
        }

        bool IDictionary<string, object>.ContainsKey(string key)
        {
            if (!PropertiesContainsKey(key))
            {
                return Extra.ContainsKey(key);
            }
            return true;
        }

        bool IDictionary<string, object>.Remove(string key)
        {
            if (!PropertiesTryRemove(key))
            {
                return Extra.Remove(key);
            }
            return true;
        }

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return PropertiesEnumerable().Concat(Extra).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, object>>)this).GetEnumerator();
        }

        public bool TryGetValue(string key, out object value)
        {
            if (!PropertiesTryGetValue(key, out value))
            {
                return Extra.TryGetValue(key, out value);
            }
            return true;
        }

        internal CancellationToken CallCancelled
        {
            get
            {
                if ((_initFlag0 & 2) != 0)
                {
                    _CallCancelled = _propertySource.GetCallCancelled();
                    _initFlag0 &= 0xfffffffd;
                }
                return _CallCancelled;
            }
            set
            {
                _initFlag0 &= 0xfffffffd;
                _flag0 |= 2;
                _CallCancelled = value;
            }
        }

        internal X509Certificate ClientCert
        {
            get
            {
                if ((_initFlag0 & 0x40000000) != 0)
                {
                    InitPropertyClientCert();
                }
                return _ClientCert;
            }
            set
            {
                _initFlag0 &= 0xbfffffff;
                _flag0 |= 0x40000000;
                _ClientCert = value;
            }
        }

        internal Action DisableRequestBuffering
        {
            get
            {
                if ((_initFlag0 & 0x800) != 0)
                {
                    InitPropertyDisableRequestBuffering();
                }
                return _DisableRequestBuffering;
            }
            set
            {
                _initFlag0 &= 0xfffff7ff;
                _flag0 |= 0x800;
                _DisableRequestBuffering = value;
            }
        }

        internal Action DisableResponseBuffering
        {
            get
            {
                if ((_initFlag0 & 0x10000) != 0)
                {
                    _DisableResponseBuffering = _propertySource.GetDisableResponseBuffering();
                    _initFlag0 &= 0xfffeffff;
                }
                return _DisableResponseBuffering;
            }
            set
            {
                _initFlag0 &= 0xfffeffff;
                _flag0 |= 0x10000;
                _DisableResponseBuffering = value;
            }
        }

        internal Action DisableResponseCompression
        {
            get
            {
                return _DisableResponseCompression;
            }
            set
            {
                _flag0 |= 0x200000;
                _DisableResponseCompression = value;
            }
        }

        internal IDictionary<string, object> Extra
        {
            get
            {
                return _extra;
            }
        }

        internal string HostAppMode
        {
            get
            {
                if ((_initFlag0 & 0x80000) != 0)
                {
                    InitPropertyHostAppMode();
                }
                return _HostAppMode;
            }
            set
            {
                _initFlag0 &= 0xfff7ffff;
                _flag0 |= 0x80000;
                _HostAppMode = value;
            }
        }

        internal string HostAppName
        {
            get
            {
                return _HostAppName;
            }
            set
            {
                _flag0 |= 0x40000;
                _HostAppName = value;
            }
        }

        internal TextWriter HostTraceOutput
        {
            get
            {
                return _HostTraceOutput;
            }
            set
            {
                _flag0 |= 0x20000;
                _HostTraceOutput = value;
            }
        }

        internal HttpContextBase HttpContextBase
        {
            get
            {
                return _HttpContextBase;
            }
            set
            {
                _flag1 |= 0x20;
                _HttpContextBase = value;
            }
        }

        internal IntegratedPipelineContext IntegratedPipelineContext
        {
            get
            {
                return _IntegratedPipelineContext;
            }
            set
            {
                _flag1 |= 4;
                _IntegratedPipelineContext = value;
            }
        }

        internal string IntegratedPipelineStage
        {
            get
            {
                return _IntegratedPipelineStage;
            }
            set
            {
                _flag1 |= 8;
                _IntegratedPipelineStage = value;
            }
        }

        internal Func<Task> LoadClientCert
        {
            get
            {
                if ((_initFlag0 & 0x80000000) != 0)
                {
                    InitPropertyLoadClientCert();
                }
                return _LoadClientCert;
            }
            set
            {
                _initFlag0 &= 0x7fffffff;
                _flag0 |= 0x80000000;
                _LoadClientCert = value;
            }
        }

        internal CancellationToken OnAppDisposing
        {
            get
            {
                if ((_initFlag0 & 0x100000) != 0)
                {
                    _OnAppDisposing = _propertySource.GetOnAppDisposing();
                    _initFlag0 &= 0xffefffff;
                }
                return _OnAppDisposing;
            }
            set
            {
                _initFlag0 &= 0xffefffff;
                _flag0 |= 0x100000;
                _OnAppDisposing = value;
            }
        }

        internal Action<Action<object>, object> OnSendingHeaders
        {
            get
            {
                return _OnSendingHeaders;
            }
            set
            {
                _flag0 |= 0x800000;
                _OnSendingHeaders = value;
            }
        }

        internal string OwinVersion
        {
            get
            {
                return _OwinVersion;
            }
            set
            {
                _flag0 |= 1;
                _OwinVersion = value;
            }
        }

        internal Stream RequestBody
        {
            get
            {
                if ((_initFlag0 & 0x200) != 0)
                {
                    _RequestBody = _propertySource.GetRequestBody();
                    _initFlag0 &= 0xfffffdff;
                }
                return _RequestBody;
            }
            set
            {
                _initFlag0 &= 0xfffffdff;
                _flag0 |= 0x200;
                _RequestBody = value;
            }
        }

        internal RequestContext RequestContext
        {
            get
            {
                return _RequestContext;
            }
            set
            {
                _flag1 |= 0x10;
                _RequestContext = value;
            }
        }

        internal IDictionary<string, string[]> RequestHeaders
        {
            get
            {
                return _RequestHeaders;
            }
            set
            {
                _flag0 |= 0x100;
                _RequestHeaders = value;
            }
        }

        internal string RequestId
        {
            get
            {
                if ((_initFlag0 & 0x400) != 0)
                {
                    _RequestId = _propertySource.GetRequestId();
                    _initFlag0 &= 0xfffffbff;
                }
                return _RequestId;
            }
            set
            {
                _initFlag0 &= 0xfffffbff;
                _flag0 |= 0x400;
                _RequestId = value;
            }
        }

        internal string RequestMethod
        {
            get
            {
                return _RequestMethod;
            }
            set
            {
                _flag0 |= 8;
                _RequestMethod = value;
            }
        }

        internal string RequestPath
        {
            get
            {
                return _RequestPath;
            }
            set
            {
                _flag0 |= 0x40;
                _RequestPath = value;
            }
        }

        internal string RequestPathBase
        {
            get
            {
                return _RequestPathBase;
            }
            set
            {
                _flag0 |= 0x20;
                _RequestPathBase = value;
            }
        }

        internal string RequestProtocol
        {
            get
            {
                if ((_initFlag0 & 4) != 0)
                {
                    _RequestProtocol = _propertySource.GetRequestProtocol();
                    _initFlag0 &= 0xfffffffb;
                }
                return _RequestProtocol;
            }
            set
            {
                _initFlag0 &= 0xfffffffb;
                _flag0 |= 4;
                _RequestProtocol = value;
            }
        }

        internal string RequestQueryString
        {
            get
            {
                if ((_initFlag0 & 0x80) != 0)
                {
                    _RequestQueryString = _propertySource.GetRequestQueryString();
                    _initFlag0 &= 0xffffff7f;
                }
                return _RequestQueryString;
            }
            set
            {
                _initFlag0 &= 0xffffff7f;
                _flag0 |= 0x80;
                _RequestQueryString = value;
            }
        }

        internal string RequestScheme
        {
            get
            {
                if ((_initFlag0 & 0x10) != 0)
                {
                    _RequestScheme = _propertySource.GetRequestScheme();
                    _initFlag0 &= 0xffffffef;
                }
                return _RequestScheme;
            }
            set
            {
                _initFlag0 &= 0xffffffef;
                _flag0 |= 0x10;
                _RequestScheme = value;
            }
        }

        internal Stream ResponseBody
        {
            get
            {
                if ((_initFlag0 & 0x8000) != 0)
                {
                    _ResponseBody = _propertySource.GetResponseBody();
                    _initFlag0 &= 0xffff7fff;
                }
                return _ResponseBody;
            }
            set
            {
                _initFlag0 &= 0xffff7fff;
                _flag0 |= 0x8000;
                _ResponseBody = value;
            }
        }

        internal IDictionary<string, string[]> ResponseHeaders
        {
            get
            {
                return _ResponseHeaders;
            }
            set
            {
                _flag0 |= 0x4000;
                _ResponseHeaders = value;
            }
        }

        internal string ResponseReasonPhrase
        {
            get
            {
                return _propertySource.GetResponseReasonPhrase();
            }
            set
            {
                _propertySource.SetResponseReasonPhrase(value);
            }
        }

        internal int ResponseStatusCode
        {
            get
            {
                return _propertySource.GetResponseStatusCode();
            }
            set
            {
                _propertySource.SetResponseStatusCode(value);
            }
        }

        internal Func<string, long, long?, CancellationToken, Task> SendFileAsync
        {
            get
            {
                if ((_initFlag1 & 1) != 0)
                {
                    _SendFileAsync = _propertySource.GetSendFileAsync();
                    _initFlag1 &= 0xfffffffe;
                }
                return _SendFileAsync;
            }
            set
            {
                _initFlag1 &= 0xfffffffe;
                _flag1 |= 1;
                _SendFileAsync = value;
            }
        }

        internal IDictionary<string, object> ServerCapabilities
        {
            get
            {
                return _ServerCapabilities;
            }
            set
            {
                _flag0 |= 0x1000000;
                _ServerCapabilities = value;
            }
        }

        internal bool ServerIsLocal
        {
            get
            {
                if ((_initFlag0 & 0x20000000) != 0)
                {
                    _ServerIsLocal = _propertySource.GetServerIsLocal();
                    _initFlag0 &= 0xdfffffff;
                }
                return _ServerIsLocal;
            }
            set
            {
                _initFlag0 &= 0xdfffffff;
                _flag0 |= 0x20000000;
                _ServerIsLocal = value;
            }
        }

        internal string ServerLocalIpAddress
        {
            get
            {
                if ((_initFlag0 & 0x8000000) != 0)
                {
                    _ServerLocalIpAddress = _propertySource.GetServerLocalIpAddress();
                    _initFlag0 &= 0xf7ffffff;
                }
                return _ServerLocalIpAddress;
            }
            set
            {
                _initFlag0 &= 0xf7ffffff;
                _flag0 |= 0x8000000;
                _ServerLocalIpAddress = value;
            }
        }

        internal string ServerLocalPort
        {
            get
            {
                if ((_initFlag0 & 0x10000000) != 0)
                {
                    _ServerLocalPort = _propertySource.GetServerLocalPort();
                    _initFlag0 &= 0xefffffff;
                }
                return _ServerLocalPort;
            }
            set
            {
                _initFlag0 &= 0xefffffff;
                _flag0 |= 0x10000000;
                _ServerLocalPort = value;
            }
        }

        internal string ServerRemoteIpAddress
        {
            get
            {
                if ((_initFlag0 & 0x2000000) != 0)
                {
                    _ServerRemoteIpAddress = _propertySource.GetServerRemoteIpAddress();
                    _initFlag0 &= 0xfdffffff;
                }
                return _ServerRemoteIpAddress;
            }
            set
            {
                _initFlag0 &= 0xfdffffff;
                _flag0 |= 0x2000000;
                _ServerRemoteIpAddress = value;
            }
        }

        internal string ServerRemotePort
        {
            get
            {
                if ((_initFlag0 & 0x4000000) != 0)
                {
                    _ServerRemotePort = _propertySource.GetServerRemotePort();
                    _initFlag0 &= 0xfbffffff;
                }
                return _ServerRemotePort;
            }
            set
            {
                _initFlag0 &= 0xfbffffff;
                _flag0 |= 0x4000000;
                _ServerRemotePort = value;
            }
        }

        internal IPrincipal ServerUser
        {
            get
            {
                return _propertySource.GetServerUser();
            }
            set
            {
                _propertySource.SetServerUser(value);
            }
        }

        private IDictionary<string, object> StrongExtra
        {
            get
            {
                if (_extra == WeakNilEnvironment)
                {
                    Interlocked.CompareExchange(ref _extra, new Dictionary<string, object>(), WeakNilEnvironment);
                }
                return _extra;
            }
        }

        int ICollection<KeyValuePair<string, object>>.Count
        {
            get
            {
                return (PropertiesKeys().Count() + Extra.Count);
            }
        }

        bool ICollection<KeyValuePair<string, object>>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        object IDictionary<string, object>.this[string key]
        {
            get
            {
                object obj2;
                if (!PropertiesTryGetValue(key, out obj2))
                {
                    return Extra[key];
                }
                return obj2;
            }
            set
            {
                if (!PropertiesTrySetValue(key, value))
                {
                    StrongExtra[key] = value;
                }
            }
        }

        ICollection<string> IDictionary<string, object>.Keys
        {
            get
            {
                return PropertiesKeys().Concat(Extra.Keys).ToArray();
            }
        }

        ICollection<object> IDictionary<string, object>.Values
        {
            get
            {
                return PropertiesValues().Concat(Extra.Values).ToArray();
            }
        }

        internal Action<IDictionary<string, object>, Func<IDictionary<string, object>, Task>> WebSocketAccept
        {
            get
            {
                if ((_initFlag1 & 2) != 0)
                {
                    InitPropertyWebSocketAccept();
                }
                return _WebSocketAccept;
            }
            set
            {
                _initFlag1 &= 0xfffffffd;
                _flag1 |= 2;
                _WebSocketAccept = value;
            }
        }
        
        internal interface IPropertySource
{
    CancellationToken GetCallCancelled();
    Action GetDisableResponseBuffering();
    CancellationToken GetOnAppDisposing();
    Stream GetRequestBody();
    string GetRequestId();
    string GetRequestProtocol();
    string GetRequestQueryString();
    string GetRequestScheme();
    Stream GetResponseBody();
    string GetResponseReasonPhrase();
    int GetResponseStatusCode();
    Func<string, long, long?, CancellationToken, Task> GetSendFileAsync();
    bool GetServerIsLocal();
    string GetServerLocalIpAddress();
    string GetServerLocalPort();
    string GetServerRemoteIpAddress();
    string GetServerRemotePort();
    IPrincipal GetServerUser();
    void SetResponseReasonPhrase(string value);
    void SetResponseStatusCode(int value);
    void SetServerUser(IPrincipal value);
    bool TryGetClientCert(ref X509Certificate value);
    bool TryGetDisableRequestBuffering(ref Action value);
    bool TryGetHostAppMode(ref string value);
    bool TryGetLoadClientCert(ref Func<Task> value);
    bool TryGetWebSocketAccept(ref Action<IDictionary<string, object>, Func<IDictionary<string, object>, Task>> value);
}
    }
}
