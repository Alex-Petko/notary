using AccessControl.Application;
using AutoMapper;
using Moq;
using Shared.Tests;

namespace Application.UseCases;

public class CreateTokenHandlerTests
{
    [Theory, CustomAutoData]
    internal async Task Handle_Ok_OkResult(
        CreateTokenCommand command,
        CancellationToken cancellationToken,
        AuthenticationDto authenticationDto,
        TokenManagerDto tokenManagerDto)
    {
        // Arrange
        var (handler, authenticationService, tokenManager, mapper) = Sut();

        mapper.Setup(x => x.Map<AuthenticationDto>(command)).Returns(authenticationDto);
        mapper.Setup(x => x.Map<TokenManagerDto>(command)).Returns(tokenManagerDto);

        authenticationService.Setup(x => x.AuthenticateAsync(authenticationDto, cancellationToken)).ReturnsAsync(AuthenticationResult.Ok);

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        var createTokenCommandResult = Assert.IsType<CreateTokenCommandResult>(result);
        Assert.Equal(CreateTokenCommandResult.Ok, createTokenCommandResult);

        mapper.Verify(x => x.Map<AuthenticationDto>(command), Times.Once);
        mapper.Verify(x => x.Map<TokenManagerDto>(command), Times.Once);
        mapper.VerifyNoOtherCalls();

        authenticationService.Verify(x => x.AuthenticateAsync(authenticationDto, cancellationToken), Times.Once);
        authenticationService.VerifyNoOtherCalls();

        tokenManager.Verify(x => x.UpdateAsync(tokenManagerDto, cancellationToken), Times.Once);
        tokenManager.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    internal async Task Handle_UserNotFound_AuthenticationFail(
        CreateTokenCommand command,
        CancellationToken cancellationToken,
        AuthenticationDto authenticationDto)
    {
        // Arrange
        var (handler, authenticationService, tokenManager, mapper) = Sut();
        mapper.Setup(x => x.Map<AuthenticationDto>(command)).Returns(authenticationDto);

        authenticationService.Setup(x => x.AuthenticateAsync(authenticationDto, cancellationToken)).ReturnsAsync(AuthenticationResult.UserNotFound);

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        var createTokenCommandResult = Assert.IsType<CreateTokenCommandResult>(result);
        Assert.Equal(CreateTokenCommandResult.AuthenticationFail, createTokenCommandResult);

        mapper.Verify(x => x.Map<AuthenticationDto>(command), Times.Once);
        mapper.VerifyNoOtherCalls();

        authenticationService.Verify(x => x.AuthenticateAsync(authenticationDto, cancellationToken), Times.Once);
        authenticationService.VerifyNoOtherCalls();

        tokenManager.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    internal async Task Handle_PasswordIncorrect_AuthenticationFail(
        CreateTokenCommand command,
        CancellationToken cancellationToken,
        AuthenticationDto authenticationDto)
    {
        // Arrange
        var (handler, authenticationService, tokenManager, mapper) = Sut();
        mapper.Setup(x => x.Map<AuthenticationDto>(command)).Returns(authenticationDto);

        authenticationService.Setup(x => x.AuthenticateAsync(authenticationDto, cancellationToken)).ReturnsAsync(AuthenticationResult.PasswordIncorrect);

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        var createTokenCommandResult = Assert.IsType<CreateTokenCommandResult>(result);
        Assert.Equal(CreateTokenCommandResult.AuthenticationFail, createTokenCommandResult);

        mapper.Verify(x => x.Map<AuthenticationDto>(command), Times.Once);
        mapper.VerifyNoOtherCalls();

        authenticationService.Verify(x => x.AuthenticateAsync(authenticationDto, cancellationToken), Times.Once);
        authenticationService.VerifyNoOtherCalls();

        tokenManager.VerifyNoOtherCalls();
    }

    private (CreateTokenCommandHandler, Mock<IAuthenticationService>, Mock<ITokenManager>, Mock<IMapper>) Sut()
    {
        var authenticationService = new Mock<IAuthenticationService>();
        var tokenManager = new Mock<ITokenManager>();
        var mapper = new Mock<IMapper>();

        return (
            new CreateTokenCommandHandler(authenticationService.Object, tokenManager.Object, mapper.Object),
            authenticationService,
            tokenManager,
            mapper
        );
    }
}