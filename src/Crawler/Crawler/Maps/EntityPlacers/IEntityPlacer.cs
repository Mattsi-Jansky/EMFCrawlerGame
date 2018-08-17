using Crawler.Queryables.Entities;

namespace Crawler.Maps.EntityPlacers
{
    public interface IEntityPlacer
    {
        Point PlaceEntity(IMap map, Entity entity);
    }
}
