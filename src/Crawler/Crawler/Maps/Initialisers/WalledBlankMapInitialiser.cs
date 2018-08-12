using Crawler.Factories;
using Crawler.Queryables.Entities;
using Crawler.Services;

namespace Crawler.Maps.Initialisers
{
    public class WalledBlankMapInitialiser : BaseMapInitialiser
    {
        private Point _size;

        public WalledBlankMapInitialiser(Point size)
        {
            _size = size;
        }

        public override IMap Initialise(EntitiesCollection entitiesCollection)
        {
            Map map = new Map(_size);

            for (int x = 0; x < _size.X; x++)
            {
                map.Set(new Point(x, 0), TileFactory.Wall());
            }

            for (int y = 0; y < _size.Y; y++)
            {
                map.Set(new Point(y, 0), TileFactory.Wall());
            }

            return map;
        }
    }
}
