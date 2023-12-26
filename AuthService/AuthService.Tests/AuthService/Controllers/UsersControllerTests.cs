using AuthService.Application;
using AuthService.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AuthService.Tests;

public class UsersControllerTests
{
    [Fact]
    public async Task Create()
    {
        // Arrange
        var mediator = new Mock<IMediator>();
        mediator.Setup(x => x.Send(It.IsAny<CreateUserRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new OkResult());

        var controller = new UsersController(mediator.Object);
        controller.ControllerContext.HttpContext = new DefaultHttpContext();

        // Act
        var result = await controller.Create(null!, default);

        // Assert
        Assert.IsType<OkResult>(result);
    }
}
