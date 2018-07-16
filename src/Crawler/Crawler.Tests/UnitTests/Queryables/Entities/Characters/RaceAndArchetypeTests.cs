using Crawler.Models;
using Crawler.Queryables.Entities.Characters;
using Xunit;

namespace Crawler.Tests.UnitTests.Queryables.Entities.Characters
{
    public class RaceAndArchetypeTests
    {
        [InlineData(Race.Human, Archetype.Warrior, Graphic.HumanWarrior)]
        [InlineData(Race.Human, Archetype.Mage, Graphic.HumanMage)]
        [InlineData(Race.Human, Archetype.Druid, Graphic.HumanDruid)]
        [InlineData(Race.Elf, Archetype.Priest, Graphic.ElfPriest)]
        [InlineData(Race.Elf, Archetype.Ranger, Graphic.ElfRanger)]
        [InlineData(Race.Elf, Archetype.Mindcrafter, Graphic.ElfMindcrafter)]
        [InlineData(Race.HalfOrc, Archetype.Warlock, Graphic.HalfOrcWarlock)]
        [InlineData(Race.Klackon, Archetype.Paladin, Graphic.KlackonPaladin)]
        [InlineData(Race.Skeleton, Archetype.Paladin, Graphic.SkeletonPaladin)]
        [Theory]
        public void CanGetCorrectGraphicFromRaceAndArchetype(Race race, Archetype archetype, Graphic expected)
        {
            var result = race.GetGraphic(archetype);
            Assert.Equal(expected, result);
        }
    }
}
