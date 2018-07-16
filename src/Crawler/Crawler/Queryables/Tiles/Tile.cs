using Crawler.Queryables.Entities;

namespace Crawler.Queryables.Tiles
{
    public class Tile : QueryableAggregator<Entity>
    {
        public TileType Type { get; set; }

        public Tile(TileType type)
        {
            Type = type;
        }
    }
}