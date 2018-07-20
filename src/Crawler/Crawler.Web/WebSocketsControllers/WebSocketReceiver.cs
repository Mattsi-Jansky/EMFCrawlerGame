using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crawler.Web.WebSocketsControllers
{
    public class WebSocketReceiver : IDisposable
    {
        private Guid _clientId;
        private readonly WebSocket _socket;
        private readonly CancellationToken _cancellationToken;
        private Queue<string> _received;
        private Object _lock = new Object();
        private Thread _thread;
        
        public WebSocketReceiver(Guid clientId, WebSocket socket, CancellationToken cancellationToken)
        {
            this._clientId = clientId;
            this._socket = socket;
            this._cancellationToken = cancellationToken;
            _received = new Queue<string>();
            _thread = new Thread(UpdateRecieved);
            _thread.Start();
        }

        public bool HasString()
        {
            lock (_lock)
            {
                return _received.Any();
            }
        }

        public string Get()
        {
            lock (_lock)
            {
                return _received.Dequeue();
            }
        }

        private async void UpdateRecieved()
        {
            while (_socket.State == WebSocketState.Open)
            {
                var received = await ReceiveStringAsync(_socket, _cancellationToken);
                lock (_lock)
                {
                    _received.Enqueue(received);
                }
            }
        }

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
                while (!result.EndOfMessage && result.Count > 0);

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

        public void Dispose()
        {
            _thread.Join();
        }
    }
}
