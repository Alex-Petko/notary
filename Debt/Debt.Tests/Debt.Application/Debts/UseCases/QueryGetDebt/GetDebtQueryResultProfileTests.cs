using AutoMapper;
using DebtManager.Application;

namespace Application.Profiles;

public class GetDebtQueryResultProfileTests
{
    [Fact]
    public void AutoMapperTest()
    {
        // Arrange
        var configuration = new MapperConfiguration(configure => configure.AddProfile<GetDebtQueryResultProfile>());

        // Act
        // Assert
        configuration.AssertConfigurationIsValid();
    }
}
