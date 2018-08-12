using Crawler.Queryables.Entities;
using Crawler.Queryables.Entities.Characters;
using Crawler.Queryables.Entities.Components;

namespace Crawler.Factories
{
    public static class MobFactory
    {
        public static Entity GenerateMob()
        {
            var mob = new Entity();
            
            mob.Add(new GraphicComponent(Race.Skeleton.GetGraphic(Archetype.Warrior)));
            mob.Add(new PositionComponent());
            mob.Add(new CharacterComponent("skelly"));
            
            return mob;
        }
    }
}