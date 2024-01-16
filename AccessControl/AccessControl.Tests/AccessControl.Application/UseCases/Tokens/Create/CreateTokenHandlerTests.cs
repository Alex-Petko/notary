using AccessControl.Application;
using AccessControl.Domain;
using AccessControl.Infrastructure;
using AutoMapper;
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
        var (handler, tokenManager, mapper, unitOfWork) = Sut();
        mapper.Setup(x => x.Map<User>(request)).Returns(user);
        unitOfWork.Setup(x => x.Users.ContainsAsync(user)).ReturnsAsync(true);

        // Act
        var result = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.IsType<OkResult>(result);

        tokenManager.Verify(x => x.UpdateAsync(user.Login), Times.Once());
        tokenManager.VerifyNoOtherCalls();

        mapper.Verify(x => x.Map<User>(request), Times.Once());
        mapper.VerifyNoOtherCalls();

        unitOfWork.Verify(x => x.Users.ContainsAsync(user), Times.Once());
        unitOfWork.VerifyNoOtherCalls();
    }

    [Theory, CustomAutoData]
    public async Task Handle_UserNotFound_NotFoundResult(CreateTokenRequest request, CancellationToken cancellationToken, User user)
    {
        // Arrange
        var (handler, tokenManager, mapper, unitOfWork) = Sut();
        mapper.Setup(x => x.Map<User>(request)).Returns(user);
        unitOfWork.Setup(x => x.Users.ContainsAsync(user)).ReturnsAsync(false);

        // Act
        var result = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.IsType<NotFoundResult>(result);

        tokenManager.Verify(x => x.UpdateAsync(user.Login), Times.Never());
        tokenManager.VerifyNoOtherCalls();

        mapper.Verify(x => x.Map<User>(request), Times.Once());
        mapper.VerifyNoOtherCalls();

        unitOfWork.Verify(x => x.Users.ContainsAsync(user), Times.Once());
        unitOfWork.VerifyNoOtherCalls();
    }

    private (CreateTokenHandler, Mock<ITokenManager>, Mock<IMapper>, Mock<IUnitOfWork>) Sut()
    {
        var tokenManager = new Mock<ITokenManager>();
        var mapper = new Mock<IMapper>();
        var unitOfWork = new Mock<IUnitOfWork>();

        return (
            new CreateTokenHandler(tokenManager.Object, mapper.Object, unitOfWork.Object),
            tokenManager,
            mapper,
            unitOfWork
        );
    }
}