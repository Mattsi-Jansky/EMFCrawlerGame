using Crawler.Commands;
using Crawler.Models;
using Crawler.Queryables.Entities.Characters;
using Crawler.Tests.Support;
using Xunit;

namespace Crawler.Tests.FeatureTests
{
    public class MoveTests : BaseCrawlGameTests
    {
        [Theory]
        [InlineData(1,0)]
        [InlineData(0, 1)]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        public void EntitiesShouldMoveFromOneTileToAnother(int directionX, int directionY)
        {
            Point startingPoint = new Point(5, 5);
            Point direction = new Point(directionX, directionY);
            Point targetPoint = startingPoint.Add(direction);
            var graphic = Graphic.HumanWarrior;
            var id = InitialiseBlankGameWithCharacter(Race.Human, Archetype.Warrior);
            Game.Tick();
            AssertTileContainsGraphic(startingPoint, graphic);

            var moveCommand = Game.GetCommandFactory(id).Move(direction);
            Game.AddCommand(id, moveCommand);
            Game.Tick();

            AssertTileContainsGraphic(targetPoint, graphic);
        }

        [InlineData(1, 1)]
        [InlineData(0, 2)]
        [InlineData(2, 0)]
        [InlineData(999, 999)]
        [InlineData(-999, -999)]
        [InlineData(-1, -1)]
        [InlineData(1, 1)]
        [Theory]
        public void GivenTargetPointMoreThan1TileAway_WhenMoving_ShouldDoNothing(int directionX, int directionY)
        {
            Point startingPoint = new Point(5, 5);
            Point direction = new Point(directionX, directionY);
            var graphic = Graphic.HumanWarrior;
            var id = InitialiseBlankGameWithCharacter(Race.Human, Archetype.Warrior);
            Game.Tick();
            AssertTileContainsGraphic(startingPoint, graphic);

            var moveCommand = Game.GetCommandFactory(id).Move(direction);
            Game.AddCommand(id, moveCommand);
            Game.Tick();

            AssertTileContainsGraphic(startingPoint, graphic);
        }
    }
}