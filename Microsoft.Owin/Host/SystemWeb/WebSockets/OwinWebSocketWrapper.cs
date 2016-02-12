using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Host.SystemWeb.Infrastructure;
using Microsoft.Owin.Properties;

namespace Microsoft.Owin.Host.SystemWeb.WebSockets
{
    internal class OwinWebSocketWrapper : IDisposable
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly WebSocketContext _context;

        private const string TraceName = "Microsoft.Owin.Host.SystemWeb.WebSockets.OwinWebSocketWrapper";
        private readonly ITrace _trace = TraceFactory.Create(TraceName);


        internal OwinWebSocketWrapper(WebSocketContext context)
        {
            _context = context;
            _cancellationTokenSource = new CancellationTokenSource();
            Environment = new ConcurrentDictionary<string, object>
            {
                [OwinConstants.WebSocket.SendAsync] = new Func<ArraySegment<byte>, int, bool, CancellationToken, Task>(SendAsync),
                [OwinConstants.WebSocket.ReceiveAsync] =
                    new Func<ArraySegment<byte>, CancellationToken, Task<Tuple<int, bool, int>>>(ReceiveAsync),
                [OwinConstants.WebSocket.CloseAsync] = new Func<int, string, CancellationToken, Task>(CloseAsync),
                [OwinConstants.WebSocket.CallCancelled] = _cancellationTokenSource.Token,
                [OwinConstants.WebSocket.Version] = "1.0",
                [typeof (WebSocketContext).FullName] = _context
            };
        }

        internal void Cancel()
        {
            try
            {
                _cancellationTokenSource.Cancel(false);
            }
            catch (ObjectDisposedException)
            {
            }
            catch (AggregateException exception)
            {
                _trace.WriteError(Resources.Trace_WebSocketException, exception);
            }
        }

        internal Task CloseAsync(int status, string description, CancellationToken cancel)
        {
            return WebSocket.CloseOutputAsync((WebSocketCloseStatus) status, description, cancel);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cancellationTokenSource.Dispose();
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
            throw new ArgumentOutOfRangeException(nameof(webSocketMessageType), webSocketMessageType, string.Empty);
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
            throw new ArgumentOutOfRangeException(nameof(messageType), messageType, string.Empty);
        }

        internal async Task<Tuple<int, bool, int>> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancel)
        {
            var nativeResult = await WebSocket.ReceiveAsync(buffer, cancel);
            if (nativeResult.MessageType != WebSocketMessageType.Close)
                return new Tuple<int, bool, int>(EnumToOpCode(nativeResult.MessageType), nativeResult.EndOfMessage,
                    nativeResult.Count);
            var closeStatus = nativeResult.CloseStatus;
            Environment[OwinConstants.WebSocket.ClientCloseStatus] = closeStatus.HasValue
                ? ((int) closeStatus.GetValueOrDefault())
                : 0x3e8;
            Environment[OwinConstants.WebSocket.ClientCloseDescription] = nativeResult.CloseStatusDescription ??
                                                                           string.Empty;
            return new Tuple<int, bool, int>(EnumToOpCode(nativeResult.MessageType), nativeResult.EndOfMessage,
                nativeResult.Count);
        }

        private Task RedirectSendToCloseAsync(ArraySegment<byte> buffer, CancellationToken cancel)
        {
            if ((buffer.Array == null) || (buffer.Count == 0))
            {
                return CloseAsync(0x3e8, string.Empty, cancel);
            }
            if (buffer.Count < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(buffer));
            }
            var status = (buffer.Array[buffer.Offset] << 8) | buffer.Array[buffer.Offset + 1];
            var description = Encoding.UTF8.GetString(buffer.Array, buffer.Offset + 2, buffer.Count - 2);
            return CloseAsync(status, description, cancel);
        }

        internal Task SendAsync(ArraySegment<byte> buffer, int messageType, bool endOfMessage, CancellationToken cancel)
        {
            if (messageType == 8)
            {
                return RedirectSendToCloseAsync(buffer, cancel);
            }
            if ((messageType != 9) && (messageType != 10))
            {
                return WebSocket.SendAsync(buffer, OpCodeToEnum(messageType), endOfMessage, cancel);
            }
            return Utils.CompletedTask;
        }

        internal IDictionary<string, object> Environment { get; }

        private WebSocket WebSocket => _context.WebSocket;
    }
}
