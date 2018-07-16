using System;

namespace Crawler
{
    public struct Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point Add(Point point)
        {
            return new Point(X + point.X, Y + point.Y);
        }

        public int AbsSum()
        {
            return Math.Abs(X + Y);
        }
    }
}
