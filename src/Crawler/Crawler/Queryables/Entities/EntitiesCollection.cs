using System;
using System.Collections.Generic;

namespace Crawler.Queryables.Entities
{
    public class EntitiesCollection
    {
        private readonly Dictionary<Guid, Entity> _entities = new Dictionary<Guid, Entity>();

        public void Add(Guid id, Entity entity) => _entities[id] = entity;
        public Entity Get(Guid id) => _entities[id];
        public IEnumerable<Entity> Get() => _entities.Values;
        public void Remove(Guid id) => _entities.Remove(id);
    }
}
