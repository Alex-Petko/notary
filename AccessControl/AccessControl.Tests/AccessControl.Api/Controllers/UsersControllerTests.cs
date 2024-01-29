using AccessControl.Api;
using AccessControl.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Tests;

namespace Api.Controllers;

public class UsersControllerTests
{
    [Theory, CustomAutoData]
    internal async Task Create_Ok_Ok(CreateUserCommand command, CancellationToken cancellationToken)
    {
        // Arrange
        var (controller, mediator) = Sut();
        mediator.Setup(x => x.Send(command, cancellationToken)).ReturnsAsync(CreateUserCommandResult.Ok);

        // Act
        var result = await controller.CreateCommand(command, cancellationToken);

        // Assert
        Assert.IsType<OkResult>(result);

        mediator.Verify(x => x.Send(command, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    internal async Task Create_CreateUserFail_Conflict(CreateUserCommand command, CancellationToken cancellationToken)
    {
        // Arrange
        var (controller, mediator) = Sut();
        mediator.Setup(x => x.Send(command, cancellationToken)).ReturnsAsync(CreateUserCommandResult.CreateUserFail);

        // Act
        var result = await controller.CreateCommand(command, cancellationToken);

        // Assert
        Assert.IsType<ConflictResult>(result);

        mediator.Verify(x => x.Send(command, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    private static (UsersController, Mock<IMediator>) Sut()
        => TestHelper.Sut<UsersController>(x => new(x));
}