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

        public WebSocketsController(RequestDelegate next, string path)
        {
            _next = next;
            _path = path;
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

            while (currentSocket.State == WebSocketState.Open)
            {
                var received = await ReceiveStringAsync(currentSocket, cancellationToken);
                Tick(received, currentSocket,cancellationToken);
            }

            await currentSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", cancellationToken);
            currentSocket.Dispose();
        }

        protected abstract void Tick(string recieved, WebSocket socket, CancellationToken cancellationToken);

        private static async Task<string> ReceiveStringAsync(WebSocket socket,
            CancellationToken ct = default(CancellationToken))
        {
            var buffer = new ArraySegment<byte>(new byte[8192]);
            using (var ms = new MemoryStream())
            {
                WebSocketReceiveResult result;
                do
                {
                    ct.ThrowIfCancellationRequested();

                    result = await socket.ReceiveAsync(buffer, ct);
                    ms.Write(buffer.Array, buffer.Offset, result.Count);
                }
                while (!result.EndOfMessage);

                ms.Seek(0, SeekOrigin.Begin);
                if (result.MessageType != WebSocketMessageType.Text)
                {
                    return null;
                }

                using (var reader = new StreamReader(ms, Encoding.UTF8))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }

        protected static Task SendStringAsync(WebSocket socket, string data,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            var segment = new ArraySegment<byte>(buffer);
            return socket.SendAsync(segment, WebSocketMessageType.Text, true, cancellationToken);
        }
    }
}
