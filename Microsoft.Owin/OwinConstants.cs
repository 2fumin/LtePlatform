namespace Microsoft.Owin
{
    internal static class OwinConstants
    {
        public const string CallCancelled = "owin.CallCancelled";
        public const string OwinVersion = "owin.Version";
        public const string RequestBody = "owin.RequestBody";
        public const string RequestHeaders = "owin.RequestHeaders";
        public const string RequestId = "owin.RequestId";
        public const string RequestMethod = "owin.RequestMethod";
        public const string RequestPath = "owin.RequestPath";
        public const string RequestPathBase = "owin.RequestPathBase";
        public const string RequestProtocol = "owin.RequestProtocol";
        public const string RequestQueryString = "owin.RequestQueryString";
        public const string RequestScheme = "owin.RequestScheme";
        public const string ResponseBody = "owin.ResponseBody";
        public const string ResponseHeaders = "owin.ResponseHeaders";
        public const string ResponseProtocol = "owin.ResponseProtocol";
        public const string ResponseReasonPhrase = "owin.ResponseReasonPhrase";
        public const string ResponseStatusCode = "owin.ResponseStatusCode";

        internal static class Builder
        {
            public const string AddSignatureConversion = "builder.AddSignatureConversion";
            public const string DefaultApp = "builder.DefaultApp";
        }

        internal static class CommonKeys
        {
            public const string Addresses = "host.Addresses";
            public const string AppName = "host.AppName";
            public const string AppMode = "host.AppMode";
            public const string Capabilities = "server.Capabilities";
            public const string ClientCertificate = "ssl.ClientCertificate";
            public const string Host = "host";
            public const string IsLocal = "server.IsLocal";
            public const string LocalIpAddress = "server.LocalIpAddress";
            public const string LocalPort = "server.LocalPort";
            public const string OnAppDisposing = "host.OnAppDisposing";
            public const string OnSendingHeaders = "server.OnSendingHeaders";
            public const string Path = "path";
            public const string Port = "port";
            public const string RemoteIpAddress = "server.RemoteIpAddress";
            public const string RemotePort = "server.RemotePort";
            public const string Scheme = "scheme";
            public const string TraceOutput = "host.TraceOutput";
        }

        internal static class OpaqueConstants
        {
            public const string CallCancelled = "opaque.CallCancelled";
            public const string Stream = "opaque.Stream";
            public const string Upgrade = "opaque.Upgrade";
            public const string Version = "opaque.Version";
        }

        internal static class Security
        {
            public const string Authenticate = "security.Authenticate";
            public const string Challenge = "security.Challenge";
            public const string SignIn = "security.SignIn";
            public const string SignOut = "security.SignOut";
            public const string SignOutProperties = "security.SignOutProperties";
            public const string User = "server.User";
            public const string DataProtectionProvider = "security.DataProtectionProvider";
        }

        internal static class SendFiles
        {
            public const string Concurrency = "sendfile.Concurrency";
            public const string SendAsync = "sendfile.SendAsync";
            public const string Support = "sendfile.Support";
            public const string Version = "sendfile.Version";
        }

        internal static class WebSocket
        {
            public const string Accept = "websocket.Accept";
            public const string CallCancelled = "websocket.CallCancelled";
            public const string ClientCloseDescription = "websocket.ClientCloseDescription";
            public const string ClientCloseStatus = "websocket.ClientCloseStatus";
            public const string CloseAsync = "websocket.CloseAsync";
            public const string ReceiveAsync = "websocket.ReceiveAsync";
            public const string SendAsync = "websocket.SendAsync";
            public const string SubProtocol = "websocket.SubProtocol";
            public const string Version = "websocket.Version";
        }
    }
}
