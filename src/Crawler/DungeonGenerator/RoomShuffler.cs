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

        public RoomShuffler(Random random, int xSize, int ySize)
        {
            _random = random;
            _xSize = xSize;
            _ySize = ySize;

            _minShuffleSize = xSize / 25;
            _maxShuffleSize = xSize / 10;
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
            int xDiff = _random.Next(_maxShuffleSize - _minShuffleSize) + _minShuffleSize;
            int yDiff = _random.Next(_maxShuffleSize - _minShuffleSize) + _minShuffleSize;
            int widthDiff = _random.Next(3) + 1;
            int heightDiff = _random.Next(3) + 1;

            int x = _random.Next(2) == 0 ? room.X + xDiff : room.X - xDiff;
            int y = _random.Next(2) == 0 ? room.Y + yDiff : room.Y - yDiff;
            int width = _random.Next(2) == 0 ? room.Width + widthDiff : room.Width - widthDiff;
            int height = _random.Next(2) == 0 ? room.Height + heightDiff : room.Height - heightDiff;

            room.X = x;
            room.Y = y;
            room.Width = width;
            room.Height = height; 

            if (!IsLegalRoom(room) || IntersectsAnExistingRoom(room, rooms))
            {
                if (counter > 3) return oldRoom;
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