using Crawler.Maps;
using Crawler.Maps.EntityPlacers;
using Crawler.Queryables.Entities;

namespace Crawler.Tests.Support
{
    public class TestEntityPlacer : IEntityPlacer
    {
        public Point PlaceCharacter(IMap map, Entity entity)
        {
            return new Point(5,5);
        }
    }
}
