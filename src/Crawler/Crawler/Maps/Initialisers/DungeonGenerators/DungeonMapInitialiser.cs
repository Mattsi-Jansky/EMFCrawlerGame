using System;
using System.Collections.Generic;
using System.Text;
using Crawler.Factories;
using Crawler.Services.DungeonGenerators;
using DungeonGenerators;

namespace Crawler.Maps.Initialisers.DungeonGenerators
{
    public class DungeonMapInitialiser : BaseMapInitialiser
    {
        private readonly Random _random;

        public DungeonMapInitialiser(Random random)
        {
            _random = random;
        }

        public override IMap Initialise()
        {
            DungeonGenerator generator = new DungeonGenerator(120,60, 6, 6);
            
            generator.Generate();
            var translator = new DungeonGenerationModelTranslator(generator.Map);
            var map = translator.Initialise();
            AddFeatures(generator, map);

            return map;
        }

        private void AddFeatures(DungeonGenerator generator, IMap map)
        {
            var featureFactory = new RoomFeatureFactory(_random);
            foreach (var room in generator.Rooms)
            {
                var feature = featureFactory.GetAny();
                feature.Apply(map, room);
            }
        }
    }
}
