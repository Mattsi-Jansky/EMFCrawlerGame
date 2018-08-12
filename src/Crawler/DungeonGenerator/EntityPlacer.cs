using System;
using System.Collections.Generic;
using System.Drawing;

namespace DungeonGenerators
{
    public class EntityPlacer
    {
        private Cartographer _cartographer;
        private Random _random;
        private readonly int _maxNumberOfMobsPerRoom = 10;
        private readonly int _minimumNumberOfMobsPerRoom = 0;
        
        public EntityPlacer(Cartographer cartographer, Random random)
        {
            _cartographer = cartographer;
            _random = random;
        }

        public void AddMobs(IList<Rectangle> rooms)
        {
            foreach (var room in rooms)
            {
                var maxNumberOfMobsThatwillFitInRoom = (room.X - 1)  * (room.Y - 1);
                var numberOfMobs = _random.Next(GetSmallest(_maxNumberOfMobsPerRoom, maxNumberOfMobsThatwillFitInRoom));
                for (int i = 0; i < numberOfMobs; i++)
                {
                    AddMob(room);
                }
            }
        }

        private void AddMob(Rectangle room)
        {
            int x = _random.Next(_cartographer.tiles.Length);
            int y = _random.Next(_cartographer.tiles[x].Length);

            if (_cartographer.tiles[x][y] == Tile.Floor)
            {
                _cartographer.tiles[x][y] = Tile.Mob;
            }
            else AddMob(room);
        }
        
        private int GetSmallest(int a, int b)
        {
            if (a < b) return a;
            else return b;
        }
    }
}