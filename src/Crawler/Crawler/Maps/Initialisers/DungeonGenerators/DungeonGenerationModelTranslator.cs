﻿using System;
using System.Collections.Generic;
using System.Text;
using Crawler.Factories;
using Crawler.Maps;
using Crawler.Maps.Initialisers;
using Crawler.Queryables.Entities;
using Crawler.Queryables.Tiles;
using DungeonGenerators;
using Tile = Crawler.Queryables.Tiles.Tile;
using TileModel = global::DungeonGenerators.Tile;

namespace Crawler.Services.DungeonGenerators
{
    public class DungeonGenerationModelTranslator : BaseMapInitialiser
    {
        private TileModel[][] _mapModel;
        private Map _map;

        public DungeonGenerationModelTranslator(TileModel[][] map)
        {
            _mapModel = map;
        }

        public override IMap Initialise(EntitiesCollection entitiesCollection)
        {
            var size = new Point(_mapModel.Length, _mapModel[0].Length);
            _map = new Map(size);
            Random random = new Random();
            int colour = random.Next(4);

            for (int x = 0; x < size.X; x++)
            {
                for (int y = 0; y < size.Y; y++)
                {
                    TranslateTile(new Point(x, y), _mapModel[x][y], colour);
                }
            }

            return _map;
        }

        private void TranslateTile(Point to, TileModel from, int colour)
        {
            switch (from)
            {
                case TileModel.Floor:
                    _map.Set(to, TileFactory.Floor(colour));
                    break;
                case TileModel.Wall:
                    var isNorthWall = this.IsNorthWall(to);
                    _map.Set(to, TileFactory.Wall(isNorthWall, colour));
                    break;
                default:
                    _map.Set(to, TileFactory.Wall(14, 0));
                    break;
            }
        }

        private bool IsNorthWall(Point to)
        {
            var south = to.South();
            if (south.X < 0 || south.Y < 0
                || south.X >= _mapModel.Length 
                || south.Y >= _mapModel[0].Length) return false;
            bool isNorthFacing = _mapModel[south.X][south.Y] != TileModel.Wall;
            return isNorthFacing;
        }
    }
}
