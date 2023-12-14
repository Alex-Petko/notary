using AuthService.Application;
using AuthService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AuthService.Tests;

public class UsersControllerTests
{
    [Fact]
    public async Task Create_Ok()
    {
        // Arrange
        var userCreator = new Mock<IUserCreator>();
        userCreator.Setup(x => x.ExecuteAsync(It.IsAny<CreateUserDto>())).ReturnsAsync(true);

        var controller = new UsersController(userCreator.Object);

        var dto = new CreateUserDto("1", "2");

        // Act
        var result = await controller.Create(dto);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task Create_BadRequestResult()
    {
        // Arrange
        var userCreator = new Mock<IUserCreator>();
        userCreator.Setup(x => x.ExecuteAsync(It.IsAny<CreateUserDto>())).ReturnsAsync(false);

        var controller = new UsersController(userCreator.Object);

        var dto = new CreateUserDto("1", "2");

        // Act
        var result = await controller.Create(dto);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
}
