using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using SecurityHelper.Tests.Attributes;

namespace SecurityHelper.Tests
{
    public class TriangleRectangleValidatorTests
    {
        [Theory]
        [InlineAutoMoqData(3, 4, 5)]
        internal void Should_ReceiveTrue_When_SendingValidTriangleRectangleSizes(double sizeA, double sizeB, double diagonnal,
            TriangleRectangleValidator sut)
        {
            // Act
            var actual = sut.IsTriangleRectangle(sizeA, sizeB, diagonnal);

            // Assert
            actual.Should().BeTrue();
        }

        [Theory]
        [AutoMoqData]
        internal void Should_ReceiveFalse_When_SendingInvalidTriangleRectangleSizes(double sizeA, double sizeB, double diagonnal,
            TriangleRectangleValidator sut)
        {
            // Act
            var actual = sut.IsTriangleRectangle(sizeA, sizeB, diagonnal);

            // Assert
            actual.Should().BeFalse();
        }

        [Theory]
        [InlineAutoMoqData(3, 4, 5)]
        [AutoMoqData]
        internal void Should_Log_When_SendingValidOrInvalidTriangleRectangleSizes(double sizeA, double sizeB, double diagonnal,
            [Frozen] Mock<ILogger> logger, TriangleRectangleValidator sut)
        {
            // Act
            _ = sut.IsTriangleRectangle(sizeA, sizeB, diagonnal);

            // Assert
            logger.Verify(x => x.LogInformation(It.Is<string>(y => y == "Request made to IsTriangleRectangle: {SideA}, {SideB} and {Diagonnal}"), sizeA, sizeB, diagonnal));
        }
    }
}
