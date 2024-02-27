using AutoFixture;
using AutoFixture.Xunit2;

namespace Shared.Tests;

public sealed class CustomAutoData : AutoDataAttribute
{
    public CustomAutoData() : base(FixtureFactory)
    {
    }

    private static IFixture FixtureFactory()
    {
        var fixture = new Fixture().Customize(new CompositeCustomization(
            new SupportMutableValueTypesCustomization()));

        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        return fixture;
    }
}