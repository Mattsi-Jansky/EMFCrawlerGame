using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DungeonGenerators
{
    public class Cartographer
    {
        public static readonly Tile[] FloorTiles = {Tile.Floor};

        public Tile[][] tiles { get; private set; }

        public Cartographer(int xSize, int ySize)
        {
            GenerateEmptyTiles(xSize, ySize);
        }

        private void GenerateEmptyTiles(int xSize, int ySize)
        {
            tiles = new Tile[xSize][];

            for (int x = 0; x < xSize; x++)
            {
                tiles[x] = new Tile[ySize];
                for (int y = 0; y < ySize; y++)
                {
                    tiles[x][y] = Tile.Nothing;
                }
            }
        }

        public void DrawRooms(IList<Rectangle> rooms)
        {
            foreach (var room in rooms)
            {
                for (int x = 0; x < room.Width; x++)
                {
                    for (int y = 0; y < room.Height; y++)
                    {
                        int tileX = room.X + x;
                        int tileY = room.Y + y;
                        var tile = tileX == 0 || tileX == tiles.Length - 1 ||
                                   tileY == 0 || tileY == tiles[0].Length - 1
                            ? Tile.Wall
                            : Tile.Floor;
                        tiles[tileX][tileY] = tile;
                    }
                }
            }
        }

        public void DrawCoridoors(IList<Coridoor> coridoors)
        {
            foreach (var coridoor in coridoors)
            {
                DrawCoridoor(coridoor);
            }
        }

        private void DrawCoridoor(Coridoor coridoor)
        {
            //West wall
            DrawLine(new Point(coridoor.StartX - 1, coridoor.StartY),
                new Point(coridoor.StartX - 1, coridoor.EndY), Tile.Floor);
            //Coridoor
            DrawLine(new Point(coridoor.StartX, coridoor.StartY),
                new Point(coridoor.StartX, coridoor.EndY), Tile.Floor);
            //East wall
            DrawLine(new Point(coridoor.StartX + 1, coridoor.StartY),
                new Point(coridoor.StartX + 1, coridoor.EndY), Tile.Floor);

            //North wall
            DrawLine(new Point(coridoor.StartX, coridoor.EndY - 1),
                new Point(coridoor.EndX, coridoor.EndY - 1), Tile.Floor);
            //Coridoor
            DrawLine(new Point(coridoor.StartX, coridoor.EndY),
                new Point(coridoor.EndX, coridoor.EndY), Tile.Floor);
            //South wall
            DrawLine(new Point(coridoor.StartX, coridoor.EndY + 1),
                new Point(coridoor.EndX, coridoor.EndY + 1), Tile.Floor);
        }

        public void DrawLine(Point from, Point to, Tile tile, Tile? replacement = null)
        {
            var directionX = from.X > to.X ? -1 :
                from.X == to.X ? 0 : 1;
            var directionY = from.Y > to.Y ? -1 :
                from.Y == to.Y ? 0 : 1;

            while (from.X != to.X
                   || from.Y != to.Y)
            {
                ReplaceIfMatching(from.X, from.Y, tile, replacement);

                from.X += directionX;
                from.Y += directionY;
            }
        }

        private void ReplaceIfMatching(int x, int y, Tile tile, Tile? match)
        {
            if (match == null || tiles[x][y] == match)
            {
                tiles[x][y] = tile;
            }
        }

        public void DrawWalls()
        {
            for (int x = 0; x < tiles.Length; x++)
            {
                for (int y = 0; y < tiles[x].Length; y++)
                {
                    if (tiles[x][y] == Tile.Floor)
                    {
                        DrawWallIfTileHasNeighbouringNothing(x, y);
                    }
                }
            }
        }

        private void DrawWallIfTileHasNeighbouringNothing(int x, int y)
        {
            IList<Tile> surroundingTiles = new List<Tile>();
            AddTileIfValid(surroundingTiles, x, y - 1);
            AddTileIfValid(surroundingTiles, x, y + 1);
            AddTileIfValid(surroundingTiles, x + 1, y);
            AddTileIfValid(surroundingTiles, x - 1, y);
            AddTileIfValid(surroundingTiles, x + 1, y - 1);
            AddTileIfValid(surroundingTiles, x - 1, y - 1);
            AddTileIfValid(surroundingTiles, x + 1, y + 1);
            AddTileIfValid(surroundingTiles, x - 1, y + 1);

            if (surroundingTiles.Any(t => t == Tile.Nothing))
            {
                tiles[x][y] = Tile.Wall;
            }
        }

        private void AddTileIfValid(IList<Tile> list, int x, int y)
        {
            if (x > 0 && x < tiles.Length
                      && y > 0 && y < tiles[x].Length)
            {
                list.Add(tiles[x][y]);
            }
        }
    }
}