using Crawler.Maps;
using Crawler.Queryables.Entities;

namespace Crawler.Services
{
    public class PutEntityService
    {
        protected IMap Map;

        public PutEntityService(IMap map)
        {
            Map = map;
        }

        public void Put(Entity entity, Point position)
        {
            Map.Add(entity, position);
            entity.SetPosition(position);
        }
    }
}