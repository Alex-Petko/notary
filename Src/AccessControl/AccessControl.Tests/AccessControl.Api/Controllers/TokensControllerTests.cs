using AccessControl.Api;
using AccessControl.Application;
using AutoFixture.Xunit2;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Tests;

namespace Api.Controllers;

public class TokensControllerTests
{
    [Theory, AutoData]
    internal async Task Create_Ok_Ok(CreateTokenCommand command, CancellationToken cancellationToken)
    {
        // Arrange
        var (controller, mediator) = Sut();
        mediator.Setup(x => x.Send(command, cancellationToken)).ReturnsAsync(CreateTokenCommandResult.Ok);

        // Act
        var result = await controller.CreateCommand(command, cancellationToken);

        // Assert
        Assert.IsType<OkResult>(result);

        mediator.Verify(x => x.Send(command, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    [Theory, AutoData]
    internal async Task Create_AuthenticationFail_NotFound(CreateTokenCommand command, CancellationToken cancellationToken)
    {
        // Arrange
        var (controller, mediator) = Sut();
        mediator.Setup(x => x.Send(command, cancellationToken)).ReturnsAsync(CreateTokenCommandResult.AuthenticationFail);

        // Act
        var result = await controller.CreateCommand(command, cancellationToken);

        // Assert
        Assert.IsType<NotFoundResult>(result);

        mediator.Verify(x => x.Send(command, cancellationToken), Times.Once);
        mediator.VerifyNoOtherCalls();
    }

    private static (TokensController, Mock<IMediator>) Sut()
        => TestHelper.Sut<TokensController>(x => new(x));
}