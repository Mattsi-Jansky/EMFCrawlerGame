using Crawler.Models;
using Crawler.Queryables.Entities;
using Crawler.Queryables.Entities.Components;
using Crawler.Queryables.Tiles;

namespace Crawler.Services
{
    public static class TileFactory
    {
        private static int envTileStart = 403;

        public static Tile Floor()
        {
            return Floor(5, 0);
        }

        private static Tile Floor(int tile, int colour)
        {
            var floor = CreateFloor(GetEnvGraphic(tile, colour));
            return new Tile(floor);
        }

        private static Entity CreateFloor(Graphic graphic)
        {
            var floor = new Entity();
            floor.Add(new GraphicComponent(graphic));
            return floor;
        }

        public static Tile Wall()
        {
            return Wall(false, 0);
        }

        public static Tile Wall(bool isNorthWall, int colour)
        {
            int tile = isNorthWall ? 0 : 4;
            return Wall(tile, colour);
        }

        public static Tile Wall(int tile, int colour)
        {
            var wall = CreateWall(tile, colour);
            return new Tile(wall);
        }

        private static Entity CreateWall(int tile, int colour)
        {
            var wall = new Entity();
            wall.Add(new GraphicComponent(GetEnvGraphic(tile, colour)));
            //todo: add blocking component
            return wall;
        }

        private static Graphic GetEnvGraphic(int tile, int colour)
        {
            return (Graphic)envTileStart + (colour * 16) + tile;
        }
    }
}
