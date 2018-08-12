using Crawler.Models;
using Crawler.Queryables.Entities;
using Crawler.Queryables.Entities.Characters;
using Crawler.Queryables.Entities.Components;

namespace Crawler.Factories
{
    public static class MobFactory
    {
        public static Entity GenerateMob(MobModel model)
        {
            var mob = new Entity();
            
            mob.Add(new GraphicComponent(model.Graphic));
            mob.Add(new PositionComponent());
            mob.Add(new CharacterComponent(model.Name));
            
            return mob;
        }
    }
}