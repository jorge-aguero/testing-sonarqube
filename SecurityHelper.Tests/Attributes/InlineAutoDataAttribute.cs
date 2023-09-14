using AutoFixture;
using AutoFixture.Xunit2;

namespace SecurityHelper.Tests.Attributes
{
    internal class InlineAutoDataAttribute<T> : InlineAutoDataAttribute where T : ICustomization, new()
    {
        // Change to a lambda with body if you need to further custome the fixture
        public InlineAutoDataAttribute(params object[] objects) : base(new AutoDataAttribute<T>(), objects) { }
    }
}