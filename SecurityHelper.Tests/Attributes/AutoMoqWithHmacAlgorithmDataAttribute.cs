using AutoFixture.AutoMoq;
using AutoFixture;
using AutoFixture.Xunit2;
using JWT.Algorithms;

namespace SecurityHelper.Tests.Attributes
{
    internal class AutoMoqWithHmacAlgorithmDataAttribute : AutoDataAttribute
    {
        // Change to a lambda with body if you need to further custome the fixture
        public AutoMoqWithHmacAlgorithmDataAttribute() : base(() =>
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
#pragma warning disable CS0618 // Type or member is obsolete
            fixture.Register<IJwtAlgorithm>(() => new HMACSHA256Algorithm());
#pragma warning restore CS0618 // Type or member is obsolete
            return fixture;
        })
        {
        }
    }
}