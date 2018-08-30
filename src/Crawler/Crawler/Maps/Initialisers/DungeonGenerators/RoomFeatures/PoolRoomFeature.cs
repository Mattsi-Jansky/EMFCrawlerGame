using System;
using System.Collections.Generic;
using System.Drawing;
using Crawler.Factories;
using Crawler.Models;
using Crawler.Queryables.Entities;
using Crawler.Queryables.Tiles;

namespace Crawler.Maps.Initialisers.DungeonGenerators.RoomFeatures
{
    public class PoolRoomFeature : BaseRoomFeature
    {
        private readonly WeaponFactory _weaponFactory;
        private readonly Func<Tile> _getPoolTile;
        private readonly IList<MobModel> _mobList;
        
        public PoolRoomFeature(Random random, WeaponFactory weaponFactory, Func<Tile> getPoolTile, IList<MobModel> mobList) : base(random)
        {
            _weaponFactory = weaponFactory;
            _getPoolTile = getPoolTile;
            this._mobList = mobList;
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
                        map.Set(position, _getPoolTile());
                    }
                }
            }

            var model = _mobList[Random.Next(_mobList.Count)];
            var newMob = MobFactory.GenerateMob(model, _weaponFactory);
            entitiesCollection.Add(newMob.Id, newMob);
            AddEntityToMiddleOfRoom(newMob, room, map);
        }
    }
}