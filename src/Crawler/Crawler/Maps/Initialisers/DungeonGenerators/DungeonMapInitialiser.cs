using System;
using System.Collections.Generic;
using System.Text;
using Crawler.Factories;
using Crawler.Maps.EntityPlacers;
using Crawler.ObjectResolvers;
using Crawler.Queryables.Entities;
using Crawler.Services.DungeonGenerators;
using DungeonGenerators;

namespace Crawler.Maps.Initialisers.DungeonGenerators
{
    public class DungeonMapInitialiser : BaseMapInitialiser
    {
        private readonly Random _random;
        private readonly IEntityPlacer _entityPlacer;
        private readonly ItemFactory _itemFactory;

        public DungeonMapInitialiser(Random random, IEntityPlacer entityPlacer)
        {
            _random = random;
            _entityPlacer = entityPlacer;
            _itemFactory = new ItemFactory(random);
        }

        public override IMap Initialise(EntitiesCollection entitiesCollection)
        {
            var generator = new DungeonGenerator(120,60, 6, 6);
            
            generator.Generate();
            var translator = new DungeonGenerationModelTranslator(generator.Map);
            var map = translator.Initialise(entitiesCollection);
            //AddFeatures(generator, map, entitiesCollection);
            AddItems(map, _entityPlacer);

            return map;
        }

        private void AddFeatures(DungeonGenerator generator, IMap map, EntitiesCollection entitiesCollection)
        {
            var featureFactory = new RoomFeatureFactory(_random);
            foreach (var room in generator.Rooms)
            {
                var feature = featureFactory.GetAny();
                feature.Apply(map, room, entitiesCollection);
            }
        }
        
        private void AddItems(IMap map, IEntityPlacer entityPlacer)
        {
            for (int i = 0; i < 50; i++)
            {
                var item = _itemFactory.GetItem();
                var point = entityPlacer.PlaceEntity(map, item);
                item.SetPosition(point);
                map.Add(item, point);
            }
        }
    }
}
