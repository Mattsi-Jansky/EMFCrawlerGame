using System;
using System.Collections.Generic;
using System.Drawing;

namespace DungeonGenerators
{
    public class DungeonGenerator
    {
        private readonly int _xSize;
        private readonly int _ySize;
        private readonly int _minRoomSize;
        private readonly int _minBufferSize;
        private readonly Random _random;
        private readonly int _minNoRooms;

        public IList<Rectangle> Rooms;
        public Tile[][] Map;

        public DungeonGenerator(int xSize, int ySize, int minRoomSize, int minBufferSize, int minNoRooms = 4)
        {
            _xSize = xSize;
            _ySize = ySize;
            _minRoomSize = minRoomSize;
            _minBufferSize = minBufferSize;
            _minNoRooms = minNoRooms;
            _random = new Random();
        }

        public DungeonGenerator() : this(128, 128, 4, 11)
        {
        }
        
        public void Generate()
        {
            var cartographer = new Cartographer(_xSize, _ySize);
            var roomGenerator = new RoomGenerator(_random, _xSize, _ySize, _minRoomSize, _minBufferSize, _minNoRooms);

            Rooms = roomGenerator.GenerateRooms();
            cartographer.DrawRooms(Rooms);
            new CoridoorGenerator(Rooms, cartographer, _random).LinkRooms();
            cartographer.DrawWalls();
            roomGenerator.StripWalls(Rooms);
            //new EntityPlacer(cartographer, _random).AddMobs(Rooms);

            Map = cartographer.tiles;
        }
    }
}