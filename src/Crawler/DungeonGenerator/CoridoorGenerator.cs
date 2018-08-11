using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DungeonGenerators.Pathfinding;

namespace DungeonGenerators
{
    public class CoridoorGenerator
    {
        private readonly IList<Rectangle> _rooms;
        private readonly Cartographer _cartographer;
        private readonly Random _random;
        private IList<Coridoor> _coridoors;

        public CoridoorGenerator(IList<Rectangle> rooms, Cartographer cartographer, Random random)
        {
            _rooms = rooms;
            _cartographer = cartographer;
            _random = random;
        }

        public void LinkRooms()
        {
            _coridoors = new List<Coridoor>();
            for (int i = 0; i < _rooms.Count - 1; i++)
            {
                AddRandomUniqueCoridoor();
            }

            _cartographer.DrawCoridoors(_coridoors);

            EnsureEveryRoomIsTraversible();
        }

        private void EnsureEveryRoomIsTraversible()
        {
            if (!IsEveryRoomTraversible())
            {
                if (_coridoors.Count < TotalPossibleNumberOfUniqueCoridoors())
                {
                    AddRandomUniqueCoridoor();
                    _cartographer.DrawCoridoors(_coridoors);
                    EnsureEveryRoomIsTraversible();
                }
            }
        }

        private bool IsEveryRoomTraversible()
        {
            var pathChecker = new PathChecker(_cartographer.tiles);

            foreach (var roomFrom in _rooms)
            {
                foreach (var roomTo in _rooms)
                {
                    if (!pathChecker.CanPathBetween(roomFrom.Center(), roomTo.Center()))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private int TotalPossibleNumberOfUniqueCoridoors()
        {
            int n = _rooms.Count - 2;
            return ((n + 2) * (n + 1)) / 2;
        }

        private void AddRandomUniqueCoridoor()
        {
            var coridoors = new List<Coridoor>
            {
                GenerateRandomUniqueCoridoor(),
                GenerateRandomUniqueCoridoor(),
                GenerateRandomUniqueCoridoor()
            };

            _coridoors.Add(coridoors.OrderBy(x => x.Length()).First());
        }

        private Coridoor GenerateRandomUniqueCoridoor()
        {
            int roomOneIndex = GetRandomRoomsIndex();
            int roomTwoIndex = GetRandomRoomsIndex(roomOneIndex);
            var roomOne = _rooms[roomOneIndex];
            var roomTwo = _rooms[roomTwoIndex];
            var coridoor = new Coridoor(roomOne.Center(), roomTwo.Center());

            EnsureCoridoorHasValidCoordinates(ref coridoor);
            if (DoesCoridoorAlreadyExist(coridoor)) return GenerateRandomUniqueCoridoor();
            else return coridoor;
        }

        private void EnsureCoridoorHasValidCoordinates(ref Coridoor coridoor)
        {
            if (coridoor.StartX == 0) coridoor.StartX = 1;
            if (coridoor.StartY == 0) coridoor.StartY = 1;
            if (coridoor.EndX == 0) coridoor.EndX = 1;
            if (coridoor.EndY == 0) coridoor.EndY = 1;
            if (coridoor.StartX == _cartographer.tiles.Length - 2) coridoor.StartX = _cartographer.tiles.Length - 3;
            if (coridoor.StartY == _cartographer.tiles[0].Length - 2) coridoor.StartY = _cartographer.tiles[0].Length - 3;
            if (coridoor.EndX == _cartographer.tiles.Length - 2) coridoor.EndX = _cartographer.tiles.Length - 3;
            if (coridoor.EndY == _cartographer.tiles[0].Length - 2) coridoor.EndY = _cartographer.tiles[0].Length - 3;
        }

        private int GetRandomRoomsIndex(int previous = -1)
        {
            var index = _random.Next(_rooms.Count);
            if (index == previous) return GetRandomRoomsIndex(previous);
            else return index;
        }

        private bool DoesCoridoorAlreadyExist(Coridoor newCoridoor)
        {
            foreach (var existing in _coridoors)
            {
                if(existing.Equals(newCoridoor)) return true;
            }

            return false;
        }
    }
}