using AccessControl.Application;

namespace Application.Services;

public class DateTimeProviderTests
{
    [Fact]
    public void UtcNow_Ok_Ok()
    {
        // Arrange
        var provider = new DateTimeProvider();

        // Act
        var utcNow = provider.UtcNow;

        // Assert
        Assert.Equal(0, DateTime.UtcNow.Subtract(utcNow).Milliseconds);
    }
}
