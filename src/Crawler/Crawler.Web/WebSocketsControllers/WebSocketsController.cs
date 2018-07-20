using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Crawler.Web.WebSocketsControllers
{
    /// <summary>
    /// Big thanks to Gunnar Peipman who's had work is shamelessly abased here:
    /// https://gunnarpeipman.com/aspnet/aspnet-core-websocket-chat/
    /// </summary>
    public abstract class WebSocketsController
    {
        private readonly RequestDelegate _next;
        private string _path;
        private readonly Dictionary<Guid, WebSocketReceiver> _receivers;

        public WebSocketsController(RequestDelegate next, string path)
        {
            _next = next;
            _path = path;
            _receivers = new Dictionary<Guid, WebSocketReceiver>();
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest 
                || !context.Request.Path.Value.Equals(_path))
            {
                await _next.Invoke(context);
                return;
            }

            CancellationToken cancellationToken = context.RequestAborted;
            WebSocket currentSocket = await context.WebSockets.AcceptWebSocketAsync();
            Guid clientId = Guid.NewGuid();
            Add(clientId);
            _receivers[clientId] = new WebSocketReceiver(clientId, currentSocket, cancellationToken);

            while (currentSocket.State == WebSocketState.Open)
            {
                Tick(clientId, _receivers[clientId], currentSocket, cancellationToken);
            }

            await currentSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", cancellationToken);
            currentSocket.Dispose();
            _receivers[clientId].Dispose();
            _receivers.Remove(clientId);
        }

        protected abstract void Add(Guid clientId);

        protected abstract void Tick(Guid clientId, WebSocketReceiver receiver, WebSocket socket, CancellationToken cancellationToken);

        protected static Task SendStringAsync(WebSocket socket, string data,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            var segment = new ArraySegment<byte>(buffer);
            return socket.SendAsync(segment, WebSocketMessageType.Text, true, cancellationToken);
        }
    }
}
