using AuthService.Application;
using AuthService.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;

namespace AuthService.Tests;

public class TokensControllerTests
{
    [Fact]
    public async Task Create_OkResult()
    {
        // Arrange
        var tokenGenerator = new Mock<ITokenGenerator>();
        tokenGenerator
            .Setup(x => 
                x.ExecuteAsync(
                    It.IsAny<CreateTokenDto>(), 
                    It.IsAny<string>(), 
                    It.IsAny<int>()))
            .ReturnsAsync("3");

        var dateTimeProvider = new Mock<IDateTimeProvider>();
        dateTimeProvider.Setup(x => x.UtcNow).Returns(DateTime.UnixEpoch);

        var jwtOptions = new Mock<IOptionsSnapshot<JwtOptions>>();
        jwtOptions.Setup(x => x.Value.Key).Returns(new string('x', 16));
        jwtOptions.Setup(x => x.Value.ExpiresMinutes).Returns(1);

        var controller = new TokensController(
            tokenGenerator.Object, 
            dateTimeProvider.Object, 
            jwtOptions.Object);

        controller.ControllerContext.HttpContext = new DefaultHttpContext();

        var dto = new CreateTokenDto("1", "2");

        // Act
        var result = await controller.Create(dto); 

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task Create_BadRequestResult()
    {
        // Arrange
        var tokenGenerator = new Mock<ITokenGenerator>();
        tokenGenerator
            .Setup(x =>
                x.ExecuteAsync(
                    It.IsAny<CreateTokenDto>(),
                    It.IsAny<string>(),
                    It.IsAny<int>()))
            .ReturnsAsync(value: null);

        var dateTimeProvider = new Mock<IDateTimeProvider>();
        dateTimeProvider.Setup(x => x.UtcNow).Returns(DateTime.UnixEpoch);

        var jwtOptions = new Mock<IOptionsSnapshot<JwtOptions>>();
        jwtOptions.Setup(x => x.Value.Key).Returns(new string('x', 16));
        jwtOptions.Setup(x => x.Value.ExpiresMinutes).Returns(1);

        var controller = new TokensController(
            tokenGenerator.Object,
            dateTimeProvider.Object,
            jwtOptions.Object);

        var dto = new CreateTokenDto("1", "2");

        // Act
        var result = await controller.Create(dto);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
}
