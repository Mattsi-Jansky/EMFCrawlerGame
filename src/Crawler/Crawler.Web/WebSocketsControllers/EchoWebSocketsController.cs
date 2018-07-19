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

        protected override void Tick(string recieved, WebSocket socket, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(recieved))
            {
                SendStringAsync(socket, recieved, cancellationToken);
            }
        }
    }
}
