using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Threading;
using Crawler.Web.GameContainers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Crawler.Web.WebSocketsControllers
{
    public class ObserveGameWebSocketsController : WebSocketsController
    {
        ThreadLocal<Stopwatch> _localStopwatch = new ThreadLocal<Stopwatch>(() => new Stopwatch());

        public ObserveGameWebSocketsController(RequestDelegate next) : base(next, "/observe/")
        {
            _localStopwatch = new ThreadLocal<Stopwatch>(() => new Stopwatch());
        }

        protected override void Add(Guid clientId)
        {
            _localStopwatch.Value.Start();
        }

        protected override void Tick(Guid clientId, WebSocketReceiver receiver, WebSocket socket, CancellationToken cancellationToken)
        {
            var timer = _localStopwatch.Value;
            if (timer.ElapsedMilliseconds >= GameContainer.GameLoopTickTime)
            {
                timer.Reset();
                SendStringAsync(socket, JsonConvert.SerializeObject(GameContainer.Instance.GetGraphicState()), cancellationToken);
                timer.Start();
            }
        }
    }
}
