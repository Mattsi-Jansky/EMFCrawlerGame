using System;
using System.Drawing;

namespace DungeonGenerators
{
    public struct Coridoor
    {
        public int StartX, StartY;
        public int EndX, EndY;

        public Coridoor(Point start, Point end)
        {
            if (start.X < end.X)
            {
                StartX = start.X;
                StartY = start.Y;
                EndX = end.X;
                EndY = end.Y;
            }
            else
            {
                StartX = end.X;
                StartY = end.Y;
                EndX = start.X;
                EndY = start.Y;
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Coridoor)) return false;
            var coridoor = (Coridoor) obj;
            return ((coridoor.StartX == StartX && coridoor.StartY == StartY
                   && coridoor.EndX == EndX && coridoor.EndY == EndY)
                || (coridoor.EndX == StartX && coridoor.EndY == StartY
                    && coridoor.StartX == EndX && coridoor.StartY == EndY));
        }

        public int Length()
        {
            return Math.Abs(StartX - EndX) + Math.Abs(StartY - EndY);
        }
    }
}