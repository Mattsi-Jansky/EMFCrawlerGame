using System;
using Crawler.Models;
using Crawler.Queryables.Entities;

namespace Crawler.Factories.ItemGenerators
{
    public class WeaponGenerator : IItemGenerator
    {
        private Random random;
        private WeaponFactory _factory;

        public WeaponGenerator(Random random)
        {
            this.random = random;
            _factory = new WeaponFactory();
        }

        public Entity Generate()
        {
            Weapon weapon = (Weapon) random.Next(12) + 1;
            var component =_factory.Get(weapon);
            component.Droppable = true;
            return component.AsEntity();
        }
    }
}