using System;
using System.Collections.Generic;

namespace Crawler.Web.Services
{
    public class ClientTrackingService
    {
        private const int MaxClients = 16;
        private readonly CrawlGame _game;
        private readonly Dictionary<Guid, DateTime> _clients;
        private readonly Object _clientsLock = new Object();
        private double timeoutInSeconds = 60;

        public ClientTrackingService(CrawlGame game)
        {
            this._game = game;
            _clients = new Dictionary<Guid, DateTime>();
        }

        public void UpdateTimeout(Guid id)
        {
            lock (_clientsLock)
            {
                _clients[id] = DateTime.UtcNow;
            }
        }

        public void RemoveOldClients()
        {
            lock (_clientsLock)
            {
                var clientKeysToRemove = new List<Guid>();
                foreach (var client in _clients)
                {
                    if (DateTime.UtcNow.Subtract(client.Value).TotalSeconds >= timeoutInSeconds)
                    {
                        _game.PlayerCharactersService.Delete(client.Key);
                        clientKeysToRemove.Add(client.Key);
                    }
                }

                foreach (var key in clientKeysToRemove)
                {
                    _clients.Remove(key);
                }
            }
        }

        public bool Contains(Guid id)
        {
            lock (_clientsLock)
            {
                return _clients.ContainsKey(id);
            }
        }

        public bool HasReachedMaxClients()
        {
            lock (_clientsLock)
            {
                return _clients.Count >= MaxClients;
            }
        }
    }
}