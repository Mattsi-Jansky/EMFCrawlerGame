using Crawler.Queryables.Tiles;

namespace Crawler.Maps.Initialisers
{
    public class WalledBlankMapInitialiser : BaseMapInitialiser
    {
        private Point _size;

        public WalledBlankMapInitialiser(Point size)
        {
            _size = size;
        }

        public override IMap Initialise()
        {
            Map map = new Map(_size);

            for (int x = 0; x < _size.X; x++)
            {
                map.Get(new Point(x, 0)).Type = TileType.Wall;
                map.Get(new Point(x, _size.Y-1)).Type = TileType.Wall;
            }

            for (int y = 0; y < _size.Y; y++)
            {
                map.Get(new Point(0, y)).Type = TileType.Wall;
                map.Get(new Point(_size.X - 1, 0)).Type = TileType.Wall;
            }

            return map;
        }
    }
}
