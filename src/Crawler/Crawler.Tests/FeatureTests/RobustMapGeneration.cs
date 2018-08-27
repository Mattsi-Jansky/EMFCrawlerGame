using System;
using Crawler.Maps.EntityPlacers;
using Crawler.Maps.Initialisers.DungeonGenerators;
using Crawler.Queryables.Entities;
using DungeonGenerators;
using Xunit;

namespace Crawler.Tests.FeatureTests
{
    public class RobustMapGeneration
    {
        [Fact]
        public void MapGenerationShouldNotThrowAnyExceptionWithinOneThousandRuns()
        {
            var initialiser = new DungeonMapInitialiser(new Random(), new RandomEntityPlacer());
            for (int i = 0; i < 1000; i++)
            {
                initialiser.Initialise(new EntitiesCollection());
            }
        }
    }
}