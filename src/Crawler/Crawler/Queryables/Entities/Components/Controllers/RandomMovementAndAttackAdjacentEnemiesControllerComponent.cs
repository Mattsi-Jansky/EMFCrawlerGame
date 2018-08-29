using System;
using Crawler.Commands;

namespace Crawler.Queryables.Entities.Components.Controllers
{
    public class RandomMovementAndAttackAdjacentEnemiesControllerComponent : ControllerComponent
    {
        private Random _random;

        public RandomMovementAndAttackAdjacentEnemiesControllerComponent(Random random, Guid id) : base(id)
        {
            this._random = random;
        }

        public override ICommand GetCommand()
        {
            var opponentDirection = MobQueryService.GetNearbyOpponentDirection();
            if (opponentDirection.HasValue)
            {
                return CommandFactory.Move(new Point(opponentDirection.Value.X, opponentDirection.Value.Y));
            }
            else if (ShouldMove())
            {
                bool isVertical = _random.Next(2) == 0;

                int x = isVertical ? 0 : OneOrMinusOne();
                int y = isVertical ? OneOrMinusOne() : 0;

                return CommandFactory.Move(new Point(x, y));
            }
            else return null;
        }

        private bool ShouldMove()
        {
            return _random.Next(5) == 0;
        }

        private int OneOrMinusOne()
        {
            return _random.Next(2) == 0 ? 1 : -1;
        }
    }
}