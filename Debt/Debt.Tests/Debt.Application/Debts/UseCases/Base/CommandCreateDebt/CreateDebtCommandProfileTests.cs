using AutoMapper;
using DebtManager.Application;

namespace Application.Profiles;

public class CreateDebtCommandProfileTests
{
    [Fact]
    public void AutoMapperTest()
    {
        // Arrange
        var configuration = new MapperConfiguration(configure => configure.AddProfile<CreateDebtCommandProfile>());

        // Act
        // Assert
        configuration.AssertConfigurationIsValid();
    }
}
