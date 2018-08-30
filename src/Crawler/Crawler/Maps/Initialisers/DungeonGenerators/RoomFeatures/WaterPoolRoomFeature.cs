using System;
using System.Drawing;
using Crawler.Factories;
using Crawler.Models;
using Crawler.Queryables.Entities;

namespace Crawler.Maps.Initialisers.DungeonGenerators.RoomFeatures
{
    public class WaterPoolRoomFeature : BaseRoomFeature
    {
        private readonly WeaponFactory _weaponFactory; 
        
        public WaterPoolRoomFeature(Random random, WeaponFactory weaponFactory) : base(random)
        {
            _weaponFactory = weaponFactory;
        }

        public override void Apply(IMap map, Rectangle room, EntitiesCollection entitiesCollection)
        {
            var poolWidth = room.Width / 2;
            var poolHeight = room.Height / 2;
            var startX = room.X + room.Width / 4;
            var startY = room.Y + room.Height / 4;

            for (int x = 0; x < poolWidth; x++)
            {
                for (int y = 0; y < poolHeight; y++)
                {
                    var position = new Point(startX + x, startY + y);
                    
                    if (x == 0 || x == poolWidth - 1
                               || y == 0 || y == poolHeight - 1)
                    {
                        map.Set(position, TileFactory.Floor(Graphic.WeirdGridTile));
                    }
                    else
                    {
                        map.Set(position, TileFactory.Water());
                    }
                }
            }

            var mobList = MobModelFactory.WaterThemedMobs;
            var model = mobList[Random.Next(mobList.Count)];
            var newMob = MobFactory.GenerateMob(model, _weaponFactory);
            entitiesCollection.Add(newMob.Id, newMob);
            AddEntityToMiddleOfRoom(newMob, room, map);
        }
    }
}