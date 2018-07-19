using System;
using Crawler.Maps.Initialisers;
using Crawler.Models;
using Crawler.Observers;
using Crawler.Queryables.Entities.Characters;
using Xunit;

namespace Crawler.Tests.Support
{
    public class BaseCrawlGameTests
    {
        protected CrawlGame Game { get; set; }
        protected ICrawlObserver Observer { get; set; }
        protected BaseMapInitialiser MapInitialiser { get; set; }

        protected Guid InitialiseBlankGameWithCharacter()
        {
            return InitialiseBlankGameWithCharacter(Race.Human, Archetype.Warrior);
        }

        protected Guid InitialiseBlankGameWithCharacter(Race race, Archetype archetype)
        {
            MapInitialiser = new TestBlankMapInitialiser();
            InitialiseGame();
            return Game.AddCharactersService.Add(new NewCharacterRequest(race, archetype));
        }

        protected void InitialiseBlankGame()
        {
            MapInitialiser = new TestBlankMapInitialiser();
            InitialiseGame();
        }

        protected void InitialiseGame()
        {
            Game = new CrawlGame(MapInitialiser, new TestEntityPlacer());
            Observer = Game.Observer;
        }

        public void AssertTileContainsGraphic(Point location, Graphic graphic)
        {
            var tile = ObserveTile(location);
            Assert.Contains(graphic, tile);
        }

        protected Graphic[] ObserveTile(Point location)
        {
            return Observer.Observe(location);
        }
    }
}

