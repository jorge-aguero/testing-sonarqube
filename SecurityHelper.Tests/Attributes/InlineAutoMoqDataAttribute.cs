using AutoFixture.Xunit2;

namespace SecurityHelper.Tests.Attributes
{
    internal class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
    {
        // Change to a lambda with body if you need to further custome the fixture
        public InlineAutoMoqDataAttribute(params object[] objects) : base(new AutoMoqDataAttribute(), objects) { }
    }
}