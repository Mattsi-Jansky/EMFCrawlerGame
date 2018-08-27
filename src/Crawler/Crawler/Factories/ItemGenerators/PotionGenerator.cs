using System;
using Crawler.Models;
using Crawler.Queryables.Entities;
using Crawler.Queryables.Entities.Components;

namespace Crawler.Factories.ItemGenerators
{
    public class PotionGenerator : IItemGenerator
    {
        private Random _random;

        public PotionGenerator(Random random)
        {
            _random = random;
        }

        public Entity Generate()
        {
            return GenerateHealthPotion();
        }

        private Entity GenerateHealthPotion()
        {
            var entity = new Entity();
            
            entity.Add(new GraphicComponent(Graphic.Potion_red));
            entity.Add(new PositionComponent());
            entity.Add(new NameComponent("Health Potion"));
            
            return entity;
        }
    }
}