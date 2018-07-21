using System;
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
                    tileGraphics.Add(GetGraphicForTileType(tile.Type));
                    tile.GetGraphics(ref tileGraphics);

                    newRepresentation[x][y] = new TileGraphics(tileGraphics.ToArray());
                }
            }

            _representation = newRepresentation;
        }

        //todo replace this with a integer-based approach, ie map the enums values
        private Graphic GetGraphicForTileType(TileType tileType)
        {
            switch(tileType)
            {
                case TileType.Floor:
                    return Graphic.Floor;
                case TileType.Wall:
                    return Graphic.Wall;
                default:
                    throw new NotImplementedException("No graphic for tile type");
            }
        }
    }
}