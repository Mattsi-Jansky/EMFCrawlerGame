using System;
using System.Collections.Generic;
using Crawler.Factories.ItemGenerators;
using Crawler.Queryables.Entities;

namespace Crawler.Factories
{
    public class ItemFactory
    {
        private Random _random;
        private static IList<IItemGenerator> _itemGenerators;

        public ItemFactory(Random random)
        {
            _random = random;
            _itemGenerators = new List<IItemGenerator>
            {
                new HatGenerator(_random),
                new PotionGenerator(_random),
                new GoldGenerator(),
                new WeaponGenerator(_random)
            };
        }

        public Entity GetItem()
        {
            var generator = _itemGenerators[_random.Next(_itemGenerators.Count)];
            return generator.Generate();
        }
    }
}