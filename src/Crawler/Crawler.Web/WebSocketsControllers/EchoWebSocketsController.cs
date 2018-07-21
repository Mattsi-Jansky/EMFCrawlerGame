using System;
using System.Net.WebSockets;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace Crawler.Web.WebSocketsControllers
{
    public class EchoWebSocketsController : WebSocketsController
    {
        public EchoWebSocketsController(RequestDelegate next) : base(next, "/echo/")
        {
        }

        protected override void Add(Guid clientId)
        {

        }

        protected override void Tick(Guid clientId, WebSocketReceiver receiver, WebSocket socket, CancellationToken cancellationToken)
        {
            if (receiver.HasString())
            {
                SendStringAsync(socket, receiver.Get(), cancellationToken);
            }
        }
    }
}
