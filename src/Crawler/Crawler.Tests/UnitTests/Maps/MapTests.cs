using System.Collections.Generic;
using Crawler.Maps;
using Crawler.Models;
using Crawler.Queryables.Entities;
using Crawler.Queryables.Entities.Components;
using Crawler.Queryables.Tiles;
using Moq;
using Xunit;

namespace Crawler.Tests.UnitTests.Maps
{
    public class MapTests
    {
        [Fact]
        public void ShouldPlaceEntityInMap()
        {
            var size = new Point(10, 10);
            var location = new Point(5,5);
            Map map = new Map(size);
            var mockEntity = new Mock<Entity>();
            mockEntity.Setup(x => x.CanMove()).Returns(false);
            map.Add(mockEntity.Object, location);

            //TODO: Improve this assertion with a different query
            Assert.False(map.Get(location).CanMove());
        }

        [Fact]
        public void ShouldInitialiseBlankNonVoidMap()
        {
            var size = new Point(10,10);
            Map map = new Map(size);

            for (int x = 0; x < size.X; x++)
            {
                for (int y = 0; y < size.Y; y++)
                {
                    var tile = map.Get(new Point(x,y));
                    Assert.NotNull(tile);
                    Assert.Equal(TileType.Floor, tile.Type);
                }
            }
        }

        [Fact]
        public void ShouldPlaceEntity()
        {
            var entity = new Entity();
            entity.Add(new GraphicComponent(Graphic.HumanWarrior));
            var size = new Point(10, 10);
            Map map = new Map(size);
            var location = new Point(5, 5);

            map.Add(entity, location);
            IList<Graphic> graphics = new List<Graphic>();
            map.Get(location).GetGraphics(ref graphics);

           Assert.Contains(Graphic.HumanWarrior, graphics);
        }
    }
}
