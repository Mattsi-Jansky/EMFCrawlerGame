using System;
using System.Collections.Generic;
using Crawler.Factories;
using Crawler.Models;
using Crawler.Queryables.Tiles;

namespace Crawler.Maps.Initialisers.DungeonGenerators.RoomFeatures
{
    public class LavaPoolRoomFeature : PoolRoomFeature
    {
        public LavaPoolRoomFeature(Random random, WeaponFactory weaponFactory) 
            : base(random, weaponFactory, TileFactory.Lava, MobModelFactory.FireThemedMobs)
        {
        }
    }
}