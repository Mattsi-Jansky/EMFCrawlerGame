using Crawler.Queryables.Entities;

namespace Crawler.Queryables.Tiles
{
    public class Tile : QueryableAggregator<Entity>
    {
        public Tile(Entity entity)
        {
            Add(entity);
        }
    }
}