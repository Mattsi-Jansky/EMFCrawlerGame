using System;
using System.Threading;
using Crawler.Models;
using Crawler.Queryables.Entities.Characters;
using Crawler.Tests.Support;
using Xunit;

namespace Crawler.Tests.UnitTests
{
    public class GameTests : BaseCrawlGameTests
    {
        [Fact]
        public void WhenAddingCommand_ShouldBeThreadSafe()
        {
            InitialiseBlankGame();
            Guid character1 = Game.AddCharactersService.Add(new NewCharacterRequest(Race.Human, Archetype.Warrior));
            var thread1 = new Thread(() => AddManyCommands(character1));
            Guid character2 = Game.AddCharactersService.Add(new NewCharacterRequest(Race.Human, Archetype.Warrior));
            var thread2 = new Thread(() => AddManyCommands(character2));
            Guid character3 = Game.AddCharactersService.Add(new NewCharacterRequest(Race.Human, Archetype.Warrior));
            var thread3 = new Thread(() => AddManyCommands(character3));
            Guid character4 = Game.AddCharactersService.Add(new NewCharacterRequest(Race.Human, Archetype.Warrior));
            var thread4 = new Thread(() => AddManyCommands(character4));
            Game.Tick();

            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();
            thread4.Join();

            Game.Tick();
        }

        private void AddManyCommands(Guid id)
        {
            for (int i = 0; i < 1000; i++)
            {
                Game.AddCommand(id, Game.GetCommandFactory(id).Move(new Point(1,1)));
            }
        }

        [Fact]
        public void GivenCommandAlreadyAdded_WhenAddingCommand_ShouldNotCauseException()
        {
            InitialiseBlankGame();
            var id = Game.AddCharactersService.Add(new NewCharacterRequest(Race.Human, Archetype.Warrior));
            Game.Tick();
            Game.AddCommand(id, Game.GetCommandFactory(id).Move(new Point(1, 1)));

            Game.AddCommand(id, Game.GetCommandFactory(id).Move(new Point(1, 1)));
        }
    }
}

