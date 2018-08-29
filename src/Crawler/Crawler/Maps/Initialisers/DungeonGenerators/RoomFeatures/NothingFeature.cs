using System.Drawing;
using Crawler.Queryables.Entities;

namespace Crawler.Maps.Initialisers.DungeonGenerators.RoomFeatures
{
    public class NothingFeature : IRoomFeature
    {
        public void Apply(IMap map, Rectangle room, EntitiesCollection entitiesCollection)
        {
            //Do nothing
        }
    }
}