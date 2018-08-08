using System;
using System.Collections.Generic;
using System.Text;
using Crawler.Maps;
using Crawler.Maps.Initialisers;
using Crawler.Queryables.Tiles;
using DungeonGenerators;
using Tile = Crawler.Queryables.Tiles.Tile;
using TileModel = global::DungeonGenerators.Tile;

namespace Crawler.Services.DungeonGenerators
{
    public class DungeonGenerationModelTranslator : BaseMapInitialiser
    {
        private IDungeonGenerator _dungeonGenerator;
        private TileModel[][] mapModel;

        public DungeonGenerationModelTranslator(IDungeonGenerator dungeonGenerator)
        {
            _dungeonGenerator = dungeonGenerator;
        }

        public override IMap Initialise()
        {
            mapModel = _dungeonGenerator.Generate();
            var size = new Point(mapModel.Length, mapModel[0].Length);
            var map = new Map(size);

            for (int x = 0; x < size.X; x++)
            {
                for (int y = 0; y < size.Y; y++)
                {
                    TranslateTile(map.Get(new Point(x, y)), mapModel[x][y]);
                }
            }

            return map;
        }

        private void TranslateTile(Tile to, TileModel from)
        {
            switch (from)
            {
                case TileModel.Floor:
                    to.Type = TileType.Floor;
                    break;
                case TileModel.Wall:
                    to.Type = TileType.Wall;
                    break;
                default:
                    to.Type = TileType.Wall;
                    break;
            }
        }
    }
}
