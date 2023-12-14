using AuthService.Application;
using AuthService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AuthService.Tests;

public class TokensControllerTests
{
    [Fact]
    public async Task Create_OkObjectResult()
    {
        // Arrange
        var tokenGenerator = new Mock<ITokenGenerator>();
        tokenGenerator.Setup(x => x.ExecuteAsync(It.IsAny<CreateTokenDto>())).ReturnsAsync("3");
        var controller = new TokensController(tokenGenerator.Object);

        var dto = new CreateTokenDto("1", "2");

        // Act
        var result = await controller.Create(dto);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        var token = Assert.IsType<string>(okObjectResult.Value);
        Assert.Equal("3", token);
    }

    [Fact]
    public async Task Create_BadRequestResult()
    {
        // Arrange
        var tokenGenerator = new Mock<ITokenGenerator>();
        tokenGenerator.Setup(x => x.ExecuteAsync(It.IsAny<CreateTokenDto>())).ReturnsAsync(value: null);
        var controller = new TokensController(tokenGenerator.Object);

        var dto = new CreateTokenDto("1", "2");

        // Act
        var result = await controller.Create(dto);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
}
