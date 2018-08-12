using System;
using System.Collections.Generic;
using System.Drawing;

namespace DungeonGenerators
{
    public class RoomShuffler
    {
        private readonly Random _random;
        private readonly int _xSize;
        private readonly int _ySize;
        private readonly int _minShuffleSize;
        private readonly int _maxShuffleSize;
        private readonly int _maxNumberOfShuffleIterations = 30;

        public RoomShuffler(Random random, int xSize, int ySize)
        {
            _random = random;
            _xSize = xSize;
            _ySize = ySize;

            _minShuffleSize = 3;
            _maxShuffleSize = 10;
        }

        public void ShuffleRooms(IList<Rectangle> rooms)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                rooms[i] = ShuffleRoom(rooms[i], rooms);
            }
        }

        private bool IsLegalRoom(Rectangle newRoom)
        {
            return newRoom.X > 0 && newRoom.Y > 0 &&
                   newRoom.X + newRoom.Width < _xSize && 
                   newRoom.Y + newRoom.Height < _ySize &&
                   newRoom.Width > 0 && newRoom.Height > 0;
        }

        private Rectangle ShuffleRoom(Rectangle room, IList<Rectangle> rooms, int counter = 0)
        {
            var oldRoom = room;
            int xDiff = _random.Next(_maxShuffleSize - _minShuffleSize + 1) + _minShuffleSize;
            int yDiff = _random.Next(_maxShuffleSize - _minShuffleSize + 1) + _minShuffleSize;
            
            int x = _random.Next(2) == 0 ? room.X + xDiff : room.X - xDiff;
            int y = _random.Next(2) == 0 ? room.Y + yDiff : room.Y - yDiff;

            room.X = x;
            room.Y = y;

            if (!IsLegalRoom(room) || IntersectsAnExistingRoom(room, rooms))
            {
                if (counter > _maxNumberOfShuffleIterations) return oldRoom;
                else return ShuffleRoom(oldRoom, rooms, counter + 1);
            }

            return room;
        }

        private bool IntersectsAnExistingRoom(Rectangle newRoom, IList<Rectangle> rooms)
        {
            //Check room as bigger than it is, because walls will be added later.
            newRoom.Width += 3;
            newRoom.Height += 3;

            foreach (var room in rooms)
            {
                if (room.IntersectsWith(newRoom)) return true;
            }

            return false;
        }
    }
}