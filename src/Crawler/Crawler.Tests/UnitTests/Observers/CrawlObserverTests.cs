using System.Threading;
using Crawler.Maps.Initialisers;
using Crawler.Models;
using Crawler.Queryables.Entities.Characters;
using Crawler.Tests.Support;
using Xunit;

namespace Crawler.Tests.UnitTests.Observers
{
    public class CrawlObserverTests : BaseCrawlGameTests
    {
        [Fact]
        public void ShouldSubstituteWallTilesForWallGraphic()
        {
            MapInitialiser = new WalledBlankMapInitialiser(new Point(10,10));
            InitialiseGame();
            var result = ObserveTile(new Point(0, 0));

            Assert.Equal(Graphic.WallGray, result.Graphics[0]);
        }

        [Fact]
        public void ShouldSubstituteFloorTilesForFloorGraphic()
        {
            MapInitialiser = new WalledBlankMapInitialiser(new Point(10, 10));
            InitialiseGame();
            var result = ObserveTile(new Point(1, 1));

            Assert.Equal(Graphic.FloorGray, result.Graphics[0]);
        }

        [Fact]
        public void ShouldSubstituteHumanWarriorCharacterForHumanWarriorGraphic()
        {
            InitialiseBlankGameWithCharacter(Race.Human, Archetype.Warrior);
            Game.Tick();
            var result = ObserveTile(new Point(5, 5));

            Assert.Equal(Graphic.HumanWarrior, result.Graphics[1]);
        }

        //todo improve this test once movement command is working
        [Fact]
        public void ObserveShouldBeThreadSafe()
        {
            MapInitialiser = new WalledBlankMapInitialiser(new Point(10, 10));
            InitialiseBlankGame();

            var thread1 = new Thread(CallTickManyTimes);
            var thread2 = new Thread(ObserveManyTilesManyTiles);
            var thread3 = new Thread(CallTickManyTimes);
            var thread4 = new Thread(ObserveManyTilesManyTiles);

            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();
            thread4.Join();
        }

        // Incirectly calls Observer.Update()
        private void CallTickManyTimes()
        {
            for (int i = 0; i < 1000; i++)
            {
                Game.Tick();
            }
        }

        private void ObserveManyTilesManyTiles()
        {
            for (int i = 0; i < 1000; i++)
            {
                for (int x = 0; x < 5; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        var result = Observer.Observe(new Point(x, y));
                        Assert.Contains(Graphic.FloorGray, result.Graphics);
                    }
                }
            }
        }
    }
}
