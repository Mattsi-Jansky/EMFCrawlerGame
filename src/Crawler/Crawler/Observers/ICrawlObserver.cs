using Crawler.Maps;
using Crawler.Models;

namespace Crawler.Observers
{
    public interface ICrawlObserver
    {
        void Update(IMap map);
        TileGraphics Observe(Point location);
        TileGraphics[][] Observe();
    }
}