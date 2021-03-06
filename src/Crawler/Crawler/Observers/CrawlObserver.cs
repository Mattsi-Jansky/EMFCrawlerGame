﻿using System;
using System.Collections.Generic;
using System.Linq;
using Crawler.Maps;
using Crawler.Models;
using Crawler.Queryables.Tiles;

namespace Crawler.Observers
{
    public class CrawlObserver : ICrawlObserver
    {
        private IMap _map;
        private TileGraphics[][] _representation;

        public virtual void Update(IMap map)
        {
            _map = map;
            UpdateRepresentation();
        }

        public TileGraphics Observe(Point location)
        {
            return _representation[location.X][location.Y];
        }

        public TileGraphics[][] Observe()
        {
            return _representation;
        }

        private void UpdateRepresentation()
        {
            var newRepresentation = new TileGraphics[_map.Size.X][];
            for (int x = 0; x < _map.Size.X; x++)
            {
                newRepresentation[x] = new TileGraphics[_map.Size.Y];
                for (int y = 0; y < _map.Size.Y; y++)
                {
                    IList<Graphic> tileGraphics = new List<Graphic>();

                    var tile = _map.Get(new Point(x, y));
                    tile.GetGraphics(ref tileGraphics);
                    var displayText = tile.GetDisplayText();

                    newRepresentation[x][y] = new TileGraphics(tileGraphics.ToArray(), displayText);
                }
            }

            _representation = newRepresentation;
        }
    }
}