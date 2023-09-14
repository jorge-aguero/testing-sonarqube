using FluentAssertions;

namespace SecurityHelper.Tests
{
    public class NameGeometricFigureRequestTests
    {
        [Fact]
        public void Should_NotThrowException_When_SidesWithinRange()
        {
            // Arrange
            var sides = (new Random()).Next(3, 6);

            // Act
            var act = () => new NameGeometricFigureRequest { Sides = sides };

            // Assert
            act.Should().NotThrow<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(2)]
        [InlineData(7)]
        public void Should_NotThrowException_When_SidesOutsideOfRange(int sides)
        {
            // Arrange
            const string expectedMessage =
                $"Specified argument was out of the range of valid values. (Parameter '{nameof(NameGeometricFigureRequest.Sides)}')";

            // Act
            var act = () => new NameGeometricFigureRequest { Sides = sides };

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage(expectedMessage)
                .Where(ex => ex.ParamName == nameof(NameGeometricFigureRequest.Sides));
        }
    }
}
