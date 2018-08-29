using System;
using Crawler.Models;
using Crawler.Queryables.Entities;
using Crawler.Queryables.Entities.Characters;
using Crawler.Queryables.Entities.Components;
using Crawler.Queryables.Entities.Components.Controllers;

namespace Crawler.Factories
{
    public static class MobFactory
    {
        public static Entity GenerateMob(MobModel model, WeaponFactory weaponFactory)
        {
            var mob = new Entity();
            
            mob.Add(new GraphicComponent(model.Graphic));
            mob.Add(new PositionComponent());
            mob.Add(new NameComponent(model.Name));
            mob.Add(new RandomMovementControllerComponent(new Random(), mob.Id));
            mob.Add(new BlockingComponent());
            mob.Add(new CharacterComponent(model.Stats.str, model.Stats.dex, model.Stats.con, model.Stats.wis));
            mob.Add(weaponFactory.Get(model.Weapon));
            
            return mob;
        }
    }
}