using AutoFixture;
using AutoFixture.Xunit2;

namespace SecurityHelper.Tests.Attributes
{
    internal class AutoDataAttribute<T> : AutoDataAttribute where T : ICustomization, new()
    {
        public AutoDataAttribute()
            : base(() => new Fixture()
            .Customize(new T()))
        {
        }
    }
}