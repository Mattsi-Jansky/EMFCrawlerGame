using System;
using System.Collections.Generic;
using System.Text;
using Crawler.Services.DungeonGenerators;
using DungeonGenerators;

namespace Crawler.Maps.Initialisers.DungeonGenerators
{
    public class SimpleDungeonGenerator : BaseMapInitialiser
    {

        public override IMap Initialise()
        {
            DungeonGenerator generator = new DungeonGenerator(60,30, 2, 4);
            var translator = new DungeonGenerationModelTranslator(generator);
            return translator.Initialise();
        }
    }
}
