using Crawler.Maps;
using Crawler.Maps.Initialisers;
using Crawler.Queryables.Entities;

namespace Crawler.Tests.Support
{
    public class TestEntityMapInitialiser : BaseMapInitialiser
    {
        private Entity entity;

        public TestEntityMapInitialiser(Entity entity)
        {
            this.entity = entity;
        }

        public override IMap Initialise()
        {
            Map map = new Map(new Point(10,10));
            map.Add(entity, new Point(5,5));
            return map;
        }
    }
}