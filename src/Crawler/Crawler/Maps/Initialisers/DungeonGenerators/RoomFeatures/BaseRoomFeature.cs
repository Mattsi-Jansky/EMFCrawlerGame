using System;
using System.Drawing;
using Crawler.Queryables.Entities;

namespace Crawler.Maps.Initialisers.DungeonGenerators.RoomFeatures
{
    public abstract class BaseRoomFeature : IRoomFeature
    {
        protected Random Random;

        protected BaseRoomFeature(Random random)
        {
            Random = random;
        }

        protected void AddEntityRandomly(Entity entity, Rectangle room, IMap map)
        {
            var point = new Point(room.X + Random.Next(room.Width),
                room.Y + Random.Next(room.Height));

            if (!map.Get(point).IsBlocked())
            {
                map.Add(entity, point);
                entity.SetPosition(point);
            }
            else AddEntityRandomly(entity, room, map);
        }

        public abstract void Apply(IMap map, Rectangle room, EntitiesCollection entitiesCollection);
    }
}