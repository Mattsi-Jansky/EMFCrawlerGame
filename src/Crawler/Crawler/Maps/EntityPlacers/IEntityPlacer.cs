using Crawler.Queryables.Entities;

namespace Crawler.Maps.EntityPlacers
{
    public interface IEntityPlacer
    {
        Point PlaceCharacter(IMap map, Entity entity);
    }
}
