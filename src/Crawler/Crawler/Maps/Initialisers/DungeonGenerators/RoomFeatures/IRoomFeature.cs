using System.Drawing;

namespace Crawler.Maps.Initialisers.DungeonGenerators.RoomFeatures
{
    public interface IRoomFeature
    {
        void Apply(IMap map, Rectangle room);
    }
}