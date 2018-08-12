using System.Drawing;
using Crawler.Queryables.Entities;

namespace Crawler.Maps.Initialisers.DungeonGenerators.RoomFeatures
{
    public interface IRoomFeature
    {
        void Apply(IMap map, Rectangle room, EntitiesCollection entitiesCollection);
    }
}