using AutoFixture.AutoMoq;
using AutoFixture;
using AutoFixture.Xunit2;

namespace SecurityHelper.Tests.Attributes
{
    internal class AutoMoqDataAttribute : AutoDataAttribute
    {
        // Change to a lambda with body if you need to further custome the fixture
        public AutoMoqDataAttribute() : base(() => new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }

    internal class AutoMoqDataAttribute<T> : AutoDataAttribute where T : ICustomization, new()
    {
        public AutoMoqDataAttribute()
            : base(() => new Fixture()
            .Customize(new CompositeCustomization(new AutoMoqCustomization(), new T())))
        {
        }
    }
}