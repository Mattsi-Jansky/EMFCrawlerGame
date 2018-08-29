using Crawler.Queryables.Entities;

namespace Crawler.Queryables.Tiles
{
    public class Tile : QueryableAggregator<Entity>
    {
        public Tile(Entity entity)
        {
            Add(entity);
        }

        public Entity GetDeadEntity()
        {
            foreach (var entity in Queryables)
            {
                if (entity.IsDead())
                {
                    return entity;
                }
            }

            return null;
        }
    }
}