using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Host.SystemWeb.Infrastructure;

namespace Microsoft.Owin.Host.SystemWeb.WebSockets
{
    internal class OwinWebSocketWrapper : IDisposable
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly WebSocketContext _context;
        private readonly IDictionary<string, object> _environment;

        private readonly ITrace _trace =
            TraceFactory.Create("Microsoft.Owin.Host.SystemWeb.WebSockets.OwinWebSocketWrapper");

        private const string TraceName = "Microsoft.Owin.Host.SystemWeb.WebSockets.OwinWebSocketWrapper";

        internal OwinWebSocketWrapper(WebSocketContext context)
        {
            this._context = context;
            this._cancellationTokenSource = new CancellationTokenSource();
            this._environment = new ConcurrentDictionary<string, object>();
            this._environment["websocket.SendAsync"] =
                new Func<ArraySegment<byte>, int, bool, CancellationToken, Task>(this.SendAsync);
            this._environment["websocket.ReceiveAsync"] =
                new Func<ArraySegment<byte>, CancellationToken, Task<Tuple<int, bool, int>>>(this.ReceiveAsync);
            this._environment["websocket.CloseAsync"] = new Func<int, string, CancellationToken, Task>(this.CloseAsync);
            this._environment["websocket.CallCancelled"] = this._cancellationTokenSource.Token;
            this._environment["websocket.Version"] = "1.0";
            this._environment[typeof (WebSocketContext).FullName] = this._context;
        }

        internal void Cancel()
        {
            try
            {
                this._cancellationTokenSource.Cancel(false);
            }
            catch (ObjectDisposedException)
            {
            }
            catch (AggregateException exception)
            {
                this._trace.WriteError(Resources.Trace_WebSocketException, exception);
            }
        }

        internal Task CloseAsync(int status, string description, CancellationToken cancel)
        {
            return this.WebSocket.CloseOutputAsync((WebSocketCloseStatus) status, description, cancel);
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._cancellationTokenSource.Dispose();
            }
        }

        private static int EnumToOpCode(WebSocketMessageType webSocketMessageType)
        {
            switch (webSocketMessageType)
            {
                case WebSocketMessageType.Text:
                    return 1;

                case WebSocketMessageType.Binary:
                    return 2;

                case WebSocketMessageType.Close:
                    return 8;
            }
            throw new ArgumentOutOfRangeException("webSocketMessageType", webSocketMessageType, string.Empty);
        }

        private static WebSocketMessageType OpCodeToEnum(int messageType)
        {
            switch (messageType)
            {
                case 1:
                    return WebSocketMessageType.Text;

                case 2:
                    return WebSocketMessageType.Binary;

                case 8:
                    return WebSocketMessageType.Close;
            }
            throw new ArgumentOutOfRangeException("messageType", messageType, string.Empty);
        }

        internal async Task<Tuple<int, bool, int>> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancel)
        {
            WebSocketReceiveResult nativeResult = await this.WebSocket.ReceiveAsync(buffer, cancel);
            if (nativeResult.MessageType == WebSocketMessageType.Close)
            {
                WebSocketCloseStatus? closeStatus = nativeResult.CloseStatus;
                this._environment["websocket.ClientCloseStatus"] = closeStatus.HasValue
                    ? ((int) closeStatus.GetValueOrDefault())
                    : 0x3e8;
                this._environment["websocket.ClientCloseDescription"] = nativeResult.CloseStatusDescription ??
                                                                        string.Empty;
            }
            return new Tuple<int, bool, int>(EnumToOpCode(nativeResult.MessageType), nativeResult.EndOfMessage,
                nativeResult.Count);
        }

        private Task RedirectSendToCloseAsync(ArraySegment<byte> buffer, CancellationToken cancel)
        {
            if ((buffer.Array == null) || (buffer.Count == 0))
            {
                return this.CloseAsync(0x3e8, string.Empty, cancel);
            }
            if (buffer.Count < 2)
            {
                throw new ArgumentOutOfRangeException("buffer");
            }
            int status = (buffer.Array[buffer.Offset] << 8) | buffer.Array[buffer.Offset + 1];
            string description = Encoding.UTF8.GetString(buffer.Array, buffer.Offset + 2, buffer.Count - 2);
            return this.CloseAsync(status, description, cancel);
        }

        internal Task SendAsync(ArraySegment<byte> buffer, int messageType, bool endOfMessage, CancellationToken cancel)
        {
            if (messageType == 8)
            {
                return this.RedirectSendToCloseAsync(buffer, cancel);
            }
            if ((messageType != 9) && (messageType != 10))
            {
                return this.WebSocket.SendAsync(buffer, OpCodeToEnum(messageType), endOfMessage, cancel);
            }
            return Utils.CompletedTask;
        }

        internal IDictionary<string, object> Environment
        {
            get { return this._environment; }
        }

        private WebSocket WebSocket
        {
            get { return this._context.WebSocket; }
        }

    }
}
