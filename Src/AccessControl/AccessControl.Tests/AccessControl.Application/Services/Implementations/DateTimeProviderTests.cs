using AccessControl.Application;

namespace Application.Services;

public class DateTimeProviderTests
{
    [Fact]
    public void UtcNow_Ok_Ok()
    {
        // Arrange
        var provider = new NowGetService();

        // Act
        var utcNow = provider.Now;

        // Assert
        Assert.Equal(0, DateTime.UtcNow.Subtract(utcNow).Milliseconds);
    }
}