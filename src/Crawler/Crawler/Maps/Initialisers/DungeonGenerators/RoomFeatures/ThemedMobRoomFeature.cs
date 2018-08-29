using System;
using System.Collections.Generic;
using System.Drawing;
using Crawler.Factories;
using Crawler.Models;
using Crawler.Queryables.Entities;

namespace Crawler.Maps.Initialisers.DungeonGenerators.RoomFeatures
{
    public class ThemedMobRoomFeature : BaseRoomFeature
    {
        private readonly IList<MobModel> _mobModels;
        private readonly int _maxNumberOfMobsPerRoom = 10;
        private readonly WeaponFactory _weaponFactory; 

        public ThemedMobRoomFeature(IList<MobModel> mobModels, Random random, WeaponFactory weaponFactory) : base(random)
        {
            _mobModels = mobModels;
            _weaponFactory = weaponFactory;
            Random = random;
        }

        public override void Apply(IMap map, Rectangle room, EntitiesCollection entitiesCollection)
        {
            var maxNumberOfMobsThatwillFitInRoom = (room.X)  * (room.Y);
            var numberOfMobs = Random.Next(GetSmallest(_maxNumberOfMobsPerRoom, maxNumberOfMobsThatwillFitInRoom));
            for (int i = 0; i < numberOfMobs; i++)
            {
                var model = _mobModels[Random.Next(_mobModels.Count)];
                var newMob = MobFactory.GenerateMob(model, _weaponFactory);
                entitiesCollection.Add(newMob.Id, newMob);
                AddEntityRandomly(newMob, room, map);
            }
        }
        
        private int GetSmallest(int a, int b)
        {
            if (a < b) return a;
            else return b;
        }
    }
}