using System;
using Crawler.Queryables.Entities.Components;

namespace Crawler.Queryables.Entities
{
    public class Entity : QueryableAggregator<Component>
    {
        public Guid Id { get;}

        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Entity(Component component) : this()
        {
            Add(component);
        }
    }
}
