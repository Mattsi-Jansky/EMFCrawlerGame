using System;
using System.Collections.Generic;
using System.Text;
using Crawler.Tests.Support;
using Xunit;

namespace Crawler.Tests.UnitTests.Queryables.Entities.Components
{
    public class CharacterNameDisplayTextComponentTests : BaseCrawlGameTests
    {
        [Fact]
        public void ShouldAddCharacterNameToDisplayText()
        {
            InitialiseBlankGameWithCharacter();
            Game.Tick();

            var tileGraphics = Observer.Observe(new Point(5, 5));

            Assert.Equal(TestCharacterName, tileGraphics.Text);
        }
    }
}
