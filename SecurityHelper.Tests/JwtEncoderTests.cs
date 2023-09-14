using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using JWT.Algorithms;
using Microsoft.Extensions.Options;
using Moq;
using SecurityHelper.Jwt;
using SecurityHelper.Options;
using SecurityHelper.Tests.Attributes;

namespace SecurityHelper.Tests
{
    public class JwtEncoderTests
    {
        [Theory, AutoMoqData<JwtAlgorithmCustomization>]
        public void Should_GiveValidToken_When_Encoding(string audience, JwtOptions jwtConfiguration,
            [Frozen] Mock<IOptions<JwtOptions>> jwtOptionsMock, JwtEncoder sut)
        {
            // Arrange
            jwtOptionsMock.SetupGet(x => x.Value).Returns(jwtConfiguration);

            // Act
            var token = sut.Encode(audience, null);

            // Assert
            token.Should()
                .NotBeNull()
                .And.NotBeEmpty();
            token.Split('.').Should().HaveCount(3);
        }

        internal class JwtAlgorithmCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
#pragma warning disable CS0618 // Type or member is obsolete
                fixture.Register<IJwtAlgorithm>(() => new HMACSHA256Algorithm());
#pragma warning restore CS0618 // Type or member is obsolete
            }
        }
    }
}