using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Jansk.Pathfinding;

namespace DungeonGenerators.Pathfinding
{
    public class PathChecker
    {
        private TileWrapper[][] _map;

        public PathChecker(Tile[][] tiles)
        {
            _map = new TileWrapper[tiles.Length][];
            for (int x = 0; x < tiles.Length; x++)
            {
                _map[x] = new TileWrapper[tiles[x].Length];
                for (int y = 0; y < _map[x].Length; y++)
                {
                    _map[x][y] = new TileWrapper(new Point(x, y), tiles[x][y]);
                }
            }
        }

        private Func<TileWrapper, TileWrapper, int> heuristic = delegate (TileWrapper from, TileWrapper to)
        {
            return Math.Abs(from.Position.X - to.Position.X) + Math.Abs(from.Position.Y - to.Position.Y);
        };

        private Func<TileWrapper, TileWrapper[]> Neighbours()
        {
            return delegate (TileWrapper tile)
            {
                var neighbours = new List<TileWrapper>();
                if (tile.Position.X - 1 >= 0 && !IsBlocking(tile.Position.X - 1, tile.Position.Y))
                    neighbours.Add(_map[tile.Position.X - 1][tile.Position.Y]);
                if (tile.Position.X + 1 < _map.Length && !IsBlocking(tile.Position.X + 1, tile.Position.Y))
                    neighbours.Add(_map[tile.Position.X + 1][tile.Position.Y]);
                if (tile.Position.Y + 1 < _map[tile.Position.X].Length && !IsBlocking(tile.Position.X, tile.Position.Y + 1))
                    neighbours.Add(_map[tile.Position.X][tile.Position.Y + 1]);
                if (tile.Position.Y - 1 >= 0 && !IsBlocking(tile.Position.X, tile.Position.Y - 1))
                    neighbours.Add(_map[tile.Position.X][tile.Position.Y - 1]);

                return neighbours.ToArray();
            };
        }

        private bool IsBlocking(int x, int y)
        {
            return !Cartographer.FloorTiles.Contains(_map[x][y].Type);
        }

        private Func<TileWrapper, int> IndexMap()
        {
            return tile => (tile.Position.X * _map.Length) + tile.Position.Y;
        }

        public bool CanPathBetween(Point from, Point to)
        {
            if (from.Equals(to)) return true;

            var pathFinder = new PathFinder<TileWrapper>(heuristic, _map.Length * _map[0].Length);
            var path = pathFinder.Path(_map[from.X][from.Y], _map[to.X][to.Y], IndexMap(), Neighbours());

            return path.Any();
        }
    }

    internal class TileWrapper
    {
        public Point Position;
        public Tile Type;

        public TileWrapper(Point position, Tile type)
        {
            Position = position;
            Type = type;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TileWrapper)) return false;
            var tileWrapper = (TileWrapper) obj;
            return Position.X == tileWrapper.Position.X
                   && Position.Y == tileWrapper.Position.Y;
        }
    }
}
