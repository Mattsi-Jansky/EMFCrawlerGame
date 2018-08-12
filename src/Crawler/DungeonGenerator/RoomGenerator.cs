using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DungeonGenerators
{
    public class RoomGenerator
    {
        private readonly Random _random;
        private readonly int _xSize;
        private readonly int _ySize;
        private readonly int _minRoomSize;
        private readonly int _minTotalBufferSize;
        private readonly int _minSectorSizeToSplit;
        private readonly int _minNoRooms;
        private readonly RoomShuffler _roomShuffler;

        public RoomGenerator(Random random, int xSize, int ySize, int minRoomSize, int minTotalBufferSize, int minNoRooms)
        {
            _random = random;
            _xSize = xSize;
            _ySize = ySize;
            _minRoomSize = minRoomSize;
            _minTotalBufferSize = minTotalBufferSize;
            _minNoRooms = minNoRooms;

            _minSectorSizeToSplit = (_minRoomSize + _minTotalBufferSize) * 2;
            _roomShuffler = new RoomShuffler(_random, xSize, ySize);
        }

        public IList<Rectangle> GenerateRooms()
        {
            Rectangle root = new Rectangle(0, 0, _xSize, _ySize);
            var initial = SplitSector(root);
            var rooms = RandomiseSectors(initial);
            AddBufferToRooms(rooms);
            _roomShuffler.ShuffleRooms(rooms);

            if (rooms.Count > _minNoRooms) return rooms;
            else return GenerateRooms();
        }

        private IList<Rectangle> RandomiseSectors(IEnumerable<Rectangle> sectors)
        {
            var result = new List<Rectangle>();

            foreach (var sector in sectors)
            {
                var randomisedSectors = RandomiseSector(sector);

                foreach (var randomisedSector in randomisedSectors)
                {
                    result.Add(randomisedSector);
                }
            }

            return result;
        }

        private IList<Rectangle> RandomiseSector(Rectangle sector, int counter = 0)
        {
            var result = new List<Rectangle>();

            if (ShouldSplitRoom(counter) && sector.Width > _minSectorSizeToSplit
                                         && sector.Height > _minSectorSizeToSplit)
            {
                var splitQuadrants = SplitSector(sector);

                foreach (var quadrant in splitQuadrants)
                {
                    var randomised = RandomiseSector(quadrant, counter + 1);

                    foreach (var newSector in randomised)
                    {
                        result.Add(newSector);
                    }
                }
            }
            else if (ShouldAddRoom(counter))
            {
                result.Add(sector);
            }

            return result;
        }

        private bool ShouldSplitRoom(int counter)
        {
            int modifier = counter > 2 ? 2 : counter;
            return _random.Next(4 + modifier) < 3;
        }

        private bool ShouldAddRoom(int counter)
        {
            int modifier = counter > 2 ? 2 : counter;
            return counter == 0 || _random.Next(6 + modifier) > 2;
        }

        private void AddBufferToRooms(IList<Rectangle> rooms)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                var room = rooms[i];

                int maxTotalBufferSize = GetSmallestSizeDimension(room.Width, room.Height) - _minRoomSize;
                var xBuffer = (_random.Next(maxTotalBufferSize - _minTotalBufferSize) + _minTotalBufferSize) / 2;
                var yBuffer = (_random.Next(maxTotalBufferSize - _minTotalBufferSize) + _minTotalBufferSize) / 2;

                room.X += xBuffer;
                room.Width -= xBuffer;
                room.Y += yBuffer;
                room.Height -= yBuffer;

                rooms[i] = room;
            }
        }

        private int GetSmallestSizeDimension(int a, int b)
        {
            if (a < b) return a;
            else return b;
        }

        private Rectangle[] SplitSector(Rectangle parent)
        {
            if (_random.Next(2) == 0) return SplitSectorIntoQuadrants(parent);
            else return SplitSectorIntoHalves(parent);
        }

        private Rectangle[] SplitSectorIntoQuadrants(Rectangle parent)
        {
            Rectangle[] result = new Rectangle[4];

            result[0] = new Rectangle(parent.X, parent.Y, parent.Width / 2, parent.Height / 2);
            result[1] = new Rectangle(parent.X + parent.Width / 2, parent.Y, parent.Width / 2, parent.Height / 2);
            result[2] = new Rectangle(parent.X, parent.Y + parent.Height / 2, parent.Width / 2, parent.Height / 2);
            result[3] = new Rectangle(parent.X + parent.Width / 2, parent.Y + parent.Height / 2, parent.Width / 2, parent.Height / 2);

            return result;
        }

        private Rectangle[] SplitSectorIntoHalves(Rectangle parent)
        {
            if (_random.Next(2) == 0) return SplitSectorIntoHalvesVertically(parent);
            else return SplitSectorIntoHalvesHorizontally(parent);
        }

        private Rectangle[] SplitSectorIntoHalvesVertically(Rectangle parent)
        {
            Rectangle[] result = new Rectangle[2];

            result[0] = new Rectangle(parent.X, parent.Y, parent.Width / 2, parent.Height);
            result[1] = new Rectangle(parent.X + parent.Width / 2, parent.Y, parent.Width / 2, parent.Height);

            return result;
        }

        private Rectangle[] SplitSectorIntoHalvesHorizontally(Rectangle parent)
        {
            Rectangle[] result = new Rectangle[2];

            result[0] = new Rectangle(parent.X, parent.Y, parent.Width, parent.Height / 2);
            result[1] = new Rectangle(parent.X, parent.Y + parent.Height / 2, parent.Width, parent.Height / 2);

            return result;
        }

        public void StripWalls(IList<Rectangle> rooms)
        {
            for(int i = 0; i < rooms.Count; i++)
            {
                var room = rooms[i];

                room.Width -= 2;
                room.Height -= 2;
                room.X += 1;
                room.Y += 1;

                rooms[i] = room;
            }
        }
    }
}