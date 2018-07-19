using System;
using System.Collections.Generic;
using System.Linq;
using Crawler.Maps;
using Crawler.Models;
using Crawler.Queryables.Tiles;

namespace Crawler.Observers
{
    //todo make this threadsafe
    public class CrawlObserver : ICrawlObserver
    {
        private IMap _map;
        private bool _isCached;
        private Graphic[][][] _representation;

        public virtual void Update(IMap map)
        {
            _map = map;
            _isCached = false;
        }

        public Graphic[] Observe(Point location)
        {
            if (_isCached)
            {
                return _representation[location.X][location.Y];
            }
            else
            {
                UpdateRepresentation();
                return _representation[location.X][location.Y];
            }
        }

        public Graphic[][][] Observe()
        {
            return _representation;
        }

        private void UpdateRepresentation()
        {
            var newRepresentation = new Graphic[_map.Size.X][][];
            for (int x = 0; x < _map.Size.X; x++)
            {
                newRepresentation[x] = new Graphic[_map.Size.Y][];
                for (int y = 0; y < _map.Size.Y; y++)
                {
                    IList<Graphic> tileGraphics = new List<Graphic>();

                    var tile = _map.Get(new Point(x, y));
                    tileGraphics.Add(GetGraphicForTileType(tile.Type));
                    tile.GetGraphics(ref tileGraphics);

                    newRepresentation[x][y] = tileGraphics.ToArray();
                }
            }

            _representation = newRepresentation;
            _isCached = true;
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