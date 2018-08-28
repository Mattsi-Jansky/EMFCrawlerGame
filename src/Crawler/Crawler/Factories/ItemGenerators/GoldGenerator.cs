using Crawler.Models;
using Crawler.Queryables.Entities;
using Crawler.Queryables.Entities.Components;

namespace Crawler.Factories.ItemGenerators
{
    public class GoldGenerator : IItemGenerator
    {
        public Entity Generate()
        {
            var entity = new Entity();
            entity.Add(new PositionComponent());
            entity.Add(new GoldComponent(1));
            entity.Add(new NameComponent("Gold"));

            return entity;
        }
    }
}