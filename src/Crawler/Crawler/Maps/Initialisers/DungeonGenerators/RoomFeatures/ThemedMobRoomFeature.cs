using System;
using System.Collections.Generic;
using System.Drawing;
using Crawler.Factories;
using Crawler.Models;

namespace Crawler.Maps.Initialisers.DungeonGenerators.RoomFeatures
{
    public class ThemedMobRoomFeature : BaseRoomFeature
    {
        private readonly IList<MobModel> _mobModels;
        private readonly int _maxNumberOfMobsPerRoom = 10;

        public ThemedMobRoomFeature(IList<MobModel> mobModels, Random random) : base(random)
        {
            _mobModels = mobModels;
            Random = random;
        }

        public override void Apply(IMap map, Rectangle room)
        {
            var maxNumberOfMobsThatwillFitInRoom = (room.X)  * (room.Y);
            var numberOfMobs = Random.Next(GetSmallest(_maxNumberOfMobsPerRoom, maxNumberOfMobsThatwillFitInRoom));
            for (int i = 0; i < numberOfMobs; i++)
            {
                var model = _mobModels[Random.Next(_mobModels.Count)];
                AddEntityRandomly(MobFactory.GenerateMob(model), room, map);
            }
        }
        
        private int GetSmallest(int a, int b)
        {
            if (a < b) return a;
            else return b;
        }
    }
}