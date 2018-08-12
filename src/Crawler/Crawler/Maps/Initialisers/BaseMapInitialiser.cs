using Crawler.Queryables.Entities;

namespace Crawler.Maps.Initialisers
{
    public abstract class BaseMapInitialiser
    {
        public abstract IMap Initialise(EntitiesCollection entitiesCollection);
    }
}