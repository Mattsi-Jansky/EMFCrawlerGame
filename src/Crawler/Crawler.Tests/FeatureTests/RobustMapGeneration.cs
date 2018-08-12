using System;
using Crawler.Maps.Initialisers.DungeonGenerators;
using Crawler.Queryables.Entities;
using Xunit;

namespace Crawler.Tests.FeatureTests
{
    public class RobustMapGeneration
    {
        [Fact]
        public void MapGenerationShouldNotThrowAnyExceptionWithinOneThousandRuns()
        {
            var initialiser = new DungeonMapInitialiser(new Random());
            for (int i = 0; i < 1000; i++)
            {
                initialiser.Initialise(new EntitiesCollection());
            }
        }
    }
}