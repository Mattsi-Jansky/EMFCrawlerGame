using System;
using Crawler.Maps;
using Crawler.Queryables.Entities;
using Crawler.Services;
using Crawler.Tests.Support;
using Moq;
using Xunit;

namespace Crawler.Tests.UnitTests.Services
{
    public class MoveEntityServiceTests : BaseCrawlGameTests
    {
        [Fact]
        public void ShouldMoveEntity()
        {
            var fromPosition = new Point(1, 1);
            var targetPosition = new Point(9, 9);
            Mock<Entity> entityMock = new Mock<Entity>();
            Mock<IMap> mapMock = new Mock<IMap>();
            var service = new MoveEntityService(mapMock.Object);
            entityMock.Setup(x => x.GetPosition(ref It.Ref<Point?>.IsAny))
                .Callback(new GetPositionCallback((ref Point? position) => { position = fromPosition; }));

            service.Move(entityMock.Object, targetPosition);

            entityMock.Verify(x => x.GetPosition(ref It.Ref<Point?>.IsAny));
            mapMock.Verify(x => x.Remove(entityMock.Object, fromPosition));
            mapMock.Verify(x => x.Add(entityMock.Object, targetPosition));
            entityMock.Verify(x => x.SetPosition(targetPosition));
        }

        delegate void GetPositionCallback(ref Point? position);

        [Fact]
        public void WhenEntityHasNoPosition_Move_ThrowsInvalidOperationException()
        {
            var fromPosition = new Point(1, 1);
            var targetPosition = new Point(9, 9);
            Mock<Entity> entityMock = new Mock<Entity>();
            Mock<IMap> mapMock = new Mock<IMap>();
            var service = new MoveEntityService(mapMock.Object);

            var exception = Assert.Throws<InvalidOperationException>(() => service.Move(entityMock.Object, targetPosition));
            Assert.Equal("Entity must have a position to move", exception.Message);
        }
    }
}
