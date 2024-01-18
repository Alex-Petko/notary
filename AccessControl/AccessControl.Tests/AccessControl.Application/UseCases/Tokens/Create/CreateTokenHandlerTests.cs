using AccessControl.Application;
using AccessControl.Domain;
using AccessControl.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Tests;

namespace Application.UseCases;

public class CreateTokenHandlerTests
{
    [Theory, CustomAutoData]
    public async Task Handle_Ok_OkResult(CreateTokenRequest request, CancellationToken cancellationToken, User user)
    {
        // Arrange
        var (handler, tokenManager, passwordHasher, unitOfWork) = Sut();
        unitOfWork.Setup(x => x.Users.FindAsync(request.Login)).ReturnsAsync(user);
        passwordHasher.Setup(x => x.VerifyHashedPassword(user, user.PasswordHash, request.Password)).Returns(PasswordVerificationResult.Success);

        // Act
        var result = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.IsType<OkResult>(result);

        unitOfWork.Verify(x => x.Users.FindAsync(request.Login), Times.Once);
        unitOfWork.VerifyNoOtherCalls();

        passwordHasher.Verify(x => x.VerifyHashedPassword(user, user.PasswordHash, request.Password), Times.Once);
        passwordHasher.VerifyNoOtherCalls();

        tokenManager.Verify(x => x.UpdateAsync(request.Login), Times.Once);
        tokenManager.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Handle_PasswordCorrectButRehashNeeded_OkResult(CreateTokenRequest request, CancellationToken cancellationToken, User user)
    {
        // Arrange
        var (handler, tokenManager, passwordHasher, unitOfWork) = Sut();
        unitOfWork.Setup(x => x.Users.FindAsync(request.Login)).ReturnsAsync(user);
        passwordHasher.Setup(x => x.VerifyHashedPassword(user, user.PasswordHash, request.Password)).Returns(PasswordVerificationResult.SuccessRehashNeeded);

        // Act
        var result = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.IsType<OkResult>(result);

        unitOfWork.Verify(x => x.Users.FindAsync(request.Login), Times.Once);
        unitOfWork.VerifyNoOtherCalls();

        passwordHasher.Verify(x => x.VerifyHashedPassword(user, user.PasswordHash, request.Password), Times.Once);
        passwordHasher.VerifyNoOtherCalls();

        tokenManager.Verify(x => x.UpdateAsync(request.Login), Times.Once);
        tokenManager.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Handle_UserNotFound_NotFoundResult(CreateTokenRequest request, CancellationToken cancellationToken)
    {
        // Arrange
        var (handler, tokenManager, passwordHasher, unitOfWork) = Sut();
        unitOfWork.Setup(x => x.Users.FindAsync(request.Login)).ReturnsAsync((User)null!);

        // Act
        var result = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.IsType<NotFoundResult>(result);

        unitOfWork.Verify(x => x.Users.FindAsync(request.Login), Times.Once);
        unitOfWork.VerifyNoOtherCalls();

        passwordHasher.VerifyNoOtherCalls();

        tokenManager.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Handle_PasswordIncorrect_NotFoundResult(CreateTokenRequest request, CancellationToken cancellationToken, User user)
    {
        // Arrange
        var (handler, tokenManager, passwordHasher, unitOfWork) = Sut();
        unitOfWork.Setup(x => x.Users.FindAsync(request.Login)).ReturnsAsync(user);
        passwordHasher.Setup(x => x.VerifyHashedPassword(user, user.PasswordHash, request.Password)).Returns(PasswordVerificationResult.Failed);

        // Act
        var result = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.IsType<NotFoundResult>(result);

        unitOfWork.Verify(x => x.Users.FindAsync(request.Login), Times.Once);
        unitOfWork.VerifyNoOtherCalls();

        passwordHasher.Verify(x => x.VerifyHashedPassword(user, user.PasswordHash, request.Password), Times.Once);
        passwordHasher.VerifyNoOtherCalls();

        tokenManager.VerifyNoOtherCalls();
    }

    private (CreateTokenHandler, Mock<ITokenManager>, Mock<IPasswordHasher>, Mock<IUnitOfWork>) Sut()
    {
        var tokenManager = new Mock<ITokenManager>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var passwordHasher = new Mock<IPasswordHasher>();

        return (
            new CreateTokenHandler(tokenManager.Object, unitOfWork.Object, passwordHasher.Object),
            tokenManager,
            passwordHasher,
            unitOfWork
        );
    }
}