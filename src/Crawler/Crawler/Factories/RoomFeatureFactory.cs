using System;
using System.Collections.Generic;
using Crawler.Maps.Initialisers.DungeonGenerators.RoomFeatures;

namespace Crawler.Factories
{
    public class RoomFeatureFactory
    {
        private Random _random;
        private static IList<IRoomFeature> _roomFeatures = null;

        public RoomFeatureFactory(Random random)
        {
            _random = random;
            if(_roomFeatures == null) Initiate();
        }

        public IRoomFeature GetAny()
        {
            return _roomFeatures[_random.Next(_roomFeatures.Count)];
        }

        private void Initiate()
        {
            _roomFeatures = new List<IRoomFeature>();
            foreach (var theme in MobModelFactory.MobThemes)
            {
                _roomFeatures.Add(new ThemedMobRoomFeature(theme, _random));
            }
        }
    }
}