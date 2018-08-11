using Crawler.Models;
using Crawler.Queryables.Entities;
using Crawler.Queryables.Entities.Components;
using Crawler.Queryables.Tiles;
using Crawler.Services;

namespace Crawler.Maps
{
    public class Map : IMap
    {
        public Point Size { get; }
        private Tile[][] tiles;

        public Map(Point size)
        {
            Size = size;
            tiles = new Tile[Size.X][];
            for (int x = 0; x < Size.X; x++)
            {
                tiles[x] = new Tile[Size.Y];
                for(int y = 0; y < Size.Y; y++)
                {
                    tiles[x][y] = TileFactory.Floor();
                }
            }
        }

        public void Add(Entity entity, Point point)
        {
            tiles[point.X][point.Y].Add(entity);
        }

        public void Remove(Entity entity, Point point)
        {
            tiles[point.X][point.Y].Remove(entity);
        }

        public Tile Get(Point point)
        {
            return tiles[point.X][point.Y];
        }

        public void Set(Point point, Tile tile)
        {
            tiles[point.X][point.Y] = tile;
        }
    }
}
