using System;

namespace Crawler.Models
{
    public class Dice
    {
        private readonly int _rolls;
        private readonly int _sides;

        public Dice(int rolls, int sides)
        {
            this._rolls = rolls;
            this._sides = sides;
        }

        public int[] RollAll(Random random)
        {
            int[] result = new int[_rolls];

            for (int i = 0; i < _rolls; i++)
            {
                result[i] = RollOne(random);
            }

            return result;
        }

        public int RollOne(Random random)
        {
            return random.Next(_sides) + 1;
        }

        public int Average()
        {
            return (_sides / 2) * _rolls;
        }

        public bool Greater(Dice b)
        {
            return Average() > b.Average();
        }
    }
}