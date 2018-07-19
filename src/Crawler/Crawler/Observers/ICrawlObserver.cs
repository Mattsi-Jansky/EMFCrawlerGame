using Crawler.Maps;
using Crawler.Models;

namespace Crawler.Observers
{
    public interface ICrawlObserver
    {
        void Update(IMap map);
        Graphic[] Observe(Point location);
        Graphic[][][] Observe();
    }
}