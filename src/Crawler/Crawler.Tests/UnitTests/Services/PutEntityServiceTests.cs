using Crawler.Maps;
using Crawler.Queryables.Entities;
using Crawler.Services;
using Moq;
using Xunit;

namespace Crawler.Tests.UnitTests.Services
{
    public class PutEntityServiceTests
    {
        [Fact]
        public void ShouldPlaceEntityAtLocation()
        {
            var position = new Point(5, 5);
            Mock<Entity> entityMock = new Mock<Entity>();
            Mock<IMap> mapMock = new Mock<IMap>();
            var service = new PutEntityService(mapMock.Object);
            
            service.Put(entityMock.Object, position);

            mapMock.Verify(x => x.Add(entityMock.Object, position));
            entityMock.Verify(x => x.SetPosition(position));
        }
    }
}