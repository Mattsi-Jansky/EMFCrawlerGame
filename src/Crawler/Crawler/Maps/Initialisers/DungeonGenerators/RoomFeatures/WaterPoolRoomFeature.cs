using System;
using System.Collections.Generic;
using Crawler.Factories;
using Crawler.Models;
using Crawler.Queryables.Tiles;

namespace Crawler.Maps.Initialisers.DungeonGenerators.RoomFeatures
{
    public class WaterPoolRoomFeature : PoolRoomFeature
    {
        public WaterPoolRoomFeature(Random random, WeaponFactory weaponFactory) : base(random, weaponFactory,
            TileFactory.Water, MobModelFactory.WaterThemedMobs)
        {
        }
    }
}