using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using SecurityHelper.Tests.Attributes;

namespace SecurityHelper.Tests
{
    public class GeometricFigureNamerTests
    {
        [Theory]
        [InlineAutoData(3, "Triangle")]
        [InlineAutoData(4, "Quadrilateral")]
        [InlineAutoData(5, "Pentagon")]
        [InlineAutoData(6, "Hexagon")]
        internal void Should_ReturnExpectedFigure_When_PassingNameRequest(int sides, string description, GeometricFigureNamer sut)
        {
            // Arrange
            var expected = new NamedGeometricFigure
            {
                Sides = sides,
                Description = description
            };

            // Act
            var actual = sut.NameGeometricFigure(new NameGeometricFigureRequest { Sides = sides });

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [AutoData<NameGeometricFigureRequestCustomization>]
        internal void Should_NotReturnNull_WhenPassingNameRequest(NameGeometricFigureRequest request, GeometricFigureNamer sut)
        {
            // Act
            var actual = sut.NameGeometricFigure(request);

            // Assert
            actual.Should().NotBeNull();
        }

        internal class NameGeometricFigureRequestCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                var random = new Random();
                fixture.Register(()
                    => new NameGeometricFigureRequest { Sides = random.Next(3, 6) });
            }
        }
    }
}
