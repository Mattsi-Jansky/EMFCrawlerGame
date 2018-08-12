using Crawler.Maps;
using Crawler.Maps.Initialisers;
using Crawler.Queryables.Entities;

namespace Crawler.Tests.Support
{
    public class TestBlankMapInitialiser : BaseMapInitialiser
    {
        public override IMap Initialise(EntitiesCollection entitiesCollection)
        {
            return new Map(new Point(10, 10));
        }
    }
}