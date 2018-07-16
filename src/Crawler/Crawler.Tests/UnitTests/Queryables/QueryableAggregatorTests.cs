using System;
using Crawler.Queryables;
using Moq;
using Xunit;

namespace Crawler.Tests.UnitTests.Queryables
{
    public class QueryableAggregatorTests
    {
        [InlineData(true)]
        [InlineData(false)]
        [Theory]
        public void ShouldForwardCanMove(bool expected)
        {
            Mock<IQueryable> mockQueryable = new Mock<IQueryable>();
            mockQueryable.Setup(x => x.CanMove()).Returns(expected);
            QueryableAggregator<IQueryable> aggregator = new QueryableAggregator<IQueryable>();
            aggregator.Add(mockQueryable.Object);
            var result = aggregator.CanMove();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GivenSomeQueryablesCanMoveAndOthersCannot_WhenCanMoveCalled_ShouldReturnFalse()
        {
            Mock<IQueryable> mockQueryable = new Mock<IQueryable>();
            Mock<IQueryable> mockQueryable2 = new Mock<IQueryable>();
            Mock<IQueryable> mockQueryable3 = new Mock<IQueryable>();
            mockQueryable.Setup(x => x.CanMove()).Returns(true);
            mockQueryable2.Setup(x => x.CanMove()).Returns(false);
            mockQueryable3.Setup(x => x.CanMove()).Returns(false);
            QueryableAggregator<IQueryable> aggregator = new QueryableAggregator<IQueryable>();
            aggregator.Add(mockQueryable.Object);
            aggregator.Add(mockQueryable2.Object);
            aggregator.Add(mockQueryable3.Object);
            var result = aggregator.CanMove();

            Assert.False(result);
        }

        [Fact]
        public void GetPosition_ShouldReturnFirsNonNulltResult()
        {
            Point expectedPosition = new Point(9,9);
            Point unexepctedPosition = new Point(1,1);
            Mock<IQueryable> mockQueryable = new Mock<IQueryable>();
            Mock<IQueryable> mockQueryable2 = new Mock<IQueryable>();
            Mock<IQueryable> mockQueryable3 = new Mock<IQueryable>();
            Mock<IQueryable> mockQueryable4 = new Mock<IQueryable>();
            mockQueryable2.Setup(x => x.GetPosition(ref It.Ref<Point?>.IsAny))
                .Callback(new GetPositionCallback((ref Point? position) => { position = null; }));
            mockQueryable3.Setup(x => x.GetPosition(ref It.Ref<Point?>.IsAny))
                .Callback(new GetPositionCallback((ref Point? position) => { position = expectedPosition; }));
            mockQueryable4.Setup(x => x.GetPosition(ref It.Ref<Point?>.IsAny))
                .Callback(new GetPositionCallback((ref Point? position) => { position = unexepctedPosition; }));
            QueryableAggregator<IQueryable> aggregator = new QueryableAggregator<IQueryable>();
            aggregator.Add(mockQueryable.Object);
            aggregator.Add(mockQueryable2.Object);
            aggregator.Add(mockQueryable3.Object);
            aggregator.Add(mockQueryable4.Object);

            var result = new Point?();
            aggregator.GetPosition(ref result);

            Assert.NotNull(result);
            Assert.Equal(expectedPosition, result.Value);
        }

        delegate void GetPositionCallback(ref Point? position);

        [Fact]
        public void GivenNoPositionComponent_GetPosition_ShouldThrowInvalidOperationException()
        {
            QueryableAggregator<IQueryable> aggregator = new QueryableAggregator<IQueryable>();
            var result = new Point?();
            Assert.Throws<InvalidOperationException>(() => aggregator.GetPosition(ref result));
        }
    }
}
