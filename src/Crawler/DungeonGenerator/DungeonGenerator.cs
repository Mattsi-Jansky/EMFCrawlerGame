using System;

namespace DungeonGenerators
{
    public class DungeonGenerator : IDungeonGenerator
    {
        private readonly int _xSize;
        private readonly int _ySize;
        private readonly int _minRoomSize;
        private readonly int _minBufferSize;
        private readonly Random _random;
        private readonly int _minNoRooms;

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
        
        public Tile[][] Generate()
        {
            var cartographer = new Cartographer(_xSize, _ySize);
            var roomGenerator = new RoomGenerator(_random, _xSize, _ySize, _minRoomSize, _minBufferSize, _minNoRooms);

            var rooms = roomGenerator.GenerateRooms();
            cartographer.DrawRooms(rooms);
            new CoridoorGenerator(rooms, cartographer, _random).LinkRooms();
            cartographer.DrawWalls();
            new EntityPlacer(cartographer, _random).AddMobs(rooms);

            return cartographer.tiles;
        }
    }
}