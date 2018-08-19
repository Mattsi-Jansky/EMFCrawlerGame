using System;
using System.Collections.Concurrent;
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
        private readonly ConcurrentDictionary<Guid, Stopwatch> _timers;

        public ObserveGameWebSocketsController(RequestDelegate next) : base(next, "/observe/")
        {
            _timers = new ConcurrentDictionary<Guid, Stopwatch>();
        }

        protected override void Add(Guid clientId)
        {
            var timer = new Stopwatch();
            timer.Start();
            _timers[clientId] = timer;
        }

        protected override void Tick(Guid clientId, WebSocketReceiver receiver, WebSocket socket, CancellationToken cancellationToken)
        {
            var timer = _timers[clientId];
            if (timer.ElapsedMilliseconds >= GameContainer.GameLoopTickTime)
            {
                timer.Reset();
                SendStringAsync(socket, JsonConvert.SerializeObject(GameContainer.Instance.GetGraphicState()), cancellationToken);
                timer.Start();
            }
        }
    }
}
