using System.Collections.Generic;
using Crawler.Commands;
using Crawler.Maps;
using Crawler.ObjectResolvers;
using Crawler.Queryables.Entities;
using Crawler.Tests.Support;
using Moq;
using Xunit;

namespace Crawler.Tests.UnitTests.ObjectResolvers
{
    public class ObjectResolverTests
    {
        [Fact]
        public void WhenResolvingMap_ShouldKeepSameInstanceInitialised()
        {
            ObjectResolver resolver = new ObjectResolver();
            Mock<IMap> mapMock = new Mock<IMap>();
            resolver.Initialise(mapMock.Object, new TestEntityPlacer(), new EntitiesCollection());

            var result = resolver.Resolve<IMap>();
            Assert.Equal(mapMock.Object, result);
        }

        [Fact]
        public void WhenResolvingMoveCommand_ShouldOverrideParameters()
        {
            ObjectResolver resolver = new ObjectResolver();
            Mock<IMap> mapMock = new Mock<IMap>();
            var entity = new Entity();
            var targetPosition = new Point(5, 6);
            resolver.Initialise(mapMock.Object, new TestEntityPlacer(), new EntitiesCollection());

            var result =
                resolver.Resolve<MoveCommand>(new Dictionary<string, object>
                {
                    {"id", entity.Id},
                    {"direction", targetPosition}
                });
            Assert.NotNull(result);
        }
    }
}
