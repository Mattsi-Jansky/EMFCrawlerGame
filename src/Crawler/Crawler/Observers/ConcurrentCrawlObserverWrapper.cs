using System;
using Crawler.Maps;
using Crawler.Models;

namespace Crawler.Observers
{
    public class ConcurrentCrawlObserverWrapper : ICrawlObserver
    {
        private CrawlObserver _observer;
        private Object _lock = new Object();

        public ConcurrentCrawlObserverWrapper()
        {
            this._observer = new CrawlObserver();
        }

        public void Update(IMap map)
        {
            lock (_lock)
            {
                _observer.Update(map);
            }
        }

        public TileGraphics Observe(Point location)
        {
            lock (_lock)
            {
                return _observer.Observe(location);
            }
        }

        public TileGraphics[][] Observe()
        {
            lock (_lock)
            {
                return _observer.Observe();
            }
        }
    }
}
