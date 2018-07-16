using Crawler.Queryables.Entities;
using Crawler.Queryables.Tiles;

namespace Crawler.Maps
{
    public interface IMap
    {
        Point Size { get; }
        void Add(Entity entity, Point point);
        void Remove(Entity entity, Point point);
        Tile Get(Point point);
    }
}