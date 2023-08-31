using AutoFixture;
using JWT.Algorithms;
using Microsoft.Extensions.Options;
using Moq;
using SecurityHelper.Jwt;
using SecurityHelper.Options;

namespace SecurityHelper.Tests
{
    public class JwtEncoderTests
    {
        private readonly Mock<IOptions<JwtOptions>> _jwtOptionsMock;
        private readonly IJwtAlgorithm _jwtAlgorithmMock;
        private readonly JwtEncoder _jwtEncoder;
        private readonly Fixture _fixture;

        public JwtEncoderTests()
        {
            _fixture = new Fixture();

            _jwtOptionsMock = new Mock<IOptions<JwtOptions>>();
            var jwtConfiguration = _fixture.Create<JwtOptions>();
            _jwtOptionsMock.SetupGet(x => x.Value).Returns(jwtConfiguration);

#pragma warning disable CS0618 // Type or member is obsolete
            _jwtAlgorithmMock = new HMACSHA256Algorithm();
#pragma warning restore CS0618 // Type or member is obsolete

            _jwtEncoder = new JwtEncoder(_jwtOptionsMock.Object, _jwtAlgorithmMock);
        }

        [Fact]
        public void Encode_ShouldGiveValidToken()
        {
            // Arrange
            var audience = _fixture.Create<string>();

            // Act
            var token = _jwtEncoder.Encode(audience, null);

            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
            Assert.Equal(3, token.Split('.').Length);
        }
    }
}